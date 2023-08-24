namespace LangAssembler.Lexer.Models.Type;

/// <summary>
/// Represents a type of token that can be identified during the tokenization process.
/// </summary>
public readonly struct TokenType : ITokenType
{
    /// <summary>
    /// Gets the name of the token type, that is used in debugging and logging.
    /// </summary>
    public string DebugName { get; }
    
    /// <summary>
    /// Initializes a new instance of the TokenType with a given debug name.
    /// </summary>
    /// <param name="debugName">The debug name for this token type.</param>
    public TokenType(string debugName)
    {
        DebugName = debugName;
    }

}