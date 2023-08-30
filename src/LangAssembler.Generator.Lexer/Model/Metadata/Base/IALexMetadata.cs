using LangAssembler.Generator.Lexer.Model.Base;
using LangAssembler.Generator.Lexer.Model.Literal.Base;

namespace LangAssembler.Generator.Lexer.Model.Metadata.Base;

public interface IALexMetadata : IALexElement
{
    public IALexLiteral? GetValue(string name);
}