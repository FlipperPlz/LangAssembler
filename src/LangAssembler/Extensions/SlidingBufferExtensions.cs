using LangAssembler.Models.Buffer;

namespace LangAssembler.Extensions;

public static class SlidingBufferExtensions
{
    public static Span<byte> GetRange(this ISlidingBuffer buffer, long start, long end) => 
        buffer.PeekAt(start, end - start);
}