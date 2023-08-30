namespace LangAssembler.Generator.Lexer.Model.Base;

public interface IALexFile : IALexAnnotatedElement
{
    IALexFile IALexElement.LexAsmFile => this;
}