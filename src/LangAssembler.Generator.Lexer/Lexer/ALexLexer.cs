using System.Text;
using LangAssembler.Extensions;
using LangAssembler.Lexer;
using LangAssembler.Lexer.Models.Type;
using LangAssembler.Lexer.Models.Type.Types;
using LangAssembler.Lexer.Models.TypeSet;
using LangAssembler.Options;
using LangAssembler.Processors.Base;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Generator.Lexer.Lexer;

public sealed class ALexLexer : TokenSetLexer<ALexTokenSet>
{
    public ALexLexer(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    public ALexLexer(
        BinaryReader reader,
        Encoding encoding,
        StringProcessorDisposalOption option,
        ILogger<IStringProcessor>? logger = default,
        int? length = null,
        long? stringStart = null
    ) : base(reader, encoding, option, logger, length, stringStart)
    {
    }

    protected override ITokenType GetNextMatch(ITokenTypeSet set, int tokenStart, int? currentChar)
    {
        var token = currentChar switch
        {
            '+' => ALexTokenSet.PlusToken,
            '*' => ALexTokenSet.AsteriskToken,
            '|' => MatchBar(set, tokenStart, currentChar),
            '[' => MatchLeftSquare(set, tokenStart, currentChar),
            ']' => ALexTokenSet.RightSquareToken,
            '(' => ALexTokenSet.LeftParenthesisToken,
            ')' => ALexTokenSet.RightParenthesisToken,
            '{' => ALexTokenSet.LeftCurlyToken,
            '}' => ALexTokenSet.RightCurlyToken,
            ';' => ALexTokenSet.SemicolonToken,
            ':' => MatchAssignment(set, tokenStart, currentChar),
            _ => InvalidToken
        };

        if (token is IInvalidTokenType)
        {
            
        }

        return token;
    }

    private ITokenType MatchAssignment(ITokenTypeSet set, int tokenStart, int? currentChar)
    {
        if (this.PeekForward() != ':')
        {
            return InvalidToken;
        }

        this.MoveForward();
        return this.PeekForward() != '=' ? InvalidToken : ALexTokenSet.AssignmentToken;
    }

    private ITokenType MatchLeftSquare(ITokenTypeSet set, int tokenStart, int? currentChar)
    {
        if (this.PeekForward() != '|')
        {
            return ALexTokenSet.LeftSquareToken;
        }

        this.MoveForward();
        return ALexTokenSet.LeftMetaToken;
    }

    private ITokenType MatchBar(ITokenTypeSet set, int tokenStart, int? currentChar)
    {
        if (this.PeekForward() != ']')
        {
            return ALexTokenSet.BarToken;
        }

        this.MoveForward();
        return ALexTokenSet.RightMetaToken;
    }
}