using LangAssembler.Lexer.Models.TypeSet;

namespace LangAssembler.Lexer.Providers;

/// <summary>
/// Provides facilities for managing and retrieving instances of token type sets.
/// </summary>
public static class TokenSetProvider
{
    /// <summary>
    /// A collection of token type set instances, keyed by their type.
    /// </summary>
    private static readonly Dictionary<Type, ITokenTypeSet> Instances = new();

    /// <summary>
    /// Locates an instance of a specific token type set, creating it if it does not exist.
    /// </summary>
    /// <typeparam name="TTokenTypeSet">The type of the token type set.</typeparam>
    /// <returns>An instance of the specified token type set.</returns>
    public static TTokenTypeSet LocateSet<TTokenTypeSet>() where TTokenTypeSet : ITokenTypeSet
    {
        var type = typeof(TTokenTypeSet);
        if (Instances.TryGetValue(type, out var instance))
        {
            return (TTokenTypeSet)instance;
        }

        throw new Exception("Could not locate token set.");
    }

    /// <summary>
    /// Adds an instance of a specific token type set to the collection.
    /// </summary>
    /// <typeparam name="TTokenTypeSet">The type of the token type set.</typeparam>
    /// <param name="set">The instance of the token type set to add.</param>
    internal static void AddSet<TTokenTypeSet>(TTokenTypeSet set) where TTokenTypeSet : ITokenTypeSet
    {
        var type = typeof(TTokenTypeSet);
        Instances.TryAdd(type, set);
    }
}