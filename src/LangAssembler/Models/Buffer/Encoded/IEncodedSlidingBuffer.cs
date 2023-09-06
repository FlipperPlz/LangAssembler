using System.Text;

namespace LangAssembler.Models.Buffer.Encoded;

public interface IEncodedSlidingBuffer : ISlidingBuffer
{
    public Encoding Encoding { get; }
}