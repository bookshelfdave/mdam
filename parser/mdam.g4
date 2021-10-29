grammar mdam;

/*
 * -------------------------------------------------------------------
 *
 *   Copyright (c) 2021 Dave Parfitt
 *
 *   This file is provided to you under the Apache License,
 *   Version 2.0 (the "License"); you may not use this file
 *   except in compliance with the License.  You may obtain
 *   a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing,
 *   software distributed under the License is distributed on an
 *   "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 *   KIND, either express or implied.  See the License for the
 *   specific language governing permissions and limitations
 *   under the License.
 * -------------------------------------------------------------------
 */

mdam_terms:
    (mdam_term DOT)+;

mdam_term:
    mdam_atom
    | mdam_int
    | mdam_float
    | mdam_string
    ;

mdam_int:         INT;
mdam_float:       FLOAT;
mdam_string:      STRING;
mdam_atom:        ID | IDSTRING;
mdam_tuple:       LCURLY (tupleitems+=mdam_term (COMMA tupleitems+=mdam_term)*)? RCURLY;

COMMA:         ',';
LSQUARE:       '[';
RSQUARE:       ']';
//MAPOP:         '=>';
LCURLY:        '{';
RCURLY:        '}';
//LESSTHAN:      '<';
//GREATERTHAN:   '>';
//COLON:         ':';
//TRUE:          'true';
//FALSE:         'false';
//AT:            '@';
//HASH:          '#';
DOT:           '.';

ID          :       [a-z][A-Za-z0-9]*;
IDSTRING  :  '\'' (IDESC|.)*? '\'';

fragment IDESC : '\\\'' | '\\\\' ;

INT         :   ('-')? DIGIT+;
FLOAT       :   ('-')? DIGIT+ DOT DIGIT*
               | ('-')?DOT DIGIT+
            ;
fragment DIGIT  : '0' .. '9';



STRING  :  '"' (ESC|.)*? '"';
fragment ESC : '\\"' | '\\\\' ;

LINE_COMMENT  : '%' .*? '\r'? '\n' -> skip ;

WS      :       [ \t\r\n]+ -> skip;
