using LangAssembler.Lexer.Base;

namespace LangAssembler.Lexer.Extensions;

public static class LexerExtensions
{
    /// <summary>
    /// Gets the farthest index that a previous match reached in the lexer.
    /// If there are no previous matches, it returns the current lexer position.
    /// </summary>
    /// <param name="lexer">The instance of ILexer to retrieve the farthest index from.</param>
    /// <returns>Returns the farthest index a previous match reached, or the current lexer position.</returns>
    public static long GetFarthestIndexed(this ILexer lexer) => 
        lexer.PreviousMatches.LastOrDefault() is {} last 
            ? last.TokenStart + last.TokenLength() 
            : lexer.Position;

}