using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Literal.Base;

namespace LangAssembler.Generator.Lexer.Model.Literal;

public struct ALexRuleReference : IALexLiteral
{
    public IALexFile LexAsmFile { get; }
    public string Value { get; }
    public ALexValueType ValueType => ALexValueType.RuleName;
    
    public ALexRuleReference(string value, IALexFile root)
    {
        LexAsmFile = root;
        Value = value;
    }
    
    //TODO locate rule method and constructor for rule
}