\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Chess board handling/initialisation

120 ARRAY board
120 ARRAY initial-board
32  ARRAY board-hooks	        \ list of XTs to execute when board changes
0   VALUE #board-hooks		\ number of XTs in >board-hooks<

0 VALUE black-king-square
0 VALUE white-king-square
0 VALUE far-moved-pawn		\ the only pawn that can be struck en passante
TRUE VALUE white?		\ color of current party

: add-board-hook  ( xt -- )
   #board-hooks board-hooks !
   #board-hooks 1+ TO #board-hooks ;
: update-board  ( xt -- )
   #board-hooks 0 ?DO
      I board-hooks @ EXECUTE
   LOOP ;

: king-square  ( -- square )
   white? IF white-king-square ELSE black-king-square THEN ;
: opponent-king-square  ( -- square )
   white? IF black-king-square ELSE white-king-square THEN ;
: position-king  ( square -- )
   white? IF  TO white-king-square ELSE  TO black-king-square THEN ;
: >square  ( x y -- square )  2 + 10 *   + 1+ ;
: >xy  ( square -- x y )  1- 10 /MOD  2 - ;
: xy-board!  ( piece x y -- )   >square board ! ;
: xy-board@  ( x y -- )   >square board @ ;
: >delta-xy  ( square1 square2 -- dx dy )
   \ return vector >square2-square1<
   10 /MOD  ROT 10 /MOD  SWAP >R -  SWAP R> -  SWAP ;
: bishop-xy?  ( dx dy -- flag )  ABS SWAP ABS = ;
: rook-xy?  ( dx dy -- flag )  0= SWAP 0= XOR ;
: bishop-direction?  ( square1 square2 -- flag )  >delta-xy bishop-xy? ;
: rook-direction?  ( square1 square2 -- flag )  >delta-xy rook-xy? ;
create-array x-direction-table
   -1 , -1 , -1 , -1 , -1 , -1 , -1 , 0 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 
create-array y-direction-table
   -10 , -10 , -10 , -10 , -10 , -10 , -10 , 0 ,
   10 , 10 , 10 , 10 , 10 , 10 , 10 , 
: delta-xy>direction  ( dx dy -- direction )
   \ convert vector into single-step increment >direction<
   7 + y-direction-table @  SWAP 7 + x-direction-table @ + ;
: >direction  ( square1 square2 -- direction)
   \ return single-step increment to get from square2 to square1
   >delta-xy delta-xy>direction ;

0 CONSTANT empty-square		\ types of pieces
1 CONSTANT pawn
2 CONSTANT knight
3 CONSTANT bishop
4 CONSTANT rook
5 CONSTANT queen
6 CONSTANT king
7 CONSTANT border
7 CONSTANT piece-mask

0 VALUE my-pawn			\ values that shouldn't be calculated twice
0 VALUE my-knight
0 VALUE my-bishop
0 VALUE my-rook
0 VALUE my-queen
0 VALUE my-king
0 VALUE opponent-pawn		
0 VALUE opponent-knight
0 VALUE opponent-bishop
0 VALUE opponent-rook
0 VALUE opponent-queen
0 VALUE opponent-king

 8 CONSTANT f-white		\ flags that are ORed with the piece type
16 CONSTANT f-unmoved
32 CONSTANT f-piece		\ set on all pieces
64 CONSTANT f-castled		\ only set on kings if already castled

\ complex masks
piece-mask f-white OR        CONSTANT full-piece-mask
full-piece-mask f-unmoved OR CONSTANT hash-piece-mask
f-white f-piece OR	     CONSTANT color-piece-mask

f-white CONSTANT w
      0 CONSTANT b

f-piece VALUE opponent		  \ opponent pieces masked by color-piece-mask
f-piece f-white OR VALUE my-piece \ my pieces masked by color-piece-mask
1 VALUE pawn-direction		  \ direction in which pawns can move (+1|-1)

: other-party ( -- )
   opponent my-piece TO opponent TO my-piece
   white? 0= TO white?
   pawn-direction NEGATE TO pawn-direction ;
: set-party  ( white? -- )
   0= white? = IF other-party THEN ;

: squares:  ( -- )
   8 0 DO
      REFILL 0= -16 AND THROW    8 0 DO   I J >square CONSTANT   LOOP
   LOOP ;
squares:
   a1 b1 c1 d1 e1 f1 g1 h1
   a2 b2 c2 d2 e2 f2 g2 h2
   a3 b3 c3 d3 e3 f3 g3 h3
   a4 b4 c4 d4 e4 f4 g4 h4
   a5 b5 c5 d5 e5 f5 g5 h5
   a6 b6 c6 d6 e6 f6 g6 h6
   a7 b7 c7 d7 e7 f7 g7 h7
   a8 b8 c8 d8 e8 f8 g8 h8

: init-kings  ( -- ) \ search the kings to setup the king square values
   100 20 DO
      I board @  DUP piece-mask AND king = IF
	 f-white AND
	 IF  I TO white-king-square   ELSE   I TO black-king-square THEN
      ELSE DROP THEN
   LOOP ;
