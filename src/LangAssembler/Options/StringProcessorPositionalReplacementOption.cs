namespace LangAssembler.Core.Options;

/// <summary>
/// Enumeration for text replacement position options.
/// </summary>
public enum StringProcessorPositionalReplacementOption : byte
{
    /// <summary>
    /// HugRight indicates the position should be set to the upper bound.
    /// </summary>
    HugRight,

    /// <summary>
    /// HugLeft indicates the position should be set to the lower bound.
    /// </summary>
    HugLeft,

    /// <summary>
    /// KeepRemaining indicates the position should be the total length minus the remaining value.
    /// </summary>
    KeepRemaining,

    /// <summary>
    /// Reset indicates the position should be reset.
    /// </summary>
    Reset,

    /// <summary>
    /// DontTouch indicates the position should be kept as it is.
    /// </summary>
    DontTouch
}