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
    public readonly BinaryReader DocumentReader;

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

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentProcessor"/> class using a <see cref="Stream"/> and a logger.
    /// </summary>
    /// <param name="stream">The stream containing the document.</param>
    /// <param name="encoding">The character encoding to use.</param>
    /// <param name="logger">The logger instance.</param>
    public DocumentProcessor(Stream stream, Encoding encoding, ILogger<IStringProcessor>? logger) : base(stream, encoding)
    {
        Logger = logger;
        DocumentReader = new BinaryReader(DocumentStream, DocumentEncoding, true);
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentProcessor"/> class using a <see cref="BinaryReader"/> and a logger.
    /// </summary>
    /// <param name="reader">The reader for the document.</param>
    /// <param name="encoding">The character encoding to use.</param>
    /// <param name="logger">The logger instance.</param>
    public DocumentProcessor(BinaryReader reader, Encoding encoding, ILogger<IStringProcessor>? logger) : base(reader.BaseStream, encoding)
    {
        Logger = logger;
        DocumentReader = reader;
        ResetStepper();
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="DocumentProcessor"/> class.
    /// </summary>
    ~DocumentProcessor()
    {
        Dispose(true);
    }

    /// <summary>Formats the value of the current instance using the specified format.</summary>
    /// <param name="format">The format to use.
    /// -or-
    /// A null reference (<see langword="Nothing" /> in Visual Basic) to use the default format defined for the type of the <see cref="T:System.IFormattable" /> implementation.</param>
    /// <param name="formatProvider">The provider to use to format the value.
    /// -or-
    /// A null reference (<see langword="Nothing" /> in Visual Basic) to obtain the numeric format information from the current locale setting of the operating system.</param>
    /// <returns>The value of the current instance in the specified format.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        //fixme make lazy virtual; EditableDocumentProcessor will let us know when edits were made
        var position = DocumentPosition;
        DocumentStream.Seek(0, SeekOrigin.Begin);
        var contents = DocumentEncoding.GetString(DocumentReader.ReadBytes((int)DocumentStream.Length));
        DocumentPosition = position;
        return contents;
    }

    /// <summary>
    /// Resets the contents of the stepper and resets the window.
    /// </summary>
    /// <param name="content">The new content to write to buffer.</param>
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

    public int? PeekAt(long position)
    {
        var startPosition = DocumentPosition;
        DocumentPosition = position - 1;
        var result = DocumentReader.Read();
        DocumentPosition = startPosition;
        return result;
    }

    /// <summary>
    /// Gets a string that represents a specified substring in this document.
    /// </summary>
    /// <value>The string at the specified range in this document.</value>
    public string this[Range range]
    {
        get
        {
            int end = range.End.Value, start = range.Start.Value;
            var length = end - start;
            var position = DocumentPosition;
            DocumentPosition = start;
            var contents = DocumentReader.ReadBytes(length);
            DocumentPosition = position;
            return DocumentEncoding.GetString(contents);
        }
    }

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

    /// <summary>
    /// Jumps to a certain position within the document and correctly sets the state of the processor,
    /// including factors such as the current line and column numbers.
    /// </summary>
    /// <param name="position">The position to jump to within the document.</param>
    /// <param name="assumeSameLine">
    /// If set to true, the method will assume that the new position is on the same line as the current position.
    /// </param>
    /// <returns>The new position within the document, or null if the jump was not successful.</returns>
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