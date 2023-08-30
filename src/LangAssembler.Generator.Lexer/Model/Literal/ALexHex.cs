using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Literal.Base;

namespace LangAssembler.Generator.Lexer.Model.Literal;

public struct ALexHex : IALexLiteral
{
    public IALexFile LexAsmFile { get; }
    public string Value { get; }
    public ALexValueType ValueType => ALexValueType.Hex;
    
    public ALexHex(string value, IALexFile root)
    {
        LexAsmFile = root;
        Value = value;
    }
}