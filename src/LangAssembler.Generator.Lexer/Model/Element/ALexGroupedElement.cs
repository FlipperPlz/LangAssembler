using System.Collections;
using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Element.Base;

namespace LangAssembler.Generator.Lexer.Model.Element;

public struct ALexGroupedElement : IALexGroupedElement
{
    public IALexFile LexAsmFile { get; }
    public IEnumerable<IALexValue> Elements { get; set; }
    
    public ALexGroupedElement(IEnumerable<IALexValue> elements, IALexFile lexAsmFile)
    {
        LexAsmFile = lexAsmFile;
        Elements = elements;
    }

    
}