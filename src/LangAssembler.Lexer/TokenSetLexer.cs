using System.Text;
using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Extensions;
using LangAssembler.Lexer.Models.Type;
using LangAssembler.Lexer.Models.Type.Types;
using LangAssembler.Lexer.Models.TypeSet;
using LangAssembler.Options;
using LangAssembler.Processors;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Lexer;

/// <summary>
/// Represents a Lexer that can handle a set of Tokens.
/// </summary>
/// <typeparam name="TTokenSet">The type of the tokens that this Lexer can process.</typeparam>
public abstract class TokenSetLexer<TTokenSet> : Lexer, IBoundLexer<TTokenSet>
    where TTokenSet : class, ITokenTypeSet, new()
{
    /// <summary>
    /// Gets the order of token inheritance.
    /// </summary>
    protected virtual IEnumerable<ITokenTypeSet> TokenInheritanceOrder => 
        new List<ITokenTypeSet> { DefaultTokenTypeSet };

    /// <summary>
    /// Gets the default token type set (singleton) associated with the lexer.
    /// </summary>
    public static TTokenSet DefaultTokenTypeSet => TokenSetExtensions.GetTokenSet<TTokenSet>();

    protected TokenSetLexer(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    protected TokenSetLexer(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }

    /// <summary>
    /// This method is designed to identify the next token type within the specified input buffer
    /// </summary>
    /// <remarks>
    /// Looks for a valid token down the chain of inheritance.
    /// </remarks>
    /// <inheritdoc cref="Lexer.GetNextMatch"/>
    protected sealed override ITokenType GetNextMatch(int tokenStart, char? currentChar)
    {
        foreach (var set in TokenInheritanceOrder)
        {
            var match = GetNextMatch(set, tokenStart, currentChar);
            if (match is not IInvalidTokenType)
            {
                return match;
            }
        }

        return InvalidToken;
    }
    
    /// <summary>
    /// template method that will be implemented by derived classes to provide custom token matching logic.
    /// </summary>
    /// <param name="set">The token set to match.</param>
    /// <param name="tokenStart">The start position of the token.</param>
    /// <param name="currentChar">The current character being processed.</param>
    /// <returns>Returns the matched token type.</returns>
    protected abstract ITokenType GetNextMatch(ITokenTypeSet set, int tokenStart, char? currentChar);
}