namespace LangAssembler.Extensions;

public static class StreamExtensions
{

    public static Span<byte> AsSpan(this Stream stream, int bufferSize = 1024)
    {
        if (stream is MemoryStream memoryStream)
        {
            memoryStream.GetBuffer().AsSpan(0, (int)memoryStream.Length);
        }

        var oldPosition = stream.Position;
        stream.Seek(0, SeekOrigin.Begin);
        var memory = new Memory<byte>(new byte[bufferSize]);
        int read, totalRead = 0;
        while ((read = stream.Read(memory.Span[totalRead..])) > 0)
        {
            totalRead += read;
        }
        stream.Seek(oldPosition, SeekOrigin.Begin);

        return memory.Span[..totalRead];
    }
}