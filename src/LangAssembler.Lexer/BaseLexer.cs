using System.Text;
using LangAssembler.Core.Options;
using LangAssembler.Extensions;
using LangAssembler.Lexer.Events.Arguments;
using LangAssembler.Lexer.Events.Delegates;
using LangAssembler.Lexer.Extensions;
using LangAssembler.Lexer.Models.Match;
using LangAssembler.Lexer.Models.Type;
using LangAssembler.Lexer.Models.Type.Types;
using LangAssembler.Processors;
using LangAssembler.Processors.Tracked;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Lexer;

public abstract class BaseLexer : TrackedEditableStringProcessor, ILexer
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
    
    protected readonly List<ITokenMatch> _previousMatches = new();
    
    /// <summary>
    /// Provides enumerable access to <see cref="_previousMatches"/>.
    /// </summary>
    /// <inheritdoc cref="ILexer.PreviousMatches"/>
    public IEnumerable<ITokenMatch> PreviousMatches => _previousMatches;

    /// <summary>
    /// Gets the last matched token.
    /// </summary>
    public ITokenMatch? LastMatch { get; protected set; }
    
    protected BaseLexer(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    protected BaseLexer(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }
    
    /// <summary>
    /// Locates the next match in the buffer starting from a given position. 
    /// This method is to be implemented by derived classes based on their specific token types and rules.
    /// </summary>
    /// <param name="tokenStart">The starting position to begin search.</param>
    /// <param name="currentChar">The current character under analysis.</param>
    /// <returns>The type of the next token that was matched.</returns>
    protected abstract ITokenType LocateNextMatch(int tokenStart, char? currentChar);
    
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
        if(_previousMatches.Count == 0 || match.TokenStart >= _previousMatches[^1].TokenStart)
        {
            _previousMatches.Add(match);
        }

        var index = _previousMatches.BinarySearch(match, Comparer<ITokenMatch>.Create((x, y) => x.TokenStart.CompareTo(y.TokenStart)));
        _previousMatches.Insert(index < 0 ? ~index : index, match);
    }

    /// <summary>
    /// Locates the next match and registers it into the system.
    /// </summary>
    /// <returns>The match found.</returns>
    public virtual ITokenMatch LexToken()
    {
        var tokenStart = Position;
        var type = LocateNextMatch(tokenStart, this.MoveForward());
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
    protected ITokenMatch CreateInvalidMatch(int tokenStart) =>
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
    protected ITokenMatch CreateTokenMatch(ITokenType type, int tokenStart) =>
        new TokenMatch(this.GetRange(tokenStart..Position), tokenStart, this, type);
    
    /// <summary>
    /// Removes a specific token match from the sequence of prior matches, and triggers the TokenRemoved event.
    /// </summary>
    /// <inheritdoc cref="ILexer.RemoveTokenMatch"/>
    /// <param name="tokenMatch">The specific token match to be removed.</param>
    public void RemoveTokenMatch(ITokenMatch tokenMatch)
    {
        if (!EventsMuted)
        {
            TokenRemoved?.Invoke(this, tokenMatch);
        }

        _previousMatches.Remove(tokenMatch);
        this.RemoveRange(tokenMatch.CurrentIndex(), out _, StringProcessorPositionalReplacementOption.KeepRemaining);
    }

    /// <summary>
    /// Replaces the text of a matched token, and triggers the TokenEdited event.
    /// </summary>
    /// <inheritdoc cref="ILexer.ReplaceTokenMatchText"/>
    /// <param name="tokenMatch">The token match to have its text replaced.</param>
    /// <param name="text">The new text to replace the current token text.</param>
    public void ReplaceTokenMatchText(ITokenMatch tokenMatch, string text)
    {
        var oldContents = tokenMatch.ToSubstring();
        tokenMatch.TokenText = text;
        ReplaceRange(tokenMatch.CurrentIndex(), text, out _, StringProcessorPositionalReplacementOption.KeepRemaining);
        if (!EventsMuted)
        {
            TokenEdited?.Invoke(this, new TokenMatchEditedEventArgs(tokenMatch, oldContents));
        }
    }
}