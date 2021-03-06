\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Move handling

\
\ Warning: moves must be evaluated via >eval-moves< before they can be
\ executed, else fly-eval won't work! (I use to forget this)
\

DEFER eval-move  ( to from class x -- eval )
DEFER eval-moves  ( -- )	\ evaluate moves in current move list

-32000 CONSTANT -infinity	\ evaluation constants
 32000 CONSTANT +infinity
 32767 CONSTANT undefined
-31900 CONSTANT check-mate
 0     VALUE    stale-mate

0 VALUE curr-abs-eval		\ current absolute evaluation (set by do-move)
0 VALUE curr-check?		\ is current party in check?
0 VALUE black-move-target	\ target of most recent move of black
0 VALUE white-move-target	\ target of most recent move of white
0 VALUE black-move-orig
0 VALUE white-move-orig
0 VALUE move-list		\ (currently used) list of moves
0 VALUE #moves			\ number of moves in move-list
0 VALUE moves-evaluated?	\ are moves in current list evaluated?

\ classes of moves
 0 CONSTANT #move-normal
 1 CONSTANT #move-strike
 2 CONSTANT #move-strike-ep	\ strike en passante
 3 CONSTANT #move-pawn-far	\ pawn that moves 2 sq. (from org. position)
 4 CONSTANT #move-trans-knight	\ pawn transformation to knight
 5 CONSTANT #move-trans-queen	\ pawn transformation to queen
 6 CONSTANT #move-king
 7 CONSTANT #move-king-strike
 8 CONSTANT #move-castle-near	\ eg. Ke1-g1
 9 CONSTANT #move-castle-far    \ eg. Ke1-c1
10 CONSTANT #move-null		\ "pass" (used by null move heuristic)

record		\ move data layout
   1 CELLS offset move-eval
   1 CELLS offset move-class
   1 CELLS offset move-from
   1 CELLS offset move-to
end-record /move

: set-move-vars  ( -- )
	s" set-move-vars" print-def
   \ update all variables that are usually updated by >do-move< & friends
   check? TO curr-check?
   0 TO black-move-orig 0 TO black-move-target
   0 TO white-move-orig 0 TO white-move-target ;
' set-move-vars add-board-hook 

: mdup  ( to from class eval -- to from class eval to from class eval )
	s" mdup" print-def
   2OVER 2OVER ;
: mdrop  ( to from class eval -- )
	s" mdrop" print-def
   2DROP 2DROP ;
: my-eval  ( eval -- eval|-eval ) 	s" my-eval" print-def white? 0= IF NEGATE THEN ;
: abs-eval  ( eval -- eval|-eval ) 	s" abs-eval" print-def white? 0= IF NEGATE THEN ;
: opponent-eval  ( eval -- eval|-eval ) 	s" opponent-eval" print-def white? IF NEGATE THEN ;
: curr-eval  ( -- eval ) 	s" curr-eval" print-def curr-abs-eval my-eval ;
: move-target  ( -- square )
	s" move-target" print-def
   white? IF white-move-target ELSE black-move-target THEN ;
: opponent-move-target  ( -- square )
	s" opponent-move-target" print-def
   white? IF black-move-target ELSE white-move-target THEN ;
: to-move-target  ( square -- )
	s" to-move-target" print-def
   white? IF TO white-move-target ELSE TO black-move-target THEN ;
: move-orig  ( -- square )
	s" move-orig" print-def
   white? IF white-move-orig ELSE black-move-orig THEN ;
: opponent-move-orig  ( -- square )
	s" opponent-move-orig" print-def
   white? IF black-move-orig ELSE white-move-orig THEN ;
: to-move-orig  ( square -- )
	s" to-move-orig" print-def
   white? IF TO white-move-orig ELSE TO black-move-orig THEN ;
: move-squares  ( -- to from )
	s" move-squares" print-def
   white? IF white-move-target white-move-orig
   ELSE black-move-target black-move-orig THEN ;
: opponent-move-squares  ( -- to from )
	s" opponent-move-squares" print-def
   white? IF black-move-target black-move-orig
   ELSE white-move-target white-move-orig THEN ;
: to-move-squares  ( to from -- )
	s" to-move-squares" print-def
   white? IF TO white-move-orig TO white-move-target
   ELSE TO black-move-orig TO black-move-target THEN ;
