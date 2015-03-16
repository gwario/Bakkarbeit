\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Generate list of valid capture moves to a given target square

0 VALUE move-gen-to	\ target of moves for target move generation
0 VALUE target-piece	\ straight moving piece, masked with >full-piece-mask<

\
\ target move generation helper routines
\
: generate-move-from  ( from class -- )
	s" generate-move-from" print-def
   DUP #move-strike = curr-check? 0= AND IF
      OVER might-cause-check? 0= IF
	 move-gen-to -ROT undefined add-move EXIT
      THEN
   THEN  move-gen-to -ROT undefined ?add-move ;

: pawn-trans-target-move  ( from -- )
	s" pawn-trans-target-move" print-def
   DUP #move-trans-queen generate-move-from
       #move-trans-knight generate-move-from ;
: pawn-target-move  ( direction -- )
	s" pawn-target-move" print-def
   ?direction move-gen-to +  DUP get-piece-masked my-pawn = IF
      move-gen-to pawn-trans? IF  pawn-trans-target-move
      ELSE  #move-strike generate-move-from THEN
   ELSE DROP THEN ;
: knight-target-move  ( direction -- )
	s" knight-target-move" print-def
   ?direction move-gen-to +  DUP get-piece-masked my-knight = IF
      #move-strike generate-move-from
   ELSE DROP THEN ;
: straight-target-move  ( direction -- )
	s" straight-target-move" print-def
   move-gen-to				( S: direction square )
   BEGIN OVER + DUP board @ ?DUP UNTIL	( S: direction square piece )
   full-piece-mask AND target-piece = IF
      #move-strike generate-move-from   DROP
   ELSE 2DROP THEN ;

\
\ Generate lists of all moves to a given target
\
: pawn-target-moves  ( -- )
	s" pawn-target-moves" print-def
   -11 pawn-target-move  -9 pawn-target-move ;
: knight-target-moves  ( -- )
	s" knight-target-moves" print-def
   21 knight-target-move  -21 knight-target-move
   19 knight-target-move  -19 knight-target-move
   12 knight-target-move  -12 knight-target-move
    8 knight-target-move   -8 knight-target-move ;
: bishop-target-moves  ( -- ) \ also handles queen target moves!
	s" bishop-target-moves" print-def
   my-bishop TO target-piece
   11 straight-target-move  -11 straight-target-move
    9 straight-target-move   -9 straight-target-move ;
: rook-target-moves  ( -- ) \ also handles queen target moves!
	s" rook-target-moves" print-def
   my-rook TO target-piece
   10 straight-target-move  -10 straight-target-move
    1 straight-target-move   -1 straight-target-move ;
: queen-target-moves  ( -- )
	s" queen-target-moves" print-def
   my-queen TO target-piece
   11 straight-target-move  -11 straight-target-move
    9 straight-target-move   -9 straight-target-move 
   10 straight-target-move  -10 straight-target-move
    1 straight-target-move   -1 straight-target-move ;
: king-target-move  ( -- )
	s" king-target-move" print-def
   move-gen-to king-square - ABS  DUP 9 12 WITHIN SWAP  1 = OR IF
      king-square #move-king-strike generate-move-from
   THEN ;

\
\ Generate single moves to a given target
\
: ?single-move  ( -- )
	s" ?single-move" print-def
   POSTPONE #moves
   POSTPONE IF
   POSTPONE EXIT
   POSTPONE THEN ; IMMEDIATE
: pawn-single-target-move  ( -- )
	s" pawn-single-target-move" print-def
   pawn my-piece OR full-piece-mask AND TO my-pawn
   -11 pawn-target-move ?single-move   -9 pawn-target-move ;
: knight-single-target-move  ( -- )
	s" knight-single-target-move" print-def
   knight my-piece OR full-piece-mask AND TO my-knight
   21 knight-target-move ?single-move  -21 knight-target-move ?single-move
   19 knight-target-move ?single-move  -19 knight-target-move ?single-move
   12 knight-target-move ?single-move  -12 knight-target-move ?single-move
    8 knight-target-move ?single-move   -8 knight-target-move ;
