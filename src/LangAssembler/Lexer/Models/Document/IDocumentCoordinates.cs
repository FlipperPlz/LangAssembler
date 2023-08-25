namespace LangAssembler.Lexer.Models.Document;

/// <summary>
/// Defines methods and properties to represent coordinates within a document.
/// </summary>
public interface IDocumentCoordinates
{
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
}