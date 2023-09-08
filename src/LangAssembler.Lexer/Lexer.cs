using LangAssembler.Extensions;
using LangAssembler.IO;
using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Events.Arguments;
using LangAssembler.Lexer.Events.Delegates;
using LangAssembler.Lexer.Extensions;
using LangAssembler.Lexer.Models.Match;
using LangAssembler.Lexer.Models.Type;
using LangAssembler.Lexer.Models.Type.Types;
using LangAssembler.Models.Doc;

namespace LangAssembler.Lexer;

/// <summary>
/// Provides a base implementation for a lexer. 
/// </summary>
public abstract class Lexer : DocumentReader, ILexer
{
    protected static readonly ITokenType InvalidToken = InvalidTokenType.Instance;

    /// <summary>
    /// A flag indicating if event handling has been muted or not.
    /// </summary>
    public bool EventsMuted { get; protected set; } = false;

    /// <summary>
    /// Event that is triggered when a token is matched and events arent muted.
    /// </summary>
    public event TokenMatchHandler? TokenMatched;

    /// <summary>
    /// Event that is triggered when a token is edited and events arent muted.
    /// </summary>
    public event EventHandler<TokenMatchEditedEventArgs>? TokenEdited;

    /// <summary>
    /// Event that is triggered when a token is removed and events arent muted.
    /// </summary>
    public event EventHandler<ITokenMatch>? TokenRemoved;
    
    protected readonly SortedList<long, ITokenMatch> EditablePreviousMatches = new();
    
    /// <summary>
    /// Provides enumerable access to <see cref="EditablePreviousMatches"/>.
    /// </summary>
    /// <inheritdoc cref="ILexer.PreviousMatches"/>
    public IEnumerable<ITokenMatch> PreviousMatches => EditablePreviousMatches.Values;

    /// <summary>
    /// Gets the last matched token.
    /// </summary>
    public ITokenMatch? LastMatch { get; protected set; }
    
    
    protected Lexer(Document document, bool leaveOpen = false) : base(document, leaveOpen)
    {
    }

    /// <summary>
    /// This method is designed to identify the next token type within the specified input buffer
    /// </summary>
    /// <remarks>
    /// Invokes an abstract function, <see cref="LocateNextMatch"/>, which requires implementation by any class
    /// that subclasses <see cref="Lexer"/>.These subclasses define their own rules for matching token types.
    /// </remarks>
    /// <param name="tokenStart">
    /// An index within the input buffer where the search for the next token type commences.
    /// </param>
    /// <param name="currentChar">
    /// A specific character from the input buffer at the position specified by <see cref="tokenStart"/>.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="ITokenType"/> interface, representing the type of the next matched token.
    /// </returns>
    protected virtual ITokenType GetNextMatch(long tokenStart, int? currentChar) => 
        LocateNextMatch(tokenStart, currentChar);

    
    /// <summary>
    /// Locates the next match in the buffer starting from a given position. 
    /// This method is to be implemented by derived classes based on their specific token types and rules.
    /// </summary>
    /// <param name="tokenStart">The starting position to begin search.</param>
    /// <param name="currentChar">The current character under analysis.</param>
    /// <returns>The type of the next token that was matched.</returns>
    protected abstract ITokenType LocateNextMatch(long tokenStart, int? currentChar);
    
    /// <summary>
    /// Handles the TokenMatched event. 
    /// </summary>
    /// <remarks>
    /// If event handling is not muted, this method invokes the TokenMatched event. Regardless of the event state,
    /// it also sets the LastMatch property and adds the match to the previous matches list.
    /// </remarks>
    /// <param name="match">The token match associated with the event.</param>
    protected virtual void OnTokenMatched(ITokenMatch match)
    {
        if (!EventsMuted)
        {
            TokenMatched?.Invoke(ref match);
        }
        LastMatch = match;
        AddPreviousMatch(match);
    }
    
