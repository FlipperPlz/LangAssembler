using LangAssembler.Lexer.Models.TypeSet;

namespace LangAssembler.Lexer.Extensions;

public static class TokenSetExtensions
{
    public static TTokenSet GetTokenSet<TTokenSet>() where TTokenSet : class, ITokenTypeSet, new()
    {
        throw new NotImplementedException();
    }
}