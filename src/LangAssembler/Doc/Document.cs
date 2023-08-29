using System.Text;
using LangAssembler.Doc.Enumerations;
using LangAssembler.Doc.Line;

namespace LangAssembler.Doc;

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
    /// Gets the type of line feed used in the document.
    /// </summary>
    public DocumentLineFeed LineFeedType { get; }
    
    /// <summary>
    /// Gets the column number within the line in the document.
    /// </summary>
    public int ColumnNumber { get; protected set; } = 1;

    /// <summary>
    /// Represents a list of line information that can be edited. 
    /// </summary>
    private readonly List<DocumentLineInfo> _editableLineInfo = new ();

    /// <summary>
    /// Gets an enumeration of line information for each line in the document.
    /// </summary>
    public IEnumerable<DocumentLineInfo> LineInfos => _editableLineInfo;

    /// <summary>
    /// Gets the information for the current line in the document.
    /// </summary>
    public DocumentLineInfo CurrentLineInfo { get; protected set; }

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
    public long DocumentPosition
    {
        get => DocumentStream.Position;
        protected set => DocumentStream.Seek(value, SeekOrigin.Begin);
    }

    /// <summary>
    /// Constructor that creates a new document.
    /// </summary>
    /// <param name="stream">The stream of the document.</param>
    /// <param name="encoding">The encoding of the document stream.</param>
    /// <param name="lineFeed">The type of line feed used in the document. (optional)</param>
    protected Document(Stream stream, Encoding encoding, DocumentLineFeed lineFeed = DocumentLineFeed.NotPicky)
    {
        if (stream.CanSeek)
        {
            throw new NotSupportedException("Document stream must be seekable");
        }

        stream.Seek(0, SeekOrigin.Begin);
        CurrentLineInfo = CreateAndAddLine(1, 0);
        DocumentStream = stream;
        DocumentEncoding = encoding;
        LineFeedType = lineFeed;
    }

    /// <summary>
    /// Finalizer that ensures the document's resources are released.
    /// </summary>
    ~Document()
    {
        Dispose();
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
        CurrentLineInfo = GetOrCreateLine(CurrentLineInfo.LineNumber + 1, DocumentPosition + 1);
    }

    /// <summary>
    /// Inserts or replaces a DocumentLineInfo instance into _editableLineInfo.
    /// _editableLineInfo is sorted by DocumentLineInfo.LineNumber. If an item with the same
    /// LineNumber already exists in _editableLineInfo, it is replaced.
    /// </summary>
    /// <param name="lineInfo">The DocumentLineInfo instance to be inserted or replaced.</param>
    /// <returns>The inserted or replaced DocumentLineInfo instance.</returns>
    protected DocumentLineInfo InsertLine(DocumentLineInfo lineInfo)
    {
        var index = _editableLineInfo.BinarySearch(lineInfo);
        switch (index)
        {
            case < 0: 
                _editableLineInfo.Insert(~index, lineInfo);
                break;
            default:
                _editableLineInfo[index] = lineInfo;
                break;       
        }
        return lineInfo;
    }

    /// <summary>
    /// Retrieves or, if necessary, creates line information for a specific line.
    /// </summary>
    /// <param name="lineNumber">The line number to get or create information for.</param>
    /// <param name="lineStart">The position at which the line starts.</param>
    /// <returns>The line information for the line number provided.</returns>
    protected virtual DocumentLineInfo GetOrCreateLine(long lineNumber, long lineStart)
    {
        if (_editableLineInfo.FirstOrDefault(it => it.LineNumber == lineNumber) is { } lnInfo)
        {
            return lnInfo;
        }

        return CreateAndAddLine(lineNumber, lineStart);
    }

    /// <summary>
    /// Resets the document by clearing all lineInfo instances and positions.
    /// </summary>
    protected void ResetDocument()
    {
        DocumentPosition = 0;
        _editableLineInfo.Clear();
        CurrentLineInfo = CreateAndAddLine(1, DocumentPosition);
    }

    /// <summary>
    /// Creates a new DocumentLineInfo with given lineNumber and lineStart, 
    /// and adds it to the _editableLineInfo list using InsertLine method.
    /// </summary>
    /// <param name="lineNumber">The line number for the new DocumentLineInfo.</param>
    /// <param name="lineStart">The line start for the new DocumentLineInfo.</param>
    /// <returns>The created DocumentLineInfo.</returns>
    private DocumentLineInfo CreateAndAddLine(long lineNumber, long lineStart) =>
        InsertLine(new DocumentLineInfo(this, lineNumber, lineStart));
    
}