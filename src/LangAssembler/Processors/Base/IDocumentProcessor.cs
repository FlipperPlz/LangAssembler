using LangAssembler.Doc;
using LangAssembler.Extensions;

namespace LangAssembler.Processors.Base;

public interface IDocumentProcessor : IDocument, IStringProcessor
{
    /// <summary>
    /// This is the length of the content loaded.
    /// </summary>
    /// <remarks>
    /// In document streams you should always opt for the document length
    /// </remarks>
    int IStringProcessor.Length => (int) this.GetDocumentLength();

    /// <summary>
    /// This is the current position of the sliding window within the content buffer.
    /// </summary>
    /// /// <remarks>
    /// In document streams you should always opt for the document position
    /// </remarks>
    int IStringProcessor.Position => (int) this.GetDocumentPosition();

    int? JumpTo(long position, bool assumeSameLine);

    /// <summary>
    /// Jumps to a certain position and correctly sets <see cref="IStringProcessor.CurrentChar"/> and <see cref="IStringProcessor.PreviousChar"/>.
    /// </summary>
    /// <param name="position">The position to jump to.</param>
    int? IStringProcessor.JumpTo(int position) => JumpTo(position);
}