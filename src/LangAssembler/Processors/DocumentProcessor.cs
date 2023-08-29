using System.Text;
using LangAssembler.Doc;
using LangAssembler.Processors.Base;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Processors;

public class DocumentProcessor : Document, IDocumentProcessor
{
    
    public ILogger<IStringProcessor>? Logger { get; }
    public readonly BinaryReader DocumentReader;
    public char? CurrentChar { get; protected set; }
    public char? PreviousChar { get; protected set; }
    
    public DocumentProcessor(Stream stream, Encoding encoding, ILogger<IStringProcessor>? logger) : base(stream, encoding)
    {
        DocumentReader = new BinaryReader(DocumentStream, DocumentEncoding, true);
        Logger = logger;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var reader = new StreamReader(DocumentStream, DocumentEncoding);
        return reader.ReadToEnd();
    }

    public void ResetStepper(string? content = null)
    {
        if (!DocumentStream.CanWrite && content is not null)
        {
            throw new InvalidOperationException();
        }

        if (content is not null)
        {
            var contents = DocumentEncoding.GetBytes(content);
            var length = contents.Length;
            DocumentStream.SetLength(length);
            DocumentStream.Write(contents, 0, length);
        }
        
        DocumentPosition = 0;
        CurrentChar = DocumentReader.ReadChar();
        PreviousChar = null;
        EditableLineInfo.Clear();
        ColumnNumber = 1;
    }

    public string this[Range range] => throw new NotImplementedException();
    

    protected override void Dispose(bool disposedManaged)
    {
        if (disposedManaged)
        {
            DocumentReader.Close();
        }
    }

    public char? JumpTo(long position, bool assumeSameLine)
    {
        throw new NotImplementedException();
    }
}