using System.Text;
using LangAssembler.Core;
using LangAssembler.Core.Options;
using LangAssembler.Processors;

namespace LangAssembler.Extensions;

/// <summary>
/// Provides helpers for IStringStepper.
/// </summary>
public static class StringProcessorExtensions
{
    /// <summary>
    /// Increments the position of string processor forward by count.
    /// </summary>
    /// <param name="processor">The processor instance to act upon.</param>
    /// <param name="count">Number of positions to move forward.</param>
    /// <returns><see cref="IStringProcessor.CurrentChar"/></returns>
    public static char? MoveForward(this IStringProcessor processor, int count = 1)
    {
        return processor.JumpTo(processor.Position + count);
    }
    
    /// <summary>
    /// Moves the position of string processor backward by count.
    /// </summary>
    /// <param name="processor">The processor instance to act upon.</param>
    /// <param name="count">Number of positions to move backward.</param>
    /// <returns><see cref="IStringProcessor.CurrentChar"/></returns>
    public static char? MoveBackward(this IStringProcessor processor, int count = 1)
    {
        return processor.JumpTo(processor.Position - count);
    }
    
    /// <summary>
    /// Gets a range of text from a processor.
    /// </summary>
    /// <param name="processor">The processor instance to act upon.</param>
    /// <param name="range">The range to retrieve</param>
    /// <returns>Substring of <see cref="IStringProcessor.Content"/></returns>
    public static string GetRange(this IStringProcessor processor, Range range) => processor[range];
    
    /// <summary>
    /// Looks at the character at a specified position without changing the string processor's position.
    /// </summary>
    /// <param name="processor">The processor instance to act upon.</param>
    /// <param name="position">The position to peek at.</param>
    /// <returns>The character at the peeked position.</returns>
    public static char? PeekAt(this IStringProcessor processor, int position) => processor.ToString(null, null).GetOrNull(position);
    
    /// <summary>
    /// Calculate the new position for text replacement based on
    /// the given TextReplacementPositionOption value.
    /// </summary>
    /// <param name="processor">The processor to act upon</param>
    /// <param name="remaining">The remaining number of characters after the replacement.</param>
    /// <param name="lowerBound">The lower bound value.</param>
    /// <param name="upperBound">The upper bound value.</param>
    /// <param name="endOption">The position option for replacement.</param>
    internal static void JumpToReplaceEnd(this IStringProcessor processor, int remaining, int lowerBound, int upperBound,
        StringProcessorPositionalReplacementOption endOption)
    {
        switch (endOption)
        {
            case StringProcessorPositionalReplacementOption.HugRight:
                processor.JumpTo(upperBound);
                break;
            case StringProcessorPositionalReplacementOption.HugLeft:
                processor.JumpTo(lowerBound);
                break;
            case StringProcessorPositionalReplacementOption.KeepRemaining:
                processor.JumpTo(processor.Length - remaining);
                break;
            case StringProcessorPositionalReplacementOption.DontTouch:
                processor.JumpTo(processor.Position);
                break;
            case StringProcessorPositionalReplacementOption.Reset:
                processor.ResetStepper();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(endOption), endOption, null);
        }
    }
    
    /// <summary>
    /// Moves multiple characters forward from the current position.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <param name="count">Number of characters to move forward.</param>
    /// <returns>The string that was scanned</returns>
    public static string MoveForwardMulti(this IStringProcessor processor, int count = 1)
    {
        int currentPosition = processor.Position, endPosition = currentPosition + count;
        if (endPosition > processor.Length)
        {
            endPosition = processor.Length;
        }

        var str = processor.GetRange(currentPosition..endPosition);
        processor.JumpTo(endPosition);
        return str;
    }
    
    /// <summary>
    /// Moves multiple characters backward from the current position.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <param name="count">Number of characters to move backward.</param>
    /// <returns>The string that was scanned</returns>
    public static string MoveBackwardMulti(this IStringProcessor processor, int count = 1)
    {
        int currentPosition = processor.Position, endPosition = currentPosition - count;
        if (endPosition > processor.Length)
        {
            endPosition = processor.Length;
        }

        var str = processor.GetRange(endPosition..currentPosition);
        processor.JumpTo(endPosition);
        return str;
    }
    
    /// <summary>
    /// Reads and moves the position forward by a certain count.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <param name="count">The number of characters to read.</param>
    /// <param name="includeFirst">Should the first character be included in the count.</param>
    /// <returns>The string read.</returns>
    public static string ReadChars(this IStringProcessor processor, int count, bool includeFirst = false)
    {
        var i = 0;
        var builder = new StringBuilder();
        if (includeFirst && count >= 1)
        {
            i++;
            builder.Append(processor.CurrentChar);
        }

        while (i != count)
        {
            i++;
            builder.Append(processor.MoveForward());
        }

        return builder.ToString();
    }
    
    /// <summary>
    /// Evaluates a condition for a string processor and returns a string containing all characters evaluated as true.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <param name="condition">The condition applied to each character in the string processor.</param>
    /// <returns>The string containing characters that satisfied the condition.</returns>
    public static string GetWhile(this IStringProcessor processor, Func<bool> condition)
    {
        var builder = new StringBuilder();
        while (condition())
        {
            builder.Append(processor.CurrentChar);
        }

        return builder.ToString();
    }
    
    /// <summary>
    /// Checks if a particular text region in the string processor matches the specified text.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <param name="text">The text to match.</param>
    /// <param name="comparison">The type of string comparison to perform.</param>
    /// <returns>The boolean value indicating whether the region matches the given text.</returns>
    public static bool RegionMatches(this IStringProcessor processor, string text, StringComparison comparison = StringComparison.CurrentCulture) =>
        processor.MoveForwardMulti(text.Length).Equals(text, comparison);
    
    /// <summary>
    /// Erases the current character from the string processor.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <returns>The character that was erased, or null if no character was removed.</returns>
    public static char? EraseCurrent(this IEditableStringProcessor processor) => EraseChar(processor, processor.Position);
    
    
    /// <summary>
    /// Removes a range of characters from the string processor.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <param name="range">The range of positions to remove.</param>
    /// <param name="removedText">The text that was removed.</param>
    /// <param name="endPositionOption">Where to move the position to after replacement</param>
    public static void RemoveRange(this IEditableStringProcessor processor, Range range, out string removedText, StringProcessorPositionalReplacementOption endPositionOption = StringProcessorPositionalReplacementOption.DontTouch) =>
        processor.ReplaceRange(range, "", out removedText, endPositionOption);
    
    /// <summary>
    /// Erases the previous character from the string processor.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <returns>The character that was erased, or null if no character was removed.</returns>
    public static char? ErasePrevious(this IEditableStringProcessor processor) => EraseChar(processor, processor.Position - 1);

    /// <summary>
    /// Erases a specific character from the string processor.
    /// </summary>
    /// <param name="processor">The string processor</param>
    /// <param name="position">The position of the character to erase.</param>
    /// <returns>The character that was erased, or null if no character was removed.</returns>
    public static char? EraseChar(this IEditableStringProcessor processor, int position)
    {
        RemoveRange(processor, position..position, out _);
        return processor.JumpTo(processor.Position);
    }
    
    
}