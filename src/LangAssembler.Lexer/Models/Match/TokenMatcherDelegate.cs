using LangAssembler.Lexer.Base;

namespace LangAssembler.Lexer.Models.Match;

public delegate bool TokenMatcher(ILexer lexer, long tokenStart, int? currentChar);