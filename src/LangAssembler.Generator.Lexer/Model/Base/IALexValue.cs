using LangAssembler.Generator.Lexer.Model.Literal.Base;

namespace LangAssembler.Generator.Lexer.Model.Base;

public interface IALexValue : IALexLanguageElement
{
    string Value { get; }
    ALexValueType ValueType { get; }
}