: pack-squares  ( to from -- x ) 	s" pack-squares" print-def 7 LSHIFT OR ;
: unpack-squares  ( x -- to from ) 	s" unpack-squares" print-def DUP 127 AND  SWAP 7 RSHIFT ;

: moves  ( n1 -- n2 ) 	s" moves" print-def /move * ;
: move@  ( a-addr -- to from class eval ) 	s" move@" print-def DUP 2 CELLS + 2@   ROT 2@ ;
: move!  ( to from class eval a-addr -- ) 	s" move!" print-def DUP >R 2!   R> 2 CELLS + 2! ;
: move,  ( to from class eval -- ) 	s" move," print-def here 4 cells allot move! ;
: get-move  ( index -- to from class eval ) 	s" get-move" print-def moves move-list +  move@ ;
: set-move  ( to from class eval index ) 	s" set-move" print-def moves move-list +  move! ;
: get-eval  ( index -- eval ) 	s" get-eval" print-def moves move-list +   move-eval @ ;
: get-orig  ( index -- square ) 	s" get-orig" print-def moves move-list +   move-from  @ ;
: set-eval  ( eval index -- ) 	s" set-eval" print-def moves move-list +   move-eval ! ;
: add-move  ( to from class eval -- ) 	s" add-move" print-def move,   #moves 1+ TO #moves ;
: get-target  ( index -- to ) 	s" get-target" print-def moves move-list +   move-to @ ;
: get-move-class  ( index -- class ) 	s" get-move-class" print-def moves move-list +  move-class @ ;
: get-move-squares  ( index -- from to )
	s" get-move-squares" print-def
   moves move-list +  DUP move-from @  SWAP move-to @ ;
: get-move-noeval  ( index -- to from class )
	s" get-move-noeval" print-def
   moves move-list +  DUP move-from 2@  ROT move-class @ ;
: capture-move?  ( index -- flag )
	s" capture-move?" print-def
   DUP get-target empty? 0=  SWAP get-move-class #move-strike-ep = OR ;
: castle-move?  ( index -- flag )
	s" castle-move?" print-def
   get-move-class DUP  #move-castle-far =  SWAP #move-castle-near = OR ;
: swap-moves  ( index1 index2 -- )
	s" swap-moves" print-def
   moves move-list +  SWAP moves move-list +
   2DUP 2>R 2DUP 2>R   2@ ROT 2@   R> 2! R> 2!
   R> 2 CELLS +  R> 2 CELLS +
   2DUP 2>R   2@ ROT 2@   R> 2! R> 2! ;
