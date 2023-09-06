using System.Text;

namespace LangAssembler.Models.Doc.Source;

public abstract class DocumentSource : IDisposable, IAsyncDisposable
{
    public abstract Stream Stream { get; }
    public abstract string Name { get; }
    public abstract bool IsVirtual { get; }
    
    public static implicit operator Stream(DocumentSource d) => d.Stream;
    public virtual bool CanResize => CanWrite;
    public virtual bool CanWrite => Stream.CanWrite;
    public long Position { get => Stream.Position; set => Stream.Seek(value, SeekOrigin.Begin); }
    public long Length => Stream.Length;
    private bool _disposed;
    
    ~DocumentSource()
    {
        Dispose(false);
    }

    #region Default Source Types Instances

    public class FileSystem : DocumentSource
    {
        public readonly FileInfo File;
        protected readonly bool Expandable;

        public override FileStream Stream { get; }
        
        public string DocumentPath => File.FullName;
        public override string Name => File.Name;
        public override bool IsVirtual => false;
        public override bool CanResize => Expandable;

        public FileSystem(FileInfo info, FileMode mode, FileAccess access)
        {
            File = info;
            Stream = File.Open(mode, access);
            Expandable = mode != FileMode.Append && !info.IsReadOnly && access != FileAccess.Read;
        }

        #region IDisposable Support
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
        #endregion
    }

    public class Virtual : DocumentSource
    {
        public override MemoryStream Stream { get; }
        public override string Name { get; }
        public sealed override bool IsVirtual => true;
        public override bool CanWrite => true;


        public Virtual(string name, MemoryStream stream)
        {
            Name = name;
            Stream = stream;
        }

        public Virtual(string name, string text, Encoding encoding) : this(name,CreateVirtualStream(encoding.GetBytes(text)))
        {
            
        }

        private static MemoryStream CreateVirtualStream(ReadOnlySpan<byte> buffer)
        {
            var stream = new MemoryStream(buffer.Length);
            stream.Write(buffer);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        
        #region IDisposable Support
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
        #endregion
    }
    #endregion
    
    
    #region IDisposable Support
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
            _disposed = true;
        }
    }

    protected virtual ValueTask DisposeAsync(bool disposing)
    {
        if (_disposed)
        {
            return ValueTask.CompletedTask;
        }
        
        if (disposing)
        {
            _disposed = true;
        }

        return ValueTask.CompletedTask;
    }
    #endregion
    
}