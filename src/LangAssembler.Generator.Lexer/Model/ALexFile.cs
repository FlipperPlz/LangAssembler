using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Metadata.Base;

namespace LangAssembler.Generator.Lexer.Model;

public class ALexFile : IALexFile
{
    public IALexMetadata Metadata { get; }
    
    public ALexFile(IALexMetadata metadata)
    {
        Metadata = metadata;
    }

}