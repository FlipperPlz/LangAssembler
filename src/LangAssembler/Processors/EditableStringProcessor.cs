using System.Text;
using System.Text.RegularExpressions;
using LangAssembler.Core.Options;
using LangAssembler.Extensions;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Processors;

public class EditableStringProcessor : StringProcessor, IEditableStringProcessor
{
    public EditableStringProcessor(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    public EditableStringProcessor(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }

    public void ReplaceRange(Range range, string replacement, out string replacedText,
        StringProcessorPositionalReplacementOption endOption)
    {
        int start = range.Start.Value, end = range.End.Value;
        var remaining = Length - Position;

        if (start < 0 || start > Content.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(range), "Starting index is out of bounds.");
        }

        if (end < start || end > Content.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(range), "Ending index is out of bounds.");
        }


        replacedText = this.GetRange(range);
        Content = string.Concat(Content.AsSpan(0, start), replacement, Content.AsSpan(end));
        this.JumpToReplaceEnd(remaining, start, end, endOption);
    }

    public void ReplaceAll(Regex pattern, string replaceWith, StringProcessorPositionalReplacementOption endOption)
    {
        var remaining = Length - Position;
        Content = pattern.Replace(Content, replaceWith);
        this.JumpToReplaceEnd(remaining, remaining, remaining, endOption);
    }
}