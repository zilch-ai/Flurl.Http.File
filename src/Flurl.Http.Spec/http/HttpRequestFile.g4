grammar HttpRequestFile;

// Parsers
file: assignment* first? more*;
assignment: AT variable EQ value EOL;
variable: ID;
value: STRING | LIQUID;
first: request;
more: (SEP (requestId)? EOL) request;
requestId: TEXT+;
request: verb url version? EOL (header+ EOL)? body?;
verb: VERB;
url: URL;
version: VER;
header: headerKey COLON headerValue EOL;
headerKey: ID;
headerValue: TEXT+;
body: (TEXT+ EOL)+;

// Tokens
EOL: '\r'? '\n';						            // New line
SEP: '###';                                         // Separator
COMMENT: ('#' | '//') ~[\r\n]+ -> channel(HIDDEN);  // Comment
WS: [ \t]+ -> skip;                                 // White spaces
STRING: '"' ~["\r\n]* '"';                          // String literal enclosed by double quotes
LIQUID: '{{' ~["\r\n]+ '}}';                        // Liquid string template
VERB: 'GET' | 'POST' | 'PUT' | 'DELETE';            // HTTP verb
URL: ('http://' | 'https://') ~[ \t\r\n]+;          // HTTP url
VER: 'HTTP/' ('1.0' | '1.1' | '2' | '3');           // HTTP version
AT: '@';                                            // Variable prefix
EQ: '=';                                            // Assignment operator
COLON: ':';                                         // key-value separator
ID: [a-zA-Z_][a-zA-Z0-9_]*;                         // Identifier for variable name
TEXT: ~[@=: \t\r\n]+;                                  // Any text
UNEXPECTED: .;                                      // Unrecognized character for ANTLR diag
