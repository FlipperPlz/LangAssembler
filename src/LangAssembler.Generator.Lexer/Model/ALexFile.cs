using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Metadata.Base;
using LangAssembler.Generator.Lexer.Model.Rule;
using LangAssembler.Generator.Lexer.Model.Rule.Base;

namespace LangAssembler.Generator.Lexer.Model;

public class ALexFile : IALexFile
{
    public IALexMetadata Metadata { get; }
    public IEnumerable<IALexRule> Rules { get; }
    
    public ALexFile(IALexMetadata metadata, IEnumerable<IALexRule> rules)
    {
        Metadata = metadata;
        Rules = rules;
    }

    public override string ToString() =>
        $"{Metadata}\n{string.Join('\n', Rules.Select(it => it.ToString()))}";
}