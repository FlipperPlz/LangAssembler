using LangAssembler.Models.Buffer.Encoded;

namespace LangAssembler.Extensions;

public static class SlidingEncodedBufferExtensions
{
    public static string GetRange(this IEncodedSlidingBuffer buffer, long start, long end) =>
        buffer.Encoding.GetString(buffer.PeekAt(start, end - start));
        
}