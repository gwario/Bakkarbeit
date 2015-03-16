\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Move generator

0 VALUE move-gen-piece		\ piece for which moves are generated
0 VALUE move-gen-from		\ square of piece for which moves are generated

DEFER generate-move-to  ( square class -- )
DEFER generate-moves-from  ( square -- )

\
\ Routines for partially generating moves of piece >move-gen-from<
\
: normal-move  ( direction -- )
   move-gen-from +  DUP empty? IF
      #move-normal generate-move-to EXIT
   THEN DROP ;
: strike-move  ( direction -- )
   move-gen-from +  DUP opponent? IF
      #move-strike generate-move-to EXIT
   THEN DROP ;
: strike?-move  ( direction -- ) \ either strike or normal move
   move-gen-from +  DUP board @ ?DUP IF
      color-piece-mask AND opponent = IF
	 #move-strike generate-move-to EXIT
      THEN DROP EXIT
   THEN
   #move-normal generate-move-to ;

: pawn-far-move  ( direction -- ) \ move of pawn by 2 squares
   move-gen-piece f-unmoved AND IF
      move-gen-from +   DUP empty?  OVER 10 ?direction - empty? AND IF
	 #move-pawn-far generate-move-to EXIT
      THEN
   THEN  DROP ;
: strike-ep-move  ( direction -- ) \ strike en passante
   move-gen-from +   DUP -10 ?direction + far-moved-pawn = IF
      DUP empty? IF
	 #move-strike-ep generate-move-to EXIT
      THEN
   THEN  DROP ;
: pawn-trans-move  ( square -- ) \ pawn transformation at square
   DUP #move-trans-queen generate-move-to
       #move-trans-knight generate-move-to ;
: pawn-normal-move  ( direction -- )
   move-gen-from +   DUP empty? IF
      DUP pawn-trans? IF  pawn-trans-move 
      ELSE  #move-normal generate-move-to THEN
   ELSE DROP THEN ;
: pawn-strike-move  ( direction -- )
   move-gen-from +   DUP opponent? IF
      DUP pawn-trans? IF  pawn-trans-move
      ELSE  #move-strike generate-move-to THEN
   ELSE DROP THEN ;

: straight-peaceful-moves  ( direction -- ) \ generate non-capturing moves
   move-gen-from		 ( S: direction square )
   BEGIN OVER + DUP empty? WHILE
      DUP #move-normal generate-move-to
   REPEAT 2DROP ;
: straight-strike-moves  ( direction -- ) \ generate capturing moves only
   move-gen-from		( S: direction square )
   BEGIN  OVER +
      DUP board @ ?DUP IF			\ are we on a non-empty square?
	 color-piece-mask AND opponent = IF	\ opponent piece for capture?
	    DUP #move-strike generate-move-to
	 THEN 2DROP EXIT
      THEN
   AGAIN ;
: straight-moves  ( direction -- ) \ generate all moves into direction
   move-gen-from		( S: direction square )
   BEGIN  OVER +
      DUP board @ ?DUP IF			\ are we on a non-empty square?
	 color-piece-mask AND opponent = IF	\ opponent piece for capture?
	    DUP #move-strike generate-move-to
	 THEN 2DROP EXIT
      ELSE					\ normal non-capturing move
	 DUP #move-normal generate-move-to
      THEN
   AGAIN ;
: king-move  ( direction -- )
   move-gen-from +  DUP board @ ?DUP IF
      color-piece-mask AND opponent = IF	\ capturing move
	 #move-king-strike generate-move-to EXIT
      THEN DROP EXIT
   THEN						\ non-capturing move
   #move-king generate-move-to ;
: king-strike-move  ( direction -- )
   move-gen-from + DUP opponent? IF
      #move-king-strike generate-move-to EXIT
   THEN DROP ;
: king-peaceful-move  ( direction -- )
   move-gen-from + DUP empty? IF
      #move-king generate-move-to EXIT
   THEN DROP ;
