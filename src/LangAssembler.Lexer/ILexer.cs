using LangAssembler.Core;
using LangAssembler.Lexer.Events.Delegates;
using LangAssembler.Lexer.Models.Match;
using LangAssembler.Processors;
using TokenEditedHandler = System.EventHandler<LangAssembler.Lexer.Events.Arguments.TokenMatchEditedEventArgs>;
using TokenRemovedHandler = System.EventHandler<LangAssembler.Lexer.Models.Match.ITokenMatch>;

namespace LangAssembler.Lexer;

public interface ILexer : IEditableStringProcessor
{
    public event TokenMatchHandler? TokenMatched;
    public event TokenEditedHandler? TokenEdited;
    public event TokenRemovedHandler? TokenRemoved; 
    
    public bool EventsMuted { get; }
    
    public TokenMatch? LastMatch { get; }
    public IEnumerable<TokenMatch> PreviousMatches { get; }
    
    public void RemoveTokenMatch(ITokenMatch tokenMatch);
    public void ReplaceTokenMatchText(ITokenMatch tokenMatch, string text);
}