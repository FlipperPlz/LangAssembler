namespace LangAssembler.Generator.Lexer.Model.Rule.Base;

[Flags]
public enum ALexRuleModifier : byte
{
    None = 0,
    Private = 1,
    External = 2
}