\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Main load file to use on standard ANS Forth systems.

s" trace.txt" r/w create-file throw constant tracefile-id

: print-word ( c-addr c -- )
	cr type
;

: print-def ( c-addr n -- ) \ usage: s" <wordname>" print-def
	['] print-word tracefile-id outfile-execute
;

: bye ( -- )
	cr
	tracefile-id close-file
	bye
;

.( Loading Brainless...) CR
.( License: GPLv3. NO WARRANTY!) CR

: file-prefix  ( -- c-addr u ) 	s" file-prefix" print-def S" ./" ;
: file-suffix  ( -- c-addr u ) 	s" file-suffix" print-def S" .fs" ;

: append  ( c-addr1 u1 c-addr2 u2 -- c-addr3 u3 ) \ append str2 to str1 -> str3
	s" append" print-def
   2>R 2DUP +  2R@ ROT SWAP MOVE 2R> NIP + ;

: load-part  ( "filename" -- )
	s" load-part" print-def
   PAD 0
   file-prefix append
   BL WORD COUNT append
   file-suffix append
   2DUP ." [" TYPE SPACE
   INCLUDED
   ." ]" ;

0 VALUE compilation-finished?

load-part environ
load-part options
load-part utils
load-part board
load-part hash
load-part drawing
load-part string
load-part epd
load-part threats
load-part searchdefs
load-part repeat
load-part moves
load-part ttable
load-part eval
load-part flyeval
load-part movegen
\ load-part sglmove
load-part tmovegen
load-part quiescence
load-part null
load-part moveconv
load-part killer
load-part sorting
load-part search
load-part history
load-part tui

TRUE TO compilation-finished?

TRUE option run-tui-startup

run-tui-startup [IF] tui-startup [THEN]
   


