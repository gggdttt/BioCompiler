grammar Syntax;


program
    : stat* EOF
    ;

stat
    : moving
    | spliting
    | droplet
    ;

droplet
    : ID '=' 'droplet(' INT ',' INT ',' INT',' INT ')' ';'
    ;


moving
    :'Move' '(' ID',' INT ',' INT ')' ';'
    ;

spliting
    : ID ',' ID '=' 'Split' '(' ID ','  INT ',' INT ','INT ','INT')'';'
    ;


ID
 : [a-zA-Z_] [a-zA-Z_0-9]*
 ;

INT
 : [0-9]+
 ;

COMMENT
 : '#' ~[\r\n]* -> skip
 ;

SPACE
 : [ \t\r\n] -> skip
 ;


