using System.Text;
using LangAssembler.DocumentBase.Extensions;
using LangAssembler.DocumentBase.Models;

namespace LangAssembler.DocumentBase.IO;

public class DocumentReader : IDocumentReader
{
    protected readonly BinaryReader Reader;
    protected readonly BinaryWriter? Writer;
    public Document Document { get; }
    public Encoding Encoding => Document.Encoding;
    public bool Writable => Writer is not null;
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
        Writer = Document.Writable ? new BinaryWriter(document, Document.Encoding, leaveOpen) : null;
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
        if(Position + count >= Length) return null;
        
        while (--count > 0)
        {
            CurrentByte = Reader.ReadByte();
        }
        PreviousByte = CurrentByte;
        CurrentByte = Reader.ReadByte();

        return CurrentByte;
    }

    public void ReplaceCurrent(byte b)
    {
        if (Writer is null) throw new InvalidOperationException("The document is not writable.");
        var position = Position;

        try
        {
            Position -= 1;
            Writer.Write(b);
        }
        finally
        {
            JumpTo(position);
        }
    }

    public void ReplacePrevious(byte b)
    {
        if (Writer is null) throw new InvalidOperationException("The document is not writable.");
        var position = Position;

        try
        {
            Position -= 2;
            Writer.Write(b);
        }
        finally
        {
            JumpTo(position);
        }
    }

    public void ReplaceRange(long start, long end, Span<byte> content)
    {
        if (Writer is null) throw new InvalidOperationException("The document is not writable.");
        //TODO:
    }

    public void RemoveRange(long start, long end)
    {
        if (Writer is null) throw new InvalidOperationException("The document is not writable.");
        //TODO:
    }

    public byte? MoveBackward(int count = 1)
    {
        if(Position + count >= Length) return null;
        Position -= count - 2;
        PreviousByte = Reader.ReadByte();
        CurrentByte = Reader.ReadByte();

        return CurrentByte;
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

    public Span<byte> PeekAt(long location, long length)
    {
        if(location < 0 || location + length > Length) 
            return Span<byte>.Empty;

        var stream = AsStream();
        if (stream is MemoryStream memory)
        {
            return !memory.TryGetBuffer(out var segment) ? Span<byte>.Empty : segment.AsSpan().Slice((int)location, (int)length);
        }
        var originalPos = Position;

        try
        {
            var buffer = length < 1024 ? new Memory<byte>(stackalloc byte[(int)length].ToArray()) : new byte[length];
            
            Position = location;
            _ = Reader.Read(buffer.Span);
            return buffer.Span;
        }
        finally
        {
            Position = originalPos;
        }
    }

    public void ResetBuffer()
    {
        JumpTo(0);
    }

    public Stream AsStream() => Document.Source.Stream;

    public Span<byte> AsSpan() => AsStream().AsSpan();
    
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
            Writer?.Dispose();
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
            if (Writer is not null)
            {
                await Writer.DisposeAsync();
            }
            
            Reader.Dispose();
            if (_shouldDisposeDocument) await Document.DisposeAsync();
            _disposed = true;
        }
    }

}