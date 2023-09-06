using LangAssembler.DocumentBase.Models;
using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Models.Type;
using LangAssembler.Lexer.Models.Type.Types;
using LangAssembler.Lexer.Models.TypeSet;
using LangAssembler.Lexer.Providers;

namespace LangAssembler.Lexer;

/// <summary>
/// Represents a Lexer that can handle a set of Tokens.
/// </summary>
/// <typeparam name="TTokenSet">The type of the tokens that this Lexer can process.</typeparam>
public abstract class TokenSetLexer<TTokenSet> : Lexer, IBoundLexer<TTokenSet>
    where TTokenSet : class, ITokenTypeSet
{
    /// <summary>
    /// Gets the order of token inheritance.
    /// </summary>
    protected virtual IEnumerable<ITokenTypeSet> TokenInheritanceOrder => 
        new List<ITokenTypeSet> { DefaultTokenTypeSet };

    /// <summary>
    /// Gets the default token type set (singleton) associated with the lexer.
    /// </summary>
    public static TTokenSet DefaultTokenTypeSet => TokenSetProvider.LocateSet<TTokenSet>();

    /// <summary>
    /// This method is designed to identify the next token type within the specified input buffer
    /// </summary>
    /// <remarks>
    /// Looks for a valid token down the chain of inheritance.
    /// </remarks>
    /// <inheritdoc cref="Lexer.GetNextMatch"/>
    protected sealed override ITokenType LocateNextMatch(long tokenStart, int? currentChar)
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
    protected abstract ITokenType GetNextMatch(ITokenTypeSet set, long tokenStart, int? currentChar);

    protected TokenSetLexer(Document document, bool leaveOpen = false) : base(document, leaveOpen)
    {
    }
}