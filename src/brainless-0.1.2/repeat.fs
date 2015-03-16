\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Repetition detection for detecting draws

\
\ This uses a simple closed hashing algorithm
\
256 CONSTANT position-hash-size
7   CONSTANT position-hash-probe
0   VALUE #hashed-positions
position-hash-size 1- CONSTANT position-hash-mask

position-hash-size hash-array position-hashs

: clear-position-hashs  ( -- )
   0 position-hashs  position-hash-size /hash * ERASE ;

' clear-position-hashs add-board-hook

: remember-position  ( -- )
   hash @ 
   BEGIN		
      position-hash-mask AND		( S: #entry )
      DUP position-hashs DUP hash@ zero-hash? IF
	 #hashed-positions 1+ TO #hashed-positions
	 >R hash hash@ R> hash!   DROP EXIT
      THEN DROP   position-hash-probe +
   AGAIN ;
0 VALUE most-recent-match
: forget-position  ( -- )
   0 TO most-recent-match
   hash @ 
   BEGIN
      position-hash-mask AND		( S: #entry )
      DUP position-hashs DUP hash@ hash hash@ hash= IF
	 DUP TO most-recent-match
      THEN  SWAP position-hash-probe + SWAP
      hash@ zero-hash?
   UNTIL
   DROP most-recent-match ?DUP IF
      /hash ERASE   #hashed-positions 1- TO #hashed-positions
   THEN ;
: remembering-position?  ( -- )
   hash @
   BEGIN		
      position-hash-mask AND		( S: #entry )
      DUP position-hashs DUP hash@ hash hash@ hash= IF
	 2DROP TRUE EXIT
      THEN
      hash@ zero-hash? IF
	 DROP FALSE EXIT
      THEN  position-hash-probe +
   AGAIN ;

      