using System.Text;
using LangAssembler.Doc;
using LangAssembler.Processors.Base;

namespace LangAssembler.Processors.Tracked;


/// <summary>
/// Defines methods and properties that a string processor must implement to be considered as a tracked string processor with associated document details.
/// It implements members of IStringProcessor and IDocument interfaces.
/// </summary>
/// <seealso cref="IStringProcessor"/>
public interface ITrackedStringProcessorBase : IStringProcessor
{
    
}