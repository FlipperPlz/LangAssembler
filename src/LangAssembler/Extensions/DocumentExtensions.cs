using LangAssembler.Doc;
using LangAssembler.Processors;
using LangAssembler.Processors.Tracked;

namespace LangAssembler.Extensions;

public static class DocumentExtensions
{

    /// <summary>
    /// Retrieves the line information for the specified line number from the document.
    /// </summary>
    /// <param name="document">The IDocument instance containing line information.</param>
    /// <param name="lineNumber">The line number for which the information should be retrieved.</param>
    /// <returns>The DocumentLineInfo for the specified line number, or null if the line does not exist.</returns>
    public static DocumentLineInfo? GetLineInfo(this IDocument document, long lineNumber) =>
        document.LineInfos.FirstOrDefault(l => l.LineNumber == lineNumber);

    /// <summary>
    /// Gets the current position within the document stream of the specified document.
    /// </summary>
    /// <param name="document">The document for which the current position in the stream is returned.</param>
    /// <returns>The current position in the stream of the document.</returns>
    public static long GetDocumentPosition(this IDocument document) => document.DocumentStream.Position;

    /// <summary>
    /// Gets the length in bytes of the stream in a document.
    /// </summary>
    /// <param name="document">The document for which the length in bytes of the stream is returned.</param>
    /// <returns>The length in bytes of the stream of the document.</returns>
    public static long GetDocumentLength(this IDocument document) => document.DocumentStream.Length;

}