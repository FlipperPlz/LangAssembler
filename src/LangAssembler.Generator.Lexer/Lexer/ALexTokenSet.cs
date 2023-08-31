using LangAssembler.Lexer.Models.Type;
using LangAssembler.Lexer.Models.TypeSet;

namespace LangAssembler.Generator.Lexer.Lexer;

public sealed class ALexTokenSet : TokenTypeSet
{
    public static readonly ALexTokenSet Instance = new();

    private ALexTokenSet()
    {
        
    }
    
    public static readonly ITokenType LeftMetaToken = 
        new TokenType("meta.left");
    public static readonly ITokenType RightMetaToken = 
        new TokenType("meta.right");
    public static readonly ITokenType LeftParenthesisToken = 
        new TokenType("parenthesis.left");
    public static readonly ITokenType RightParenthesisToken = 
        new TokenType("parenthesis.right");
    public static readonly ITokenType LeftCurlyToken = 
        new TokenType("curly.left");
    public static readonly ITokenType RightCurlyToken = 
        new TokenType("curly.right");
    public static readonly ITokenType LeftSquareToken = 
        new TokenType("square.left");
    public static readonly ITokenType RightSquareToken = 
        new TokenType("square.right");
    public static readonly ITokenType AsteriskToken = 
        new TokenType("asterisk");
    public static readonly ITokenType PlusToken = 
        new TokenType("plus");
    public static readonly ITokenType BarToken = 
        new TokenType("bar");
    public static readonly ITokenType SemicolonToken = 
        new TokenType("semicolon");
    public static readonly ITokenType AssignmentToken = 
        new TokenType("assignment");
    public static readonly ITokenType StringToken = 
        new TokenType("string");
    public static readonly ITokenType HexToken = 
        new TokenType("hex");
    public static readonly ITokenType PrivateModifierToken = 
        new TokenType("modifier.private");
    public static readonly ITokenType ExternalModifierToken = 
        new TokenType("modifier.external");
    
    protected override void InitializeTypes()
    {
        InitializeType(LeftMetaToken);
        InitializeType(RightMetaToken);
        InitializeType(LeftParenthesisToken);
        InitializeType(RightParenthesisToken);
        InitializeType(LeftCurlyToken);
        InitializeType(RightCurlyToken);
        InitializeType(LeftSquareToken);
        InitializeType(RightSquareToken);
        InitializeType(PrivateModifierToken);
        InitializeType(ExternalModifierToken);
        InitializeType(AsteriskToken);
        InitializeType(PlusToken);
        InitializeType(BarToken);
        InitializeType(SemicolonToken);
        InitializeType(AssignmentToken);
        InitializeType(StringToken);
        InitializeType(HexToken);
    }
}