namespace LangAssembler.Generator.Lexer.Model.Rule.Base;

public static class ALexRuleModifierExtensions
{

    public static string TokenText(this ALexRuleModifier modifier)
    {
        var external = (modifier & ALexRuleModifier.External) == ALexRuleModifier.External ? "external" : string.Empty;
        var @private = (modifier & ALexRuleModifier.Private) == ALexRuleModifier.Private ? "private" : string.Empty;
        return $"{@private} {external}";
    }
}