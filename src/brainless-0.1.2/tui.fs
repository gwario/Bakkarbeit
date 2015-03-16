\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Terminal user interface

: .party  ( -- )  \ printy which party's move it is
   white? IF ." White" ELSE ." Black" THEN SPACE ." moves." ;
: look  ( -- )  \ Print board and party
   .board .party ;
: graphical  ( -- )  \ toggle use of utf-8 chess glyphs
   utf8-terminal? 0= ['] utf8-terminal? >BODY !
   look ;
: huge  ( -- )   huge-board look ;
: normal  ( -- )  normal-board look ;
: small  ( -- )   small-board look ;

: init  ( -- )  init-board hist-record look ;
: clear  ( -- )  empty-board look ;
: ?square  ( square -- square )
   DUP a1 h8 1+ WITHIN 0= ABORT" Invalid square!" ;
: ?empty  ( square -- square )
   DUP empty? 0= ABORT" Square not empty! (use `remove' first)" ;
: ?not-empty  ( field -- )
   DUP empty? ABORT" Square is empty!" ;
: add  ( piece color square -- )
   ?square ?empty   -ROT OR 
   OVER initial-board @ full-piece-mask AND OVER =   f-unmoved AND   OR
   f-piece OR SWAP board !
   update-board ;
: remove  ( square -- )
   ?square ?not-empty   remove-piece   update-board ;
: xsave
   BL WORD COUNT
   2DUP file-exists? IF
      epd-append-to-file
      ." Current position appended to file as entry " .
   ELSE
     epd-create-file
      ." Created new file, current position is entry 0"
   THEN ;
: xload  ( "name" -- ) \ load entry from file
   BL WORD COUNT epd-read-file ;
: save  ( "name" -- )   BL WORD COUNT epd-create-file ;
: load  ( "name" -- )  0 xload ;
   
: ?find-move  ( i*x from to -- i*x index | )
   find-move 0= ABORT" Invalid move!" ;
: ?valid-move  ( i*x from to --  |i*x )
   generate-moves find-move forget-moves 0= ABORT" Invalid move!"   DROP ;
: m  ( from to -- ) \ perform a move
   2DUP ?valid-move
   generate-moves eval-moves ?find-move DUP display-move SPACE
   get-move do-move forget-moves
   hist-record
   look ;
: cm  ( -- ) \ let the computer move
   ." Hmm..."  generate-moves eval-moves calculate-move
   CR ." my move is " DUP display-move SPACE
   get-move do-move forget-moves
   hist-record
   look ;
: lm  ( -- ) \ print list of moves
   generate-moves ?eval-moves .emoves forget-moves ;
: undo  ( -- )  \ undo last move
   hist-undo
   look ;
: best  ( -- ) \ print evaluated and sorted list of moves
   generate-moves sort-moves .emoves forget-moves ;
: demo  ( -- )
   2 2 DO
      CR I 2 / 3 .R ." : " cm 
      KEY? IF LEAVE THEN
   LOOP ;
FALSE VALUE had-strength?
: strength  ( n -- )  \ set strength of computer player
   0 MAX 10 MIN >R
   R@ 5 < IF R@ 1+ 2* ELSE max-think-depth THEN TO max-think-limit
   R@ DUP * 1+ TO abort-time
   CR ." Playing strength of computer set to " R> 0 .R ." ."
   TRUE TO had-strength? ;
: ?default-strength  ( -- ) \ set default strength, if no strength set yet
   had-strength? 0= IF
      2 strength
   THEN ;

\ Help text
WORDLIST CONSTANT help-wid
GET-CURRENT  help-wid SET-CURRENT
text: bye
   bye
   
   Quit brainless.  Since game history is saved in a file, Brainless returns
   to the current board position when started again.
;text
text: init
   init
   
   Set chess board to initial position.  White moves.
;text
text: save
   save <filename>
   
   Save current position to <filename>
;text
text: load
   load <filename>
   
   Load position from <filename>
;text
text: m
   <square1> <square2> m
   
   Perform move from <square1> to <square2>.
   Example:    e2 e4 m
;text
text: cm
   cm
   
   Let the computer move.
;text
text: undo
   undo
   
   Undo last move.  Call repeatedly to undo more than one move.
;text
text: look
   look
   
   Show the board and print which party moves.
   See also 'graphical'
;text
text: graphical
   graphical
   
   Toggle the use of Unicode chess-piece glyphs
   instead of ASCII characters PNBRQK when drawing
   the chess board.
;text
text: small
   small
   
   Use small chess board display.
;text
text: normal
   normal
   
   Use medium chess board display.  This is the
   default.
;text
text: huge
   huge
   
   Use large chess board display.  
;text
text: strength
   <N> strength

   Set strength of computer player to <N>.
   <N> is an integer in range 0..10 .
;text
text: help
   help <command>

   Display help for <command>
;text

SET-CURRENT

: help  ( "name" -- )
   BL WORD COUNT help-wid SEARCH-WORDLIST 0= IF
      CR ."     Type 'look' to see the board."
      CR ."     Type '<from> <to> m' to make a move."
      CR ."     Type 'cm' to let the computer move."
      CR ."     Type 'help <command>' for specific help."
      CR ."     Type 'bye' to quit Brainless."
      CR ."     I know these commands/topics:"
      GET-ORDER   help-wid 1 SET-ORDER WORDS   SET-ORDER
      EXIT
   THEN
   EXECUTE ;

: tui-startup  ( -- )  \ ui entry
   ?default-strength
   ?history-load
   look
   CR ." If startled, type 'help'!" ;
   
\ Customize Emacs highlighting and indentation for this file
0 [IF]
   Local Variables:
   compile-command: "..."
   forth-local-words:
   (
    (("text:") non-immediate (font-lock-type-face . 1)
    "[ \t\n]" nil string (font-lock-variable-name-face . 1))
    ((";text") non-immediate (font-lock-keyword-face . 1))
    )
   forth-local-indent-words:
   ((("text:") (0 . 2) (0 . 2))
    ((";text") (-2 . 0) (0 . -2)))
   End:
[THEN]
