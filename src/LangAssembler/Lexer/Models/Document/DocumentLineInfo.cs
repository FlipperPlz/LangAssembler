using LangAssembler.Processors.Tracked;

namespace LangAssembler.Lexer.Models.Document;

public class DocumentLineInfo
{
    private Range _lineIndex;
    public Range LineIndex
    {
        get => _lineIndex;
        set
        {
            _lineIndex = value;
            LineLength = _lineIndex.End.Value - _lineIndex.Start.Value;
        }
    }
    
    public int LineLength { get; private set; }

    
    public DocumentLineInfo(Range lineIndex) => LineIndex = lineIndex;
    
    
    public bool IsInLine(int position) =>
        position >= LineIndex.Start.Value && position <= LineIndex.End.Value;

    public string GetLineText(ITrackedStringProcessorBase processor) =>
        processor[_lineIndex];
}