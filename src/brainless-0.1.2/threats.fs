\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Check threats (most important for checking check condition)

\
\ Check whether a specified square is attacked
\
: opponent-pawn?  ( square -- flag ) 	s" opponent-pawn?" print-def get-piece-masked opponent-pawn = ;
: opponent-knight?  ( square -- flag ) 	s" opponent-knight?" print-def get-piece-masked opponent-knight = ;
: opponent-king?  ( square -- flag ) 	s" opponent-king?" print-def get-piece-masked opponent-king = ;
: opponent-bishop?  ( square direction -- flag )
	s" opponent-bishop?" print-def
   CELLS SWAP board                         ( S: direction-cells square-addr )
   BEGIN OVER + DUP @ ?DUP UNTIL   NIP NIP  ( S: piece )
   full-piece-mask AND DUP opponent-bishop =  SWAP opponent-queen = OR ;
: opponent-rook?  ( square direction -- flag )
	s" opponent-rook?" print-def
   CELLS SWAP board                         ( S: direction-cells square-addr )
   BEGIN OVER + DUP @ ?DUP UNTIL   NIP NIP  ( S: piece )
   full-piece-mask AND DUP opponent-rook =  SWAP opponent-queen = OR ;

: threatened-by-pawn?  ( square -- flag )
	s" threatened-by-pawn?" print-def
   DUP 9 ?direction + opponent-pawn?
   SWAP 11 ?direction + opponent-pawn? OR ;
: threatened-by-knight?  ( square -- flag )
	s" threatened-by-knight?" print-def
   DUP  21 - opponent-knight?      OVER 19 - opponent-knight? OR
   OVER 12 - opponent-knight? OR   OVER  8 - opponent-knight? OR
   OVER  8 + opponent-knight? OR   OVER 12 + opponent-knight? OR
   OVER 19 + opponent-knight? OR   SWAP 21 + opponent-knight? OR ;
: threatened-by-bishop?  ( square -- flag )
	s" threatened-by-bishop?" print-def
   DUP  -11 opponent-bishop?     OVER  -9 opponent-bishop? OR
   OVER   9 opponent-bishop? OR  SWAP  11 opponent-bishop? OR ;
: threatened-by-rook?  ( square -- flag )
	s" threatened-by-rook?" print-def
   DUP  -10 opponent-rook?     OVER   -1 opponent-rook? OR
   OVER   1 opponent-rook? OR  SWAP   10 opponent-rook? OR ;
: threatened-by-king?  ( square -- flag )
	s" threatened-by-king?" print-def
   opponent-king-square - ABS  DUP 9 12 WITHIN  SWAP 1 = OR ; 
(  DUP  11 - opponent-king?     OVER 10 - opponent-king? OR
   OVER  9 - opponent-king? OR  OVER  1 - opponent-king? OR
   OVER  1 + opponent-king? OR  OVER  9 + opponent-king? OR
   OVER 10 + opponent-king? OR  SWAP 11 + opponent-king? OR ; )
: threatened-by-piece?  ( dst-square -- flag )
	s" threatened-by-piece?" print-def
   DUP threatened-by-pawn?   ?DUP IF NIP EXIT THEN
   DUP threatened-by-knight? ?DUP IF NIP EXIT THEN
   DUP threatened-by-bishop? ?DUP IF NIP EXIT THEN
   DUP threatened-by-rook?   ?DUP IF NIP EXIT THEN
   threatened-by-king? ;
: threatened-by-weak-piece?  ( dst-square -- flag )
	s" threatened-by-weak-piece?" print-def
   DUP threatened-by-pawn?
   OVER threatened-by-knight? OR
   SWAP threatened-by-bishop? OR ;
: threatened-by-opponent?  ( dst-square -- flag )
	s" threatened-by-opponent?" print-def
   opponent-pieces  threatened-by-piece? ;
: threatened-by-weak-opponent?  ( dst-square -- flag )
	s" threatened-by-weak-opponent?" print-def
   opponent-pieces  threatened-by-weak-piece? ;
: threatened-by-opponent-pawn?  ( dst-square -- flag )
	s" threatened-by-opponent-pawn?" print-def
   opponent-pieces  threatened-by-pawn? ;
: check?  ( -- flag ) 	s" check?" print-def king-square threatened-by-opponent? ;
: other-check?  ( -- flag ) 	s" other-check?" print-def other-party check? other-party ;

\
\ Check whether a piece on a specified square attacks a piece on another
\ specified square
\
0 VALUE threatening-square

: white-pawn-threatens?  ( square -- flag )
	s" white-pawn-threatens?" print-def
   threatening-square - DUP 11 = SWAP 9 = OR ;
: black-pawn-threatens?  ( square -- flag )
	s" black-pawn-threatens?" print-def
   threatening-square - DUP -11 = SWAP -9 = OR ;
