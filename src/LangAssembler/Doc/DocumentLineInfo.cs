namespace LangAssembler.Doc;


/// <summary>
/// Represents detailed information about a line of document.
/// </summary>
public class DocumentLineInfo
{    
    /// <summary>
    /// Gets the document where this line was found.
    /// </summary>
    public readonly IDocument Document;
    
    /// <summary>
    /// The number of the line in the document.
    /// </summary>
    public readonly long LineNumber;
    
    public long LineStart;
    public long LineEnd;

    /// <summary>
    /// Gets the length of the line.
    /// </summary>
    public long LineLength { get; private set; }

    /// <summary>
    /// Initializes a new instance of <see cref="DocumentLineInfo"/>, setting the start, end, and length of the line in a document.
    /// </summary>
    /// <param name="document">The document this line was found in.</param>
    /// <param name="lineNumber">The line number.</param>
    /// <param name="lineStart">Start position of the line in a document.</param>
    /// <param name="lineEnd">End position of the line in a document.</param>
    public DocumentLineInfo(IDocument document, long lineNumber, long lineStart, long lineEnd)
    {
        Document = document;
        LineNumber = lineNumber;
        ChangeLineIndex(lineStart, lineEnd);
    }

    public void ChangeLineIndex(long lineStart, long lineEnd)
    {
        
        LineStart = lineStart;
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

}