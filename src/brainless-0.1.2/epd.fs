\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ EPD Position Definition conversion and read/write access to EPD files

\
\ I didn't find any documentation about EPD, the code here was written from
\ what I saw in the GNUChess sources.
\

0 VALUE epd-file-id

\
\ Converting positions to EPD 
\
: epd-write-board-line  ( u -- )
	s" epd-write-board-line" print-def
   0 SWAP
   0 SWAP >square   DUP 9 + SWAP DO	( S: empty-count )
      I empty? 0= IF
	 DUP IF  [CHAR] 0 + write-char  0 THEN
	 I piece? IF  I board @ piece>char write-char THEN
      ELSE 1+ THEN
   LOOP  DROP ;
: epd-write-board  ( -- )
	s" epd-write-board" print-def
   0 7 DO
      I epd-write-board-line
      I IF [CHAR] / write-char THEN
   -1 +LOOP ;
: epd-write-party  ( -- )
	s" epd-write-party" print-def
   white? IF [CHAR] w ELSE [CHAR] b THEN write-char ;
: epd-write-castle  ( -- )
	s" epd-write-castle" print-def
   #characters
   e1 board @   king white-piece unmoved = IF
      h1 board @   rook white-piece unmoved =  IF [CHAR] K write-char THEN
      a1 board @   rook white-piece unmoved =  IF [CHAR] Q write-char THEN
   THEN
   e8 board @   king black-piece unmoved = IF
      h8 board @   rook black-piece unmoved =  IF [CHAR] k write-char THEN
      a8 board @   rook black-piece unmoved =  IF [CHAR] q write-char THEN
   THEN
   #characters = IF  [CHAR] - write-char THEN ;
: epd-write-ep  ( -- )
	s" epd-write-ep" print-def
   far-moved-pawn IF
      far-moved-pawn 10 ?direction +  write-square
   ELSE [CHAR] - write-char THEN ;
: position>epd  ( c-addr u1 -- u2 )
	s" position>epd" print-def
   is-string
   epd-write-board BL write-char
   epd-write-party BL write-char
   epd-write-castle BL write-char
   epd-write-ep BL write-char
   S" bm 1; id 1;" write-string 
   #characters   previous-string ;

\
\ Converting EPD back to positions
\
: epd-forward-squares  ( square1 u -- square2 )
	s" epd-forward-squares" print-def
   >R >xy SWAP R> +  DUP 8 > ABORT" Invalid column!"
   SWAP >square ;
: epd-forward-lines  ( square1 u -- square2 )
	s" epd-forward-lines" print-def
   >R >xy R> -   DUP 0 < ABORT" Invalid row!"
   NIP 0 SWAP >square ;
: ?epd-square  ( square -- )
	s" ?epd-square" print-def
   DUP 20 101 WITHIN 0= ABORT" Invalid square!"
   empty? 0= ABORT" Invalid square!" ;
: epd-read-board  ( -- )
	s" epd-read-board" print-def
   a8
   BEGIN read-char DUP BL <> OVER 0<> AND WHILE
      DUP char>piece IF
	 NIP OVER ?epd-square OVER put-piece  1 epd-forward-squares
      ELSE DUP [CHAR] 0 [CHAR] 9 WITHIN IF
	 [CHAR] 0 - epd-forward-squares
      ELSE [CHAR] / = IF
	 1 epd-forward-lines
      ELSE 1 ABORT" Invalid character in board field!"
      THEN THEN THEN
   REPEAT
   2DROP ;
: epd-read-party  ( -- )
	s" epd-read-party" print-def
   read-char CASE
      [CHAR] w OF  TRUE set-party ENDOF
      [CHAR] b OF  FALSE set-party ENDOF
      TRUE ABORT" Invalid character in party field!"
   ENDCASE ;
: epd-unmoved  ( piece square -- )
	s" epd-unmoved" print-def
   TUCK
   board @ piece-mask AND <> ABORT" Invalid character in castle field!"
   board DUP @ f-unmoved OR SWAP ! ;
: epd-read-castle  ( -- )
	s" epd-read-castle" print-def
   BEGIN read-char DUP BL <> OVER 0<> AND WHILE
      CASE
	 [CHAR] K OF  king e1 epd-unmoved   rook h1 epd-unmoved ENDOF
	 [CHAR] Q OF  king e1 epd-unmoved   rook a1 epd-unmoved ENDOF
	 [CHAR] k OF  king e8 epd-unmoved   rook h8 epd-unmoved ENDOF
	 [CHAR] q OF  king e8 epd-unmoved   rook a8 epd-unmoved ENDOF
	 [CHAR] - OF  ENDOF
	 TRUE ABORT" Invalid character in castle field!"
      ENDCASE
   REPEAT
   DROP ;
