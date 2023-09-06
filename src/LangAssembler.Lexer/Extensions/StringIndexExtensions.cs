using LangAssembler.Lexer.Models.Substring;

namespace LangAssembler.Lexer.Extensions;

public static class StringIndexExtensions
{
    /// <summary>
    /// Gets the length of the token text contained in an ITokenMatch instance. 
    /// </summary>
    /// <param name="match">The instance of ITokenMatch to get the token text length from.</param>
    /// <returns>Returns the length of the token text.</returns>
    public static long TokenLength(this ISubstring match) => match.TokenEnd - match.TokenStart;
}