namespace LangAssembler.Doc.Enumerations;

/// <summary>
/// Enum representing line feed types in a document.
/// </summary>
public enum DocumentLineFeed
{
    /// <summary>
    /// Enum representing line feed types in a document.
    /// </summary>
    CR,
    
    /// <summary>
    /// Represents a Line Feed (LF) line feed.
    /// </summary>
    LF,
    
    /// <summary>
    /// Represents a Carriage Return Line Feed (CRLF) line feed.
    /// </summary>
    CRLF,
    
    /// <summary>
    /// Represents any line feed type, i.e., not picky about the type of line feed.
    /// \r\n - one line
    /// \n - one line
    /// \n\r - two lines
    /// </summary>
    NotPicky
}