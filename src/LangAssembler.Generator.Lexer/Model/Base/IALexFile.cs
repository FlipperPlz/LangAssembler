using System.Collections;
using LangAssembler.Generator.Lexer.Model.Rule;
using LangAssembler.Generator.Lexer.Model.Rule.Base;

namespace LangAssembler.Generator.Lexer.Model.Base;

public interface IALexFile : IALexAnnotatedLanguageElement,  IEnumerable<IALexRule>
{
    IALexFile IALexLanguageElement.LexAsmFile => this;
    IEnumerable<IALexRule> Rules { get; }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    IEnumerator<IALexRule> IEnumerable<IALexRule>.GetEnumerator() =>
        Rules.GetEnumerator();
}