using System.Text;
using LangAssembler.Core.Options;
using LangAssembler.Lexer.Models.Document;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Processors.Tracked;

public class TrackedStringProcessor : StringProcessor, ITrackedStringProcessorBase
{
    public int LineNumber { get; protected set; } = 1;
    public int ColumnNumber { get; protected set; }
    public int LineStart { get; protected set; }
    public IEnumerable<DocumentLineInfo> LineInfo => EditableLineInfo;
    protected readonly List<DocumentLineInfo> EditableLineInfo = new List<DocumentLineInfo>();

    public override char? CurrentChar
    {
        get => base.CurrentChar;
        protected set => SetCurrentChar(value);
    } 
    
    public TrackedStringProcessor(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    public TrackedStringProcessor(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }
    public override void ResetStepper(string? content = null)
    {
        EditableLineInfo.Clear();
        base.ResetStepper(content);
    }

    protected virtual void SetCurrentChar(char? value)
    {
        if (value is '\n')
        {
            IncrementLine();
        }

        base.CurrentChar = value;
    }

    protected virtual void IncrementLine()
    {
        EditableLineInfo.Insert(LineNumber, new DocumentLineInfo(LineStart..Position));
        LineNumber++;
        ColumnNumber = 0;
        LineStart = Position + 1;
    }

    public virtual string? GetLineText(int lineNumber) =>
        LineInfo.ElementAtOrDefault(lineNumber)?.GetLineText(this);

}