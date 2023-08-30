grammar LexAsm;

root:
    metadata?
    typeDefinition*
    EOF;

metaProperty: ID ASSIGN literal;
typeDefinition: ID metadata? ASSIGN typeAssignment SCOLON;

metadata: LMETA (
    metaProperty*
) RMETA;

typeAssignment: element;
element: elementAtom (BAR elementAtom)*;
elementAtom: 
     elementAtom ASTERISK        #zeroOrMore      |
     elementAtom PLUS            #oneOrMore       |
     elementAtom QUESTION        #oneOrNone       |
     LSQUARE elementAtom RSQUARE #optionalElement |
     LPAREN  elementAtom RPAREN  #groupElement    |
     literal                     #rawElement      ;

literal: 
    STRING      |
    HEX_NUMBER  |
    ID          ;

HEX_NUMBER : '0' [xX] [0-9a-fA-F]+ ;
STRING: '"' (ESCAPED_CHAR | ~["\\])* '"';
ESCAPED_CHAR: '\\' [btnfr"'\\];
SQUOTE:   '\'';
ASTERISK: '*';
PLUS:     '+';
BAR:      '|';
LPAREN:   '(';
RPAREN:   ')';
LSQUARE:  '[';
RSQUARE:  ']';
LCURLY:   '{';
RCURLY:   '}';
SCOLON:   ';';
QUESTION: '?';
LMETA:    '[|';
RMETA:    '|]';
ASSIGN:   '::=';
ID:       [a-zA-Z_] [a-zA-Z0-9_]*;
WS:       [ \r\n\t] -> skip;