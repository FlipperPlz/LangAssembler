using System.Text;

namespace LangAssembler.Document;

/// <summary>
/// The Document class is an implementation of the IDocument interface, 
/// which provides line-by-line reading from a stream with concurrent
/// access to line information. 
/// </summary>
/// <remarks>
/// The Document class retains and exposes information about document
/// location such as the line number, the column number and line start. 
/// Stream read state (FullyRead) and encoding of the document stream (DocumentEncoding) 
/// are also available. The DocumentPosition represents the current position 
/// within the document. The document maintains a collection of line information (LineInfos)
/// which get populated as the document is read.  
/// </remarks>
public abstract class Document : IDocument
{
    /// <summary>
    /// Gets the line number in the document.
    /// </summary>
    public int LineNumber { get; protected set; } = 1;

    /// <summary>
    /// Gets the column number within the line in the document.
    /// </summary>
    public int ColumnNumber { get; protected set; } = 1;

    /// <summary>
    /// Gets the start index of the line in the document.
    /// </summary>
    public int LineStart { get; protected set; } = -1;

    /// <summary>
    /// Gets the length of the document.
    /// </summary>
    public int DocumentLength => (int)DocumentStream.Length;
    protected readonly List<DocumentLineInfo> EditableLineInfo = new ();

    /// <summary>
    /// Gets an enumeration of line information for each line in the document.
    /// </summary>
    public IEnumerable<DocumentLineInfo> LineInfos => EditableLineInfo;

    /// <summary>
    /// Gets if the stream has been fully read.
    /// If true, this means <see cref="IDocument.LineInfos"/> is fully populated;
    /// </summary>
    public bool FullyRead { get; protected set; } = false;

    /// <summary>
    /// Gets the encoding of the document stream.
    /// </summary>
    public Encoding DocumentEncoding { get; set; }

    /// <summary>
    /// Gets the stream of the document.
    /// </summary>
    public Stream DocumentStream { get; }

    /// <summary>
    /// Gets the current position within the document.
    /// </summary>
    public int DocumentPosition
    {
        get => (int)DocumentStream.Position;
        set => DocumentStream.Seek(value, SeekOrigin.Begin);
    }

    protected Document(Stream stream, Encoding encoding)
    {
        DocumentStream = stream;
        DocumentEncoding = encoding;
    }
    
    /// <summary>
    /// Releases all resources used by the document.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    /// <summary>
    /// Releases the unmanaged resources used by the document and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposedManaged">
    /// <c>true</c> to release both managed and unmanaged resources;
    /// <c>false</c> to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(bool disposedManaged)
    {
        if (disposedManaged)
        {
            DocumentStream.Dispose();
        }
    }
    
    /// <summary>
    /// Advances to the next line in the document by storing the end position of the current line, incrementing the line number,
    /// resetting the column count, and setting the start position for the next line.
    /// </summary>
    protected virtual void IncrementLine()
    {
        EditableLineInfo.Insert(LineNumber, new DocumentLineInfo(LineStart..DocumentPosition));
        LineNumber++;
        ColumnNumber = 0;
        LineStart = DocumentPosition + 1;
    }

}