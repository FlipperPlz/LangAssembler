using LangAssembler.Models.Buffer.Editable;
using LangAssembler.Models.Buffer.Encoded;
using LangAssembler.Models.Doc;

namespace LangAssembler.IO;

public interface IDocumentReader : IEncodedSlidingBuffer, IDisposable, IAsyncDisposable, IEditableSlidingBuffer
{
    public Document Document { get; }
}