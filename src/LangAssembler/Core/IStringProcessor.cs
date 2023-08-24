using LangAssembler.Internal;

namespace LangAssembler.Core;

/// <summary>
/// Provides an interface for a simple char by char reader, relies on a sliding window to keep track
/// of position. Currently content size is limited by <see cref="int.MaxValue"/>.
/// </summary>
public interface IStringProcessor : IDisposable, IFormattable, ILaLoggable<IStringProcessor>
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
    /// Jumps to a certain position and correctly sets <see cref="CurrentChar"/> and <see cref="PreviousChar"/>.
    /// </summary>
    /// <param name="position">The position to jump to.</param>
    public char? JumpTo(int position);
    
    /// <summary>
    /// Resets the contents of the stepper and resets the window.
    /// </summary>
    /// <param name="content">The new content to write to buffer.</param>
    public void ResetStepper(string? content = null);
    
    /// <summary>
    /// Simple interface level getter for Content.
    /// </summary>
    string IFormattable.ToString(string? format, IFormatProvider? formatProvider) => Content;

    
    /// <summary>
    /// Gets a range of text from a stepper.
    /// </summary>
    /// <param name="range">The range to retrieve</param>
    /// <returns>Substring of <see cref="Content"/></returns>
    string this[Range range] => Content[range];
}

