using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Element.Base;

namespace LangAssembler.Generator.Lexer.Model.Element;

public struct ALexOptionalElement : IALexOptionalElement
{
    public IALexValue Element { get; set; }
    public IALexFile LexAsmFile { get; }
    
    public ALexOptionalElement(IALexValue element, IALexFile root)
    {
        LexAsmFile = root;
        Element = element;
    }

}