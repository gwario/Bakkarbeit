\
\ Some test for checking certain parts of Brainless
\

S" brainless.fs" INCLUDED

3 CONSTANT fly-eval-check-limit
3 CONSTANT check-check-limit
3 CONSTANT hash-check-limit

0 VALUE check-limit
0 VALUE check-depth

: +check  ( -- ) 	s" +check" print-def check-depth 1+ TO check-depth ;
: -check  ( -- ) 	s" -check" print-def check-depth 1- TO check-depth ;
: continue-check?  ( -- ) 	s" continue-check?" print-def check-depth check-limit < ;

: check-fly-eval  ( -- )
	s" check-fly-eval" print-def
   continue-check? 0= IF EXIT THEN
   +check  generate-moves eval-moves
   #moves 0 ?DO
      I get-move do-move-undo-info
      curr-abs-eval total-eval <> ABORT" curr-abs-eval desynchronization!"
      RECURSE
      undo-move
   LOOP
   forget-moves -check ;
: perform-fly-eval-test  ( -- )
	s" perform-fly-eval-test" print-def
   CR ."  Testing whether flyeval keeps synchronized..."
   fly-eval-check-limit TO check-limit  +fly-eval check-fly-eval ;

: check-check  ( -- )
	s" check-check" print-def
   continue-check? 0= IF EXIT THEN
   +check  generate-moves 
   #moves 0 ?DO
      I get-move do-move-undo-info
      curr-check? check? <> ABORT" curr-check? desynchronisation!"
      other-check? ABORT" self-checking move was performed!"
      RECURSE
      undo-move
   LOOP
   forget-moves -check ;
: perform-check-test  ( -- )
	s" perform-check-test" print-def
   CR ."  Testing whether check state keeps synchronized..."
   check-check-limit TO check-limit  check-check ;

: check-hash  ( -- )
	s" check-hash" print-def
   continue-check? 0= IF EXIT THEN
   +check  generate-moves
   #moves 0 ?DO
      I get-move do-move-undo-info
      hash hash@ generate-hash hash hash@ hash=
      0= ABORT" hash desynchronization!"
      RECURSE
      hash hash@ hash>r
      undo-move
      hash-r>  I get-move set-move-hash hash hash@ hash=
      0= ABORT" set-move-hash: wrong hash!"
      generate-hash
   LOOP
   forget-moves -check ;
: perform-hash-test  ( -- )
	s" perform-hash-test" print-def
   CR ."  Testing whether 64bit hash keeps synchronized..."
   hash-check-limit TO check-limit  check-hash ;
      

: perform-tests  ( -- )
	s" perform-tests" print-def
   0 TO check-depth
   perform-fly-eval-test
   perform-check-test
   perform-hash-test ;

: test  ( -- )
	s" test" print-def
   ." Reading tests.epd for test positions."
   S" tests.epd" R/O OPEN-FILE THROW TO epd-fileid
   BEGIN
      CR ." Reading position..."
      epd-file-buffer c/epd-file-buffer 2 - epd-fileid READ-LINE THROW
   WHILE
      epd-file-buffer SWAP epd>position
      perform-tests
   REPEAT
   ."  end of test file" ;
