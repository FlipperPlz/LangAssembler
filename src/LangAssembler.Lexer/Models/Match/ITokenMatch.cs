using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Models.Substring;
using LangAssembler.Lexer.Models.Type;

namespace LangAssembler.Lexer.Models.Match;

/// <summary>
/// Represents a match from a tokenization operation.
/// Please avoid using refs, instead use extension methods and helpers.
/// </summary>
public interface ITokenMatch : ISubstring
{
    /// <summary>
    /// Gets the lexer that produced the token match.
    /// This can be used to access properties of the lexer such as the original text that was tokenized.
    /// This can also be used to SAFELY alter the text of the token
    /// </summary>
    /// <value>The lexer that produced the token match.</value>
    public ILexer Lexer { get; }
    
    /// <summary>
    /// Gets the original range of where matched token within the lexed buffer.
    /// </summary>
    /// <value>The index range of the token.</value>
    public Range OriginalIndex { get; }
    
    /// <summary>
    /// Gets the type of the matched token.
    /// </summary>
    /// <value>The type of the token.</value>
    public ITokenType TokenType { get; set; }
}