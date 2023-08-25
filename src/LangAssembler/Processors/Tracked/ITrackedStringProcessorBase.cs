using LangAssembler.Lexer.Models.Document;

namespace LangAssembler.Processors.Tracked;

public interface ITrackedStringProcessorBase : IStringProcessor, IDocumentCoordinates
{
    public IEnumerable<DocumentLineInfo> LineInfo { get; }

    public string? GetLineText(int lineNumber);
    
    
}