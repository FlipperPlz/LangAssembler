using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Element.Base;

namespace LangAssembler.Generator.Lexer.Model.Element;

public struct ALexConditionalElement  : IALexConditionalElement
{
    public IALexFile LexAsmFile { get; }
    public IALexValue LeftElement { get; }
    public IALexValue RightElement { get; }
    
    public ALexConditionalElement(IALexValue leftElement, IALexValue rightElement, IALexFile lexAsmFile)
    {
        LexAsmFile = lexAsmFile;
        LeftElement = leftElement;
        RightElement = rightElement;
    }
    public override string ToString() => ((IALexValue)this).Value;

}