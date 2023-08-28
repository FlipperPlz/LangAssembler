using System.Text;
using LangAssembler.Document;
using LangAssembler.Options;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Processors.Tracked;

/// <summary>
/// Represents a string processor attached to a document that contains tracking information.
/// </summary>
public class TrackedStringProcessor : StringProcessor, ITrackedStringProcessorBase
{
    /// <summary>
    /// Gets the line number in the document.
    /// </summary>
    public int LineNumber { get; protected set; } = 1;

    /// <summary>
    /// Gets the column number within the line in the document.
    /// </summary>
    public int ColumnNumber { get; protected set; }

    /// <summary>
    /// Gets the start index of the line in the document.
    /// </summary>
    public int LineStart { get; protected set; }

    protected readonly List<DocumentLineInfo> EditableLineInfo = new List<DocumentLineInfo>();

    /// <summary>
    /// Gets an enumeration of line information for each line in the document.
    /// </summary>
    public IEnumerable<DocumentLineInfo> LineInfos => EditableLineInfo;

    public bool FullyRead { get; }


    public TrackedStringProcessor(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    public TrackedStringProcessor(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }

    /// <summary>
    /// Resets the contents of the stepper and resets the window.
    /// </summary>
    /// <param name="content">The new content to write to buffer.</param>
    public override void ResetStepper(string? content = null)
    {
        EditableLineInfo.Clear();
        base.ResetStepper(content);
    }
    
    /// <summary>
    /// Advances to the next line in the document by storing the end position of the current line, incrementing the line number,
    /// resetting the column count, and setting the start position for the next line.
    /// </summary>
    protected virtual void IncrementLine()
    {
        EditableLineInfo.Insert(LineNumber, new DocumentLineInfo(LineStart..Position));
        LineNumber++;
        ColumnNumber = 0;
        LineStart = Position + 1;
    }

}