using LangAssembler.Generator.Lexer.Model.Metadata;
using LangAssembler.Generator.Lexer.Model.Metadata.Base;

namespace LangAssembler.Generator.Lexer.Model.Base;

public interface IALexAnnotatedElement : IALexElement
{
    public IALexMetadata Metadata { get; }
}