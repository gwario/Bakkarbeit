\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Transposition table

32768 option #ttentries		\ must be 2^n
\ 131072 option #ttentries	\ must be 2^n
\ 262144 option #ttentries

#ttentries 1- CONSTANT tt-mask

record		\ data layout of transposition table entry
   /hash   offset ttentry-hash
   1 CELLS offset ttentry-distance
   1 CELLS offset ttentry-up
   1 CELLS offset ttentry-low
   1 CELLS offset ttentry-bestmove
end-record /ttentry

\ actually two tables, one for white and one for black
#ttentries /ttentry * 2* ALLOCATE THROW CONSTANT ttable

: ttentry  ( index -- a-addr )
	s" ttentry" print-def
   /ttentry *
   white? [ #ttentries /ttentry * ] LITERAL AND ttable + + ;
: tt-clear-entry  ( index -- )
	s" tt-clear-entry" print-def
   ttentry  undefined OVER ttentry-up !
   ttentry-hash /hash ERASE ;
: tt-entry-expired  ( index -- )
	s" tt-entry-expired" print-def
   ttentry  undefined SWAP ttentry-up ! ;
: tt-lookup  ( index -- a-addr|0 ) \ return entry's address if it matches
	s" tt-lookup" print-def
   ttentry DUP   ttentry-hash hash@  hash hash@ hash=  AND ;
: tt-free-entry  ( index -- a-addr|0 ) \ return entry's address if unused
	s" tt-free-entry" print-def
   ttentry DUP   ttentry-up @ undefined =  AND ;
: tt-retrieve  ( -- a-addr|0 ) \ retrieve hash table entry
	s" tt-retrieve" print-def
   hash @ tt-mask AND tt-lookup ;
: tt-retrieve-free  ( -- a-addr|0 ) \ retrieve free hash table entry
	s" tt-retrieve-free" print-def
   hash @ tt-mask AND tt-free-entry ;
: tt-retrieve-move  ( move-index -- a-addr|0 ) \ retrieve entry for move
	s" tt-retrieve-move" print-def
   hash hash@ hash>r
   get-move set-move-hash other-party tt-retrieve other-party
   hash-r> hash hash! ;
: tt-set-hash  ( a-addr -- ) \ set hash field of given entry
	s" tt-set-hash" print-def
   >R   hash hash@   R> ttentry-hash hash! ;
: tt-store  ( -- a-addr ) \ get entry to store current position in
	s" tt-store" print-def
   hash @ tt-mask AND ttentry   DUP tt-set-hash ;
: tt-set-bestmove  ( from to -- )
	s" tt-set-bestmove" print-def
   hash @ tt-mask AND ttentry >R
   R@ ttentry-hash hash@  hash hash@ hash=
   R@ ttentry-up @  undefined = OR IF
      R@ ttentry-distance @  horizon-distance > 0= IF
	 R@ tt-set-hash
	 pack-squares R> ttentry-bestmove ! EXIT
      THEN
   THEN
   R> DROP 2DROP ;
: tt-get-bestmove  ( -- from to | 0 0 )
	s" tt-get-bestmove" print-def
   tt-retrieve ?DUP IF  ttentry-bestmove @ unpack-squares ELSE 0 0 THEN ;

: tt-clear  ( -- ) \ clear the transposition table
	s" tt-clear" print-def
   #ttentries 0 ?DO
      I tt-clear-entry  other-party I tt-clear-entry other-party
   LOOP ;
: tt-expired  ( -- ) \ set all entries to expire
	s" tt-expired" print-def
   #ttentries 0 ?DO
      I tt-entry-expired  other-party I tt-entry-expired other-party
   LOOP ;
   
tt-clear



