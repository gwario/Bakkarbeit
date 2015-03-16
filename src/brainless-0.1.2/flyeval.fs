\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Dynamic "on-the-fly" chessboard evaluation

\ threat delta evaluation (handles threats that are indirectly influenced by
\ the moving piece)
\
: rook-threats-delta-eval  ( direction -- delta-eval )
   \ get evaluation delta value resulting from change of rook/queen threats
   \ caused by adding a new piece (or removing, if delta-eval is substracted)
   eval-square board   BEGIN  OVER CELLS +  DUP @ ?DUP UNTIL
   DUP piece-mask AND  DUP queen =  SWAP rook = OR IF   
      NIP SWAP NEGATE eval-straight-threats
      SWAP f-white AND IF  NEGATE THEN EXIT
   THEN
   2DROP DROP 0 ;
: bishop-threats-delta-eval  ( direction -- delta-eval )
   \ same as `rook-threats-delta-eval', just for bishops/queens
   eval-square board   BEGIN  OVER CELLS +  DUP @ ?DUP UNTIL
   DUP piece-mask AND  DUP queen =  SWAP bishop = OR IF
      NIP SWAP NEGATE eval-straight-threats
      SWAP f-white AND IF  NEGATE THEN EXIT
   THEN
   2DROP DROP 0 ;
: threats-delta-eval  ( -- delta-eval )
   0
   -11 bishop-threats-delta-eval +   -10 rook-threats-delta-eval +
    -9 bishop-threats-delta-eval +    -1 rook-threats-delta-eval +
     1 rook-threats-delta-eval +       9 bishop-threats-delta-eval +
    10 rook-threats-delta-eval +      11 bishop-threats-delta-eval + ;

\
\ piece specific evaluation
\

\ pawn evaluation
\
: pawn-delta-eval  ( -- delta-eval ) \ eval changes, not handled by pawn-eval
   set-this-pawn&king
   pawn-weight eval-pawn-threats +	( S: eval )
   double-pawn-weight	    10  ?pawn-eval
   double-pawn-weight	   -10  ?pawn-eval
   chained-pawn-weight	   -11  ?pawn-eval
   chained-pawn-weight	    -9  ?pawn-eval
   chained-pawn-weight	    11  ?pawn-eval
   chained-pawn-weight	     9  ?pawn-eval
   neighbor-pawn-weight	     1  ?pawn-eval
   neighbor-pawn-weight	    -1  ?pawn-eval
   king-front-guard-weight -10 this-pawn-dir * ?king-eval
   king-side-guard-weight  -11 this-pawn-dir * ?king-eval
   king-side-guard-weight   -9 this-pawn-dir * ?king-eval
   pawn-row-eval ;

vector-table: (piece-delta-eval)  ( piece -- eval )
   ( empty)  ' noop ,
   ( pawn)   ' pawn-delta-eval ,      ( knight) ' knight-eval ,
   ( bishop) ' bishop-eval ,          ( rook)   ' rook-eval ,
   ( queen)  ' queen-eval ,           ( king)   ' king-eval ,
     
: piece-delta-eval  ( -- eval )
   eval-piece piece-mask AND (piece-delta-eval)
   eval-piece f-white AND 0= IF NEGATE THEN ;

\
\ total delta evaluation
\
0 VALUE fly-eval		\ currently generated evaluation

: delta-eval  ( piece square -- delta-eval )
   TO eval-square TO eval-piece 
   piece-delta-eval threats-delta-eval + ;
: eval-put  ( piece square -- )
   delta-eval fly-eval + TO fly-eval ;
: eval-remove  ( field -- )
   DUP board @ ?DUP IF
      SWAP delta-eval fly-eval SWAP - TO fly-eval
   ELSE  DROP THEN ;
: eval-replace  ( piece square -- )
   DUP eval-remove eval-put ;

\
\ evaluate moves 
\
0 VALUE fly-eval-piece
0 VALUE fly-eval-square

: fly-eval-strike-move  ( to -- )
   fly-eval-piece moved SWAP eval-replace ;
: fly-eval-normal-move  ( to -- )
   fly-eval-piece moved SWAP eval-put ;
: fly-eval-strike-ep-move  ( to -- )
   fly-eval-piece moved SWAP 2DUP eval-put TUCK put-piece
   DUP 10 ?direction - eval-remove
   remove-piece ;
: fly-eval-trans-knight  ( to -- )
   knight my-piece OR SWAP eval-replace ;
: fly-eval-trans-queen  ( to -- )
   queen my-piece OR SWAP eval-replace ;
: fly-eval-castle-near  ( to -- )
   fly-eval-piece moved castled SWAP TUCK   2DUP eval-put put-piece
   white? IF f1 h1 ELSE f8 h8 THEN
   DUP eval-remove DUP board @     ( S: to to2 from2 was-rook )
   OVER remove-piece ROT OVER SWAP eval-put   SWAP board !
   remove-piece ;
: fly-eval-castle-far  ( to -- )
   fly-eval-piece moved castled SWAP TUCK   2DUP eval-put put-piece
   white? IF d1 a1 ELSE d8 a8 THEN
   DUP eval-remove DUP board @     ( S: to to2 from2 was-rook )
   OVER remove-piece ROT OVER SWAP eval-put   SWAP board !
   remove-piece ;
   
vector-table: (fly-eval-move)  ( to class -- )
   ' fly-eval-normal-move ,     ' fly-eval-strike-move , 
   ' fly-eval-strike-ep-move ,  ' fly-eval-normal-move ,
   ' fly-eval-trans-knight ,    ' fly-eval-trans-queen ,
   ' fly-eval-normal-move ,	' fly-eval-strike-move ,
   ' fly-eval-castle-near ,     ' fly-eval-castle-far ,

: fly-eval-move  ( to from class 0 -- eval )
   DROP curr-abs-eval TO fly-eval
   SWAP DUP TO fly-eval-square   board @ TO fly-eval-piece
   fly-eval-square DUP eval-remove   remove-piece
   (fly-eval-move)
   fly-eval-piece fly-eval-square board !
   #evals 1+ TO #evals
   fly-eval ;

: fly-eval-moves  ( -- ) \ evaluate complete move-list, in an optimized way
   0 TO fly-eval-square   border TO fly-eval-piece
   TRUE TO moves-evaluated?
   #moves 0 ?DO
      I get-move DROP
      SWAP fly-eval-square OVER = IF  DROP 
      ELSE		\ evaluate remove for each piece once only!
	 fly-eval-piece fly-eval-square board !
	 curr-abs-eval TO fly-eval
	 DUP TO fly-eval-square board @ TO fly-eval-piece
	 fly-eval-square DUP eval-remove remove-piece
      THEN
      fly-eval -ROT
      (fly-eval-move) fly-eval I set-eval
      TO fly-eval
   LOOP
   fly-eval-piece fly-eval-square board !
   #evals #moves + TO #evals ;

: +fly-eval  ( -- )
   total-eval TO curr-abs-eval
   ['] fly-eval-move IS eval-move 
   ['] fly-eval-moves IS eval-moves ;
: -fly-eval  ( -- )
   ['] (eval-move) IS eval-move
   ['] (eval-moves) IS eval-moves ;

.( fly-eval enabled  )
init-board +fly-eval

