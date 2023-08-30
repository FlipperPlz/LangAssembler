using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Literal.Base;
using LangAssembler.Generator.Lexer.Model.Metadata.Base;

namespace LangAssembler.Generator.Lexer.Model.Metadata;

public class ALexMetadata : IALexMetadata
{
    public readonly Dictionary<string, IALexLiteral> Properties = new ();
    public IALexFile LexAsmFile { get; }
    
    public ALexMetadata(IALexFile root)
    {
        LexAsmFile = root;
    }
    
    public ALexMetadata(Dictionary<string, IALexLiteral> properties, IALexFile root) : this(root)
    {
        Properties = properties;
    }

    public IALexLiteral? GetValue(string name) => Properties.GetValueOrDefault(name);
}