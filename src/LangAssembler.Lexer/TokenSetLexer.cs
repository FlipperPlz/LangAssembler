using LangAssembler.Lexer.Base;
using LangAssembler.Lexer.Models.Type;
using LangAssembler.Lexer.Models.TypeSet;
using LangAssembler.Lexer.Providers;
using LangAssembler.Models.Doc;

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
        foreach (var t in TokenInheritanceOrder.SelectMany(e => e).Where(t => ShouldMatchType(t) && TryMatchType(tokenStart, currentChar, t)))
        {
            return t;
        }

        return InvalidToken;
    }

    protected bool TryMatchType(long tokenStart, int? currentChar, ITokenType type) =>
        type.Matches(this, tokenStart, currentChar);

    protected virtual bool ShouldMatchType(ITokenType tokenType) => true;

    protected TokenSetLexer(Document document, bool leaveOpen = false) : base(document, leaveOpen)
    {
    }
}