using System.ComponentModel.DataAnnotations;

namespace LangAssembler.SlidingWindow;

public interface ISlidingBuffer
{
    public long Position { get; }
    public long Length { get; }
    
    public byte CurrentByte { get; }
    public byte PreviousByte { get; }

    public byte JumpTo();
    public Span<byte> PeekAt(long location, [Range(0, int.MaxValue)] int length);

    public Stream AsStream();
    public Span<byte> AsSpan();
}