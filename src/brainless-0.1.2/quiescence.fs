\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Generate quiescence moves

: useful-capture?  ( square -- flag ) \ return whether capture is non-futil
   board @ piece-mask AND piece-weights @ curr-eval +   \ estimate evaluation
   alpha 128 - > ;					\ and check for cutoff
: (generate-quiescence-move-to)  ( to class -- )
   OVER empty? 0= IF
      OVER useful-capture? IF  (generate-move-to) EXIT THEN
   THEN
   DUP #move-trans-queen =  OVER #move-trans-knight = OR IF
      (generate-move-to) EXIT
   THEN
   OVER opponent-king-square move-gen-piece piece-would-threaten? IF
      (generate-move-to) EXIT
   THEN
   move-gen-from might-cause-opponent-check? IF
      2DUP move-gen-from SWAP 0 checking-move? IF
	 (generate-move-to) EXIT
      THEN
   THEN
   2DROP ;
: (generate-quiesc-capture-to)  ( to class -- )
   OVER useful-capture? IF  (generate-move-to) EXIT THEN
   2DROP ;
: append-quiescence-moves  ( -- )
   ['] (generate-quiescence-move-to) IS generate-move-to
   ['] (generate-moves-from) IS generate-moves-from
   (generate-moves) ;
: append-quiescence-captures  ( -- )
   ['] (generate-quiesc-capture-to) IS generate-move-to
   ['] (generate-strike-moves-from) IS generate-moves-from
   (generate-moves) ;
: generate-quiescence-moves  ( -- )  new-moves append-quiescence-moves ;
: generate-quiescence-captures  ( -- )  new-moves append-quiescence-captures ;
