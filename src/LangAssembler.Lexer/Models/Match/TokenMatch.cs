using LangAssembler.Lexer.Extensions;
using LangAssembler.Lexer.Models.Type;

namespace LangAssembler.Lexer.Models.Match;

/// <summary>
/// Represents a match from a tokenization operation.
/// </summary>
public class TokenMatch : ITokenMatch
{
    
    public string TokenText { get; set; }
    public int TokenStart { get; set; }
    public ILexer Lexer { get; }
    public Range OriginalIndex { get; }
    public ITokenType TokenType { get; set; }
    
    public TokenMatch(string tokenText, int tokenStart, ILexer lexer, ITokenType tokenType)
    {
        TokenText = tokenText;
        TokenStart = tokenStart;
        Lexer = lexer;
        OriginalIndex = tokenStart..this.TokenEnd();
        TokenType = tokenType;
    }

}