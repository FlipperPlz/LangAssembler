using LangAssembler.Processors;

namespace LangAssembler.Document;


/// <summary>
/// Represents detailed information about a line of document.
/// </summary>
public class DocumentLineInfo
{
    private Range _lineIndex;
    
    /// <summary>
    /// Gets or sets the range that represents the index of the line of document.
    /// </summary>
    public Range LineIndex
    {
        get => _lineIndex;
        set
        {
            _lineIndex = value;
            LineLength = _lineIndex.End.Value - _lineIndex.Start.Value;
        }
    }
    
    /// <summary>
    /// Gets the length of the line.
    /// </summary>
    public int LineLength { get; private set; }

    /// <summary>
    /// Initializes a new instance of the DocumentLineInfo class with the given line index.
    /// </summary>
    /// <param name="lineIndex">The range that represents the index of the line in the document.</param>
    public DocumentLineInfo(Range lineIndex) => LineIndex = lineIndex;
    
    /// <summary>
    /// Determines whether the given position is within the line.
    /// </summary>
    /// <param name="position">The position in the document.</param>
    /// <returns>true if the given position is within the line; otherwise, false.</returns>
    public bool IsInLine(int position) =>
        position >= LineIndex.Start.Value && position <= LineIndex.End.Value;

    /// <summary>
    /// Returns the text of the line.
    /// </summary>
    /// <param name="processor">An processor used to retrieve the text of the line.</param>
    /// <returns>The text of the line.</returns>
    public string GetLineText(IStringProcessor processor) =>
        processor[_lineIndex];
}