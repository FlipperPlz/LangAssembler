using System.Text.RegularExpressions;
using LangAssembler.Core.Options;

namespace LangAssembler.Processors;

/// <summary>
/// Implementation of IBisStringStepper with the added ability to edit data as you're stepping through it.
/// </summary>
public interface IEditableStringProcessor : IStringProcessor
{
    /// <summary>
    /// Replaces the range of the string with the specified replacement.
    /// </summary>
    void ReplaceRange(Range range, string replacement, out string replacedText, StringProcessorPositionalReplacementOption endOption);

    /// <summary>
    /// Replaces all pattern matches defined by the regex in the string.
    /// </summary>
    void ReplaceAll(Regex pattern, string replaceWith, StringProcessorPositionalReplacementOption endOption);
    
}