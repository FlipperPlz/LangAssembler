using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Literal.Base;
using LangAssembler.Generator.Lexer.Model.Rule.Base;

namespace LangAssembler.Generator.Lexer.Model.Rule;

public interface IALexRule : IALexAnnotatedLanguageElement
{
    string RuleName { get; }
    ALexRuleModifier RuleModifiers { get; }
    IALexValue RuleValue { get; }
}