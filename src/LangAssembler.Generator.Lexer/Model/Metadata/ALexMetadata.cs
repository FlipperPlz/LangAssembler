using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Literal.Base;
using LangAssembler.Generator.Lexer.Model.Metadata.Base;

namespace LangAssembler.Generator.Lexer.Model.Metadata;

public class ALexMetadata : IALexMetadata
{
    public readonly Dictionary<string, IALexValue> Properties = new ();
    public IALexFile LexAsmFile { get; }
    
    public ALexMetadata(IALexFile root)
    {
        LexAsmFile = root;
    }
    
    public ALexMetadata(Dictionary<string, IALexValue> properties, IALexFile root) : this(root)
    {
        Properties = properties;
    }

    public override string ToString() => $"[|\n{
        string.Join("\n",
            Properties.Select(
                it => $"{it.Key} ::= {it.Value};"
            )
        )
    }\n|]";

    public IALexValue? GetValue(string name) => Properties.GetValueOrDefault(name);
}