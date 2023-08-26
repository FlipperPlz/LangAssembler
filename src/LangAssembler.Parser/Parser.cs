using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Models.Match;
using LangAssembler.Parser.Extensions;
using LangAssembler.Parser.Models;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Parser;

public abstract class Parser<
    TASTRoot,
    TLexer,
    TParserContext
> : IParser<TASTRoot, TLexer> 
    where TLexer : ILexer 
    where TASTRoot : class, new()
    where TParserContext : IParserContext, new()
{
    /// <summary>
    /// Parses the lexers tokens into an AST.
    /// </summary>
    /// <param name="lexer">The lexer, which provides the tokens to parse.</param>
    /// <param name="logger">An optional logger for writing log messages.</param>
    /// <returns>An AST root element derived from the lexer tokens.</returns>
    public virtual TASTRoot Parse(TLexer lexer, ILogger? logger)
    {
        var root = new TASTRoot();
        var info = new TParserContext();
        var mute = false;
        
        lexer.TokenMatched += delegate(ref ITokenMatch match)
        {
            if (mute || ShouldSkipToken(root, lexer, ref match, info, logger))
            {
                return;
            }

            mute = true;
            ParseToken(root, lexer, ref match, info, logger);
            mute = false;
        };
        
        while (info.ShouldContinue())
        {
            lexer.LexToken();
        }

        return root;
    }

    /// <summary>
    /// Parses a single token into the AST.
    /// </summary>
    /// <param name="root">The root node of the AST.</param>
    /// <param name="lexer">The lexer, which provides the tokens to parse.</param>
    /// <param name="match">The current token match from the lexer.</param>
    /// <param name="info">Contextual information about the current parsing state.</param>
    /// <param name="logger">An optional logger for writing log messages.</param>
    protected abstract void ParseToken(TASTRoot root, TLexer lexer, ref ITokenMatch match, TParserContext info,
        ILogger? logger);

    /// <summary>
    /// Determines whether a specific token based on its match should be skipped during parsing.
    /// </summary>
    /// <param name="root">The root node of the AST.</param>
    /// <param name="lexer">The lexer, providing the tokens to parse.</param>
    /// <param name="match">The current token match from the lexer.</param>
    /// <param name="info">Contextual information about the current parsing state.</param>
    /// <param name="logger">An optional logger for writing log messages.</param>
    /// <returns>true if the token should be skipped; otherwise, false.</returns>
    protected virtual bool ShouldSkipToken(TASTRoot root, TLexer lexer, ref ITokenMatch match, TParserContext info, ILogger? logger)
        => false;
}