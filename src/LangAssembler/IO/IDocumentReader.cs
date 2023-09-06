using LangAssembler.Models;
using LangAssembler.Models.Buffer.Editable;
using LangAssembler.Models.Buffer.Encoded;

namespace LangAssembler.IO;

public interface IDocumentReader : IEncodedSlidingBuffer, IDisposable, IAsyncDisposable, IEditableSlidingBuffer
{
    public Document Document { get; }
}