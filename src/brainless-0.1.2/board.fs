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
	s" add-board-hook" print-def
   #board-hooks board-hooks !
   #board-hooks 1+ TO #board-hooks ;
: update-board  ( xt -- )
	s" update-board" print-def
   #board-hooks 0 ?DO
      I board-hooks @ EXECUTE
   LOOP ;

: king-square  ( -- square )
	s" king-square" print-def
   white? IF white-king-square ELSE black-king-square THEN ;
: opponent-king-square  ( -- square )
	s" opponent-king-square" print-def
   white? IF black-king-square ELSE white-king-square THEN ;
: position-king  ( square -- )
	s" position-king" print-def
   white? IF  TO white-king-square ELSE  TO black-king-square THEN ;
: >square  ( x y -- square ) 	s" >square" print-def 2 + 10 *   + 1+ ;
: >xy  ( square -- x y ) 	s" >xy" print-def 1- 10 /MOD  2 - ;
: xy-board!  ( piece x y -- ) 	s" xy-board!" print-def  >square board ! ;
: xy-board@  ( x y -- ) 	s" xy-board@" print-def  >square board @ ;
: >delta-xy  ( square1 square2 -- dx dy )
	s" >delta-xy" print-def
   \ return vector >square2-square1<
   10 /MOD  ROT 10 /MOD  SWAP >R -  SWAP R> -  SWAP ;
: bishop-xy?  ( dx dy -- flag ) 	s" bishop-xy?" print-def ABS SWAP ABS = ;
: rook-xy?  ( dx dy -- flag ) 	s" rook-xy?" print-def 0= SWAP 0= XOR ;
: bishop-direction?  ( square1 square2 -- flag ) 	s" bishop-direction?" print-def >delta-xy bishop-xy? ;
: rook-direction?  ( square1 square2 -- flag ) 	s" rook-direction?" print-def >delta-xy rook-xy? ;
create-array x-direction-table
   -1 , -1 , -1 , -1 , -1 , -1 , -1 , 0 , 1 , 1 , 1 , 1 , 1 , 1 , 1 , 
create-array y-direction-table
   -10 , -10 , -10 , -10 , -10 , -10 , -10 , 0 ,
   10 , 10 , 10 , 10 , 10 , 10 , 10 , 
: delta-xy>direction  ( dx dy -- direction )
	s" delta-xy>direction" print-def
   \ convert vector into single-step increment >direction<
   7 + y-direction-table @  SWAP 7 + x-direction-table @ + ;
: >direction  ( square1 square2 -- direction)
	s" >direction" print-def
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
	s" other-party" print-def
   opponent my-piece TO opponent TO my-piece
   white? 0= TO white?
   pawn-direction NEGATE TO pawn-direction ;
: set-party  ( white? -- )
	s" set-party" print-def
   0= white? = IF other-party THEN ;

: squares:  ( -- )
	s" squares:" print-def
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
	s" init-kings" print-def
   100 20 DO
      I board @  DUP piece-mask AND king = IF
	 f-white AND
	 IF  I TO white-king-square   ELSE   I TO black-king-square THEN
      ELSE DROP THEN
   LOOP ;
' init-kings add-board-hook
: place  ( piece square -- ) 	s" place" print-def SWAP f-piece OR  SWAP board ! ;
: place-black 	s" place-black" print-def place ;
: place-white  ( piece field -- ) 	s" place-white" print-def SWAP f-white OR   SWAP place ;
: place-b&w  ( piece field -- )  \ symmetrically place black and white pieces
	s" place-b&w" print-def
   2DUP place-white   >xy 7 SWAP - >square place-black ;