: castle-near ( -- )
   move-gen-piece f-unmoved AND IF		\ king unmoved ?
      move-gen-from 1+ DUP empty?  OVER 1+ empty? AND
      SWAP 2 + unmoved? AND IF			\ rook unmoved ?
	 check?  move-gen-from 1+ threatened-by-opponent? OR 0= IF
	    move-gen-from 2 + #move-castle-near generate-move-to
	 THEN
      THEN
   THEN ;
: castle-far ( -- )
   move-gen-piece f-unmoved AND IF		\ king unmoved ?
      move-gen-from 1- DUP empty?  OVER 1- empty? AND  OVER 2 - empty? AND
      SWAP 3 - unmoved? AND IF		        \ rook unmoved ?
	 check? move-gen-from 1- threatened-by-opponent? OR 0= IF
	    move-gen-from 2 - #move-castle-far generate-move-to
	 THEN
      THEN
   THEN ;

\
\ Routines for generating all moves of piece >move-gen-piece<
\
: pawn-moves  ( -- )
   20 ?direction pawn-far-move
   10 ?direction pawn-normal-move
    9 ?direction DUP pawn-strike-move  strike-ep-move
   11 ?direction DUP pawn-strike-move  strike-ep-move ;
: knight-moves  ( -- )
   21 strike?-move  -21 strike?-move
   19 strike?-move  -19 strike?-move
   12 strike?-move  -12 strike?-move
    8 strike?-move   -8 strike?-move ;
: bishop-moves  ( -- )
   11 straight-moves  -11 straight-moves
    9 straight-moves   -9 straight-moves ;
: rook-moves  ( -- )
   10 straight-moves  -10 straight-moves
    1 straight-moves   -1 straight-moves ;
: queen-moves  ( -- )  bishop-moves rook-moves ;
: king-moves  ( -- )
   10 king-move  -10 king-move
   11 king-move  -11 king-move
    1 king-move   -1 king-move
    9 king-move   -9 king-move
   castle-near castle-far ;

\
\ Routines for generating peaceful (non-capturing) moves only
\
: pawn-peaceful-moves  ( -- )
   20 ?direction pawn-far-move
   10 ?direction pawn-normal-move ;
: knight-peaceful-moves  ( -- )
   21 normal-move  -21 normal-move
   19 normal-move  -19 normal-move
   12 normal-move  -12 normal-move
    8 normal-move   -8 normal-move ;
: bishop-peaceful-moves  ( -- )
   11 straight-peaceful-moves  -11 straight-peaceful-moves
    9 straight-peaceful-moves   -9 straight-peaceful-moves ;
: rook-peaceful-moves  ( -- )
   10 straight-peaceful-moves  -10 straight-peaceful-moves
    1 straight-peaceful-moves   -1 straight-peaceful-moves ;
: queen-peaceful-moves  ( -- )  bishop-peaceful-moves rook-peaceful-moves ;
: king-peaceful-moves  ( -- )
   10 king-peaceful-move  -10 king-peaceful-move
   11 king-peaceful-move  -11 king-peaceful-move
    1 king-peaceful-move   -1 king-peaceful-move
    9 king-peaceful-move   -9 king-peaceful-move
   castle-near castle-far ;

\
\ Routines for generating capturing moves only
\
: pawn-strike-moves  ( -- )
    9 ?direction DUP pawn-strike-move  strike-ep-move
   11 ?direction DUP pawn-strike-move  strike-ep-move ;
: knight-strike-moves  ( -- )
   21 strike-move  -21 strike-move
   19 strike-move  -19 strike-move
   12 strike-move  -12 strike-move
    8 strike-move   -8 strike-move ;
: bishop-strike-moves  ( -- )
   11 straight-strike-moves  -11 straight-strike-moves
    9 straight-strike-moves   -9 straight-strike-moves ;
: rook-strike-moves  ( -- )
   10 straight-strike-moves  -10 straight-strike-moves
    1 straight-strike-moves   -1 straight-strike-moves ;
