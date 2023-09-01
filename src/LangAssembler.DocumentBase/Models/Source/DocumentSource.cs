using System.Text;

namespace LangAssembler.DocumentBase.Models.Source;

public abstract class DocumentSource : IDisposable, IAsyncDisposable
{
    public abstract Stream Stream { get; }
    public abstract string Name { get; }
    public abstract bool IsVirtual { get; }
    public long Position { get => Stream.Position; set => Stream.Seek(value, SeekOrigin.Begin); }
    public long Length => Stream.Length;
    private bool _disposed;
    
    public static implicit operator Stream(DocumentSource d) => d.Stream;

    
    ~DocumentSource()
    {
        Dispose(false);
    }
    
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
        if (disposing)
        {
            _disposed = true;
        }
    }

    protected virtual ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            _disposed = true;
        }

        return ValueTask.CompletedTask;
    }

    public sealed class FileSystem : DocumentSource
    {
        public readonly FileInfo File;
        public override FileStream Stream { get; }
        public string DocumentPath => File.FullName;
        public override string Name => File.Name;
        public override bool IsVirtual => false;
        
        public FileSystem(FileInfo info, FileMode mode, FileAccess access)
        {
            File = info;
            Stream = File.Open(mode, access);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stream.Dispose();
            }
            
            base.Dispose(disposing);
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await Stream.DisposeAsync();
            }
            
            await base.DisposeAsync(disposing);
        }
    }

    public class Virtual : DocumentSource
    {
        public override MemoryStream Stream { get; }
        public override string Name { get; }
        public sealed override bool IsVirtual => true;
        
        public Virtual(string name, MemoryStream stream)
        {
            Name = name;
            Stream = stream;
        }

        public Virtual(string name, string text, Encoding encoding) : this(name,
            new MemoryStream(encoding.GetBytes(text), true))
        {
            
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Stream.Dispose();
            }
            
            base.Dispose(disposing);
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await Stream.DisposeAsync();
            }
            
            await base.DisposeAsync(disposing);
        }
    }
}