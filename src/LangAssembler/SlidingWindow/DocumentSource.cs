using System.Text;

namespace LangAssembler.SlidingWindow;

public interface IDocumentSource : IDisposable, IAsyncDisposable
{
    public Stream DocumentStream { get; }
    public string DocumentName { get; }
    public bool IsVirtual { get; }

    public sealed class FileSystem : IDocumentSource
    {
        private bool _disposed;
        public readonly FileInfo DocumentFile;
        public readonly FileStream DocumentFileStream;

        public Stream DocumentStream => DocumentFileStream;
        public string DocumentPath => DocumentFile.FullName;
        public string DocumentName => DocumentFile.Name;
        public bool IsVirtual => false;
        
        public FileSystem(FileInfo info, FileMode mode, FileAccess access)
        {
            DocumentFile = info;
            DocumentFileStream = DocumentFile.Open(mode, access);
        }

        ~FileSystem()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            DocumentFileStream.Dispose();
            GC.SuppressFinalize(this);
        }
        

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
        
        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            
            if (disposing)
            {
                DocumentFileStream.Dispose();
                _disposed = true;
            }
        }

        public async Task DisposeAsync(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            
            if (disposing)
            {
                await DocumentFileStream.DisposeAsync();
                _disposed = true;
            }
        }

    }

    public sealed class Virtual : IDocumentSource
    {
        private bool _disposed;
        public Stream DocumentStream => DocumentMemoryStream;
        public readonly MemoryStream DocumentMemoryStream;
        public string DocumentName { get; set; }
        public bool IsVirtual => true;
        
        public Virtual(string name, MemoryStream stream)
        {
            DocumentName = name;
            DocumentMemoryStream = stream;
        }

        public Virtual(string name, string text, Encoding encoding) : this(name,
            new MemoryStream(encoding.GetBytes(text), true))
        {
            
        }
        
        ~Virtual()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            DocumentMemoryStream.Dispose();
            GC.SuppressFinalize(this);
        }
        

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
        
        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            
            if (disposing)
            {
                DocumentMemoryStream.Dispose();
                _disposed = true;
            }
        }

        public async Task DisposeAsync(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            
            if (disposing)
            {
                await DocumentMemoryStream.DisposeAsync();
                _disposed = true;
            }
        }

    }
}