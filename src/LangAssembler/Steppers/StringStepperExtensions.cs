using System;
using System.Text;
using LangAssembler.Extensions;
using LangAssembler.Steppers.Options;

namespace LangAssembler.Steppers;

/// <summary>
/// Provides helpers for IStringStepper.
/// </summary>
public static class StringStepperExtensions
{
    /// <summary>
    /// Increments the position of string stepper forward by count.
    /// </summary>
    /// <param name="stepper">The stepper instance to act upon.</param>
    /// <param name="count">Number of positions to move forward.</param>
    /// <returns><see cref="IStringStepper.CurrentChar"/></returns>
    public static char? MoveForward(this IStringStepper stepper, int count = 1)
    {
        return stepper.JumpTo(stepper.Position + count);
    }
    
    /// <summary>
    /// Moves the position of string stepper backward by count.
    /// </summary>
    /// <param name="stepper">The stepper instance to act upon.</param>
    /// <param name="count">Number of positions to move backward.</param>
    /// <returns><see cref="IStringStepper.CurrentChar"/></returns>
    public static char? MoveBackward(this IStringStepper stepper, int count = 1)
    {
        return stepper.JumpTo(stepper.Position - count);
    }
    
    /// <summary>
    /// Gets a range of text from a stepper.
    /// </summary>
    /// <param name="stepper">The stepper instance to act upon.</param>
    /// <param name="range">The range to retrieve</param>
    /// <returns>Substring of <see cref="IStringStepper.Content"/></returns>
    public static string GetRange(this IStringStepper stepper, Range range) => stepper[range];
    
    /// <summary>
    /// Looks at the character at a specified position without changing the string stepper's position.
    /// </summary>
    /// <param name="stepper">The stepper instance to act upon.</param>
    /// <param name="position">The position to peek at.</param>
    /// <returns>The character at the peeked position.</returns>
    public static char? PeekAt(this IStringStepper stepper, int position) => stepper.ToString(null, null).GetOrNull(position);
    
    /// <summary>
    /// Calculate the new position for text replacement based on
    /// the given TextReplacementPositionOption value.
    /// </summary>
    /// <param name="stepper">The stepper to act upon</param>
    /// <param name="remaining">The remaining number of characters after the replacement.</param>
    /// <param name="lowerBound">The lower bound value.</param>
    /// <param name="upperBound">The upper bound value.</param>
    /// <param name="endPositionOption">The position option for replacement.</param>
    internal static void JumpToReplaceEnd(this IStringStepper stepper, int remaining, int lowerBound, int upperBound,
        StepperTextReplacementPositionOption endPositionOption)
    {
        switch (endPositionOption)
        {
            case StepperTextReplacementPositionOption.HugRight:
                stepper.JumpTo(upperBound);
                break;
            case StepperTextReplacementPositionOption.HugLeft:
                stepper.JumpTo(lowerBound);
                break;
            case StepperTextReplacementPositionOption.KeepRemaining:
                stepper.JumpTo(stepper.Length - remaining);
                break;
            case StepperTextReplacementPositionOption.DontTouch:
                stepper.JumpTo(stepper.Position);
                break;
            case StepperTextReplacementPositionOption.Reset:
                stepper.ResetStepper();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(endPositionOption), endPositionOption, null);
        }
    }
    
    /// <summary>
    /// Moves multiple characters forward from the current position.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <param name="count">Number of characters to move forward.</param>
    /// <returns>The string that was scanned</returns>
    public static string MoveForwardMulti(this IStringStepper stepper, int count = 1)
    {
        int currentPosition = stepper.Position, endPosition = currentPosition + count;
        if (endPosition > stepper.Length)
        {
            endPosition = stepper.Length;
        }

        var str = stepper.GetRange(currentPosition..endPosition);
        stepper.JumpTo(endPosition);
        return str;
    }
    
    /// <summary>
    /// Moves multiple characters backward from the current position.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <param name="count">Number of characters to move backward.</param>
    /// <returns>The string that was scanned</returns>
    public static string MoveBackwardMulti(this IStringStepper stepper, int count = 1)
    {
        int currentPosition = stepper.Position, endPosition = currentPosition - count;
        if (endPosition > stepper.Length)
        {
            endPosition = stepper.Length;
        }

        var str = stepper.GetRange(endPosition..currentPosition);
        stepper.JumpTo(endPosition);
        return str;
    }
    
    /// <summary>
    /// Reads and moves the position forward by a certain count.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <param name="count">The number of characters to read.</param>
    /// <param name="includeFirst">Should the first character be included in the count.</param>
    /// <returns>The string read.</returns>
    public static string ReadChars(this IStringStepper stepper, int count, bool includeFirst = false)
    {
        var i = 0;
        var builder = new StringBuilder();
        if (includeFirst && count >= 1)
        {
            i++;
            builder.Append(stepper.CurrentChar);
        }

        while (i != count)
        {
            i++;
            builder.Append(stepper.MoveForward());
        }

        return builder.ToString();
    }
    
    /// <summary>
    /// Evaluates a condition for a string stepper and returns a string containing all characters evaluated as true.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <param name="condition">The condition applied to each character in the string stepper.</param>
    /// <returns>The string containing characters that satisfied the condition.</returns>
    public static string GetWhile(this IStringStepper stepper, Func<bool> condition)
    {
        var builder = new StringBuilder();
        while (condition())
        {
            builder.Append(stepper.CurrentChar);
        }

        return builder.ToString();
    }
    
    /// <summary>
    /// Removes a range of characters from the string stepper.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <param name="range">The range of positions to remove.</param>
    /// <param name="removedText">The text that was removed.</param>
    /// <param name="endPositionOption">Where to move the position to after replacement</param>
    public static void RemoveRange(this IMutableStringStepper stepper, Range range, out string removedText, StepperTextReplacementPositionOption endPositionOption = StepperTextReplacementPositionOption.DontTouch) =>
        stepper.ReplaceRange(range, "", out removedText, endPositionOption);
    
    /// <summary>
    /// Checks if a particular text region in the string stepper matches the specified text.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <param name="text">The text to match.</param>
    /// <param name="comparison">The type of string comparison to perform.</param>
    /// <returns>The boolean value indicating whether the region matches the given text.</returns>
    public static bool RegionMatches(this IStringStepper stepper, string text, StringComparison comparison = StringComparison.CurrentCulture) =>
        stepper.MoveForwardMulti(text.Length).Equals(text, comparison);
    
    /// <summary>
    /// Erases the current character from the string stepper.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <returns>The character that was erased, or null if no character was removed.</returns>
    public static char? EraseCurrent(this IMutableStringStepper stepper) => EraseChar(stepper, stepper.Position);
    
    /// <summary>
    /// Erases the previous character from the string stepper.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <returns>The character that was erased, or null if no character was removed.</returns>
    public static char? ErasePrevious(this IMutableStringStepper stepper) => EraseChar(stepper, stepper.Position - 1);

    /// <summary>
    /// Erases a specific character from the string stepper.
    /// </summary>
    /// <param name="stepper">The string stepper</param>
    /// <param name="position">The position of the character to erase.</param>
    /// <returns>The character that was erased, or null if no character was removed.</returns>
    public static char? EraseChar(this IMutableStringStepper stepper, int position)
    {
        RemoveRange(stepper, position..position, out _);
        return stepper.JumpTo(stepper.Position);
    }
}