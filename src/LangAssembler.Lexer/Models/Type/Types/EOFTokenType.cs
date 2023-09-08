using LangAssembler.Lexer.Base;

namespace LangAssembler.Lexer.Models.Type.Types;

/// <summary>
/// Singleton class for representing an End of File (EOF) token type.
/// This class implements the IEOFTokenType interface and has a static singleton instance for universal usage.
/// </summary>
public class EOFTokenType : IEOFTokenType
{
    /// <summary>
    /// Represents the Singleton instance of EOFTokenType.
    /// This instance is read-only and can be accessed globally.
    /// </summary>
    private const string EOFTokenName = "__EOF__";

    /// <summary>
    /// Represents the Singleton instance of EOFTokenType.
    /// This instance is read-only and can be accessed globally.
    /// </summary>
    public static readonly EOFTokenType Instance = new();

    /// <summary>
    /// Gets the debug name of the EOF token type, which is a constant "__EOF__".
    /// This property is mainly used for debugging and error reporting.
    /// </summary>
    public string DebugName => EOFTokenName;
    

    /// <summary>
    /// Private constructor to enforce the singleton pattern.
    /// This ensures that only one instance of EOFTokenType is created.
    /// </summary>
    private EOFTokenType()
    {
        
    }
}