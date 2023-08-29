using System.Text;
using LangAssembler.Doc.Enumerations;
using LangAssembler.Doc.Line;

namespace LangAssembler.Doc;

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
    /// Gets the information for the current line in the document.
    /// </summary>
    public DocumentLineInfo CurrentLineInfo { get; }
    
    /// <summary>
    /// Gets the stream of the document.
    /// </summary>
    public Stream DocumentStream { get; }
    
    /// <summary>
    /// Gets the encoding of the document stream.
    /// </summary>
    public Encoding DocumentEncoding { get; set; }
    
    /// <summary>
    /// Gets the type of line feed used in the document.
    /// </summary>
    public DocumentLineFeed LineFeedType { get; }
    
    /// <summary>
    /// Gets the current position within the document.
    /// </summary>
    public long DocumentPosition { get; }
    
}