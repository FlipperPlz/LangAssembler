using System.Collections;
using LangAssembler.Generator.Lexer.Model.Base;

namespace LangAssembler.Generator.Lexer.Model.Element.Base;

public interface IALexGroupedElement : IALexElement, IEnumerable<IALexValue>
{
    IEnumerable<IALexValue> Elements { get; }
    
    
    ALexValueType IALexValue.ValueType => ALexValueType.Grouped;
    string IALexValue.Value => $"({string.Join(" ", Elements.Select(e => e.Value))})";

    IEnumerator<IALexValue> IEnumerable<IALexValue>.GetEnumerator() =>
        Elements.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}