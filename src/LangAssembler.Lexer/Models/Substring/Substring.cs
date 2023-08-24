namespace LangAssembler.Lexer.Models.Substring;

public struct Substring : ISubstring
{
    
    public string TokenText { get; set; }
    public int TokenStart { get; set; }
    
    public Substring(string tokenText, int tokenStart)
    {
        TokenText = tokenText;
        TokenStart = tokenStart;
    }

}