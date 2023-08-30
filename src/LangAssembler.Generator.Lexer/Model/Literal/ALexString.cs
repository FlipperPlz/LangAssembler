using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Literal.Base;

namespace LangAssembler.Generator.Lexer.Model.Literal;

public struct ALexString : IALexLiteral
{
    public IALexFile LexAsmFile { get; }
    public string Value { get; }
    public ALexValueType ValueType => ALexValueType.String;
    
    public ALexString(string value, IALexFile root)
    {
        LexAsmFile = root;
        Value = value;
    }

    public override string ToString() => Value;
}