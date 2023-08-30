using LangAssembler.Generator.Lexer.Model.Base;

namespace LangAssembler.Generator.Lexer.Model.Element.Base;

public interface IALexRepeatElement : IALexElement
{
    IALexValue Element { get; }
    
    ALexValueType IALexValue.ValueType => ALexValueType.Repeat;
    string IALexValue.Value => $"${Element.Value}+";
}