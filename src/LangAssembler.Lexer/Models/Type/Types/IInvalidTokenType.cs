using LangAssembler.Lexer.Base;

namespace LangAssembler.Lexer.Models.Type.Types;

/// <summary>
/// Represents an invalid token type. This interface is used for token types that
/// were not recognized by the lexer. Extended from the ITokenType.
/// </summary>
public interface IInvalidTokenType : ITokenType
{
    bool ITokenType.Matches(ILexer tokenSetLexer, long tokenStart, int? currentChar) => true;
}