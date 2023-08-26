using LangAssembler.Lexer.Base;
using Microsoft.Extensions.Logging;

namespace LangAssembler.PreProcessor;

/// <summary>
/// Defines a contract for a preprocessor that performs initial transformation or extraction of data on a lexer before parsing.
/// </summary>
/// <typeparam name="TLexer">The type of lexer that this preprocessor processes.</typeparam>
public interface IPreProcessor<in TLexer> 
    where TLexer : ILexer
{
    /// <summary>
    /// Applies processing logic to the provided lexer.
    /// </summary>
    /// <param name="lexer">The lexer to be processed.</param>
    /// <param name="logger">Optional logger for capturing process information.</param>
    public void ProcessLexer(TLexer lexer, ILogger? logger);
}