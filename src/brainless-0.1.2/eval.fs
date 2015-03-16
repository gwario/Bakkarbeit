\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Evaluating the chessboard

0 VALUE eval-square
0 VALUE eval-piece
0 VALUE #evals

\
\ square threat evaluation
\
create-array square-weights
   0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ,   
   0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ,   
   0 , 0 , 1 , 2 , 3 , 3 , 2 , 1 , 0 , 0 ,  
   0 , 1 , 2 , 3 , 4 , 4 , 3 , 2 , 1 , 0 , 
   0 , 2 , 3 , 5 , 6 , 6 , 5 , 3 , 2 , 0 , 
   0 , 3 , 4 , 6 , 8 , 8 , 6 , 4 , 3 , 0 , 
   0 , 3 , 4 , 6 , 8 , 8 , 6 , 4 , 3 , 0 , 
   0 , 2 , 3 , 5 , 6 , 6 , 5 , 3 , 2 , 0 , 
   0 , 1 , 2 , 3 , 4 , 4 , 3 , 2 , 1 , 0 , 
   0 , 0 , 1 , 2 , 3 , 3 , 2 , 1 , 0 , 0 ,
   0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ,
   0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 ,

: eval-threat  ( direction -- eval )  eval-square + square-weights @ ;
: eval-straight-threats  ( direction -- eval )
   eval-square   0		( S: direction square eval )
   BEGIN  >R OVER +
      DUP square-weights @   R> +
      OVER board @
   UNTIL  NIP NIP ;
: eval-pawn-threats  ( -- eval )
   eval-piece f-white AND IF  11 9 ELSE -11 -9 THEN
   eval-threat SWAP eval-threat + ;
: eval-knight-threats  ( -- eval )
   -21 eval-threat   -19 eval-threat +
   -12 eval-threat +  -8 eval-threat +
     8 eval-threat +  12 eval-threat +
    19 eval-threat +  21 eval-threat + ;
: eval-bishop-threats  ( -- eval )
   11 eval-straight-threats    -11 eval-straight-threats +
    9 eval-straight-threats +   -9 eval-straight-threats + ;
: eval-rook-threats  ( -- eval )
   10 eval-straight-threats    -10 eval-straight-threats +
    1 eval-straight-threats +   -1 eval-straight-threats + ;
: eval-queen-threats  ( -- eval )
   eval-bishop-threats eval-rook-threats + ;
: eval-king-threats  ( -- eval )
   ( better this way?) 0 EXIT
   10 eval-threat    -10 eval-threat +
   11 eval-threat +  -11 eval-threat +
    1 eval-threat +   -1 eval-threat +
    9 eval-threat +   -9 eval-threat + ;

\ knight threat evaluation only depends on the piece's position, not
\ on other pieces on the board -> so we may precalculate the threat values
create-array knight-threat-table   100 CELLS ALLOT

: init-knight-threat-table  ( -- )
   100 20 DO
      I TO eval-square eval-knight-threats  I knight-threat-table !
   LOOP ;
: eval-knight-threats  ( -- eval )  eval-square knight-threat-table @ ;

init-knight-threat-table

\
\ piece-specific evaluation
\
  256 CONSTANT pawn-weight
  768 CONSTANT knight-weight
  768 CONSTANT bishop-weight
 1280 CONSTANT rook-weight
 2560 CONSTANT queen-weight
25600 CONSTANT king-weight

create-array piece-weights
   ( empty) 0 ,
   pawn-weight ,     knight-weight ,
   bishop-weight ,   rook-weight ,
   queen-weight ,    king-weight ,
   ( border) 0 ,

\ pawn evaluation
\
create-array pawn-row-weights
   0 , 0 , 0 , 2 , 4 , 8 , 10 , 20 , 40 ,
-16 CONSTANT double-pawn-weight
  8 CONSTANT chained-pawn-weight
 16 CONSTANT neighbor-pawn-weight
   
0 VALUE this-pawn	    \ pawn which is currently evaluated (pawn OR color)
0 VALUE this-king	    \ king of the currently evaluated pawn's color
0 VALUE this-pawn-dir       \ move direction of current pawn (-1 or 1)

: set-this-pawn  ( -- )
   eval-piece DUP full-piece-mask AND TO this-pawn
   f-white AND 0= 1 OR TO this-pawn-dir ;
