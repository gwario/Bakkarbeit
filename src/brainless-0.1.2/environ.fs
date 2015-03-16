\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Handle environmental dependencies

\
\ Count cell sizes
\
: count-bits  ( u -- )
   1 SWAP
   BEGIN 1 RSHIFT ?DUP WHILE  SWAP 1+ SWAP   REPEAT ;
: d-count-bits  ( ud -- )
   count-bits SWAP count-bits + ;

S" MAX-U"  ENVIRONMENT? 0= THROW count-bits    CONSTANT bits/u
S" MAX-UD" ENVIRONMENT? 0= THROW d-count-bits  CONSTANT bits/ud      

\
\ Set values that indicate some known Forth systems
\
0 VALUE bigforth?
0 VALUE gforth?
0 VALUE iforth?

S" GForth" ENVIRONMENT? [IF]
   .( GForth ) TYPE .(  detected  )   TRUE TO gforth?
[THEN]
S" bigFORTH" ENVIRONMENT? [IF]
   .( bigFORTH ) . . .( detected  )   TRUE TO bigforth?
[THEN]
S" IFORTH" ENVIRONMENT? [IF]
   DROP .( iForth detected  )   TRUE TO iforth?
[THEN]

\
\ GForth color support
\
gforth? [IF]
   INCLUDE ansi.fs
[THEN]

\
\ Optionally define words that are common in many Forth systems
\

\ Important -- I require a static and large PAD
256 CONSTANT #PAD
CREATE PAD #PAD CHARS ALLOT

: [DEF?]  ( "name" -- flag )  BL WORD FIND NIP ; IMMEDIATE
: [-DEF?]  ( "name" -- flag )  POSTPONE [DEF?] 0= ; IMMEDIATE

[-DEF?] NOOP [IF] : NOOP  ( -- ) ; [THEN]
[-DEF?] 3DUP [IF]
   : 3DUP  ( x1 x2 x3 -- x1 x2 x3 x1 x2 x3 )
      STATE @ IF
	 POSTPONE DUP  POSTPONE 2OVER  POSTPONE ROT
      ELSE  DUP 2OVER ROT  THEN ; IMMEDIATE
[THEN]
[-DEF?] 3DROP [IF]
   : 3DROP  ( x1 x2 x3 -- )
      STATE @ IF
	 POSTPONE 2DROP  POSTPONE DROP
      ELSE  2DROP DROP  THEN ; IMMEDIATE
[THEN]
[-DEF?] DEFER [-DEF?] IS OR [IF]
   : DEFER  ( "name" -- )  CREATE ['] NOOP ,  DOES> @ EXECUTE ;
   : IS  ( xt "name" -- )
      ' >BODY
      STATE @ IF  POSTPONE LITERAL  POSTPONE !  ELSE ! THEN ; IMMEDIATE
[THEN]

[-DEF?] PERFORM [IF]
   : PERFORM  ( j*x a-addr -- i*x )
      STATE @ IF  POSTPONE @  POSTPONE EXECUTE  ELSE @ EXECUTE THEN ; IMMEDIATE
[THEN]

[-DEF?] -ROT [IF]
   : -ROT  ( x1 x2 x3 -- x3 x1 x2 )
      STATE @ IF  POSTPONE ROT  POSTPONE ROT  ELSE ROT ROT THEN ; IMMEDIATE
[THEN]

[-DEF?] TIME&DATE [IF]
   .( !TIME&DATE not defined!  )
   : TIME&DATE  ( -- 0 0 0 0 0 0 )  0 0 0 0 0 0 ;
[THEN]

[-DEF?] ARRAY bigforth? AND [IF]
   \ thanks to Bernd Paysan for the following FAST code
   .( using machine-CODE ARRAYs  )
   CODE array-access ( i -- addr )
      101010 ax *4 i#) ax lea
      Next
   END-CODE MACRO
   ALSO dos
   : ARRAY  ( size -- )  ALIGN HERE >R CELLS ALLOT
   : POSTPONE array-access  R> HERE 6 - ! POSTPONE ; MACRO ;
[THEN]
[-DEF?] ARRAY iforth? AND [IF] \ thanx to Marcel Hendrix...
   : ARRAY  ( u "name" -- )
      CREATE IMMEDIATE  CELLS ALLOT   DOES> POSTPONE LITERAL EVAL" []CELL " ;
[THEN]
[-DEF?] ARRAY [IF]
   \ : ARRAY  ( u "name" -- )  CREATE CELLS ALLOT   DOES>  SWAP CELLS + ;
   : ARRAY  ( u "name" -- )
      CREATE IMMEDIATE CELLS ALLOT
      DOES>
	 STATE @ IF
	    POSTPONE CELLS  POSTPONE LITERAL  POSTPONE +
	 ELSE  SWAP CELLS +  THEN ;
[THEN]
   
[-DEF?] ARRAY iforth? AND [IF]
   : 2ARRAY  ( u "name" -- )
      CREATE IMMEDIATE  2* CELLS ALLOT
      DOES>  POSTPONE LITERAL EVAL" []DOUBLE " ;
[THEN]
[-DEF?] 2ARRAY [IF]
  : 2ARRAY  ( u "name" -- )  CREATE 2* CELLS ALLOT   DOES> SWAP 2* CELLS + ;
[THEN]


