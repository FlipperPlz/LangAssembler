using System.Text;
using LangAssembler.Extensions;
using LangAssembler.Internal;
using LangAssembler.Options;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Processors;

public class StringProcessor : LaLoggable<IStringProcessor>, IStringProcessor
{
    /// <summary>
    /// This is the content of the stepper.
    /// </summary>
    /// <remarks>Sometimes referred to as the buffer.</remarks>
    public string Content { get; protected set; }

    /// <summary>
    /// This is the length of the content loaded.
    /// </summary>
    public int Length => Content.Length - 1;

    /// <summary>
    /// This is the current position of the sliding window within the content buffer.
    /// </summary>
    public int Position { get; private set; } = -1;

    /// <summary>
    /// Gets the current character in the content at the Position. If this is null there are one of three reasons:
    /// 1. Our position is before the buffer. (BOF)
    /// 2. Our position is after the buffer. (EOF)
    /// 3. Our buffer is empty, what are you doing! D:
    /// </summary>
    public virtual char? CurrentChar { get; protected set; }

    /// <summary>
    /// Gets the character in the content just before the Position. Nullability is used the same way as its used in
    /// <see cref="IStringProcessor.CurrentChar"/>.
    /// </summary>
    public virtual char? PreviousChar { get; protected set; }
    
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the StringProcessor class with given string content.
    /// </summary>
    /// <param name="content">The string content to be processed.</param>
    /// <param name="logger">The logger used by this instance.</param>
    public StringProcessor(string content, ILogger<IStringProcessor>? logger) : base(logger)
    {
        Content = content;
    }
    
    /// <summary>
    /// Initializes a new instance of the StringProcessor class with content read from a BinaryReader given a certain encoding and disposal option.
    /// </summary>
    /// <param name="reader">The BinaryReader where the content is read.</param>
    /// <param name="encoding">The Encoding used to read the content.</param>
    /// <param name="option">The disposal option used after reading the content.</param>
    /// <param name="logger">The logger used by this instance.</param>
    /// <param name="length">The length of the content to read. If not specified, the data from the current position to the end of the stream will be read.</param>
    /// <param name="stringStart">The start position in the BinaryReader where the content begins. If not specified, the current position of the BinaryReader will be used.</param>
    public StringProcessor(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(logger)
    {
        var backupStart = reader.BaseStream.Position;
        var start = backupStart;
        if (stringStart is {  } st)
        {
            start = st;
            reader.BaseStream.Seek(st, SeekOrigin.Begin);
        }
        var byteCount = length ?? (int)(reader.BaseStream.Length - reader.BaseStream.Position);

        Content = encoding.GetString(reader.ReadBytes(byteCount));
        switch (option)
        {
            case StringProcessorDisposalOption.JumpBackToStart:
                reader.BaseStream.Seek(backupStart, SeekOrigin.Begin);
                break;
            case StringProcessorDisposalOption.JumpToStringStart:
                reader.BaseStream.Seek(start, SeekOrigin.Begin);
                break;
            case StringProcessorDisposalOption.Dispose:
                reader.Dispose();
                break;
            case StringProcessorDisposalOption.JumpToStringEnd:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
    }

    ~StringProcessor() => Dispose(false);

    /// <summary>
    /// Jumps to a certain position and correctly sets <see cref="IStringProcessor.CurrentChar"/> and <see cref="IStringProcessor.PreviousChar"/>.
    /// </summary>
    /// <param name="position">The position to jump to.</param>
    public virtual char? JumpTo(int position)
    {
        Position = position;
        PreviousChar = Content.GetOrNull(position - 1);
        return CurrentChar = Content.GetOrNull(position);
    }

    /// <summary>
    /// Resets the contents of the stepper and resets the window.
    /// </summary>
    /// <param name="content">The new content to write to buffer.</param>
    public virtual void ResetStepper(string? content = null)
    {
        if (content is not null)
        {
            Content = content;
        }

        Position = -1;
        PreviousChar = null;
        CurrentChar = null;
    }
    
    
    /// <summary>
    /// Gets the substring from the content with a specified range.
    /// </summary>
    /// <param name="index">A Range representing the start and end indices of the substring in the content.</param>
    /// <returns>The substring from the content specified by the range.</returns>
    public string this[Range index] => this.GetRange(index);

    
    protected virtual void Dispose(bool disposing)
    {
        // Check if this object has been disposed already.
        if (_disposed)
        {
            return;
        }

        if (!disposing) return;
        Content = string.Empty;
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

}