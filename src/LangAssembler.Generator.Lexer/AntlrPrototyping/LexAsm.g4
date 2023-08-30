grammar LexAsm;

root: 
    typeDefinition*
    EOF;

typeDefinition: typeIdentifier ASSIGN typeAssignment;

typeAssignment: element;
element: elementAtom (BAR elementAtom)*;
elementAtom: 
     elementAtom ASTERISK        #zeroOrMore      |
     elementAtom PLUS            #oneOrMore       |
     LSQUARE elementAtom RSQUARE #optionalElement |
     LPAREN  elementAtom RPAREN  #groupElement    |
     literal                     #rawElement;

literal: 
    STRING      |
    HEX_NUMBER  |
    ID          ;

typeIdentifier: ID (
    LCURLY 
    

    RCURLY
)?;
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

ASSIGN:   '::=';
ID:       [a-zA-Z_] [a-zA-Z0-9_]*;
WS:       [ \r\n\t] -> skip;