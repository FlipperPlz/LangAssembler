namespace LangAssembler.Lexer.Models.Substring;

public readonly struct Substring : ISubstring
{
    
    public string TokenText { get; }
    public int TokenStart { get; }
    
    public Substring(string tokenText, int tokenStart)
    {
        TokenText = tokenText;
        TokenStart = tokenStart;
    }

}