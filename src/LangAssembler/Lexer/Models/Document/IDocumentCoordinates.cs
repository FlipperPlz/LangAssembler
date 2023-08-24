namespace LangAssembler.Lexer.Models.Document;

public interface IDocumentCoordinates
{
    public int LineNumber { get; }
    public int ColumnNumber { get; }
    public int LineStart { get; }
}