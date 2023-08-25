namespace LangAssembler.Lexer.Models.Type.Types;

/// <summary>
/// Singleton class for representing an invalid token type. This class implements the IInvalidTokenType
/// interface and has a static singleton instance for universal usage.
/// </summary>
public class InvalidTokenType : IInvalidTokenType
{
    private const string InvalidTokenName = "__INVALID__";

    /// <summary>
    /// Represents the Singleton instance of InvalidTokenType.
    /// </summary>
    public static readonly InvalidTokenType Instance = new();

    /// <summary>
    /// Gets the debug name of the invalid token type, which is a constant "__INVALID__".
    /// </summary>
    public string DebugName => InvalidTokenName;

    private InvalidTokenType()
    {
        
    }

}