: queen-strike-moves  ( -- )  bishop-strike-moves rook-strike-moves ;
: king-strike-moves  ( -- )
   10 king-strike-move  -10 king-strike-move
   11 king-strike-move  -11 king-strike-move
    1 king-strike-move   -1 king-strike-move
    9 king-strike-move   -9 king-strike-move ;

\
\ Vector tables for selecting the move generation routines for a piece
\
vector-table: (piece-moves)  ( piece -- )
   ' noop ,
   ' pawn-moves ,		' knight-moves ,
   ' bishop-moves ,		' rook-moves ,
   ' queen-moves ,		' king-moves ,
   ' noop ,
vector-table: (piece-strike-moves)  ( piece -- )
   ' noop ,
   ' pawn-strike-moves ,	' knight-strike-moves ,
   ' bishop-strike-moves ,	' rook-strike-moves ,
   ' queen-strike-moves ,	' king-strike-moves ,
   ' noop ,
vector-table: (piece-peaceful-moves)  ( piece -- )
   ' noop ,
   ' pawn-peaceful-moves ,	' knight-peaceful-moves ,
   ' bishop-peaceful-moves ,	' rook-peaceful-moves ,
   ' queen-peaceful-moves ,	' king-peaceful-moves ,
   ' noop ,
   
\
\ Move list generation
\
: select-moving-piece  ( square -- piece )
   DUP TO move-gen-from   board @ DUP TO move-gen-piece ;
: (generate-move-to-nocheck)  ( to class -- ) \ generate move if not in check
   DUP #move-normal =  OVER #move-strike = OR  OVER #move-pawn-far = OR IF
      move-gen-from might-cause-check? 0= IF
	 move-gen-from SWAP undefined add-move EXIT
      THEN
   THEN
   move-gen-from SWAP undefined ?add-move ;
: (generate-move-to-check)  ( to class -- ) \ generate move if already in check
   DUP #move-normal =   OVER #move-pawn-far = OR IF
      OVER might-block-check? 0= IF	\ this move is defenitely not valid!
	 2DROP EXIT
      THEN
   THEN
   move-gen-from SWAP undefined ?add-move ;
: (generate-move-to)  ( to class -- )
   curr-check?
   IF (generate-move-to-check) ELSE (generate-move-to-nocheck) THEN ;
: (generate-moves-from)  ( square -- )
   select-moving-piece piece-mask AND (piece-moves) ;
: (generate-strike-moves-from)  ( square -- )
   select-moving-piece piece-mask AND (piece-strike-moves) ;
: (generate-peaceful-moves-from)  ( square -- )
   select-moving-piece piece-mask AND (piece-peaceful-moves) ;
: (generate-moves)  ( -- )
   100 20 DO
      I my-piece? IF
	 I generate-moves-from
      THEN
   LOOP ;
