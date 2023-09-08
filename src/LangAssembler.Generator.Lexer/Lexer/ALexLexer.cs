using LangAssembler.Lexer;
using LangAssembler.Models.Doc;

namespace LangAssembler.Generator.Lexer.Lexer;

public sealed class ALexLexer : TokenSetLexer<ALexTokenSet>
{
    public ALexLexer(Document document, bool leaveOpen = false) : base(document, leaveOpen)
    {
    }
}