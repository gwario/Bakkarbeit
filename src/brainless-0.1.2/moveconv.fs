\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Convert moves to arithmetic/SAN notation

0 VALUE use-arith-notation?

\
\ Convert moves to arithmetic notation
\
: write-check-state  ( move-index -- )
	s" write-check-state" print-def
   get-move do-move-undo-info
   moves-exist? 0= IF
      check? IF [CHAR] # ELSE [CHAR] * THEN  write-char
   ELSE
      check? IF [CHAR] + write-char THEN
   THEN  undo-move ;
: write-pawn-trans  ( move-index -- )
	s" write-pawn-trans" print-def
   get-move-class CASE
      #move-trans-knight OF S" =N" write-string ENDOF
      #move-trans-queen  OF S" =Q" write-string ENDOF
   ENDCASE ;
: write-square-separator  ( move-index -- )
	s" write-square-separator" print-def
   capture-move? IF [CHAR] x ELSE [CHAR] - THEN write-char ;
: append-move>arith  ( move-index -- )
	s" append-move>arith" print-def
   DUP get-orig write-square
   DUP write-square-separator
   DUP get-target write-square
   DUP write-pawn-trans
   write-check-state ;
: move>arith  ( c-addr u move-index -- u )
	s" move>arith" print-def
   -ROT is-string  append-move>arith
   #characters  previous-string ;

\
\ Convert moves to SAN notation
\
: same-piece&target?  ( move-index1 move-index2 -- )
	s" same-piece&target?" print-def
   2DUP get-orig get-piece-masked  SWAP get-orig get-piece-masked =
   ROT get-target  ROT get-target = AND ;
: same-rank?  ( move-index1 move-index2 -- )
	s" same-rank?" print-def
   get-orig >xy NIP  SWAP get-orig >xy NIP = ;
: same-file?  ( move-index1 move-index2 -- )
	s" same-file?" print-def
   get-orig >xy DROP  SWAP get-orig >xy DROP = ;
: unique-target?  ( move-index -- )
	s" unique-target?" print-def
   #moves 0 ?DO
      DUP I <>  OVER I same-piece&target? AND IF  DROP FALSE UNLOOP EXIT THEN
   LOOP  DROP TRUE ;
: unique-rank?  ( move-index -- )
	s" unique-rank?" print-def
   #moves 0 ?DO
      DUP I <>  OVER I same-piece&target? AND  OVER I same-rank? AND IF
	 DROP FALSE UNLOOP EXIT
      THEN
   LOOP  DROP TRUE ;
: unique-file?  ( move-index -- )
	s" unique-file?" print-def
   #moves 0 ?DO
      DUP I <>  OVER I same-piece&target? AND  OVER I same-file? AND IF
	 DROP FALSE UNLOOP EXIT
      THEN
   LOOP  DROP TRUE ;
: write-unique-orig  ( move-index -- )
	s" write-unique-orig" print-def
   DUP get-orig
   OVER unique-file? IF  NIP write-square-file EXIT THEN
   OVER unique-rank? IF  NIP write-square-rank EXIT THEN
   NIP write-square ;
: write-moving-piece  ( move-index -- )
	s" write-moving-piece" print-def
   get-orig board @ piece-mask AND
   CHARS S"  PNBRQK" DROP +  C@ write-char ;
: write-pawn-move-san  ( move-index -- )
	s" write-pawn-move-san" print-def
   DUP capture-move? IF
      DUP get-orig write-square-file
      [CHAR] x write-char
   THEN
   DUP get-target write-square
   write-pawn-trans ;
: write-castle-move-san  ( move-index -- )
	s" write-castle-move-san" print-def
   get-move-class CASE
      #move-castle-near OF  S" O-O" write-string ENDOF
      #move-castle-far  OF  S" O-O-O" write-string ENDOF
   ENDCASE ;
: write-piece-move-san  ( move-index -- ) \ output non-pawn SAN moves
	s" write-piece-move-san" print-def
   DUP write-moving-piece
   DUP unique-target? 0= IF
      DUP write-unique-orig
   THEN
   DUP capture-move? IF [CHAR] x write-char THEN
   get-target write-square ;
: append-move>san  ( move-index -- )
	s" append-move>san" print-def
   DUP castle-move? IF
      write-castle-move-san
   ELSE
      DUP get-orig pawn? IF
	 DUP write-pawn-move-san
      ELSE DUP write-piece-move-san THEN
      write-check-state
   THEN ;
: move>san  ( c-addr u move-index -- u )
	s" move>san" print-def
   -ROT is-string  append-move>san
   #characters  previous-string ;

\
\ Convert, depending on >use-arith-notation?<
\
: append-move>string  ( move-index -- u )
	s" append-move>string" print-def
   use-arith-notation? IF append-move>arith ELSE append-move>san THEN ;
: move>string  ( c-addr u move-index -- u )
	s" move>string" print-def
   use-arith-notation? IF move>arith ELSE move>san THEN ;
: display-move  ( move-index -- )
	s" display-move" print-def
   PAD #PAD ROT move>string   PAD SWAP TYPE ;
   

