using LangAssembler.Generator.Lexer.Model.Base;

namespace LangAssembler.Generator.Lexer.Model.Element.Base;

public interface IALexOptionalElement : IALexElement
{
    IALexValue Element { get; }
    
    ALexValueType IALexValue.ValueType => ALexValueType.Optional;
    string IALexValue.Value => $"[${Element.Value}]";
}