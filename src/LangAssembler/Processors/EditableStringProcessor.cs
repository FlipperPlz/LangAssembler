using System.Text;
using System.Text.RegularExpressions;
using LangAssembler.Extensions;
using LangAssembler.Options;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Processors;

public class EditableStringProcessor : StringProcessor, IEditableStringProcessor
{
    /// <inheritdoc />
    public EditableStringProcessor(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    /// <inheritdoc />
    public EditableStringProcessor(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }

    /// <summary>
    /// Replaces the range of the string with the specified replacement.
    /// </summary>
    /// <param name="range">The range of text to replace.</param>
    /// <param name="replacement">The text to replace the range with.</param>
    /// <param name="replacedText">Output parameter that the replaced text is written to.</param>
    /// <param name="endOption">Option that controls the position of the processor after the replace operation.</param>
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

    /// <summary>
    /// Replaces all pattern matches defined by the regex in the string.
    /// </summary>
    /// <param name="pattern">The Regex pattern that matches the text to be replaced.</param>
    /// <param name="replaceWith">The text to replace the matched text with.</param>
    /// <param name="endOption">Option that controls the position of the processor after the replace operation.</param>
    public void ReplaceAll(Regex pattern, string replaceWith, StringProcessorPositionalReplacementOption endOption)
    {
        var remaining = Length - Position;
        Content = pattern.Replace(Content, replaceWith);
        this.JumpToReplaceEnd(remaining, remaining, remaining, endOption);
    }
}