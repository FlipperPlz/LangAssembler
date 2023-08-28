using System.Text;
using LangAssembler.Document;

namespace LangAssembler.Processors.Tracked;


/// <summary>
/// Defines methods and properties that a string processor must implement to be considered as a tracked string processor with associated document details.
/// It implements members of IStringProcessor and IDocument interfaces.
/// </summary>
/// <seealso cref="IStringProcessor"/>
/// <seealso cref="IDocument"/>
public interface ITrackedStringProcessorBase : IStringProcessor, IDocument
{
    /// <summary>
    /// Gets the current position within the document.
    /// </summary>
    int IDocument.DocumentPosition
    {
        get => Position;
        set => JumpTo(value);
    }

    /// <summary>
    /// Gets the length of the document.
    /// </summary>
    int IDocument.DocumentLength => Length;

    // Stream IDocument.DocumentStream => throw new NotImplementedException();
    //
    // Encoding IDocument.DocumentEncoding
    // {
    //     get => throw new NotImplementedException();
    //     set => throw new NotImplementedException();
    // }
}