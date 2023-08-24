using System;
using System.Text.RegularExpressions;
using LangAssembler.Steppers.Options;

namespace LangAssembler.Steppers;

/// <summary>
/// Implementation of IBisStringStepper with the added ability to edit data as you're stepping through it.
/// </summary>
public interface IMutableStringStepper : IStringStepper
{
    /// <summary>
    /// Replaces the range of the string with the specified replacement.
    /// </summary>
    void ReplaceRange(Range range, string replacement, out string replacedText, StepperTextReplacementPositionOption endPositionOption);

    /// <summary>
    /// Replaces all pattern matches defined by the regex in the string.
    /// </summary>
    void ReplaceAll(Regex pattern, string replaceWith, StepperTextReplacementPositionOption endPositionOption);
}