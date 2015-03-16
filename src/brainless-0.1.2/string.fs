\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ String input/output buffer routines

4 CONSTANT max-strings
0 VALUE curr-string#

: string-variable  ( "name" -- )
	s" string-variable" print-def
   \ declare a variable that's specific to the current string
   CREATE max-strings CELLS ALLOT
   DOES>  ( a-addr1 -- a-addr2 )  curr-string# CELLS + ;

: new-string  ( -- )
	s" new-string" print-def
   curr-string# 1+ DUP max-strings < 0= ABORT" Too many active strings!"
   TO curr-string# ;
: previous-string  ( -- )
	s" previous-string" print-def
   curr-string# 1- TO curr-string# ;

string-variable curr-string
string-variable c/curr-string
string-variable >curr-char

: is-string  ( c-addr u -- )
	s" is-string" print-def
   new-string  c/curr-string !  curr-string !  0 >curr-char ! ;
: string  ( -- c-addr u ) 	s" string" print-def curr-string @  >curr-char @ ;
: #characters ( -- u ) 	s" #characters" print-def >curr-char @ ;

: next-char  ( char -- ) 	s" next-char" print-def 1 >curr-char +! ;
: previous-char  ( -- ) 	s" previous-char" print-def -1 >curr-char +! ;
: in-string?  ( -- flag )
	s" in-string?" print-def
   >curr-char @ c/curr-string @ <   >curr-char @ 0< 0= AND ;
: ?in-string  ( i*x --  | i*x ) 	s" ?in-string" print-def in-string? 0= ABORT" Out of string!" ;
: curr-char  ( -- a-addr ) 	s" curr-char" print-def ?in-string curr-string @ >curr-char @ + ;
: write-char  ( char -- ) 	s" write-char" print-def curr-char C!  next-char ;
: read-char  ( -- char|0 )
	s" read-char" print-def
   in-string? IF  curr-char C@  next-char  ELSE 0 THEN ;
: write-string  ( c-addr u -- ) 	s" write-string" print-def OVER + SWAP ?DO I C@ write-char LOOP ;

: write-square-file  ( square -- ) 	s" write-square-file" print-def >xy DROP [CHAR] a + write-char ;
: write-square-rank  ( square -- ) 	s" write-square-rank" print-def >xy NIP [CHAR] 1 + write-char ;
: write-square  ( square -- ) 	s" write-square" print-def DUP write-square-file write-square-rank ;

: read-square  ( -- square )
	s" read-square" print-def
   read-char DUP [CHAR] a [CHAR] i WITHIN 0= ABORT" Invalid square-file!"
   read-char DUP [CHAR] 1 [CHAR] 9 WITHIN 0= ABORT" Invalid square-rank!"
   [CHAR] 1 -  SWAP [CHAR] a -  SWAP >square ;
   



