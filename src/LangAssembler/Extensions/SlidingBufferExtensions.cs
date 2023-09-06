using LangAssembler.Models.Buffer;

namespace LangAssembler.Extensions;

public static class SlidingBufferExtensions
{
    public static Span<byte> PeekRange(this ISlidingBuffer buffer, long start, long end) => 
        buffer.PeekAt(start, end - start);
    
    public static Span<byte> PeekNext(this ISlidingBuffer buffer, long count) => 
        buffer.PeekRange(buffer.Position, buffer.Position + count);
}