\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Null move heuristic

3 VALUE null-move-threshold	\ null move heuristic will be enabled for a
                                \ party if it has more non-pawn pieces

0 VALUE white-null-moves?	\ set by ?null-moves on tree root
0 VALUE black-null-moves?

: use-null-moves?  ( -- flag )
   white? IF white-null-moves? ELSE black-null-moves? THEN ;
: to-null-moves?  ( flag -- )
   white? IF TO white-null-moves? ELSE TO black-null-moves? THEN ;
: decide-null-moves?  ( -- flag )
   count-my-non-pawn-pieces null-move-threshold > ;
: ?use-null-moves  ( -- )
   decide-null-moves? to-null-moves? other-party
   decide-null-moves? to-null-moves? other-party ;
: null-move?  ( -- flag )
   use-null-moves? IF
      think-depth 1 >  check? 0= AND
   ELSE FALSE THEN ;
