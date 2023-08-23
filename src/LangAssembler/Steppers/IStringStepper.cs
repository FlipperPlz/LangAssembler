namespace LangAssembler.Steppers;


/// <summary>
/// Provides an interface for a simple char by char reader, relies on a sliding window to keep track
/// of position. Currently content size is limited by <see cref="int.MaxValue"/>.
/// </summary>
public interface IStringStepper : IDisposable, IFormattable
{
    /// <summary>
    /// This is the content of the stepper.
    /// </summary>
    /// <remarks>Sometimes referred to as the buffer.</remarks>
    protected string Content { get; }
    
    /// <summary>
    /// This is the length of the content loaded.
    /// </summary>
    public int Length { get; }
    
    /// <summary>
    /// This is the current position of the sliding window within the content buffer.
    /// </summary>
    public int Position { get; }
    
    /// <summary>
    /// Gets the current character in the content at the Position. If this is null there are one of three reasons:
    /// 1. Our position is before the buffer. (BOF)
    /// 2. Our position is after the buffer. (EOF)
    /// 3. Our buffer is empty, what are you doing! D:
    /// </summary>
    public char? CurrentChar { get; }

    /// <summary>
    /// Gets the character in the content just before the Position. Nullability is used the same way as its used in
    /// <see cref="CurrentChar"/>.
    /// </summary>
    public char? PreviousChar { get; }
    
    
    /// <summary>
    /// Increments the position of string stepper forward by count.
    /// </summary>
    /// <param name="count">Number of positions to move forward.</param>
    public void MoveForward(int count);
    
    
    /// <summary>
    /// Moves the position of string stepper backward by count.
    /// </summary>
    /// <param name="count">Number of positions to move backward.</param>
    public char? MoveBackward(int count);
    
    /// <summary>
    /// Simple interface level getter for Content.
    /// </summary>
    string IFormattable.ToString(string? format, IFormatProvider? formatProvider) => Content;
}

