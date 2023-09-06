namespace LangAssembler.Models.Buffer.Editable;

public interface IEditableSlidingBuffer : ISlidingBuffer
{
    public void ReplaceCurrent(byte b);
    public void ReplacePrevious(byte b);

    public void ReplaceRange(long start, long end, ReadOnlySpan<byte> content);
    public void RemoveRange(long start, long end);

    bool ISlidingBuffer.CanEdit => true;
}