: unmoved  ( piece1 -- piece2 ) 	s" unmoved" print-def f-unmoved OR ;
: moved  ( piece1 -- piece2 ) 	s" moved" print-def [ f-unmoved INVERT ] LITERAL AND ;
: castled  ( piece1 -- piece2 ) 	s" castled" print-def f-castled OR ;
: white-piece  ( piece1 -- piece2 ) 	s" white-piece" print-def f-piece OR f-white OR ;
: black-piece  ( piece1 -- piece2 ) 	s" black-piece" print-def f-piece OR ;

: init-board  ( -- )
	s" init-board" print-def
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
	s" empty-board" print-def
   8 0 DO  8 0 DO  empty-square I J xy-board! LOOP LOOP
   update-board ;
: initial-square?  ( square -- flag )
	s" initial-square?" print-def
   DUP board @ full-piece-mask AND
   SWAP initial-board @ full-piece-mask AND = ;
   
: empty?  ( square -- flag ) 	s" empty?" print-def board @ 0= ;
: border?  ( square -- flag ) 	s" border?" print-def  board @ border = ;
: opponent?  ( square -- flag ) 	s" opponent?" print-def board @  color-piece-mask AND opponent = ;
: piece?  ( square -- flag ) 	s" piece?" print-def board @  f-piece AND 0<> ;
: my-piece?  ( square -- flag ) 	s" my-piece?" print-def board @  color-piece-mask AND my-piece = ;
: unmoved?  ( square -- flag ) 	s" unmoved?" print-def board @ f-unmoved AND 0<> ;
: pawn?  ( square -- flag ) 	s" pawn?" print-def board @ piece-mask AND pawn = ;

: ?direction  ( n -- n|-n ) 	s" ?direction" print-def pawn-direction * ;
: pawn-trans?  ( square -- ) \ pawn transformation possible at field?
	s" pawn-trans?" print-def
   DUP a1 [ h1 1+ ] LITERAL WITHIN
   SWAP a8 [ h8 1+ ] LITERAL WITHIN OR ;

: remove-piece  ( square -- ) 	s" remove-piece" print-def board 0 SWAP ! ;
: get-piece-masked  ( square -- piece ) 	s" get-piece-masked" print-def board @  full-piece-mask AND ;
: take-piece  ( square -- piece )
	s" take-piece" print-def
   board DUP @   [ f-unmoved INVERT ] LITERAL AND
   0 ROT ! ;
: put-piece  ( piece square -- ) 	s" put-piece" print-def board ! ;

: my-pieces  ( -- ) \ set up my-pawn ... my-king
	s" my-pieces" print-def
   my-piece f-white AND
   pawn OVER OR TO my-pawn
   knight OVER OR TO my-knight
   bishop OVER OR TO my-bishop
   rook OVER OR TO my-rook
   queen OVER OR TO my-queen
   king OR TO my-king ;
: opponent-pieces  ( -- ) \ set up opponent-pawn ... opponent-king
	s" opponent-pieces" print-def
   opponent f-white AND
   pawn OVER OR TO opponent-pawn
   knight OVER OR TO opponent-knight
   bishop OVER OR TO opponent-bishop
   rook OVER OR TO opponent-rook
   queen OVER OR TO opponent-queen
   king OR TO opponent-king ;

: count-my-non-pawn-pieces  ( -- u )
	s" count-my-non-pawn-pieces" print-def
   0   100 20 DO
      I my-piece? IF
	 I board @ piece-mask AND knight king 1+ WITHIN -
      THEN
   LOOP ;

: .square  ( square -- ) \ print square (eg. "e1")
	s" .square" print-def
   >xy  SWAP [CHAR] a + EMIT   [CHAR] 1 + EMIT ;
: piece>char  ( piece -- char )
	s" piece>char" print-def
   f-white OVER AND IF   S"  PNBRQK" ELSE  S"  pnbrqk" THEN DROP
   SWAP piece-mask AND   CHARS + C@ ;
: char>piece  ( char -- piece true | false )
	s" char>piece" print-def
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
: square-white?  ( x y -- flag ) 	s" square-white?" print-def XOR 1 AND ;