: bishop-single-target-move  ( -- )
	s" bishop-single-target-move" print-def
   bishop my-piece OR full-piece-mask AND TO target-piece
   11 straight-target-move ?single-move  -11 straight-target-move ?single-move
    9 straight-target-move ?single-move   -9 straight-target-move ;
: rook-single-target-move  ( -- )
	s" rook-single-target-move" print-def
   rook my-piece OR full-piece-mask AND TO target-piece
   10 straight-target-move ?single-move  -10 straight-target-move ?single-move
    1 straight-target-move ?single-move   -1 straight-target-move ;
: queen-single-target-move  ( -- )
	s" queen-single-target-move" print-def
   queen my-piece OR full-piece-mask AND TO target-piece
   11 straight-target-move ?single-move  -11 straight-target-move ?single-move
    9 straight-target-move ?single-move   -9 straight-target-move ?single-move
   10 straight-target-move ?single-move  -10 straight-target-move ?single-move
    1 straight-target-move ?single-move   -1 straight-target-move ;

\
\ High-level target move generation
\
: legal-move-target?  ( square -- flag )
	s" legal-move-target?" print-def
   board @ DUP piece-mask AND king <>           \ king capture is not possible
   SWAP color-piece-mask AND opponent = AND ;	\ target must be opponent
: append-moves-to  ( square -- )
	s" append-moves-to" print-def
   DUP TO move-gen-to  legal-move-target? IF
      my-pieces
      pawn-target-moves knight-target-moves
      bishop-target-moves rook-target-moves
      queen-target-moves king-target-move
   THEN ;
: generate-moves-to  ( square -- ) 	s" generate-moves-to" print-def new-moves append-moves-to ;
: generate-cheapest-move-to  ( square -- )
	s" generate-cheapest-move-to" print-def
   new-moves
   DUP TO move-gen-to  legal-move-target? IF
      pawn-single-target-move    ?single-move
      knight-single-target-move  ?single-move
      bishop-single-target-move  ?single-move
      rook-single-target-move    ?single-move
      queen-single-target-move   ?single-move
      king-target-move
   THEN ;
: generate-cheapest-weak-move-to  ( square -- )
	s" generate-cheapest-weak-move-to" print-def
   new-moves
   DUP TO move-gen-to  legal-move-target? IF
      pawn-single-target-move    ?single-move
      knight-single-target-move  ?single-move
      bishop-single-target-move  
   THEN ;
: generate-pawn-move-to  ( square -- )
	s" generate-pawn-move-to" print-def
   new-moves
   DUP TO move-gen-to opponent? IF  pawn-single-target-move THEN ;
      
: delete-moves-to  ( square -- )
	s" delete-moves-to" print-def
   #moves 0 ?DO
      DUP I get-target = IF  I delete-move RECURSE UNLOOP EXIT THEN
   LOOP DROP ;

\
\ Calculate capture-recapture balances
\
: simulate-capture  ( to from  -- from to was-target )
	s" simulate-capture" print-def
   SWAP DUP board @ >R			( S: from to  R: was-target )
   OVER board @  OVER board !		\ copy piece to >to<
   OVER empty-square SWAP board ! 	\ clear move origin
   R> ;					( S: from to was-target )
: undo-simulated-capture  ( from to was-target  -- )
	s" undo-simulated-capture" print-def
   -ROT DUP board @   ROT board !	\ move piece back to >from<
   board ! ;				\ restore >to< to >was-target<
: eval-capture-balance  ( to from class x -- delta-eval )
	s" eval-capture-balance" print-def
   2DROP simulate-capture
   DUP piece-mask AND piece-weights @ >R	( S: undo-info  R: eval )
   OVER other-party generate-cheapest-move-to
   #moves IF
      R>  0 get-move RECURSE 0 MAX -  >R
   THEN
   forget-moves other-party			( S: undo-info  R: eval )
   undo-simulated-capture R> ;			( S: eval )
   
   
   
      


