using LangAssembler.Parser.Models;

namespace LangAssembler.Parser.Extensions;

public static class ParserContextExtensions
{
    /// <summary>
    /// Determines whether the parsing process should continue based on the current state of the IParserContext.
    /// </summary>
    /// <param name="context">The IParserContext to check.</param>
    /// <returns>False if the parsing process should end, otherwise true.</returns>
    public static bool ShouldContinue(this IParserContext context) => !context.ShouldEnd;

    /// <summary>
    /// Marks the parsing context indicating that the parsing process should end.
    /// </summary>
    public static void MarkEnd(this IParserContext context) =>
        context.ShouldEnd = true;

}