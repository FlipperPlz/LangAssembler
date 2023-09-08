using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Extensions;
using LangAssembler.Lexer.Models.Match;

namespace LangAssembler.Lexer.Models.Type.Types;

/// <summary>
/// Represents the token type for the end of file (EOF).
/// </summary>
public interface IEOFTokenType : ITokenType
{
    TokenMatcher ITokenType.Matches => MatchEOF;

    public virtual bool MatchEOF(ILexer lexer, long tokenStart, int? currentChar) => 
        tokenStart > lexer.Length || currentChar == null || !lexer.HasNext();
}