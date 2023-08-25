using Microsoft.Extensions.Logging;

namespace LangAssembler.Internal;

/// <summary>
/// Defines a component that supports logging capabilities.
/// </summary>
/// <typeparam name="T">The type of the class to which the logger is scoped.</typeparam>
public interface ILaLoggable<out T>
{
    /// <summary>
    /// Gets the logger that is used by the object for writing log messages.
    /// </summary>
    public ILogger<T>? Logger { get; }
}