\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Killer and principal variation heuristic stuff

3 CELLS CONSTANT /killer
: killer!  ( to from class a-addr -- )  TUCK !  1 CELLS + 2! ;
: killer@  ( a-addr -- to from class )  DUP  1 CELLS + 2@  ROT @ ;
: killer-squares  ( a-addr -- from to )  1 CELLS + 2@ SWAP ;
: killer=  ( killer1 killer2 -- flag )  -ROT 2>R = -ROT 2R> D= AND ;
: killer-captures?  ( a-addr -- )  2 CELLS + @ opponent? ;
: no-killer  ( -- 0 0 0 )  0 0 0 ;
: kdup  ( killer -- killer killer )  DUP 2OVER ROT ;
: kdrop  ( killer -- )  2DROP DROP ;

\
\ killer history -- assigns kill counts to move from-to pairs
\
2 100 100 * * CELLS CONSTANT /killer-history
CREATE killer-history /killer-history ALLOT
0 VALUE #killers

: history-entry  ( from to -- )
   DUP empty? 100 AND +
   100 * + CELLS   killer-history + ;
: adjust-killer-history  ( from to -- )
   history-entry 1 SWAP +!  #killers 1+ TO #killers ;
: kill-count  ( from to -- count )  history-entry @ ;
: get-kill-count  ( move-index -- count )  get-move-squares history-entry @ ;
: get-rel-kill-count  ( move-index -- 0..1000 )
   #killers IF
      get-kill-count 1000 #killers */
   ELSE DROP 0 THEN ;
: clear-killer-history  ( -- )
   0 TO #killers   killer-history /killer-history ERASE ;

\
\ principal variation -- pv moves are stored excactly, as to-from-class triples
\
3 CELLS CONSTANT /pv-move	\ pv moves are stored as >to from class<

\ principal variation is an array of principal variations for each search
\ depth.
max-think-depth DUP * /pv-move * CONSTANT /principal-variation
CREATE principal-variation  /principal-variation ALLOT

CREATE saved-best-move /pv-move ALLOT	\ saved move during iterative deepening

: root-principal-move  ( -- a-addr ) \ return address of principal move at root
   think-depth /pv-move * principal-variation + ;
: principal-move  ( -- a-addr ) \ return address of current principal move
   think-depth DUP max-think-depth * +  /pv-move *  principal-variation + ;
: append-principal-variation  ( -- )
   \ append next search depth' principal variation to current depth' variation
   +depth principal-move 
   -depth principal-move /pv-move +
   max-think-depth think-depth - 1- /pv-move * MOVE ;
: is-principal-move  ( move-index -- )
   get-move-noeval principal-move killer!   append-principal-variation ;
: terminate-principal-variation  ( move-index -- )
   principal-move /pv-move ERASE ;
: is-principal-move?  ( move-index -- )
   get-move-noeval >R  root-principal-move killer@ >R D=   2R> = AND ;
: ?on-principal-variation  ( move-index -- )
   on-principal-variation? IF
      is-principal-move? TO on-principal-variation?
   ELSE DROP THEN ;
: best-move  ( -- from to class )
   principal-variation killer@ ;
: save-best-move  ( -- )  principal-variation killer@ saved-best-move killer! ;
: get-saved-best-move  ( -- )  saved-best-move killer@ ;
: get-best-move  ( -- to from class eval )
   best-move 0 mdup eval-move NIP ;
: clear-principal-variation  ( -- )
   principal-variation /principal-variation ERASE ;

\ displaying the principal variation as a string
: write-principal-variation  ( -- )
   root-principal-move 1 CELLS + @ IF
      generate-moves root-principal-move killer@ find-move-x IF
	 DUP append-move>string BL write-char
	 get-move do-move-undo-info
	 +depth RECURSE -depth undo-move 
      ELSE [CHAR] ? write-char THEN forget-moves
   ELSE [CHAR] ; write-char THEN ;
: append-pv>string  ( c-addr u1 -- u2 )
   think-depth >R  0 TO think-depth
   write-principal-variation
   R> TO think-depth ;
: principal-variation>string  ( c-addr u1 -- u2 )
   is-string append-pv>string #characters previous-string ;
: print-principal-variation  ( -- )
   PAD DUP #PAD principal-variation>string TYPE ;

\
\ Killer heuristic remembering two killers per think depth and single killers
\ associated with their preceding moves.
\

