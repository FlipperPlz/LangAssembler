using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Metadata.Base;
using LangAssembler.Generator.Lexer.Model.Rule.Base;

namespace LangAssembler.Generator.Lexer.Model.Rule;


public class ALexRule : IALexRule
{
    
    public IALexFile LexAsmFile { get; }
    public IALexMetadata Metadata { get; }
    public ALexRuleModifier RuleModifiers { get; set; }
    public string RuleName { get; set; }
    public IALexValue RuleValue { get; set; }
    
    public ALexRule(ALexRuleModifier modifiers, string name, IALexMetadata metadata, IALexValue value, IALexFile root)
    {
        LexAsmFile = root;
        Metadata = metadata;
        RuleModifiers = modifiers;
        RuleName = name;
        RuleValue = value;
    }

    public override string ToString() => $"{RuleModifiers.TokenText()} {RuleName} {Metadata} ::= {RuleValue}";


}