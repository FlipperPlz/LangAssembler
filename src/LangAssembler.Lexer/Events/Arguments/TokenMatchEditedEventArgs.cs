using LangAssembler.Lexer.Models.Match;
using LangAssembler.Lexer.Models.Substring;

namespace LangAssembler.Lexer.Events.Arguments;

/// <summary>
/// Provides data for the TokenMatchEdited event.
/// </summary>
public class TokenMatchEditedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the token match that was edited.
    /// </summary>
    public ITokenMatch Match { get; }
    
    /// <summary>
    /// Gets the old content of the token match before it was edited.
    /// </summary>
    public Substring OldContent { get; }
    
    /// <summary>
    /// Initializes a new instance of the TokenMatchEditedEventArgs class with the specified ITokenMatch instance and its old content.
    /// </summary>
    /// <param name="match">The ITokenMatch instance associated with the event.</param>
    /// <param name="oldContent">The old content of the token match before it was edited.</param>
    public TokenMatchEditedEventArgs(ITokenMatch match, Substring oldContent)
    {
        Match = match;
        OldContent = oldContent;
    }

}