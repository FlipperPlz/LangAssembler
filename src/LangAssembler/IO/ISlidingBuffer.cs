using System.ComponentModel.DataAnnotations;

namespace LangAssembler.IO;

public interface ISlidingBuffer
{
    public long Position { get; }
    public long Length { get; }
    
    public byte? CurrentByte { get; }
    public byte? PreviousByte { get; }

    public byte? JumpTo(long position);
    public byte? MoveForward([Range(0, int.MaxValue)] int count = 1);
    public byte? MoveBackward([Range(0, int.MaxValue)] int count = 1);
    public byte? PeekNext();

    public ReadOnlyMemory<byte> PeekAt(long location, [Range(0, int.MaxValue)] int length);

    public Stream AsStream();
    public Span<byte> AsSpan();
}