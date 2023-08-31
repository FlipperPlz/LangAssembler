using LangAssembler.Generator.Lexer.Model.Base;

namespace LangAssembler.Generator.Lexer.Model.Element.Base;

public interface IALexConditionalElement : IALexElement
{
    IALexValue LeftElement { get; }
    IALexValue RightElement { get; }
    
    ALexValueType IALexValue.ValueType => ALexValueType.Conditional;

    string IALexValue.Value => $"{LeftElement} | {RightElement}";
}