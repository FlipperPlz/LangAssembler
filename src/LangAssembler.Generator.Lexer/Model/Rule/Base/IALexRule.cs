using LangAssembler.Generator.Lexer.Model.Base;

namespace LangAssembler.Generator.Lexer.Model.Rule.Base;

public interface IALexRule : IALexAnnotatedLanguageElement
{
    ALexRuleModifier RuleModifiers { get; }
    string RuleName { get; }
    IALexValue RuleValue { get; }
}