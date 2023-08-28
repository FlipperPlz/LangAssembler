using System.Text;

namespace LangAssembler.Document;

/// <summary>
/// Defines methods and properties to represent a structured document.
/// </summary>
public interface IDocument : IDisposable
{
    /// <summary>
    /// Gets an enumeration of line information for each line in the document.
    /// </summary>
    public IEnumerable<DocumentLineInfo> LineInfos { get; }
    
    /// <summary>
    /// Gets the stream of the document.
    /// </summary>
    public Stream DocumentStream { get; }
    
    /// <summary>
    /// Gets the encoding of the document stream.
    /// </summary>
    public Encoding DocumentEncoding { get; set; }
    
    /// <summary>
    /// Gets the current position within the document.
    /// </summary>
    public int DocumentPosition { get; set; }
    
    /// <summary>
    /// Gets the line number in the document.
    /// </summary>
    public int LineNumber { get; }
    
    /// <summary>
    /// Gets the column number within the line in the document.
    /// </summary>
    public int ColumnNumber { get; }
    
    /// <summary>
    /// Gets the start index of the line in the document.
    /// </summary>
    public int LineStart { get; }
    
    /// <summary>
    /// Gets the length of the document.
    /// </summary>
    public int DocumentLength { get; }
}