: knight-threatens?  ( square -- flag )
	s" knight-threatens?" print-def
   threatening-square - ABS
   DUP 8 =   OVER 19 = OR   OVER 12 = OR   SWAP 21 = OR ;
: threatened-from-direction?  ( square direction -- flag )
	s" threatened-from-direction?" print-def
   SWAP
   threatening-square board @ IF
      BEGIN OVER + DUP board @ UNTIL
   ELSE
      BEGIN OVER + DUP threatening-square =  OVER board @ OR UNTIL
   THEN  NIP threatening-square = ;
: bishop-threatens?  ( square -- flag )
	s" bishop-threatens?" print-def
   DUP threatening-square >delta-xy 2DUP bishop-xy? IF
      delta-xy>direction threatened-from-direction? EXIT
   THEN 2DROP DROP FALSE ;
: rook-threatens?  ( square -- flag )
	s" rook-threatens?" print-def
   DUP threatening-square >delta-xy 2DUP rook-xy? IF
      delta-xy>direction threatened-from-direction? EXIT
   THEN 2DROP DROP FALSE ;
: queen-threatens?  ( square -- flag )
	s" queen-threatens?" print-def
   DUP threatening-square >delta-xy 2DUP bishop-xy? IF
      delta-xy>direction threatened-from-direction? EXIT
   THEN
   2DUP rook-xy? IF
      delta-xy>direction threatened-from-direction? EXIT
   THEN 2DROP DROP FALSE ;
: king-threatens?  ( square -- flag )
	s" king-threatens?" print-def
   threatening-square - ABS  DUP 9 12 WITHIN  SWAP 1 = OR ;
: nobody-threatens?  ( square -- flag ) 	s" nobody-threatens?" print-def DROP FALSE ;
   
vector-table: (piece-threatens?)  ( square piece -- flag )
   ' nobody-threatens? ,
   ' black-pawn-threatens? ,	' knight-threatens? ,
   ' bishop-threatens? ,	' rook-threatens? ,
   ' queen-threatens? ,		' king-threatens? ,
   ' nobody-threatens? ,	' nobody-threatens? ,
   ' white-pawn-threatens? ,	' knight-threatens? ,
   ' bishop-threatens? ,	' rook-threatens? ,
   ' queen-threatens? ,		' king-threatens? ,
   ' nobody-threatens? ,

: piece-would-threaten?  ( from to piece -- flag )
	s" piece-would-threaten?" print-def
   ROT TO threatening-square  full-piece-mask AND (piece-threatens?) ;
: threatens?  ( from to -- flag )
	s" threatens?" print-def
   SWAP DUP TO threatening-square get-piece-masked (piece-threatens?) ;

\
\ The following routines are used for determining whether a move requires to be
\ checked for putting ones king into check. It also helps to update the check
\ state in >curr-check?< more quickly.
\

: piece-threatening-through  ( square direction -- piece )
	s" piece-threatening-through" print-def
   \ return the piece that might threaten >square< through >threatening-square<
   SWAP
   BEGIN  OVER +  DUP threatening-square =  OVER board @  OR UNTIL
   threatening-square = IF
      threatening-square
      BEGIN OVER + DUP board @ ?DUP UNTIL  NIP NIP
   ELSE DROP empty-square THEN ;
: bishop-threatens-through?  ( square direction -- flag ) \ also handles queens
	s" bishop-threatens-through?" print-def
   piece-threatening-through
   DUP color-piece-mask AND opponent = IF
      piece-mask AND DUP queen = SWAP bishop = OR
   ELSE DROP FALSE THEN ;
: rook-threatens-through?  ( square -- flag ) \ also handles queens
	s" rook-threatens-through?" print-def
   piece-threatening-through
   DUP color-piece-mask AND opponent = IF
      piece-mask AND DUP queen = SWAP rook = OR
   ELSE DROP FALSE THEN ;
: queenlike-threatens-through?  ( square -- flag )
	s" queenlike-threatens-through?" print-def
   \ return whether square is threatened through >threatening-square< by a
   \ queen, bishop or rook
   DUP threatening-square >delta-xy
   2DUP bishop-xy? IF
      delta-xy>direction bishop-threatens-through?
   ELSE 2DUP rook-xy? IF
      delta-xy>direction rook-threatens-through?
   ELSE 2DROP DROP FALSE THEN THEN ;

\ used during quiescence move generation to generate checking moves
: might-cause-opponent-check?  ( square -- flag )
	s" might-cause-opponent-check?" print-def
   TO threatening-square opponent-king-square queenlike-threatens-through? ;

\ This routine is for use if the king isn't check already
: might-cause-check?  ( square -- flag ) \ for piece take/remove operations
	s" might-cause-check?" print-def
   TO threatening-square king-square queenlike-threatens-through? ;

\ This routine is for use if the king is already check
: might-block-check?  ( square -- flag ) \ for piece put operations
	s" might-block-check?" print-def
   TO threatening-square king-square queenlike-threatens-through? ;











