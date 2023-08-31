using LangAssembler.Doc;
using LangAssembler.Extensions;

namespace LangAssembler.Processors.Base;

/// <summary>
/// Defines methods and properties for a document processor that includes functionality
/// from both an IDocument and IStringProcessor.
/// This interface provides methods for manipulating and moving through a document during processing.
/// </summary>
public interface IDocumentOldProcessor : IDocumentOld, IStringProcessor
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

    /// <summary>
    /// Jumps to a certain position within the document and correctly sets the state of the processor,
    /// including factors such as the current line and column numbers.
    /// </summary>
    /// <param name="position">The position to jump to within the document.</param>
    /// <param name="assumeSameLine">
    /// If set to true, the method will assume that the new position is on the same line as the current position.
    /// </param>
    /// <returns>The new position within the document, or null if the jump was not successful.</returns>
    int? JumpTo(long position, bool assumeSameLine);

    /// <summary>
    /// Jumps to a certain position and correctly sets <see cref="IStringProcessor.CurrentChar"/>
    /// and <see cref="IStringProcessor.PreviousChar"/>.
    /// </summary>
    /// <param name="position">The position to jump to.</param>
    int? IStringProcessor.JumpTo(int position) => JumpTo(position);
}