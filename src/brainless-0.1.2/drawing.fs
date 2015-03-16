\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Drawing the chessboard

TRUE option ansi-terminal?	\ use ANSI Terminal codes (GForth only)
TRUE option color-terminal?	\ also use terminal colors? (GForth only)
FALSE option utf8-terminal?

0 VALUE white-field?
0 VALUE white-piece?
3 VALUE field-width
2 VALUE field-height

: small-board  ( -- ) 	s" small-board" print-def 2 to field-width  1 to field-height ;
: normal-board  ( -- ) 	s" normal-board" print-def 3 to field-width  2 to field-height ;
: huge-board  ( -- ) 	s" huge-board" print-def 5 to field-width  3 to field-height ;

[DEF?] >B [IF] : >BG 	s" >BG" print-def >B ; [THEN]
[DEF?] >F [IF] : >FG 	s" >FG" print-def >F ; [THEN]

: piece>ascii  ( piece -- c-addr u )
	s" piece>ascii" print-def
   piece>char PAD C!  PAD 1 ;
: piece>utf8  ( piece -- c-addr u )
	s" piece>utf8" print-def
   DUP piece-mask AND DUP 0= IF 2DROP  S"  " EXIT THEN
   SWAP f-white AND IF S" ♙♘♗♖♕♔" ELSE  s" ♟♞♝♜♛♚" THEN
   DROP SWAP 1- 3 * +  3 ;
: piece>string  ( piece -- c-addr u )
	s" piece>string" print-def
   utf8-terminal? IF piece>utf8 ELSE piece>ascii THEN ;
: (.piece)  ( piece -- ) 	s" (.piece)" print-def piece>string TYPE ;

\
\ Chessboard color display
\
gforth? ansi-terminal? AND [IF] \ GForth' ANSI Terminal routines

   : no-attr  ( -- ) 	s" no-attr" print-def <A A> ATTR! ;
   : white-field-attr 	s" white-field-attr" print-def  color-terminal? IF WHITE >BG ELSE INVERS THEN ;
   : black-field-attr 	s" black-field-attr" print-def  color-terminal? IF BLACK >BG THEN ;
   : field-attr  
		s" field-attr" print-def
      white-field? IF white-field-attr ELSE black-field-attr THEN ;
   : white-piece-attr  
		s" white-piece-attr" print-def
      color-terminal? IF   RED >FG BOLD field-attr
      ELSE   BOLD  THEN ;
   : black-piece-attr  
		s" black-piece-attr" print-def
      color-terminal? IF  BLUE >FG BOLD field-attr THEN ;
   : piece-attr
		s" piece-attr" print-def
      white-piece? IF white-piece-attr ELSE black-piece-attr THEN ;
   : field-spaces  ( n -- ) 	s" field-spaces" print-def <A field-attr A> ATTR! SPACES no-attr ;
   : .piece  ( piece -- )
		s" .piece" print-def
      <A piece-attr A> ATTR!
      utf8-terminal? color-terminal? AND IF  \ on color utf8 terminals,
	 f-white INVERT AND    \  only use the filled chess-piece glyphs.
      THEN  
      (.piece)  no-attr ;

[ELSE] iforth? [IF] \ iforth color code -- thanks to Marcel Hendrix
   
   : GRCOLOR  ( x "name" -- )
		s" GRCOLOR" print-def
      CREATE ,  DOES> @ 0 SWAP SYSCALL DROP ;

   #64 GRCOLOR black	#65 GRCOLOR blue
   #66 GRCOLOR green	#68 GRCOLOR red
   #78 GRCOLOR yellow	#79 GRCOLOR white

   : no-attr  ( -- ) 	s" no-attr" print-def black TO TextBGColor  white TO TextFGColor  BARE ;
   : field-attr  ( -- )
		s" field-attr" print-def
      white-field? IF white ELSE black THEN TO TextBGColor ;
   : piece-attr  ( -- )
		s" piece-attr" print-def
      white-piece? IF red ELSE blue THEN TO TextFGColor  field-attr ;
   : field-spaces  ( n -- ) 	s" field-spaces" print-def field-attr SetTerm SPACES no-attr ;
   : .piece  ( piece -- ) 	s" .piece" print-def piece-attr SetTerm (.piece) no-attr ;

[ELSE] \ no colors in other Forth systems (yet?) :-(
   
   : field-spaces  ( n -- )
		s" field-spaces" print-def
      white-field? IF BL ELSE [CHAR] : THEN
      SWAP 0 ?DO DUP EMIT LOOP   DROP ;
   : .piece  ( piece -- ) 	s" .piece" print-def (.piece) ;

[THEN] [THEN]

\
\ Display the chessboard slice by slice
\
: .field-slice  ( piece slice -- )
	s" .field-slice" print-def
   OVER f-piece AND 0<>   SWAP field-height 2/ = AND
   IF
      field-width 2/ TUCK field-spaces   .piece
      field-width 1- SWAP - field-spaces
   ELSE   DROP   field-width field-spaces THEN ;
: .vborder-slice  ( y slice -- )
	s" .vborder-slice" print-def
   SPACE   field-height 2/ = IF  1+ . ELSE  DROP 2 SPACES THEN ;
: .board-line  ( y -- )
	s" .board-line" print-def
   field-height 0 DO
      DUP I .vborder-slice
      8 0 DO
	 I OVER square-white? TO white-field?
	 I OVER xy-board@ DUP f-white AND 0<> TO white-piece?
	 J .field-slice
      LOOP
      DUP I .vborder-slice  CR
   LOOP DROP ;
: .hborder  ( -- )
	s" .hborder" print-def
   3 SPACES  8 0 DO
      field-width 2/ DUP SPACES   I [CHAR] A + EMIT
      field-width 1- SWAP - SPACES
   LOOP CR ;
: .board  ( -- )
	s" .board" print-def
   CR .hborder   0 7 DO I .board-line   -1 +LOOP   .hborder ;

