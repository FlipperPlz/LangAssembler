namespace LangAssembler.Lexer.Models.Substring;

/// <summary>
/// A simple class that keeps track of a substring and its position in the larger picture.
/// </summary>
public interface ISubstring
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
}