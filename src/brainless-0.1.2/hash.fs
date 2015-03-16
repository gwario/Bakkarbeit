\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ hash routines

\
\ make sure that a hash is always 64 bits
\
bits/u 63 > [IF]
   1 CONSTANT cells/hash
   VARIABLE hash
   : hash@  ( a-addr -- hash )  POSTPONE @ ; IMMEDIATE
   : hash!  ( hash a-addr -- )  POSTPONE ! ; IMMEDIATE
   : hash-xor  ( hash1 hash2 -- hash3 )  POSTPONE XOR ; IMMEDIATE
   : hash=  ( hash1 hash2 -- flag )  POSTPONE = ; IMMEDIATE
   : zero-hash  ( -- hash )  0 POSTPONE LITERAL ; IMMEDIATE
   : zero-hash?  ( hash -- flag )  POSTPONE 0= ; IMMEDIATE
   : random-hash  ( -- hash )
      random   random 16 LSHIFT OR  random 32 LSHIFT OR  random 48 LSHIFT OR ;
   : hash>r  ( S: hash --  R: -- hash )  POSTPONE >R ; IMMEDIATE
   : hash-r>  ( S: -- hash  R: hash -- )  POSTPONE R> ; IMMEDIATE
[ELSE] bits/ud 63 > [IF]
   2 CONSTANT cells/hash
   2VARIABLE hash
   : hash@  ( a-addr -- hash )  POSTPONE 2@ ; IMMEDIATE
   : hash!  ( hash a-addr -- )  POSTPONE 2! ; IMMEDIATE
   : hash-xor  ( hash1 hash2 -- hash3 )
      POSTPONE ROT  POSTPONE XOR  POSTPONE >R  POSTPONE XOR  POSTPONE R> ;
      IMMEDIATE
   : hash=  ( hash1 hash2 -- flag )  POSTPONE D= ; IMMEDIATE
   : zero-hash  ( -- hash )  0. POSTPONE 2LITERAL ; IMMEDIATE
   : zero-hash?  ( hash -- flag )  POSTPONE D0= ; IMMEDIATE
   : random-hash  ( -- hash )
      random random 16 LSHIFT OR  random random 16 LSHIFT OR ;
   : hash>r  ( S: hash --  R: -- hash )  POSTPONE 2>R ; IMMEDIATE
   : hash-r>  ( S: -- hash  R: hash -- )  POSTPONE 2R> ; IMMEDIATE
[ELSE] bits/ud 31 > [IF]
   4 CONSTANT cells/hash
   CREATE hash 4 CELLS ALLOT
   : hash@  ( a-addr -- hash )  DUP [ 2 CELLS ] LITERAL + 2@   ROT 2@ ;
   : hash!  ( hash a-addr -- )  DUP >R 2!   R> [ 2 CELLS ] LITERAL + 2! ;
   : hash-xor  ( hash1 hash2 -- hash3 )
      2ROT
      ROT XOR >R XOR R> 2>R
      ROT XOR >R XOR R> 2R> ;
   : hash=  ( hash1 hash2 -- flag )  2ROT D= >R D= R> AND ;
   : zero-hash  ( -- hash )
      0. 2DUP  POSTPONE LITERAL  POSTPONE LITERAL ; IMMEDIATE
   : zero-hash?  ( hash -- flag )  D0= >R D0= R> AND ;
   : random-hash  ( -- hash )
      random random random random ;
   : hash>r  ( S: hash --  R: -- hash )  POSTPONE 2>R  POSTPONE 2>R ; IMMEDIATE
   : hash-r> ( S: -- hash  R: hash -- )  POSTPONE 2R>  POSTPONE 2R> ; IMMEDIATE
[ELSE]
   CR .( Less than 32 bits/double-cell??) ABORT
[THEN] [THEN] [THEN]

cells/hash . .( cells/hash  )

cells/hash CELLS CONSTANT /hash

: hash-array  ( u -- )
   CREATE /hash * ALLOT   DOES>  ( u -- a-addr )  SWAP /hash * + ;

hash-piece-mask 1+ 100 * hash-array hash-codes
100                      hash-array pawn-hash-codes

: init-hash-codes  ( -- ) \ randomize hash codes
   ." randomizing hash codes..."
   100 0 DO
      hash-piece-mask 1+ 0 DO
	 I piece-mask AND   DUP empty-square =  SWAP border = OR IF
	    I 100 * J + hash-codes  /hash ERASE
	 ELSE
	    random-hash  I 100 * J + hash-codes  hash!
	 THEN
      LOOP
      random-hash I pawn-hash-codes hash!
   LOOP
   ." done  " ;

init-hash-codes

: update-hash  ( hash -- )  hash hash@ hash-xor  hash hash! ;
: hash-piece  ( square piece -- )
   hash-piece-mask AND 100 * +  hash-codes hash@ update-hash ;
: hash-square  ( square -- )
   DUP board @ hash-piece ;
: hash-no-far-moved-pawn  ( -- )
   0 pawn-hash-codes hash@ update-hash ;
: hash-far-moved-pawn  ( -- )
   far-moved-pawn pawn-hash-codes hash@ update-hash ;
: generate-hash  ( -- )
   hash /hash ERASE
   100 0 DO  I hash-square LOOP
   hash-far-moved-pawn ;

' generate-hash add-board-hook







