\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\ 
\ Persistent storage of history of moves

: (history-filename)  ( -- c-addr )
   [ gforth? ] [IF]
      C" ~/.brainless_history"
   [ELSE]
      C" brainless.log"
   [THEN] ;
(history-filename) option history-filename

: hist-record  ( -- )
   history-filename COUNT  ?epd-append-to-file drop ;
: hist-restore  ( -- )
   history-filename COUNT  epd-read-last ;
: hist-drop  ( -- )
   hist-restore
   epd-offset 2@ 0= SWAP 0= AND
   ABORT" Already at beginning of history"
   history-filename COUNT  epd-truncate ;
: hist-undo  ( -- )
   hist-drop
   hist-restore ;

: ?history-load  ( -- )
   ['] hist-restore CATCH IF
      history-filename COUNT DELETE-FILE DROP
      init-board 
      hist-record
      CR ." No (correct) history file.  New history file is "
   ELSE
      CR ." Restored last position from " 
   THEN
   history-filename COUNT TYPE  ." ." ;
