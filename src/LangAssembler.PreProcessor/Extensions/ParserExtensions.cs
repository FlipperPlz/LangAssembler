﻿using LangAssembler.Lexer.Base;
using LangAssembler.Parser;
using Microsoft.Extensions.Logging;

namespace LangAssembler.PreProcessor.Extensions;

public static class ParserExtensions
{
    /// <summary>
    /// Applies preprocessor on lexer and then performs parsing using parser.
    /// </summary>
    /// <typeparam name="TAstRoot">
    /// Type of the root node of the Abstract Syntax Tree (AST) that the parser will produce.
    /// </typeparam>
    /// <typeparam name="TLexer">Type of the lexer used for lexical analysis.</typeparam>
    /// <typeparam name="TProcessor">Type of the preprocessor used for preprocessing lexer.</typeparam>
    /// <param name="parser">The parser used for parsing.</param>
    /// <param name="lexer">The lexer used for lexical analysis.</param>
    /// <param name="processor">The preprocessor used for preprocessing lexer.</param>
    /// <param name="logger">An optional logger used for logging process.</param>
    public static void ProcessAndParse<TAstRoot, TLexer, TProcessor>
    (
        this IParser<TAstRoot, TLexer> parser,
        TLexer lexer,
        TProcessor? processor = default,
        ILogger? logger = default
    )
        where TAstRoot : class, new()
        where TLexer : ILexer
        where TProcessor : IPreProcessor<TLexer>
    {
        processor?.ProcessLexer(lexer, logger);
        lexer.ResetBuffer();
        parser.Parse(lexer, logger);
    }
    
    /// <summary>
    /// Executes the process of preprocessing a lexer using a new instance of a preprocessor,
    /// parses the preprocessed tokens, and finally returns the preprocessor.
    /// </summary>
    /// <typeparam name="TAstRoot">
    /// Expected concrete type of the Abstract Syntax Tree root generated by parsing.
    /// </typeparam>
    /// <typeparam name="TLexer">Concrete type of lexer that will tokenize the input text.</typeparam>
    /// <typeparam name="TProcessor">
    /// Concrete type of the preprocessor that is intended to preprocess the lexer.
    /// </typeparam>
    /// <param name="parser">An instance of a parser.</param>
    /// <param name="lexer">An instance of a lexer used to tokenize input.</param>
    /// <param name="logger">
    /// Optional entity for logging to be used in the process for debugging or information tracing purposes.
    /// </param>
    /// <returns>The preprocessor used to preprocess the lexer.</returns>
    public static TProcessor ProcessAndParse<TAstRoot, TLexer, TProcessor>
    (
        this IParser<TAstRoot, TLexer> parser,
        TLexer lexer,
        ILogger? logger = default
    )
        where TAstRoot : class, new()
        where TLexer : ILexer
        where TProcessor : IPreProcessor<TLexer>, new()
    {
        var processor = new TProcessor();
        processor.ProcessLexer(lexer, logger);
        lexer.ResetBuffer();
        parser.Parse(lexer, logger);
        return processor;
    }
}