using LangAssembler.Lexer.Models.TypeSet;

namespace LangAssembler.Lexer.Base;

/// <summary>
/// Represents a lexer bound to a specific type set.
/// The type set defines the types of tokens that the lexer can process.
/// </summary>
/// <typeparam name="TTypeSet">The types of tokens this lexer can parse.</typeparam>
public interface IBoundLexer<out TTypeSet> : ILexer where TTypeSet : ITokenTypeSet
{
    /// <summary>
    /// Gets the default token type set (singleton) associated with the lexer.
    /// </summary>
    public static virtual TTypeSet DefaultTokenTypeSet { get; } = default!;
}