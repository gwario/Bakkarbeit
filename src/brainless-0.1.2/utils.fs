\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Miscellaneous utility routines

\ TODO:
\   make some words state smart to inline themselves (including words generated
\   by offset?)
\

: secs ( -- u ) 	s" secs" print-def TIME&DATE  2DROP DROP 60 * + 60 * + ;

: create-array  ( "name" -- )
	s" create-array" print-def
   CREATE IMMEDIATE
   DOES>  ( u a-addr1 -- a-addr2 )
      STATE @ IF
	 POSTPONE CELLS  POSTPONE LITERAL  POSTPONE +
      ELSE  SWAP CELLS +  THEN ;
: create-2array  ( "name" -- )
	s" create-2array" print-def
   CREATE   DOES>  ( u a-addr1 -- a-addr2 )  SWAP 2* CELLS + ;
: vector-table:  ( "name" -- )
	s" vector-table:" print-def
   CREATE IMMEDIATE
   DOES>  ( i*x piece a-addr -- j*x )
      STATE @ IF
	 POSTPONE CELLS  POSTPONE LITERAL  POSTPONE +  POSTPONE PERFORM
      ELSE  SWAP CELLS + PERFORM  THEN ;

: record  ( -- 0 ) 	s" record" print-def 0 ;
: offset  ( n1 n2 "name" -- n3 )
	s" offset" print-def
   CREATE IMMEDIATE OVER ,  +
DOES>  ( addr1 -- addr2 )
   @ ?DUP IF	( S: addr1 offset )
      STATE @ IF   POSTPONE LITERAL  POSTPONE +   ELSE + THEN
   THEN ;
: end-record  ( n "name" -- ) 	s" end-record" print-def CONSTANT ;

: file-exists?  ( c-addr u -- flag )
	s" file-exists?" print-def
   FILE-STATUS NIP 0=  ;
\   R/O OPEN-FILE 0= DUP IF
\      SWAP CLOSE-FILE DROP
\   ELSE NIP THEN ;

74755 VALUE random-seed
: random  ( -- n ) 	s" random" print-def random-seed 1309 * 13849 + 65535 and dup TO random-seed ;

\
\ Option value handling
\
: create-option  ( x "name" -- )
	s" create-option" print-def
   CREATE ,  DOES> @ ;
: option-exists?  ( "name" -- "name" flag )
	s" option-exists?" print-def
   SAVE-INPUT  BL WORD FIND NIP
   >R RESTORE-INPUT ABORT" RESTORE-INPUT failed!" R> ;
: option  ( x "name" -- )  \ define an option with default value
	s" option" print-def
   option-exists? 0= IF create-option THEN ;
: set-option  ( x "name" -- ) \ sets the value of an option
	s" set-option" print-def
   option-exists? 0= IF    \ option doesn't exist, create it
      create-option
   ELSE			   \ else set value of option, if possible
      ' >BODY !
   THEN ;

\
\ Blocks of text to output to screen when corresponding word invoked
\
: (text:)  ( "text..." ";text<cr>" -- )
	s" (text:)" print-def
   BEGIN REFILL WHILE
      0 PARSE 2DUP S" ;text" COMPARE 0= IF 2DROP EXIT THEN
      POSTPONE CR  POSTPONE SLITERAL   POSTPONE TYPE  
   REPEAT ;
: text:  ( "name" "text" -- ) 
	s" text:" print-def
   :  (text:) 	s" (text:)" print-def POSTPONE CR  POSTPONE ; ;
