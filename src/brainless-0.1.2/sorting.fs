\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ move sorting for best cutoff

0 VALUE move-weights

: create-move-weights  ( -- )
	s" create-move-weights" print-def
   move-weights ,
   HERE #moves CELLS ALLOT TO move-weights ;
: forget-move-weights  ( -- )
	s" forget-move-weights" print-def
   move-weights HERE - ALLOT
   HERE 1 CELLS - @ TO move-weights
   -1 CELLS ALLOT ;
: move-weight  ( index -- a-addr ) 	s" move-weight" print-def CELLS move-weights + ;

200 CONSTANT killer-weight

create-array piece-move-weights  0 ,
   ( pawn) -3 ,   ( knight) -9 ,   ( bishop) -9 ,
   ( rook) -15 ,   ( queen) -30 ,    ( king) -9 ,

: ttable-adjust-weight  ( weight1 move-index -- weight2 )
	s" ttable-adjust-weight" print-def
   tt-retrieve-move ?DUP IF  >R
      R@ ttentry-up @ undefined <> IF
	 R@ ttentry-distance @ horizon-distance 1- < 0= 
	 R@ ttentry-up @ NEGATE beta < 0= AND IF
	    DROP +infinity
	 ELSE
	    R@ ttentry-up @ NEGATE MAX
	    R@ ttentry-low @ NEGATE MIN
	 THEN
      THEN
      R> DROP
   THEN ;
: weight-moves  ( -- )
	s" weight-moves" print-def
   create-move-weights
   #moves 0 ?DO
      I get-eval  ( DUP undefined <> AND)  my-eval
      I ttable-adjust-weight 
      on-principal-variation?  I is-principal-move? AND   3000 AND +
      I get-move-noeval simple-killer killer@ killer=     2000 AND +
      I get-rel-kill-count  DUP 3 > IF 50 + THEN  4 *   +
      I get-target empty? 0= IF
	 I get-orig board @ piece-mask AND piece-move-weights @ 10 *   +
      THEN
      I move-weight !
   LOOP ;
      
: next-best-move  ( -- move-index|-1 )
	s" next-best-move" print-def
   -1 -infinity
   #moves 0 ?DO
      I move-weight @ 2DUP < IF
	 NIP I SWAP ROT
      THEN DROP
   LOOP DROP
   DUP -1 <> IF -infinity OVER move-weight ! THEN ;
: swap-moves&weights  ( index1 index2 -- )
	s" swap-moves&weights" print-def
   2DUP swap-moves
   OVER move-weight @ OVER move-weight @ SWAP
   ROT move-weight !   SWAP move-weight ! ;
: sort-moves-by-weight  ( -- )
	s" sort-moves-by-weight" print-def
   #moves 2 < IF  EXIT THEN
   #moves 1- 0 ?DO
      -1 I move-weight @   ( S: best-index best-eval )
      #moves I 1+ ?DO
	 I move-weight @ 2DUP < IF NIP I SWAP ROT THEN DROP
      LOOP
      DROP DUP 0< 0= IF   I swap-moves&weights ELSE DROP THEN
   LOOP ;
   
 

   