: epd-guess-unmoved-flags  ( -- ) \ guess f-unmoved flag for non-kings/rooks
	s" epd-guess-unmoved-flags" print-def
   100 20 DO
      I board @ piece-mask AND
      DUP king <>  SWAP rook <> AND
      I piece? AND
      I initial-square? AND IF
	 I board DUP @ unmoved SWAP !
      THEN
   LOOP ;
: epd-read-ep  ( -- )
	s" epd-read-ep" print-def
   read-char [CHAR] - = IF
      0 TO far-moved-pawn
   ELSE
      previous-char read-square -10 ?direction + TO far-moved-pawn 
   THEN ;
: epd-skip-blank  ( -- )
	s" epd-skip-blank" print-def
   BEGIN read-char DUP BL = WHILE   DROP   REPEAT
   0= ABORT" Unexpected end of line!"
   previous-char ;
: epd>position  ( c-addr u -- )
	s" epd>position" print-def
   is-string
   empty-board
   epd-skip-blank
   epd-read-board epd-skip-blank
   epd-read-party epd-skip-blank
   epd-read-castle epd-skip-blank
   epd-read-ep
   epd-guess-unmoved-flags
   update-board
   previous-string ;

\
\ EPD file access
\
\ todo: close epd-fileid on errors
0 VALUE epd-fileid
128 CONSTANT c/epd-file-buffer
CREATE epd-file-buffer c/epd-file-buffer CHARS ALLOT
2VARIABLE epd-offset

: epd-close-file  ( -- )
	s" epd-close-file" print-def
   epd-fileid CLOSE-FILE THROW ;
: epd-open-file  ( fam c-addr u -- )
	s" epd-open-file" print-def
   OPEN-FILE THROW TO epd-fileid ;
: epd-read-file  ( n c-addr u -- ) \ read nth position from given file (0=1st)
	s" epd-read-file" print-def
   R/O epd-open-file
   DUP 1+ 0 ?DO
      epd-file-buffer c/epd-file-buffer 2 - epd-fileid READ-LINE THROW
      0= ABORT" Unexpected end of EPD file"
      DUP c/epd-file-buffer 2 - = ABORT" Line of EPD file too long!"
      OVER I > IF  DROP THEN
   LOOP NIP
   epd-file-buffer SWAP epd>position
   epd-close-file ;
: epd-read-last  ( c-addr u -- )
	s" epd-read-last" print-def
   \ load last line from epd file, remembering line start in epd-offset
   R/O epd-open-file   0  ( s: len )
   BEGIN
      epd-fileid FILE-POSITION THROW
      epd-file-buffer c/epd-file-buffer 2 - epd-fileid READ-LINE THROW
   WHILE
	 -rot epd-offset 2!       \ record last line offset so epd-truncate 
	 nip                      \ remove length of previous line
   REPEAT
   drop 2drop
   DUP c/epd-file-buffer 2 - = ABORT" Line of EPD file too long!"
   epd-file-buffer SWAP epd>position
   epd-close-file ;
: epd-truncate  ( c-addr u -- )  \ truncate last line read by epd-read-last
	s" epd-truncate" print-def
   R/W epd-open-file
   epd-offset 2@ epd-fileid RESIZE-FILE THROW
   epd-close-file ;
: epd-write-to-file  ( -- ) \ write epd line to currently open file
	s" epd-write-to-file" print-def
   epd-file-buffer DUP c/epd-file-buffer position>epd
   epd-fileid WRITE-LINE THROW ;
: epd-append-to-file  ( c-addr u -- n )
	s" epd-append-to-file" print-def
   \ append to epd file, return index of position added (0=1st)
   R/W OPEN-FILE THROW TO epd-fileid
   0 BEGIN  ( S: line-count)
      epd-file-buffer c/epd-file-buffer 2 - epd-fileid READ-LINE THROW
   WHILE
      c/epd-file-buffer 2 - = ABORT" Line of EPD file too long!"
      1+
   REPEAT DROP
   epd-write-to-file epd-close-file ;
: epd-create-file ( c-addr u -- ) \ create new file for current position
	s" epd-create-file" print-def
   R/W CREATE-FILE THROW TO epd-fileid
   epd-write-to-file epd-close-file ;
: ?epd-append-to-file  ( c-addr u -- n )
	s" ?epd-append-to-file" print-def
   \ append to epd file, or create new file if not existing return index of
   \ position added (0=1st)
   2DUP file-exists? IF 
      epd-append-to-file
   ELSE epd-create-file 0 THEN ;
