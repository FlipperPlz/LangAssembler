using LangAssembler.Lexer.Models.Match;
using LangAssembler.Lexer.Models.Substring;
using LangAssembler.Lexer.Models.Type;

namespace LangAssembler.Lexer.Extensions;

public static class TokenMatchExtensions
{
    
    /// <summary>
    /// Removes a matched token from the associated lexers input text.
    /// This is typically used when a token is no longer needed, or when its presence could interfere with subsequent tokenization steps.
    /// </summary>
    /// <param name="match">The instance of ITokenMatch to be removed.</param>
    public static void EraseToken(this ITokenMatch match) => match.Lexer.RemoveTokenMatch(match);

    /// <summary>
    /// Replaces the text of a matched token.
    /// </summary>
    /// <param name="match">The instance of ITokenMatch which text should be replaced.</param>
    /// <param name="text">The new text to replace the current token text.</param>
    public static void SetTokenText(this ITokenMatch match, Span<byte> text) =>
        match.Lexer.ReplaceTokenMatchText(match, text);

    /// <summary>
    /// Changes the type of a matched token.
    /// This is typically used when a token's type needs to be changed as a result of further analysis.
    /// </summary>
    /// <param name="match">The instance of ITokenMatch which type should be changed.</param>
    /// <param name="type">The new token type.</param>
    public static void RetypeToken(this ITokenMatch match, ITokenType type) =>
        match.TokenType = type;
    
    /// <summary>
    /// Converts a token match to a Substring instance with the text of the token, and position of the token's start in the original text.
    /// </summary>
    /// <param name="match">The token match instance to convert.</param>
    /// <returns>Returns a Substring instance with the token text and position from the provided token match.</returns>
    public static Substring ToSubstring(this ITokenMatch match) => new(match.TokenText, match.TokenStart, match.TokenEnd);
}