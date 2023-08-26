using LangAssembler.Lexer.Base;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Parser;

/// <summary>
/// Defines a contract for a parser which intends to parse a series of tokens into an expression tree (Abstract Syntax Tree).
/// </summary>
/// <typeparam name="TASTRoot">
/// The root node type of the Abstract Syntax Tree that is returned after the parsing process.
/// </typeparam>
/// <typeparam name="TLexer">
/// The lexer type used in the parsing process to lexically analyze the source code.
/// </typeparam>
public interface IParser<
    out TASTRoot,
    in TLexer
>
    where TASTRoot : class, new()
    where TLexer : ILexer
{
    /// <summary>
    /// Converts the given sequence of tokens (via Lexer) into an Abstract Syntax Tree (AST).
    /// </summary>
    /// <param name="lexer">
    /// The lexer that provides the sequence of tokens to parse.
    /// </param>
    /// <param name="logger">
    /// The logger to use for logging during the parsing process (if available).
    /// </param>
    /// <returns>
    /// The root node of the Abstract Syntax Tree that represents the parsed tokens.
    /// </returns>
    public TASTRoot Parse(TLexer lexer, ILogger? logger);
}