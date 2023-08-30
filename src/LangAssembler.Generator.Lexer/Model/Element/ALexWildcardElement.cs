using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Element.Base;

namespace LangAssembler.Generator.Lexer.Model.Element;

public struct ALexWildcardElement : IALexWildcardElement
{
    public IALexValue Element { get; set; }
    public IALexFile LexAsmFile { get; }
    
    public ALexWildcardElement(IALexValue element, IALexFile root)
    {
        LexAsmFile = root;
        Element = element;
    }
    public override string ToString() => ((IALexValue)this).Value;

}