' init-kings add-board-hook
: place  ( piece square -- )  SWAP f-piece OR  SWAP board ! ;
: place-black  place ;
: place-white  ( piece field -- )  SWAP f-white OR   SWAP place ;
: place-b&w  ( piece field -- )  \ symmetrically place black and white pieces
   2DUP place-white   >xy 7 SWAP - >square place-black ;
: unmoved  ( piece1 -- piece2 )  f-unmoved OR ;
: moved  ( piece1 -- piece2 )  [ f-unmoved INVERT ] LITERAL AND ;
: castled  ( piece1 -- piece2 )  f-castled OR ;
: white-piece  ( piece1 -- piece2 )  f-piece OR f-white OR ;
: black-piece  ( piece1 -- piece2 )  f-piece OR ;

: init-board  ( -- )
   0 board 120 CELLS ERASE
   20 0 DO   border DUP I board !   I 100 + board !  LOOP
   100 20 DO  border DUP I board !   I 9 + board !  10 +LOOP
   8 0 DO  pawn unmoved I 1 >square place-b&w LOOP
   rook   unmoved DUP a1 place-b&w  h1 place-b&w
   knight unmoved DUP b1 place-b&w  g1 place-b&w
   bishop unmoved DUP c1 place-b&w  f1 place-b&w
   queen  unmoved d1 place-b&w
   king   unmoved e1 place-b&w
   update-board
   0 board 0 initial-board 120 CELLS MOVE
   TRUE set-party ;
: empty-board  ( -- )
   8 0 DO  8 0 DO  empty-square I J xy-board! LOOP LOOP
   update-board ;
: initial-square?  ( square -- flag )
   DUP board @ full-piece-mask AND
   SWAP initial-board @ full-piece-mask AND = ;
   
: empty?  ( square -- flag )  board @ 0= ;
: border?  ( square -- flag )   board @ border = ;
: opponent?  ( square -- flag )  board @  color-piece-mask AND opponent = ;
: piece?  ( square -- flag )  board @  f-piece AND 0<> ;
: my-piece?  ( square -- flag )  board @  color-piece-mask AND my-piece = ;
: unmoved?  ( square -- flag )  board @ f-unmoved AND 0<> ;
: pawn?  ( square -- flag )  board @ piece-mask AND pawn = ;

: ?direction  ( n -- n|-n )  pawn-direction * ;
: pawn-trans?  ( square -- ) \ pawn transformation possible at field?
   DUP a1 [ h1 1+ ] LITERAL WITHIN
   SWAP a8 [ h8 1+ ] LITERAL WITHIN OR ;

: remove-piece  ( square -- )  board 0 SWAP ! ;
: get-piece-masked  ( square -- piece )  board @  full-piece-mask AND ;
: take-piece  ( square -- piece )
   board DUP @   [ f-unmoved INVERT ] LITERAL AND
   0 ROT ! ;
: put-piece  ( piece square -- )  board ! ;

: my-pieces  ( -- ) \ set up my-pawn ... my-king
   my-piece f-white AND
   pawn OVER OR TO my-pawn
   knight OVER OR TO my-knight
   bishop OVER OR TO my-bishop
   rook OVER OR TO my-rook
   queen OVER OR TO my-queen
   king OR TO my-king ;
: opponent-pieces  ( -- ) \ set up opponent-pawn ... opponent-king
   opponent f-white AND
   pawn OVER OR TO opponent-pawn
   knight OVER OR TO opponent-knight
   bishop OVER OR TO opponent-bishop
   rook OVER OR TO opponent-rook
   queen OVER OR TO opponent-queen
   king OR TO opponent-king ;

: count-my-non-pawn-pieces  ( -- u )
   0   100 20 DO
      I my-piece? IF
	 I board @ piece-mask AND knight king 1+ WITHIN -
      THEN
   LOOP ;

: .square  ( square -- ) \ print square (eg. "e1")
   >xy  SWAP [CHAR] a + EMIT   [CHAR] 1 + EMIT ;
: piece>char  ( piece -- char )
   f-white OVER AND IF   S"  PNBRQK" ELSE  S"  pnbrqk" THEN DROP
   SWAP piece-mask AND   CHARS + C@ ;
: char>piece  ( char -- piece true | false )
   CASE
      [CHAR] P OF  pawn white-piece ENDOF
      [CHAR] B OF  bishop white-piece ENDOF
      [CHAR] N OF  knight white-piece ENDOF
      [CHAR] R OF  rook white-piece ENDOF
      [CHAR] Q OF  queen white-piece ENDOF
      [CHAR] K OF  king white-piece ENDOF
      [CHAR] p OF  pawn black-piece ENDOF
      [CHAR] b OF  bishop black-piece ENDOF
      [CHAR] n OF  knight black-piece ENDOF
      [CHAR] r OF  rook black-piece ENDOF
      [CHAR] q OF  queen black-piece ENDOF
      [CHAR] k OF  king black-piece ENDOF
      DROP FALSE EXIT
   ENDCASE
   TRUE ;
: square-white?  ( x y -- flag )  XOR 1 AND ;


