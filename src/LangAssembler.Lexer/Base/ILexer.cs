using LangAssembler.IO;
using LangAssembler.Lexer.Events.Delegates;
using LangAssembler.Lexer.Models.Match;
using TokenEditedHandler = System.EventHandler<LangAssembler.Lexer.Events.Arguments.TokenMatchEditedEventArgs>;
using TokenRemovedHandler = System.EventHandler<LangAssembler.Lexer.Models.Match.ITokenMatch>;

namespace LangAssembler.Lexer.Base;

/// <summary>
/// Defines the properties, methods and events of a LangAssembler Lexer.
/// </summary>
public interface ILexer : IDocumentReader
{
    /// <summary>
    /// A flag indicating if event handling has been muted or not.
    /// </summary>
    public bool EventsMuted { get; }
    
    /// <summary>
    /// Gets the last matched token.
    /// </summary>
    public ITokenMatch? LastMatch { get; }
    
    /// <summary>
    /// Gets the sequence of previous matched tokens.
    /// </summary>
    public IEnumerable<ITokenMatch> PreviousMatches { get; }
    
    /// <summary>
    /// Event that is triggered when a token is matched and events arent muted.
    /// </summary>
    public event TokenMatchHandler? TokenMatched;
    
    /// <summary>
    /// Event that is triggered when a token is edited and events arent muted.
    /// </summary>
    public event TokenEditedHandler? TokenEdited;
    
    /// <summary>
    /// Event that is triggered when a token is removed and events arent muted.
    /// </summary>
    public event TokenRemovedHandler? TokenRemoved;

    /// <summary>
    /// Retrieves the next token in the sequence.
    /// </summary>
    /// <returns>The next token.</returns>
    public ITokenMatch LexToken();
    
    /// <summary>
    /// Removes a specific token from the lexers storage of tokens.
    /// </summary>
    /// <remarks>
    /// Should call TokenRemoved event somewhere down the chain of command in this function.
    /// </remarks>
    /// <param name="tokenMatch">The token match instance to be removed.</param>
    public void RemoveTokenMatch(ITokenMatch tokenMatch);
    
    /// <summary>
    /// Replaces the text of a specific matched token.
    /// </summary>
    /// <remarks>
    /// Should call TokenEdited event somewhere down the chain of command in this function.
    /// </remarks>
    /// <param name="tokenMatch">The token match instance which text should be replaced.</param>
    /// <param name="text">The new text to replace the current token text.</param>
    /// <param name="stringVersion">A quick way to pass the TokenText down. Do not lie :{</param>

    public void ReplaceTokenMatchText(ITokenMatch tokenMatch, Span<byte> text, string? stringVersion = null);
    
    public void ReplaceTokenMatchText(ITokenMatch tokenMatch, string text);
}