: set-this-pawn&king  ( -- )
   eval-piece DUP full-piece-mask AND DUP TO this-pawn
   color-piece-mask AND king OR TO this-king
   f-white AND 0= 1 OR TO this-pawn-dir ;
: pawn-row-eval  ( eval1 -- eval2 )
   eval-square 10 /   this-pawn-dir 0< IF  11 SWAP - THEN
   pawn-row-weights @ + ;
: ?pawn-eval  ( eval1 eval2 direction -- eval1 | eval1+eval2 )
   eval-square + get-piece-masked this-pawn = AND + ;
: ?king-eval  ( eval1 eval2 direction -- eval1 | eval1+eval2 )
   eval-square + get-piece-masked this-king = AND + ;
: pawn-eval  ( -- eval )
   set-this-pawn
   pawn-weight eval-pawn-threats +	( S: eval )
   double-pawn-weight   10                 ?pawn-eval
   chained-pawn-weight -11 this-pawn-dir * ?pawn-eval
   chained-pawn-weight  -9 this-pawn-dir * ?pawn-eval
   neighbor-pawn-weight  1 this-pawn-dir * ?pawn-eval
   pawn-row-eval ;

\ queen evaluation
\
24 CONSTANT queen-unmoved-weight

: queen-eval  ( -- eval )
   queen-weight eval-queen-threats +
   eval-piece f-unmoved AND 0<> queen-unmoved-weight AND + ;

\ king evaluation
\
32 CONSTANT king-castled-weight
16 CONSTANT king-unmoved-weight
12 CONSTANT king-front-guard-weight
 8 CONSTANT king-side-guard-weight
10 CONSTANT king-at-bottom-weight

0 VALUE king-guard-pawn		\ king's pawns for which is searched
0 VALUE king-guard-dir		\ direction in which those pawns are located

: set-king-guard-pawn  ( -- )
   eval-piece DUP f-white AND pawn OR TO king-guard-pawn 
   f-white AND 0= 1 OR TO king-guard-dir ;
: king-guard?  ( direction -- flag )
   king-guard-dir * eval-square + get-piece-masked king-guard-pawn = ;
: king-at-bottom?  ( -- flag )
   eval-square -10 king-guard-dir * +   border? ;
: king-eval  ( -- eval )
   king-weight ( better this way? ( eval-king-threats + )
   eval-piece f-castled AND IF
      king-castled-weight
   ELSE
      eval-piece f-unmoved AND 0<> king-unmoved-weight AND
   THEN +
   set-king-guard-pawn
   9 king-guard? king-side-guard-weight AND +
   10 king-guard? king-front-guard-weight AND +
   11 king-guard? king-side-guard-weight AND +
   king-at-bottom? king-at-bottom-weight AND + ;

\ other piece evaluations
\
: knight-eval  ( -- eval )  knight-weight eval-knight-threats + ;
: bishop-eval  ( -- eval )  bishop-weight eval-bishop-threats + ;
: rook-eval  ( -- eval )  rook-weight eval-rook-threats + ;

vector-table: (piece-eval)  ( piece -- eval )
   ( empty)  ' noop ,
   ( pawn)   ' pawn-eval ,       ( knight) ' knight-eval ,
   ( bishop) ' bishop-eval ,     ( rook)   ' rook-eval ,
   ( queen)  ' queen-eval ,      ( king)   ' king-eval ,
     
: piece-eval  ( -- eval )
   eval-piece piece-mask AND (piece-eval)
   eval-piece f-white AND 0= IF NEGATE THEN ;

\
\ total evaluation
\
: total-eval  ( -- eval )
   0  100 20 DO    ( S: eval )
      I board @ DUP TO eval-piece   f-piece AND IF
	 I TO eval-square   piece-eval +
      THEN
   LOOP
   #evals 1+ TO #evals ;

\ move evaluation
\
: (eval-move)  ( to from class 0 -- eval )
   do-move-undo-info total-eval >R undo-move R> ;
: (eval-moves)  ( -- )
   TRUE to moves-evaluated?
   #moves 0 ?DO  I get-move eval-move  I set-eval LOOP ;
: set-curr-abs-eval  ( -- ) \ update >curr-abs-eval< when board is changed
   total-eval TO curr-abs-eval ;
' set-curr-abs-eval add-board-hook

' (eval-move) IS eval-move
' (eval-moves) IS eval-moves

\ lazy (material only) move evaluation
\
: get-lazy-move-eval  ( move-index -- eval )
   get-target board @ piece-mask AND piece-weights @ curr-eval + ;