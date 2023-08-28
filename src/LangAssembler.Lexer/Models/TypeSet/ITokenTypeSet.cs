using LangAssembler.Lexer.Models.Type;

namespace LangAssembler.Lexer.Models.TypeSet;

/// <summary>
/// Defines a set of token types for a lexer. This interface allows iteration over the set of token types.
/// </summary>
public interface ITokenTypeSet : IEnumerable<ITokenType>
{
}