: append-moves  ( -- )
   ['] (generate-move-to) IS generate-move-to
   ['] (generate-moves-from) IS generate-moves-from
   (generate-moves) ;
: append-strike-moves  ( -- )
   ['] (generate-move-to) IS generate-move-to
   ['] (generate-strike-moves-from) IS generate-moves-from
   (generate-moves) ;
: append-peaceful-moves  ( -- )
   ['] (generate-move-to) IS generate-move-to
   ['] (generate-peaceful-moves-from) IS generate-moves-from
   (generate-moves) ;
: generate-moves  ( -- )  new-moves append-moves ;
: generate-strike-moves  ( -- )  new-moves append-strike-moves ;
: generate-peaceful-moves  ( -- )  new-moves append-peaceful-moves ;

\
\ Generate moves, filtering all moves with a given target
\
0 VALUE forbidden-move-target
: (generate-move-not-to)  ( to class -- )
   OVER forbidden-move-target = IF 2DROP ELSE (generate-move-to) THEN ;
: append-moves-not-to  ( square -- )
   TO forbidden-move-target
   ['] (generate-move-not-to) IS generate-move-to
   ['] (generate-moves-from) IS generate-moves-from
   (generate-moves) ;
: append-strike-moves-not-to  ( square -- )
   TO forbidden-move-target
   ['] (generate-move-not-to) IS generate-move-to
   ['] (generate-strike-moves-from) IS generate-moves-from
   (generate-moves) ;
: generate-moves-not-to  ( square -- )  new-moves append-moves-not-to ;
: generate-strike-moves-not-to  ( square -- )
   new-moves append-strike-moves-not-to ;

\
\ Generate all non-capturing pawn promotions (used for quiescence search)
\
: generate-promotions  ( -- )
   my-piece pawn OR TO my-pawn
   white? IF h7 1+ a7 ELSE h2 1+ a2 THEN
   new-moves ?DO
      I board @ DUP my-pawn = IF
	 TO move-gen-piece  I TO move-gen-from
	 I 10 ?direction + DUP empty? IF
	    pawn-trans-move ELSE DROP
	 THEN
      ELSE DROP THEN
   LOOP ;

\
\ test whether a given move is possible and generate it
\
0 VALUE single-move-to
: (generate-single-move)  ( to class -- )
   OVER single-move-to = IF
      (generate-move-to)
   ELSE 2DROP THEN ;
: generate-single-move  ( from to -- )
   \ Warning: this will generate more than a single move if pawn
   \ transformations are possible (since they have the same from-to squares)
   new-moves
   OVER my-piece? IF
      TO single-move-to
      ['] (generate-single-move) IS generate-move-to
      ['] (generate-moves-from) IS generate-moves-from
      generate-moves-from 
   ELSE 2DROP THEN ;

\ excact versions:
0 VALUE single-move-class	
: (generate-single-move-x)  ( to class -- )
   2DUP single-move-to single-move-class D= IF
      (generate-move-to)
   ELSE 2DROP THEN ;
: generate-single-move-x  ( to from class -- )
   new-moves
   OVER my-piece? IF
      TO single-move-class  SWAP TO single-move-to
      ['] (generate-single-move-x) IS generate-move-to
      ['] (generate-moves-from) IS generate-moves-from
      generate-moves-from
   ELSE 2DROP DROP THEN ;

\
\ check whether any valid move exists (else we're stale or check mate)
\
0 VALUE move-exists?
: (move-to-exists?)'  ( to class -- )  \ dead code... (for reference)
   move-exists? IF  2DROP
   ELSE  move-gen-from SWAP 0 self-checking-move? 0= TO move-exists? THEN ;
: (move-to-exists?-nocheck)  ( to class -- )  \ used when not already in check
   DUP #move-normal =  OVER #move-strike = OR  OVER #move-pawn-far = OR IF
      move-gen-from might-cause-check? 0= IF
	 TRUE TO move-exists?  2DROP EXIT
      THEN
   THEN
   move-gen-from SWAP 0 self-checking-move? 0= TO move-exists? ;
: (move-to-exists?-check)  ( to class -- )  \ used when already in check
   DUP #move-normal =   OVER #move-pawn-far = OR IF
      OVER might-block-check? 0= IF
	 2DROP EXIT			\ this move is defenitely not valid!
      THEN
   THEN
   move-gen-from SWAP 0 self-checking-move? 0= TO move-exists? ;
: (move-to-exists?)  ( to class -- ) 
   move-exists? IF  2DROP
   ELSE
      curr-check?
      IF (move-to-exists?-check) ELSE (move-to-exists?-nocheck) THEN
   THEN ;
: moves-exist?  ( -- flag )
   0 TO move-exists?
   ['] (move-to-exists?) IS generate-move-to
   ['] (generate-moves-from) IS generate-moves-from
   100 20 DO
      I my-piece? IF
	 I TO move-gen-from   I board @ DUP TO move-gen-piece
	 piece-mask AND (piece-moves)
	 move-exists? IF LEAVE THEN
      THEN
   LOOP
   move-exists? ;

   
   