: first-move  ( index -- ) \ make `index' the first move
	s" first-move" print-def
   ?DUP IF
      DUP >R get-move
      move-list DUP /move + R> moves MOVE
      move-list move!
   THEN ;
      
: new-moves  ( -- )
	s" new-moves" print-def
   moves-evaluated? , #moves , move-list ,
   0 TO #moves HERE TO move-list   0 TO moves-evaluated? ;
: forget-moves  ( -- )
	s" forget-moves" print-def
   move-list
   DUP 3 CELLS - @ TO moves-evaluated?  \ restore prior move list
   DUP 2 CELLS - @ TO #moves		
   DUP 1 CELLS - @ TO move-list
   HERE -  3 CELLS - ALLOT ;		\ free memory of current move list
: find-move  ( from to -- false | index true )
	s" find-move" print-def
   #moves 0 ?DO
      I get-move-squares 2OVER D= IF   2DROP I TRUE UNLOOP EXIT THEN   
   LOOP   2DROP FALSE ;
0 VALUE find-move-class
: find-move-x  ( to from class -- false | index true ) \ exact version
	s" find-move-x" print-def
   TO find-move-class SWAP
   #moves 0 ?DO
      I get-move-squares 2OVER D=  I get-move-class find-move-class = AND IF
	 2DROP I TRUE UNLOOP EXIT
      THEN
   LOOP   2DROP FALSE ;
: find-get-move  ( from to -- false | to from class eval true )
	s" find-get-move" print-def
   find-move IF get-move TRUE ELSE FALSE THEN ;
: delete-move  ( index -- )
	s" delete-move" print-def
   #moves 1- swap-moves  #moves 1- TO #moves ;
: sort-moves  ( -- )
	s" sort-moves" print-def
   #moves 2 < IF  EXIT THEN
   #moves 1- 0 ?DO
      -1 I get-eval   ( S: best-index best-eval )
      white? IF
	 #moves I 1+ ?DO  I get-eval 2DUP < IF NIP I SWAP ROT THEN DROP	 LOOP
      ELSE
	 #moves I 1+ ?DO  I get-eval 2DUP > IF NIP I SWAP ROT THEN DROP  LOOP
      THEN
      DROP DUP 0< 0= IF   I swap-moves ELSE DROP THEN
   LOOP ;
: ?eval-moves  ( -- )
	s" ?eval-moves" print-def
   moves-evaluated? 0= IF eval-moves THEN ;
: ?get-eval  ( move-index -- )
	s" ?get-eval" print-def
   moves-evaluated? IF get-eval ELSE get-move eval-move THEN my-eval ;
: ?eval-move  ( to from class undefined|eval -- to from class eval )
	s" ?eval-move" print-def
   DUP undefined = IF mdup eval-move NIP THEN ;
: set-move-eval  ( move-index -- )
	s" set-move-eval" print-def
   DUP get-move eval-move  SWAP set-eval ;
: ?set-move-eval  ( move-index -- )
	s" ?set-move-eval" print-def
   DUP get-eval undefined = IF set-move-eval  ELSE DROP THEN ;

\
\ Debugging support
\
: .move  ( to from class eval -- )
	s" .move" print-def
   DROP SWAP .square
   #move-strike-ep =  OVER empty? 0= OR IF  ." x" ELSE  ." -" THEN
   .square ;
: .emove  ( to from class eval -- )
	s" .emove" print-def
   DUP >R .move R> 6 .R ;
: .moves  ( -- )
	s" .moves" print-def
   #moves 0 ?DO
      I 7 AND 0= IF CR THEN
      I 1+ 2 .R ." ."    I get-move .move  2 SPACES
   LOOP ;
: .emoves  ( -- )
	s" .emoves" print-def
   #moves 0 ?DO
      I 3 AND 0= IF CR THEN
      I 1+ 2 .R ." ."    I get-move .move I get-eval 5 .R  2 SPACES
   LOOP ;

\
\ Doing/undoing moves
\
: undo-normal-move  ( to from piece -- )
	s" undo-normal-move" print-def
   SWAP put-piece   remove-piece ;
: undo-strike-move  ( to from piece removed-piece -- )
	s" undo-strike-move" print-def
   -ROT SWAP put-piece   SWAP put-piece ;
: undo-strike-ep-move  ( to from piece -- )
	s" undo-strike-ep-move" print-def
   SWAP put-piece   DUP remove-piece		         \ undo move
   pawn opponent OR  SWAP -10 ?direction +   put-piece ; \ restore struck pawn
: undo-trans-move  ( to from piece removed-piece -- )
	s" undo-trans-move" print-def
   2>R TUCK 2R>  undo-strike-move  pawn my-piece OR SWAP put-piece ;
: undo-king-move  ( to from piece  -- )
	s" undo-king-move" print-def
   OVER position-king  undo-normal-move ;
: undo-king-strike-move  ( to from piece removed-piece -- )
	s" undo-king-strike-move" print-def
   >R OVER position-king R>  undo-strike-move ;
: undo-pawn-far-move  ( to from piece -- )
	s" undo-pawn-far-move" print-def
   undo-normal-move ;
: undo-castle  ( to from king to from rook -- )
	s" undo-castle" print-def
   undo-normal-move  OVER position-king undo-normal-move ;
: undo-null-move  ( -- ) 	s" undo-null-move" print-def ;

: undo-info  ( to from -- to from piece to from )
	s" undo-info" print-def
   2DUP 2>R DUP board @ 2R> ;
: (do-normal-move)  ( to from -- normal-undo-info' )
	s" (do-normal-move)" print-def
   undo-info   take-piece SWAP put-piece ;
: do-normal-move  ( to from -- normal-undo-info )
	s" do-normal-move" print-def
   (do-normal-move) ['] undo-normal-move ;
: (do-strike-move)  ( to from -- strike-undo-info' )
	s" (do-strike-move)" print-def
   undo-info  take-piece SWAP   DUP hash-square DUP board @ -ROT  put-piece ;
: (do-strike-move-fast)  ( to from -- strike-undo-info' )
	s" (do-strike-move-fast)" print-def
   undo-info  take-piece SWAP   DUP board @ -ROT   put-piece ;
: do-strike-move  ( to from -- strike-undo-info )
	s" do-strike-move" print-def
   (do-strike-move) ['] undo-strike-move ;
: do-strike-move-fast  ( to from -- strike-undo-info )
	s" do-strike-move-fast" print-def
   (do-strike-move-fast) ['] undo-strike-move ;
: do-strike-ep-move  ( to from -- strike-ep-undo-info )
	s" do-strike-ep-move" print-def
   undo-info  take-piece SWAP
   DUP -10 ?direction +   DUP hash-square remove-piece   put-piece
   ['] undo-strike-ep-move ;
: do-strike-ep-move-fast  ( to from -- strike-ep-undo-info )
	s" do-strike-ep-move-fast" print-def
   undo-info  take-piece SWAP   DUP -10 ?direction + remove-piece   put-piece
   ['] undo-strike-ep-move ;
: do-pawn-far-move  ( to from -- normal-undo-info )
	s" do-pawn-far-move" print-def
   OVER TO far-moved-pawn   undo-info  take-piece SWAP put-piece
   ['] undo-pawn-far-move ;
: do-trans-knight-move  ( to from -- strike-undo-info )
	s" do-trans-knight-move" print-def
   OVER >R (do-strike-move)   knight my-piece OR R> put-piece
   ['] undo-trans-move ;
: do-trans-queen-move  ( to from -- strike-undo-info )
	s" do-trans-queen-move" print-def
   OVER >R (do-strike-move)   queen my-piece OR R> put-piece
   ['] undo-trans-move ;
: do-trans-knight-move-fast  ( to from -- strike-undo-info )
	s" do-trans-knight-move-fast" print-def
   OVER >R (do-strike-move-fast)   knight my-piece OR R> put-piece
   ['] undo-trans-move ;
: do-trans-queen-move-fast  ( to from -- strike-undo-info )
	s" do-trans-queen-move-fast" print-def
   OVER >R (do-strike-move-fast)   queen my-piece OR R> put-piece
   ['] undo-trans-move ;
: do-king-move  ( to from -- normal-undo-info )
	s" do-king-move" print-def
   OVER position-king (do-normal-move)  ['] undo-king-move ;
: do-king-strike-move  ( to from -- strike-undo-info )
	s" do-king-strike-move" print-def
   OVER position-king (do-strike-move)  ['] undo-king-strike-move ;
: do-king-strike-move-fast  ( to from -- strike-undo-info )
	s" do-king-strike-move-fast" print-def
   OVER position-king (do-strike-move-fast)  ['] undo-king-strike-move ;
: do-castle-near  ( to from -- normal-undo-info1 normal-undo-inf2 )
	s" do-castle-near" print-def
   ( king) undo-info   take-piece castled SWAP   DUP position-king put-piece
   ( rook) white? IF f1 h1 ELSE f8 h8 THEN
   DUP hash-square  OVER >R (do-normal-move)  R> hash-square
   ['] undo-castle ;
: do-castle-far  ( to from -- normal-undo-info1 normal-undo-inf2 )
	s" do-castle-far" print-def
   ( king) undo-info   take-piece castled SWAP   DUP position-king put-piece
   ( rook) white? IF d1 a1 ELSE d8 a8 THEN
   DUP hash-square  OVER >R (do-normal-move)  R> hash-square
   ['] undo-castle ;
: do-castle-near-fast  ( to from -- normal-undo-info1 normal-undo-inf2 )
	s" do-castle-near-fast" print-def
   ( king) undo-info   take-piece castled SWAP   DUP position-king put-piece
   ( rook) white? IF f1 h1 ELSE f8 h8 THEN  (do-normal-move)
   ['] undo-castle ;
: do-castle-far-fast  ( to from -- normal-undo-info1 normal-undo-inf2 )
	s" do-castle-far-fast" print-def
   ( king) undo-info   take-piece castled SWAP   DUP position-king put-piece
   ( rook) white? IF d1 a1 ELSE d8 a8 THEN  (do-normal-move)
   ['] undo-castle ;
: do-null-move  ( to from -- ) 	s" do-null-move" print-def 2DROP ['] undo-null-move ;

\
\ Vector table for performing moves, updating the hash
\
vector-table: (do-move)  ( to from class -- undo-info )
   ' do-normal-move ,        ' do-strike-move , 
   ' do-strike-ep-move ,     ' do-pawn-far-move ,
   ' do-trans-knight-move ,  ' do-trans-queen-move ,
   ' do-king-move ,          ' do-king-strike-move ,
   ' do-castle-near ,        ' do-castle-far ,
   ' do-null-move ,
   
\
\ Vector table for performing moves fast, without hash updates etc
\
vector-table: (do-move-fast)  ( to from class -- undo-info )
   ' do-normal-move ,		  ' do-strike-move-fast , 
   ' do-strike-ep-move-fast ,	  ' do-pawn-far-move ,
   ' do-trans-knight-move-fast ,  ' do-trans-queen-move-fast ,
   ' do-king-move ,               ' do-king-strike-move-fast ,
   ' do-castle-near-fast ,        ' do-castle-far-fast ,
   ' do-null-move ,   

: get-null-move  ( -- to from class eval )
	s" get-null-move" print-def
   move-squares #move-null curr-abs-eval ;

: update-curr-check?  ( class -- )
	s" update-curr-check?" print-def
   DUP #move-normal =  OVER #move-strike = OR  SWAP #move-pawn-far = OR IF
      opponent-move-target king-square threatens? IF
	 TRUE TO curr-check? EXIT
      THEN
      opponent-move-orig might-cause-check? IF
	 check? TO curr-check? EXIT
      THEN
      FALSE TO curr-check?
   ELSE check? TO curr-check? THEN ;
      
: do-move-undo-info  ( to from class eval -- undo-info )
	s" do-move-undo-info" print-def
   remember-position
   curr-abs-eval far-moved-pawn 2>R      ( R: was-eval was-pawn )
   TO curr-abs-eval
   hash hash@ hash>r	
   hash-far-moved-pawn  0 TO far-moved-pawn
   move-squares 2>R	
   curr-check? >R        ( R: was-eval was-pawn was-hash were-sq was-check )
   >R 2DUP to-move-squares DUP hash-square R>
   DUP >R
   (do-move) other-party
   R> update-curr-check?
   hash-far-moved-pawn opponent-move-target hash-square
   R> 2R> hash-r> 2R> ;
               ( S: undo-info  wash-check were-sq was-hash was-evalwas-pawn )

: do-move  ( to from class -- )
	s" do-move" print-def
   DEPTH 4 - >R  do-move-undo-info
   DEPTH R> - 0 ?DO DROP LOOP ;
: undo-move  ( undo-info -- )
	s" undo-move" print-def
   TO far-moved-pawn  TO curr-abs-eval  hash hash!
   other-party to-move-squares  TO curr-check?  EXECUTE
   forget-position ;
: self-checking-move?  ( to from class eval -- flag )
	s" self-checking-move?" print-def
   far-moved-pawn >R 0 TO far-moved-pawn  DROP (do-move-fast)
   check?
   R> TO far-moved-pawn >R EXECUTE R> ;			\ undo move
: checking-move?  ( to from class eval -- flag )
	s" checking-move?" print-def
   far-moved-pawn >R 0 TO far-moved-pawn  DROP (do-move-fast)
   other-check?
   R> TO far-moved-pawn >R EXECUTE R> ;			\ undo move
: ?add-move  ( to from class eval -- )
	s" ?add-move" print-def
\   2>R 2DUP .square .square SPACE 2R>
   mdup self-checking-move? 0= IF add-move ELSE mdrop THEN ;
: move-might-cause-check?  ( to from class eval -- flag )
	s" move-might-cause-check?" print-def
   DROP other-party
   DUP #move-normal =  OVER #move-strike = OR  SWAP #move-pawn-far = OR IF
      TUCK DUP board @ piece-would-threaten?
      SWAP might-cause-check? OR
   ELSE 2DROP FALSE THEN  other-party ;

\
\ Calculate the hash of the resulting board position of a move
\
: set-normal-move-hash  ( to from -- )
	s" set-normal-move-hash" print-def
   hash-far-moved-pawn hash-no-far-moved-pawn 
   DUP board @ TUCK hash-piece   moved hash-piece ;
: set-strike-move-hash  ( to from -- )
	s" set-strike-move-hash" print-def
   hash-far-moved-pawn hash-no-far-moved-pawn 
   DUP board @ TUCK hash-piece   OVER hash-square  moved hash-piece ;
: set-move-hash  ( to from class eval -- ) \ set resulting hash of move
	s" set-move-hash" print-def
   OVER CASE
      #move-normal OF 2DROP set-normal-move-hash EXIT ENDOF
      #move-strike OF 2DROP set-strike-move-hash EXIT ENDOF
   ENDCASE 
   do-move-undo-info   hash hash@ hash>r   undo-move  hash-r> hash hash! ;




