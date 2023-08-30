using LangAssembler.Generator.Lexer.Model.Base;

namespace LangAssembler.Generator.Lexer.Model.Literal.Base;

public interface IALexLiteral : IALexElement
{
    string Value { get; }
    ALexValueType ValueType { get; }
}