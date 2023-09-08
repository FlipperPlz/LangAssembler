using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Extensions;

namespace LangAssembler.Lexer.Models.Type.Types;

/// <summary>
/// Represents the token type for the end of file (EOF).
/// </summary>
public interface IEOFTokenType : ITokenType
{
    bool ITokenType.Matches(ILexer lexer, long tokenStart, int? currentChar) => 
        tokenStart > lexer.Length || currentChar == null || !lexer.HasNext();
}