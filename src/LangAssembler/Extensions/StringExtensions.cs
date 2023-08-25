namespace LangAssembler.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Retrieves a character at the specified index in the string.
    /// Returns null if the string is null, the index is out of range, or the index is negative.
    /// </summary>
    /// <param name="str">The string to index.</param>
    /// <param name="index">The index of the character to retrieve.</param>
    /// <returns>
    /// The character at the specified index or null if the string is null or the index is out of range.
    /// </returns>
    public static char? GetOrNull(this string? str, int index)
    {
        if (str is not null && index >= 0 && index < str.Length)
        {
            return str[index];
        }

        return null;
    }
}