using LangAssembler.DocumentBase.Models;
using LangAssembler.IO;

namespace LangAssembler.DocumentBase.IO;

public interface IDocumentReader : IEncodedSlidingBuffer, IDisposable, IAsyncDisposable, IEditableSlidingBuffer
{
    public Document Document { get; }
}