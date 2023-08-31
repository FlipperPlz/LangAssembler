namespace LangAssembler.Doc.Line;


/// <summary>
/// Represents detailed information about a line of document.
/// </summary>
public class DocumentLineInfo : IDocumentLineInfo
{
    /// <summary>
    /// Gets the document where the line info was found
    /// </summary>
    public IDocumentOld DocumentOld { get; }

    /// <summary>
    /// Returns the line number
    /// </summary>
    public long LineNumber { get; }

    /// <summary>
    /// Gets or sets the start of the line
    /// </summary>
    public long LineStart { get; protected set; }

    /// <summary>
    /// Gets or sets the end of the line
    /// </summary>
    public long LineEnd { get; protected set; }

    /// <summary>
    /// Gets the length of the line.
    /// </summary>
    public long LineLength { get; private set; }

    /// <summary>
    /// Initializes a new instance of <see cref="DocumentLineInfo"/>, setting the start, end, and length of the line in a document.
    /// </summary>
    /// <param name="documentOld">The document this line was found in.</param>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="lineStart">Start position of the line in a document.</param>
    /// <param name="lineEnd">End position of the line in a document.</param>
    public DocumentLineInfo(IDocumentOld documentOld, long lineNumber, long lineStart, long lineEnd) : this(documentOld, lineNumber)
    {
        ChangeLineIndex(lineStart, lineEnd);
    }
    
    public DocumentLineInfo(IDocumentOld documentOld, long lineNumber, long lineStart) : this(documentOld, lineNumber)
    {
        DocumentOld = documentOld;
        LineNumber = lineNumber;
        LineStart = lineStart;
    }
    
    public DocumentLineInfo(IDocumentOld documentOld, long lineNumber)
    {
        DocumentOld = documentOld;
        LineNumber = lineNumber;
    }
    
    /// <summary>
    /// Updates line start, end and recalculates the line length.
    /// </summary>
    /// <param name="lineStart">The start of the line.</param>
    /// <param name="lineEnd">The end of the line.</param>
    public void ChangeLineIndex(long lineStart, long lineEnd)
    {
        
        LineStart = lineStart;
        LineEnd = lineEnd;
        LineLength = LineEnd - LineStart;
    }

    
    /// <summary>
    /// Sets the end of the line and recalculates the line length
    /// </summary>
    /// <param name="lineEnd">The end of the line</param>
    public void InitializeEnd(long lineEnd)
    {
        LineEnd = lineEnd;
        LineLength = LineEnd - LineStart;
    }
    
    /// <summary>
    /// Determines whether the given position is within the line.
    /// </summary>
    /// <param name="position">The position in the document.</param>
    /// <returns>true if the given position is within the line; otherwise, false.</returns>
    public bool IsInLine(long position) =>
        position >= LineStart && position <= LineEnd;

    /// <summary>
    /// Compares the current line info with other line info by line number.
    /// </summary>
    /// <param name="other">The other line info to compare.</param>
    /// <returns>A value indicating the order of the objects being compared.</returns>
    public int CompareTo(IDocumentLineInfo? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        return other is null ? 1 : LineNumber.CompareTo(other.LineNumber);
    }
}