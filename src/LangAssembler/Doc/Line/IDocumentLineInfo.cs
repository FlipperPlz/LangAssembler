namespace LangAssembler.Doc.Line;

/// <summary>
/// Represents detailed information about a line of a document
/// </summary>
public interface IDocumentLineInfo : IComparable<IDocumentLineInfo>
{
    /// <summary>
    /// Gets the document where the line info was found
    /// </summary>
    IDocument Document { get; }
    
    /// <summary>
    /// Returns the line number
    /// </summary>
    long LineNumber { get; }
    
    /// <summary>
    /// Gets or sets the start of the line
    /// </summary>
    long LineStart { get; }
    
    /// <summary>
    /// Gets or sets the end of the line
    /// </summary>
    long LineEnd { get; }
    
    /// <summary>
    /// Gets the length of the line
    /// </summary>
    long LineLength { get; }

    /// <summary>
    /// Determines whether a position is within the line
    /// </summary>
    /// <param name="position">A position within the document</param>
    /// <returns><code>true</code> if the position is within the line; otherwise, <code>false</code></returns>
    bool IsInLine(long position);
}