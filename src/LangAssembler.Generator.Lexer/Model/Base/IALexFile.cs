namespace LangAssembler.Generator.Lexer.Model.Base;

public interface IALexFile : IALexAnnotatedLanguageElement
{
    IALexFile IALexLanguageElement.LexAsmFile => this;
}