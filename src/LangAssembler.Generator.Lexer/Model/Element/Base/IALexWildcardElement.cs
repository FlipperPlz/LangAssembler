using LangAssembler.Generator.Lexer.Model.Base;

namespace LangAssembler.Generator.Lexer.Model.Element.Base;

public interface IALexWildcardElement : IALexElement
{
    IALexValue Element { get; }
    
    ALexValueType IALexValue.ValueType => ALexValueType.Wildcard;
    string IALexValue.Value => $"${Element.Value}*";
}