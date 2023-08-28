using System.Text;

namespace LangAssembler.Document;

public class Document : IDocument
{
    public Document(Stream stream)
    {
        DocumentStream = stream;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    
    public int LineNumber { get; protected set; } = 1;
    public int ColumnNumber { get; protected set; } = 1;
    public int LineStart { get; protected set; } = -1;
    public int DocumentLength => (int)DocumentStream.Length;
    protected readonly List<DocumentLineInfo> EditableLineInfo = new List<DocumentLineInfo>();
    public IEnumerable<DocumentLineInfo> LineInfos => EditableLineInfo; 
    public Stream DocumentStream { get; }
    public Encoding DocumentEncoding { get; set; }

    public int DocumentPosition
    {
        get => (int)DocumentStream.Position;
        set => DocumentStream.Seek(value, SeekOrigin.Begin);
    }
    /// <summary>
    /// Advances to the next line in the document by storing the end position of the current line, incrementing the line number,
    /// resetting the column count, and setting the start position for the next line.
    /// </summary>
    protected virtual void IncrementLine()
    {
        EditableLineInfo.Insert(LineNumber, new DocumentLineInfo(LineStart..DocumentPosition));
        LineNumber++;
        ColumnNumber = 0;
        LineStart = DocumentPosition + 1;
    }

}