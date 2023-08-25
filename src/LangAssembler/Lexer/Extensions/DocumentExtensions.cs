using LangAssembler.Lexer.Models.Document;
using LangAssembler.Processors;
using LangAssembler.Processors.Tracked;

namespace LangAssembler.Lexer.Extensions;

public static class DocumentExtensions
{

    /// <summary>
    /// Retrieves the line information for the specified line number from the document.
    /// </summary>
    /// <param name="document">The IDocument instance containing line information.</param>
    /// <param name="lineNumber">The line number for which the information should be retrieved.</param>
    /// <returns>The DocumentLineInfo for the specified line number, or null if the line does not exist.</returns>
    public static DocumentLineInfo? GetLineInfo(this IDocument document, int lineNumber) =>
        document.LineInfos.ElementAtOrDefault(lineNumber);

    /// <summary>
    /// Retrieves the text for the specified line number from the document.
    /// </summary>
    /// <param name="document">The IDocument instance containing line information.</param>
    /// <param name="processor">The string processor to retrieve the line content.</param>
    /// <param name="lineNumber">The line number for which the text should be retrieved.</param>
    /// <returns>The text of the specified line number, or null if the line does not exist.</returns>
    public static string? GetLineText(this IDocument document, IStringProcessor processor, int lineNumber) =>
        GetLineInfo(document, lineNumber)?.GetLineText(processor);
    
    /// <summary>
    /// Retrieves the text for the specified line number from the tracked processor.
    /// </summary>
    /// <param name="trackedProcessor">The ITrackedStringProcessorBase instance linked with a document line information.</param>
    /// <param name="lineNumber">The line number for which the text should be retrieved.</param>
    /// <returns>The text of the specified line number, or null if the line does not exist.</returns>
    public static string? GetLineText(this ITrackedStringProcessorBase trackedProcessor, int lineNumber) =>
        GetLineInfo(trackedProcessor, lineNumber)?.GetLineText(trackedProcessor);
    
}