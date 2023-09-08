using LangAssembler.Lexer.Base;

namespace LangAssembler.Lexer.Models.Type;

/// <summary>
/// Defines the basic properties of a token type that can be identified during the tokenization process.
/// </summary>
public interface ITokenType
{
    /// <summary>
    /// Gets the name of the token type, that is used in debugging and logging.
    /// </summary>
    public string DebugName { get; }

    bool Matches(ILexer tokenSetLexer, long tokenStart, int? currentChar);
}