    /// <summary>
    /// Inserts the match to the correct position in previously matched tokens list.
    /// </summary>
    /// <remarks>
    /// The list is ordered based on the Token Start property of the token match.
    /// </remarks>
    /// <param name="match">The token match to add.</param>
    protected void AddPreviousMatch(ITokenMatch match)
    {
        if (!EditablePreviousMatches.ContainsKey(match.TokenStart))
        {
            EditablePreviousMatches.Add(match.TokenStart, match);
            return;
        }
        //TODO handle overlapping keys
        throw new NotImplementedException();
    }

    /// <summary>
    /// Locates the next match and registers it into the system.
    /// </summary>
    /// <returns>The match found.</returns>
    public ITokenMatch LexToken()
    {
        var tokenStart = Position;
        var type = GetNextMatch(tokenStart, MoveForward());
        var match = type is IInvalidTokenType or null
            ? CreateInvalidMatch(tokenStart)
            : CreateTokenMatch(type, tokenStart);
        if (!EventsMuted)
        {
            TokenMatched?.Invoke(ref match);
        }

        return match;
    }
    
    /// <summary>
    /// Creates a token match of an "invalid" type at the given start position.
    /// </summary>
    /// <param name="tokenStart">Start index of the token in the lexer buffer.</param>
    /// <returns>A token match of type "invalid".</returns>
    protected ITokenMatch CreateInvalidMatch(long tokenStart) =>
        CreateTokenMatch(InvalidToken, tokenStart);
    
    /// <summary>
    /// Creates a new <see cref="TokenMatch"/> instance using the specified token type and start position.
    /// </summary>
    /// <remarks>
    /// The created <see cref="TokenMatch"/> includes the text between the start position and the current position in the lexer buffer.
    /// </remarks>
    /// <param name="type">The type of the token.</param>
    /// <param name="tokenStart">The start index of the token in the lexer buffer.</param>
    /// <returns>A new <see cref="TokenMatch"/> instance.</returns>
    protected ITokenMatch CreateTokenMatch(ITokenType type, long tokenStart) =>
        new TokenMatch(this.GetRange(tokenStart, Position), tokenStart, Position, this, type);
    
    /// <summary>
    /// Removes a specific token match from the sequence of prior matches, and triggers the TokenRemoved event.
    /// </summary>
    /// <inheritdoc cref="ILexer.RemoveTokenMatch"/>
    public void RemoveTokenMatch(ITokenMatch tokenMatch)
    {
        if (!EventsMuted)
        {
            TokenRemoved?.Invoke(this, tokenMatch);
        }

        if (EditablePreviousMatches.ContainsKey(tokenMatch.TokenStart))
        {
            EditablePreviousMatches.Remove(tokenMatch.TokenStart);
            return;
        }
        
        //TODO handle overlapping keys
        throw new NotImplementedException();
    }

    public void ReplaceTokenMatchText(ITokenMatch tokenMatch, string text) =>
        ReplaceTokenMatchText(tokenMatch, Encoding.GetBytes(text));

    /// <summary>
    /// Replaces the text of a matched token, and triggers the TokenEdited event.
    /// </summary>
    /// <inheritdoc cref="ILexer.ReplaceTokenMatchText"/>
    /// <param name="tokenMatch">The token match to have its text replaced.</param>
    /// <param name="replacement">The new data to replace the current token text.</param>
    /// <param name="stringVersion">A quick way to pass the TokenText down. Do not lie :{</param>
    public void ReplaceTokenMatchText(ITokenMatch tokenMatch, Span<byte> replacement, string? stringVersion = null)
    {
        if (!Writable)
        {
            throw new InvalidOperationException($"This operation can't be performed. The document \"{Document.Source.Name}\" is not writable.");
        }
        
        var oldContents = tokenMatch.ToSubstring();
        tokenMatch.TokenText = stringVersion ?? Encoding.GetString(replacement);
        ReplaceRange(tokenMatch.TokenStart, tokenMatch.TokenEnd, replacement);
        if (!EventsMuted)
        {
            TokenEdited?.Invoke(this, new TokenMatchEditedEventArgs(tokenMatch, oldContents));
        }
    }

}