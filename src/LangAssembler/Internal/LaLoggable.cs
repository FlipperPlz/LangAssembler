using Microsoft.Extensions.Logging;

namespace LangAssembler.Internal;

/// <summary>
/// Provides a base implementation of an object that can be used for logging.
/// </summary>
/// <typeparam name="T">The type of the class to which the logger is scoped.</typeparam>
public abstract class LaLoggable<T> : ILaLoggable<T>
{
    /// <summary>
    /// Gets the logger that is used by the object for writing log messages.
    /// </summary>
    public ILogger<T>? Logger { get; }

    /// <summary>
    /// Initializes a new instance of the LaLoggable class with the specified logger.
    /// </summary>
    /// <param name="logger">The logger that is used by the object for writing log messages.</param>
    protected LaLoggable(ILogger<T>? logger)
    {
        Logger = logger;
    }
}