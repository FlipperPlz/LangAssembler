using LangAssembler.Lexer.Base;
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
        new TokenType("meta.left", MatchLeftMeta);
    public static readonly ITokenType RightMetaToken = 
        new TokenType("meta.right", MatchRightMeta);
    public static readonly ITokenType LeftParenthesisToken = 
        new TokenType("parenthesis.left", MatchLeftParenthesis);
    public static readonly ITokenType RightParenthesisToken = 
        new TokenType("parenthesis.right", MatchRightParenthesis);
    public static readonly ITokenType LeftCurlyToken = 
        new TokenType("curly.left", MatchLeftCurly);
    public static readonly ITokenType RightCurlyToken = 
        new TokenType("curly.right", MatchRightCurly);
    public static readonly ITokenType LeftSquareToken = 
        new TokenType("square.left", MatchLeftSquare);
    public static readonly ITokenType RightSquareToken = 
        new TokenType("square.right", MatchRightSquare);
    public static readonly ITokenType AsteriskToken = 
        new TokenType("asterisk", MatchAsterisk);
    public static readonly ITokenType PlusToken = 
        new TokenType("plus", MatchPlus);
    public static readonly ITokenType BarToken = 
        new TokenType("bar", MatchBar);
    public static readonly ITokenType SemicolonToken = 
        new TokenType("semicolon", MatchSemicolon);
    public static readonly ITokenType AssignmentToken = 
        new TokenType("assignment", MatchAssignOperator);
    public static readonly ITokenType PrivateModifierToken = 
        new TokenType("modifier.private", MatchPrivateModifier);
    public static readonly ITokenType ExternalModifierToken = 
        new TokenType("modifier.external", MatchExternalModifier);
    public static readonly ITokenType StringToken = 
        new TokenType("string", MatchStringModifier);
    public static readonly ITokenType HexToken = 
        new TokenType("hex", MatchHexLiteral);
    
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
    
    private static bool MatchLeftMeta(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchRightMeta(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchRightParenthesis(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchLeftParenthesis(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchRightCurly(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchLeftCurly(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchRightSquare(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchLeftSquare(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchAsterisk(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchPlus(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchBar(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchSemicolon(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchAssignOperator(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchExternalModifier(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchPrivateModifier(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchStringModifier(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
    
    private static bool MatchHexLiteral(ILexer lexer, long tokenStart, int? currentChar)
    {
        throw new NotImplementedException();
    }
}