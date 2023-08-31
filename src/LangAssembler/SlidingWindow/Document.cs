using System.Text;

namespace LangAssembler.SlidingWindow;

public abstract class Document : IDisposable, IAsyncDisposable
{
    public IDocumentSource DocumentSource { get; }
    public Encoding DocumentEncoding { get; }
    private bool _disposed;



    ~Document()
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
        if (_disposed)
        {
            return;
        }

        // ReSharper disable once InvertIf
        if (disposing)
        {
            DocumentSource.Dispose();
            _disposed = true;
        }
    }

    protected async Task DisposeAsync(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        // ReSharper disable once InvertIf
        if (disposing)
        {
            await DocumentSource.DisposeAsync();
            _disposed = true;
        }
    }
}