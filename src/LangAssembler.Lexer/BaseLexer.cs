using System.Text;
using LangAssembler.Core.Options;
using LangAssembler.Extensions;
using LangAssembler.Lexer.Events.Arguments;
using LangAssembler.Lexer.Events.Delegates;
using LangAssembler.Lexer.Extensions;
using LangAssembler.Lexer.Models.Match;
using LangAssembler.Lexer.Models.Type;
using LangAssembler.Lexer.Models.Type.Types;
using LangAssembler.Processors;
using LangAssembler.Processors.Tracked;
using Microsoft.Extensions.Logging;

namespace LangAssembler.Lexer;

public abstract class BaseLexer : TrackedEditableStringProcessor, ILexer
{
    protected static readonly ITokenType InvalidToken = null!; //TODO:

    public bool EventsMuted { get; protected set; } = false;
    public event TokenMatchHandler? TokenMatched;
    public event EventHandler<TokenMatchEditedEventArgs>? TokenEdited;
    public event EventHandler<ITokenMatch>? TokenRemoved;
    
    protected readonly List<ITokenMatch> _previousMatches = new();
    public IEnumerable<ITokenMatch> PreviousMatches => _previousMatches;
    public ITokenMatch? LastMatch { get; protected set; }
    
    public BaseLexer(string content, ILogger<IStringProcessor>? logger) : base(content, logger)
    {
    }

    public BaseLexer(BinaryReader reader, Encoding encoding, StringProcessorDisposalOption option, ILogger<IStringProcessor>? logger = default, int? length = null, long? stringStart = null) : base(reader, encoding, option, logger, length, stringStart)
    {
    }
    protected abstract ITokenType LocateNextMatch(int tokenStart, char? currentChar);

    protected void OnTokenMatched(ITokenMatch match)
    {
        if (!EventsMuted)
        {
            TokenMatched?.Invoke(ref match);
        }
        LastMatch = match;
        AddPreviousMatch(match);
    }
    
    protected void AddPreviousMatch(ITokenMatch match)
    {
        if(_previousMatches.Count == 0 || match.TokenStart >= _previousMatches[^1].TokenStart)
        {
            _previousMatches.Add(match);
        }

        var index = _previousMatches.BinarySearch(match, Comparer<ITokenMatch>.Create((x, y) => x.TokenStart.CompareTo(y.TokenStart)));
        _previousMatches.Insert(index < 0 ? ~index : index, match);
    }

    public virtual ITokenMatch LexToken() => RegisterNextMatch(Position);
    protected virtual ITokenType FindNextMatch(int tokenStart, char? currentChar) => LocateNextMatch(tokenStart, currentChar);

    private ITokenMatch RegisterNextMatch(int tokenStart)
    {
        var type = FindNextMatch(tokenStart, this.MoveForward());
        var match = type is IInvalidTokenType or null
            ? CreateInvalidMatch(tokenStart)
            : CreateTokenMatch(type, tokenStart);
        if (!EventsMuted)
        {
            TokenMatched?.Invoke(ref match);
        }

        return match;
    }
    
    protected ITokenMatch CreateInvalidMatch(int tokenStart) =>
        CreateTokenMatch(InvalidToken, tokenStart);
    
    protected ITokenMatch CreateTokenMatch(ITokenType type, int tokenStart)
    {
        var text = this.GetRange(tokenStart..Position);
        return new TokenMatch(text, tokenStart, this, type);
    }

    public void RemoveTokenMatch(ITokenMatch tokenMatch)
    {
        if (!EventsMuted)
        {
            TokenRemoved?.Invoke(this, tokenMatch);
        }

        _previousMatches.Remove(tokenMatch);
        this.RemoveRange(tokenMatch.CurrentIndex(), out _, StringProcessorPositionalReplacementOption.KeepRemaining);
    }

    public void ReplaceTokenMatchText(ITokenMatch tokenMatch, string text)
    {
        var oldContents = tokenMatch.ToSubstring();
        tokenMatch.TokenText = text;
        ReplaceRange(tokenMatch.CurrentIndex(), text, out _, StringProcessorPositionalReplacementOption.KeepRemaining);
        if (!EventsMuted)
        {
            TokenEdited?.Invoke(this, new TokenMatchEditedEventArgs(tokenMatch, oldContents));
        }
    }
}