using LangAssembler.Lexer.Models.Match;
using LangAssembler.Lexer.Models.Type;

namespace LangAssembler.Lexer.Extensions;

public static class TokenMatchExtensions
{
    
    /// <summary>
    /// Gets a range representing the position of a token within the original text.
    /// </summary>
    /// <param name="match">The instance of ITokenMatch to get the token position from.</param>
    /// <returns>Returns a range representing the start and end index of a token in the original text.</returns>
    public static Range GetTokenLocation(this ITokenMatch match) =>
        match.TokenStart..match.TokenEnd();
    
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
    public static void SetTokenText(this ITokenMatch match, string text) =>
        match.Lexer.ReplaceTokenMatchText(match, text);

    /// <summary>
    /// Changes the type of a matched token.
    /// This is typically used when a token's type needs to be changed as a result of further analysis.
    /// </summary>
    /// <param name="match">The instance of ITokenMatch which type should be changed.</param>
    /// <param name="type">The new token type.</param>
    public static void RetypeToken(this ITokenMatch match, ITokenType type) =>
        match.TokenType = type;
}