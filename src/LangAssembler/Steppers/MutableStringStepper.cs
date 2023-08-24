using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LangAssembler.Steppers.Options;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Steppers;

public class MutableStringStepper : StringStepper, IMutableStringStepper
{
    public MutableStringStepper(string content, ILogger<IStringStepper>? logger) : base(content, logger)
    {
    }

    public MutableStringStepper(BinaryReader reader, Encoding encoding, StepperDisposalOption option, ILogger<IStringStepper>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }

    public void ReplaceRange(Range range, string replacement, out string replacedText,
        StepperTextReplacementPositionOption endPositionOption)
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
        this.JumpToReplaceEnd(remaining, start, end, endPositionOption);
    }

    public void ReplaceAll(Regex pattern, string replaceWith, StepperTextReplacementPositionOption endPositionOption)
    {
        var remaining = Length - Position;
        Content = pattern.Replace(Content, replaceWith);
        this.JumpToReplaceEnd(remaining, remaining, remaining, endPositionOption);
    }
}