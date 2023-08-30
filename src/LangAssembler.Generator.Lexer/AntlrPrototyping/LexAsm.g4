grammar LexAsm;

root:
    metadata?
    rule*
    EOF;

metadata: LMETA metaProperty* RMETA;
metaProperty: ID ASSIGN literal SCOLON;
rule: ruleModifier? ID metadata? ASSIGN elementAtom SCOLON;

elementAtom: 
     elementAtom ASTERISK        #wildcardElement  |
     elementAtom PLUS            #repeatElement    |
     LSQUARE elementAtom RSQUARE #optionalElement  |
     LPAREN  elementAtom RPAREN  #groupElement     |
     literal                     #rawElement       |
     literal (BAR elementAtom)*  #elementOrElement ;     
literal: 
     STRING                      #string           |
     HEX_NUMBER                  #numeric          |
     ID                          #reference        ;
     
ruleModifier:
     PRIVATE                     #privateMod       |
     EXTERNAL                    #externalMod      ;

PRIVATE: 'private';
EXTERNAL: 'external';
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
LMETA:    '[|';
RMETA:    '|]';
ASSIGN:   '::=';
ID:       [a-zA-Z_] [a-zA-Z0-9_]*;
WS:       [ \r\n\t] -> skip;