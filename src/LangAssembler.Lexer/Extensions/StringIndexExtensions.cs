using LangAssembler.Lexer.Models.Substring;

namespace LangAssembler.Lexer.Extensions;

public static class StringIndexExtensions
{
    /// <summary>
    /// Gets the length of the token text contained in an ITokenMatch instance. 
    /// </summary>
    /// <param name="match">The instance of ITokenMatch to get the token text length from.</param>
    /// <returns>Returns the length of the token text.</returns>
    public static int TokenLength(this ISubstring match) => match.TokenText.Length;

    /// <summary>
    /// Gets the range that the current token spans within the text being tokenized.
    /// </summary>
    /// <param name="match">The instance of ITokenMatch to get the token span from.</param>
    /// <returns>Returns a range that represents the start and end index of a token in the text.</returns>
    public static Range CurrentIndex(this ISubstring match) => match.TokenStart..TokenEnd(match);

    /// <summary>
    /// Calculates the end index of a token in a text based on its start index and length.
    /// </summary>
    /// <param name="match">The instance of ITokenMatch to calculate the token end from.</param>
    /// <returns>Returns the end index of a token in the text sequence.</returns>
    public static int TokenEnd(this ISubstring match) => match.TokenStart + TokenLength(match);

}