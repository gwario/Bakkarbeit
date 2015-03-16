\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Move validator (used for stored killers&principal variation)

: single-pawn-normal-move  ( to class -- )
   OVER empty? IF
      OVER move-gen-from 10 ?direction + =  IF
	 OVER pawn-trans? 0= IF  (generate-move-to) EXIT THEN
      THEN
   THEN 2DROP ;
: single-nonpawn-normal-move  ( to class -- )
   OVER empty? IF
      OVER move-gen-from SWAP threatens? IF
	 (generate-move-to) EXIT
      THEN
   THEN 2DROP ;
: single-normal-move  ( to class -- )
   move-gen-piece piece-mask AND DUP pawn = IF
      DROP single-pawn-normal-move EXIT
   THEN
   king <> IF  single-nonpawn-normal-move EXIT THEN   2DROP ;
: single-pawn-strike-move  ( to class -- )
   OVER opponent? IF
      OVER move-gen-from 2DUP 9 ?direction + =  -ROT 11 ?direction + = OR IF
	 OVER pawn-trans? 0= IF  (generate-move-to) EXIT THEN
      THEN
   THEN 2DROP ;
: single-nonpawn-strike-move  ( to class -- )
   OVER opponent? IF
      OVER move-gen-from SWAP threatens? IF   (generate-move-to) EXIT THEN
   THEN 2DROP ;
: single-strike-move  ( to class -- )
   move-gen-piece piece-mask AND DUP pawn = IF
      DROP single-pawn-strike-move EXIT
   THEN
   king <> IF  single-nonpawn-strike-move EXIT THEN   2DROP ;
: single-pawn-far-move  ( to class -- )
   OVER move-gen-from 20 ?direction + DUP empty?   -ROT = AND IF
      move-gen-from 10 ?direction + empty?
      move-gen-piece f-unmoved AND AND IF  (generate-move-to) EXIT THEN
   THEN 2DROP ;
: single-trans-move  ( to class -- )
   OVER pawn-trans? IF
      OVER empty? IF	\ normal promotion move
	 OVER move-gen-from 10 ?direction + =
      ELSE		\ capture promotion move
	 OVER move-gen-from 2DUP 9 ?direction + =  -ROT 11 ?direction + = OR
      THEN
      IF (generate-move-to) EXIT THEN
   THEN
   2DROP ;
: single-strike-ep-move  ( to class -- )
   OVER empty? IF
      OVER -10 ?direction + far-moved-pawn = IF
	 OVER move-gen-from 2DUP 9 ?direction + =  -ROT 11 ?direction + = OR IF
	    (generate-move-to) EXIT
	 THEN
      THEN
   THEN 2DROP ;
: single-king-move  ( to class -- )
   move-gen-from king-square = IF single-nonpawn-normal-move EXIT THEN  2DROP ;
: single-king-strike-move  ( to class -- )
   move-gen-from king-square = IF single-nonpawn-strike-move EXIT THEN  2DROP ;
: single-castle-near  ( to class -- )  2DROP castle-near ;
: single-castle-far  ( to class -- )  2DROP castle-far ;

vector-table: (generate-single-class-move)  ( to class class -- )
   ' single-normal-move ,	' single-strike-move ,
   ' single-strike-ep-move ,	' single-pawn-far-move ,
   ' single-trans-move ,	' single-trans-move ,
   ' single-king-move ,		' single-king-strike-move ,
   ' single-castle-near ,	' single-castle-far ,

: generate-single-class-move  ( to from class -- )
   new-moves
   OVER my-piece? IF
      SWAP select-moving-piece DROP
      DUP (generate-single-class-move)
   ELSE 3DROP THEN ;
      
