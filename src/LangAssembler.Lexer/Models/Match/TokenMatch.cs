using LangAssembler.Lexer.Extensions;
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
    public int TokenStart { get; set; }

    /// <summary>
    /// Gets the lexer that produced the token match.
    /// This can be used to access properties of the lexer such as the original text that was tokenized.
    /// This can also be used to SAFELY alter the text of the token
    /// </summary>
    /// <value>The lexer that produced the token match.</value>
    public ILexer Lexer { get; }

    /// <summary>
    /// Gets the original range of where matched token within the lexed buffer.
    /// </summary>
    /// <value>The index range of the token.</value>
    public Range OriginalIndex { get; }

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
    /// <param name="lexer">The lexer that generated this token.</param>
    /// <param name="tokenType">The type of the token.</param>
    public TokenMatch(string tokenText, int tokenStart, ILexer lexer, ITokenType tokenType)
    {
        TokenText = tokenText;
        TokenStart = tokenStart;
        Lexer = lexer;
        OriginalIndex = tokenStart..this.TokenEnd();
        TokenType = tokenType;
    }

}