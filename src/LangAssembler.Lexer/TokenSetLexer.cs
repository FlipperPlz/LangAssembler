using System.Text;
using LangAssembler.Lexer.Models.TypeSet;
using LangAssembler.Options;
using LangAssembler.Processors;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Lexer;

/// <summary>
/// Provides a base implementation of a lexer bound to a specific token type set.
/// </summary>
/// <typeparam name="TTokenTypes">The type of the token type set.</typeparam>
public abstract class TokenSetLexer<TTokenTypes> : BaseLexer, IBoundLexer<TTokenTypes>
    where TTokenTypes : class, ITokenTypeSet, new()
{
    /// <summary>
    /// Gets the default token type set (singleton) associated with the lexer.
    /// </summary>
    public TTokenTypes DefaultTokenTypeSet { get; } = null!;
    
    protected TokenSetLexer(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    protected TokenSetLexer(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }

}