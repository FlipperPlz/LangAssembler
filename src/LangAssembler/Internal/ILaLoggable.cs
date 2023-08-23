using Microsoft.Extensions.Logging;

namespace LangAssembler.Internal;

internal interface ILaLoggable<out T>
{
    public ILogger<T> Logger { get; }
}