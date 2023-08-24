using LangAssembler.Lexer.Events.Delegates;
using LangAssembler.Lexer.Models.Match;
using LangAssembler.Processors;
using LangAssembler.Processors.Tracked.Base;
using TokenEditedHandler = System.EventHandler<LangAssembler.Lexer.Events.Arguments.TokenMatchEditedEventArgs>;
using TokenRemovedHandler = System.EventHandler<LangAssembler.Lexer.Models.Match.ITokenMatch>;

namespace LangAssembler.Lexer;

public interface ILexer : ITrackedStringProcessorBase
{
    public bool EventsMuted { get; }
    public ITokenMatch? LastMatch { get; }
    public IEnumerable<ITokenMatch> PreviousMatches { get; }
    
    public event TokenMatchHandler? TokenMatched;
    public event TokenEditedHandler? TokenEdited;
    public event TokenRemovedHandler? TokenRemoved;


    public ITokenMatch LexToken();
    
    public void RemoveTokenMatch(ITokenMatch tokenMatch);
    public void ReplaceTokenMatchText(ITokenMatch tokenMatch, string text);
}