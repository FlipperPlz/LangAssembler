using System.Text;
using LangAssembler.DocumentBase.Extensions;
using LangAssembler.DocumentBase.Models;
using LangAssembler.IO;

namespace LangAssembler.DocumentBase.IO;

public class DocumentReader : IEncodedSlidingBuffer, IDisposable, IAsyncDisposable
{
    public readonly Document Document;
    
    protected readonly BinaryReader Reader;
    public Encoding Encoding => Document.Encoding;
    public long Length => Document.Source.Length;
    public long Position { get => Document.Source.Position; protected set => Document.Source.Position = value; }
    
    public byte? CurrentByte { get; protected set; }
    public byte? PreviousByte { get; protected set; }
    
    private bool _disposed;
    private readonly bool _shouldDisposeDocument;
    
    public DocumentReader(Document document, bool leaveOpen = false)
    {
        _shouldDisposeDocument = !leaveOpen;
        Document = document;
        Reader = new BinaryReader(document, Document.Encoding, leaveOpen);
    }

    ~DocumentReader() => Dispose(false);
    
    
    public byte? JumpTo(long position)
    {
        if(--position < 0 || position >= Length) return null;
        Position = position;
        PreviousByte = null;
        CurrentByte = Reader.ReadByte();

        return CurrentByte;
    }

    public byte? MoveForward(int count = 1)
    {
        throw new NotImplementedException();
    }

    public byte? MoveBackward(int count = 1)
    {
        throw new NotImplementedException();
    }

    public byte? PeekNext()
    {
        if(Position + 1 >= Length) return null;
        try
        {
            return Reader.ReadByte();
        }
        finally
        {
            Position -= 1;
        }
    }

    public ReadOnlyMemory<byte> PeekAt(long location, int length)
    {
        if(location < 0 || location + length > Length) 
            return ReadOnlyMemory<byte>.Empty;

        var stream = AsStream();
        if (stream is MemoryStream memory)
        {
            return !memory.TryGetBuffer(out var segment) ? ReadOnlyMemory<byte>.Empty : segment.AsMemory().Slice((int)location, length);
        }
        var originalPos = Position;

        try
        {
            var result = new Memory<byte>(new byte[length]);

            Position = location;
            _ = Reader.Read(result.Span);
            return result;
        }
        finally
        {
            Position = originalPos;
        }
    }

    public Stream AsStream() => Document.Source.Stream;

    public Span<byte> AsSpan() => AsStream().ToSpan();


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            Reader.Dispose();
            if (_shouldDisposeDocument) Document.Dispose();
            _disposed = true;
        }
    }

    protected async ValueTask DisposeAsync(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            Reader.Dispose();
            if (_shouldDisposeDocument) await Document.DisposeAsync();
            _disposed = true;
        }
    }
}