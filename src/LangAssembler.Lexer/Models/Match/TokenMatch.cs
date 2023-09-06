using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Models.Type;

namespace LangAssembler.Lexer.Models.Match;

/// <summary>
/// Represents a match from a tokenization operation.
/// </summary>
public class TokenMatch : ITokenMatch
{
    /// <summary>
    /// Gets the matched token text.
    /// </summary>
    /// <value>A reference to the token text.</value>
    public string TokenText { get; set; }

    /// <summary>
    /// Gets the index at which the matched token starts in the text being tokenized.
    /// </summary>
    /// <value>A reference to the token start index.</value>
    public long TokenStart { get; set; }

    public long TokenEnd { get; set; }

    /// <summary>
    /// Gets the lexer that produced the token match.
    /// This can be used to access properties of the lexer such as the original text that was tokenized.
    /// This can also be used to SAFELY alter the text of the token
    /// </summary>
    /// <value>The lexer that produced the token match.</value>
    public ILexer Lexer { get; }

    public long OriginalStart { get; }
    public long OriginalEnd { get; }

    /// <summary>
    /// Gets the type of the matched token.
    /// </summary>
    /// <value>The type of the token.</value>
    public ITokenType TokenType { get; set; }

    /// <summary>
    /// Initializes a new instance of the TokenMatch class.
    /// </summary>
    /// <param name="tokenText">The text of the token.</param>
    /// <param name="tokenStart">The index where the token starts in the source text.</param>
    /// <param name="tokenEnd">The index where the token ends in the source text.</param>
    /// <param name="lexer">The lexer that generated this token.</param>
    /// <param name="tokenType">The type of the token.</param>
    public TokenMatch(string tokenText, long tokenStart, long tokenEnd, ILexer lexer, ITokenType tokenType)
    {
        TokenText = tokenText;
        TokenStart = tokenStart;
        TokenEnd = tokenEnd;
        Lexer = lexer;
        OriginalStart = TokenStart;
        OriginalEnd = TokenEnd;
        TokenType = tokenType;
    }

}