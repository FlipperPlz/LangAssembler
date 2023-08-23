using LangAssembler.Extensions;

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
        stepper.MoveForward(count);
        return stepper.CurrentChar;
    }
    
    /// <summary>
    /// Moves the position of string stepper backward by count.
    /// </summary>
    /// <param name="stepper">The stepper instance to act upon.</param>
    /// <param name="count">Number of positions to move backward.</param>
    /// <returns><see cref="IStringStepper.CurrentChar"/></returns>
    public static char? MoveBackward(this IStringStepper stepper, int count = 1)
    {
        stepper.MoveForward(count);
        return stepper.CurrentChar;
    }


    /// <summary>
    /// Looks at the character at a specified position without changing the string stepper's position.
    /// </summary>
    /// <param name="stepper">The stepper instance to act upon.</param>
    /// <param name="position">The position to peek at.</param>
    /// <returns>The character at the peeked position.</returns>
    public static char? PeekAt(this IStringStepper stepper, int position) => stepper.ToString(null, null).GetOrNull(position);
}