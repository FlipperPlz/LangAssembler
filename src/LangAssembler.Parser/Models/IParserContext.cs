namespace LangAssembler.Parser.Models;

/// <summary>
/// Defines a contract for a parsing context which provides the necessary properties and 
/// methods needed throughout a parsing process.
/// </summary>
public interface IParserContext
{
    /// <summary>
    /// Gets or sets a value indicating whether the parsing process should end.
    /// </summary>
    /// <value>true if the parsing should end; otherwise, false.</value>
    public bool ShouldEnd { get; set; }
}