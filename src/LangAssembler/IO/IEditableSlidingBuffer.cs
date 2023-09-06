namespace LangAssembler.IO;

public interface IEditableSlidingBuffer : ISlidingBuffer
{
    public void ReplaceCurrent(byte b);
    public void ReplacePrevious(byte b);

    public void ReplaceRange(long start, long end, Span<byte> content);
    public void RemoveRange(long start, long end);
    
}