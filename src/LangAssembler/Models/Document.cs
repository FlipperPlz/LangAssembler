using System.Text;
using LangAssembler.Models.Lang;
using LangAssembler.Models.Source;

namespace LangAssembler.Models;

public class Document : IDisposable, IAsyncDisposable
{
    #region Static Factory Methods
    private static readonly Dictionary<DocumentSource, Document> Documents = new();
    
    public static Document Of<TLanguage>(DocumentSource source, Encoding? encoding = null) where TLanguage : Language, new() =>
        Documents.TryGetValue(source, out var document) ? document : CreateDocument<TLanguage>(source, encoding);

    public static Document CreateDocument<TLanguage>(DocumentSource source, Encoding? encoding = null)
        where TLanguage : Language, new()
    {
        var doc = new Document(source, Language.Of<TLanguage>(), encoding);
        Documents.Add(source, doc);
        return doc;
    }
    public static implicit operator Stream(Document d) => d.Source;
    
    #endregion

    #region Properties
    public DocumentSource Source { get; }
    public Encoding Encoding { get; }
    public Language Language { get; }
    public bool Writable => Source.CanWrite;
    #endregion
    
    #region Constructor and Finalizer

    public Document(DocumentSource source, Language? language = null, Encoding? encoding = null)
    {
        Source = source;
        Language = language ?? Language.PlainTextLanguage; 
        Encoding = encoding ?? Language.Encoding;
    }
    
    ~Document()
    {
        Dispose(false);
    }

    #endregion
    
    #region IDisposable Support 

    private bool _disposed;
    
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
            Documents.Remove(Source);
            Source.Dispose();
            _disposed = true;
        }
    }

    protected virtual async Task DisposeAsync(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        // ReSharper disable once InvertIf
        if (disposing)
        {
            Documents.Remove(Source);
            await Source.DisposeAsync();
            _disposed = true;
        }
    }
    #endregion
}