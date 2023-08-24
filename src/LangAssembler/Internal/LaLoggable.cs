using Microsoft.Extensions.Logging;

namespace LangAssembler.Internal;

public abstract class LaLoggable<T> : ILaLoggable<T>
{
    public ILogger<T>? Logger { get; }

    protected LaLoggable(ILogger<T>? logger)
    {
        Logger = logger;
    }
}