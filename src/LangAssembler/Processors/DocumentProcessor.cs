using System.Text;
using LangAssembler.Doc;
using LangAssembler.Processors.Base;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Processors;

public class DocumentProcessor : Document, IDocumentProcessor
{
    /// <summary>
    /// Gets the logger that is used by this object for writing log messages.
    /// </summary>
    public ILogger<IStringProcessor>? Logger { get; }
    
    /// <summary>
    /// Gets the reader for the current document.
    /// </summary>
    public readonly StreamReader DocumentReader;

    /// <summary>
    /// Gets the current character in the content at the Position. If this is null there are one of three reasons:
    /// 1. Our position is before the buffer. (BOF)
    /// 2. Our position is after the buffer. (EOF)
    /// 3. Our buffer is empty, what are you doing! D:
    /// </summary>
    public int? CurrentChar { get; protected set; }

    /// <summary>
    /// Gets the character in the content just before the Position. Nullability is used the same way as its used in
    /// <see cref="IStringProcessor.CurrentChar"/>.
    /// </summary>
    public int? PreviousChar { get; protected set; }

    /// <inheritdoc />
    public DocumentProcessor(Stream stream, Encoding encoding, ILogger<IStringProcessor>? logger) : base(stream, encoding)
    {
        DocumentReader = new StreamReader(DocumentStream, DocumentEncoding, true);
        Logger = logger;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        //TODO make lazy virtual; EditableDocumentProcessor will let us know when edits were made
        var position = DocumentPosition;
        DocumentStream.Seek(0, SeekOrigin.Begin);
        var contents = DocumentReader.ReadToEnd();
        DocumentPosition = position;
        return contents;
    }

    public void ResetStepper(string? content = null)
    {
        if (!DocumentStream.CanWrite && content is not null)
        {
            throw new InvalidOperationException("DocumentStream is not writable!!!");
        }

        if (content is not null)
        {
            var contents = DocumentEncoding.GetBytes(content);
            var length = contents.Length;
            DocumentStream.SetLength(length);
            DocumentStream.Write(contents, 0, length);
        }
        
        ResetDocument();
        CurrentChar = DocumentReader.Read();
        PreviousChar = null;
        ColumnNumber = 1;
    }

    public string this[Range range] => throw new NotImplementedException();


    /// <summary>
    /// Releases the unmanaged resources used by the document and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposedManaged">
    /// <c>true</c> to release both managed and unmanaged resources;
    /// <c>false</c> to release only unmanaged resources.
    /// </param>
    protected override void Dispose(bool disposedManaged)
    {
        if (disposedManaged)
        {
            DocumentReader.Close();
        }
    }

    public int? JumpTo(long position, bool assumeSameLine)
    {
        if (position < 0 || position >= DocumentStream.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }

        
        DocumentStream.Seek(position -= 2, SeekOrigin.Begin);
        PreviousChar = DocumentReader.Read();
        CurrentChar = DocumentReader.Read();
        

        if(assumeSameLine)
        {
            // If we assume position stays on the same line, just increase column number by diff
            var columnDiff = position - (DocumentPosition - 1);
            ColumnNumber += (int)columnDiff;
        }
        else
        {
            throw new NotImplementedException();
        }

        return CurrentChar;
    }
}