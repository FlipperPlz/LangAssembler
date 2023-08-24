using LangAssembler.Lexer.Models.Match;

namespace LangAssembler.Lexer.Events.Delegates;

/// <summary>
/// Delegate for handling token match events.
/// </summary>
/// <param name="match">A reference to the matched token. This delegate may update the details of the match.</param>
public delegate void TokenMatchHandler(ref ITokenMatch match);