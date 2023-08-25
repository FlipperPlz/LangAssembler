namespace LangAssembler.Options;

/// <summary>
/// Represents the options for handling a string processor after performing an operation.
/// </summary>
public enum StringProcessorDisposalOption : byte
{
    /// <summary>
    /// The string processor will return to the initial start position after the operation.
    /// </summary>
    JumpBackToStart,
    
    /// <summary>
    /// The string processor will align to the start of the string after the operation.
    /// </summary>
    JumpToStringStart,
    
    /// <summary>
    /// The string processor will align to the end of the string after the operation.
    /// </summary>
    JumpToStringEnd,
    
    /// <summary>
    /// The string processor will be disposed after the operation.
    /// </summary>
    Dispose
}