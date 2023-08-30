using LangAssembler.Generator.Lexer.Model.Base;

namespace LangAssembler.Generator.Lexer.Model.Literal.Base;

public interface IALexLiteral : IALexValue
{
    ALexLiteralType LiteralType => (ALexLiteralType)ValueType;
}