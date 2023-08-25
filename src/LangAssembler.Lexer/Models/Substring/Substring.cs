namespace LangAssembler.Lexer.Models.Substring;

/// <summary>
/// This struct represents a token substring in the language assembler lexer.
/// </summary>
public struct Substring : ISubstring
{
    /// <summary>
    /// Gets the matched token text.
    /// </summary>
    /// <value>A reference to the token text.</value>
    public string TokenText { get; set; }

    /// <summary>
    /// Gets the index at which the matched token starts in the text being tokenized.
    /// </summary>
    /// <value>A reference to the token start index.</value>
    public int TokenStart { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the Substring struct.
    /// </summary>
    /// <param name="tokenText">The text of the token.</param>
    /// <param name="tokenStart">The start index of the token.</param>
    public Substring(string tokenText, int tokenStart)
    {
        TokenText = tokenText;
        TokenStart = tokenStart;
    }

}