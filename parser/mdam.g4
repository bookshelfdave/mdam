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
    (mdam_term)+;

mdam_term: mdam_structure   // f(t1..tn) 
         | mdam_variable    // capital
         ;
         
mdam_structure: struct_name=ID LPAREN (subterms+=mdam_term (COMMA subterms+=mdam_term)*)? RPAREN;
mdam_variable: VARID;


COMMA:         ',';
LSQUARE:       '[';
RSQUARE:       ']';

LPAREN:        '(';
RPAREN:        ')';
LCURLY:        '{';
RCURLY:        '}';
DOT:           '.';

ID          :       [a-z][A-Za-z0-9]*;
VARID       :       [A-Z][A-Za-z0-9]*;


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
