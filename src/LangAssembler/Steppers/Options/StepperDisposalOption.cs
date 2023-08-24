namespace LangAssembler.Steppers.Options;

public enum StepperDisposalOption : byte
{
    JumpBackToStart,
    JumpToStringStart,
    JumpToStringEnd,
    Dispose
}