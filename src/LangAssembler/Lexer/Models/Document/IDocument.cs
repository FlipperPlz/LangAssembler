namespace LangAssembler.Lexer.Models.Document;

/// <summary>
/// Defines methods and properties to represent a structured document.
/// </summary>
public interface IDocument : IDocumentCoordinates, IDisposable
{
    /// <summary>
    /// Gets an enumeration of line information for each line in the document.
    /// </summary>
    public IEnumerable<DocumentLineInfo> LineInfos { get; }
    
    /// <summary>
    /// Gets the current position within the document.
    /// </summary>
    public int DocumentPosition { get; }

    
    /// <summary>
    /// Gets the length of the document.
    /// </summary>
    public int DocumentLength { get; }

}