using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Element.Base;

namespace LangAssembler.Generator.Lexer.Model.Element;

public struct ALexRepeatElement : IALexRepeatElement
{
    public IALexValue Element { get; set; }
    public IALexFile LexAsmFile { get; }
    
    public ALexRepeatElement(IALexValue element, IALexFile root)
    {
        LexAsmFile = root;
        Element = element;
    }

}