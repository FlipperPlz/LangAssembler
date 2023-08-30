namespace LangAssembler.Generator.Lexer.Model.Base;

[Flags]
public enum ALexValueType
{
    String,
    Hex,
    RuleName,
    Wildcard,
    Repeat,
    Optional,
    Grouped,
    Conditional
}