using System.Text;
using LangAssembler.Document.Models.Lang;
using LangAssembler.Document.Models.Source;
using LangAssembler.SlidingWindow;

namespace LangAssembler.Document.Models;

public class Document : IDisposable, IAsyncDisposable
{
    #region static
    private static readonly Dictionary<IDocumentSource, Document> Documents = new();
    
    public static Document Of<TLanguage>(IDocumentSource source, Encoding? encoding = null) where TLanguage : Language, new() =>
        Documents.TryGetValue(source, out var document) ? document : CreateDocument<TLanguage>(source, encoding);

    public static Document CreateDocument<TLanguage>(IDocumentSource source, Encoding? encoding = null)
        where TLanguage : Language, new()
    {
        var doc = new Document(source, Language.Of<TLanguage>(), encoding);
        Documents.Add(source, doc);
        return doc;
    }
        
    #endregion
    public IDocumentSource DocumentSource { get; }
    public Encoding DocumentEncoding { get; }
    public Language DocumentLanguage { get; }
    
    private bool _disposed;

    protected Document(IDocumentSource source, Language? language = null, Encoding? encoding = null)
    {
        DocumentSource = source;
        DocumentLanguage = language ?? Language.PlainTextLanguage; 
        DocumentEncoding = encoding ?? DocumentLanguage.LanguageEncoding;
    }


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
            Documents.Remove(DocumentSource);
            DocumentSource.Dispose();
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
            Documents.Remove(DocumentSource);
            await DocumentSource.DisposeAsync();
            _disposed = true;
        }
    }
}