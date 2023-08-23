using Microsoft.Extensions.Logging;

namespace LangAssembler.Internal;

public interface ILaLoggable<out T>
{
    public ILogger<T> Logger { get; }
}