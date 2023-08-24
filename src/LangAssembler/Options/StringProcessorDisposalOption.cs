namespace LangAssembler.Core.Options;

public enum StringProcessorDisposalOption : byte
{
    JumpBackToStart,
    JumpToStringStart,
    JumpToStringEnd,
    Dispose
}