max-think-depth 2 * /killer *     CONSTANT /simple-killers
2 100 100 * * /killer * CONSTANT /associated-killers
max-think-depth /killer *     CONSTANT /fast-killers
CREATE simple-killers      /simple-killers ALLOT
\ CREATE associated-killers  /associated-killers ALLOT
CREATE associated-captures /associated-killers ALLOT
CREATE fast-killers        /fast-killers ALLOT

: simple-killer  ( -- a-addr )  think-depth 2 * /killer * simple-killers + ;
: 2nd-simple-killer  ( -- a-addr )  simple-killer /killer + ;
: swap-simple-killers  ( -- )
   simple-killer DUP >R killer@  R@ /killer + killer@
   R@ killer!  R> /killer + killer! ;
: is-simple-killer  ( to from class -- )
   kdup simple-killer killer@ killer= IF 3DROP EXIT THEN
   swap-simple-killers simple-killer killer! ;
\ : associated-killer  ( -- a-addr )
\   100 100 * white? AND
\   opponent-move-orig 100 * +
\   opponent-move-target +  /killer *
\   associated-killers + ;
: associated-capture  ( -- a-addr )
   100 100 * white? AND
   opponent-move-orig 100 * +
   opponent-move-target +  /killer *
   associated-captures + ;
: is-associated-killer  ( to from class -- )
   2>R DUP opponent? 2R> ROT IF
      kdup associated-capture killer!
   THEN kdrop ;
\   disbled..
\   think-depth think-limit <   curr-check? 0= AND IF  
\      associated-killer killer!  \ normal killers are futil during quiescence
\   ELSE kdrop THEN 
: fast-killer  ( -- a-addr )  think-depth /killer * fast-killers + ;
: is-fast-killer  ( to from class -- )
   kdup is-simple-killer   is-associated-killer ;
: generate-fast-killer  ( -- )
   no-killer fast-killer killer!
   on-principal-variation? IF
      principal-move killer@ kdup generate-single-move-x #moves IF
	 fast-killer killer! EXIT
      THEN kdrop forget-moves
   THEN
   tt-get-bestmove generate-single-move #moves IF
      0 get-move-noeval fast-killer killer! EXIT
   THEN forget-moves
   opponent-move-target DUP opponent? IF
      DUP threatened-by-opponent-pawn? 0= IF
	 generate-cheapest-move-to #moves IF
	    0 get-move-noeval fast-killer killer! EXIT
	 THEN forget-moves
      ELSE DROP THEN
   ELSE DROP THEN
   simple-killer killer@ kdup generate-single-move-x #moves IF
      fast-killer killer!  EXIT
   THEN kdrop forget-moves
   2nd-simple-killer killer@ kdup generate-single-move-x #moves IF
      swap-simple-killers fast-killer killer!  EXIT
   THEN kdrop forget-moves
   associated-capture killer@ kdup generate-single-move-x #moves IF
      fast-killer killer!  EXIT
   THEN kdrop forget-moves
   ( return empty move list) new-moves ;
: generate-fast-strike-killer  ( -- ) \ only generate capturing killers
   no-killer fast-killer killer!
   on-principal-variation? IF
      principal-move killer-captures? IF
	 principal-move killer@ kdup generate-single-move-x #moves IF
	    fast-killer killer! EXIT
	 THEN kdrop forget-moves
      THEN
   THEN
(   opponent-move-target DUP opponent?  IF
      DUP board @ piece-mask AND rook < 0= IF
	 generate-cheapest-weak-move-to  #moves IF
	    0 get-move-noeval fast-killer killer!  EXIT
	 THEN forget-moves
      ELSE DROP THEN
   ELSE DROP THEN )
   associated-capture killer-captures? IF
      associated-capture killer@ kdup generate-single-move-x #moves IF
	 fast-killer killer!  EXIT
      THEN kdrop forget-moves
   THEN
   simple-killer killer-captures? IF
      simple-killer killer@ kdup generate-single-move-x #moves IF
	 fast-killer killer!  EXIT
      THEN kdrop forget-moves
   THEN
   ( return empty move list) new-moves ;
: delete-fast-killer  ( -- )
   fast-killer killer@ find-move-x IF delete-move THEN ;
: clear-associated-killers  ( -- )
\   associated-killers /associated-killers ERASE
   associated-captures /associated-killers ERASE ;
: clear-simple-killers  ( -- )
   simple-killers /simple-killers ERASE ;

: is-killer  ( move-index -- )
   DUP get-move-noeval is-fast-killer
   get-move-squares adjust-killer-history ;

: init-killers  ( -- ) \ reset for new move computation
   clear-killer-history \ clear-principal-variation
   clear-associated-killers ;

clear-simple-killers clear-associated-killers clear-killer-history
clear-principal-variation





