\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ New Tree search 

: switch-alpha-beta  ( -- )  alpha NEGATE beta NEGATE TO alpha TO beta ;
: check-mate-eval  ( -- eval )  -infinity 1+  think-depth + ;
: ?game-ended  ( -- )
   moves-exist? 0= IF
      check? ABORT" check mate"
      TRUE   ABORT" stale mate"
   THEN ;

0 VALUE cutoff?
: is-cutoff?  ( eval -- flag )  beta < 0= DUP TO cutoff? ;
: adjust-alpha  ( move-index eval -- )
   DUP alpha < 0= IF
      TO alpha  is-principal-move
   ELSE 2DROP THEN ;
: remember-bestmove  ( move-index -- ) 
   think-depth think-limit < IF
      get-move-squares tt-set-bestmove
   ELSE DROP THEN ;
: best-move-eval  ( curr-eval -- curr-eval|better-eval )
   0 TO cutoff?
   BEGIN next-best-move DUP 0< 0= WHILE		( S: eval move-i )
      DUP >R eval-move-recursive		( S: eval eval2 ) ( R: move-i )
      0 TO cutoff?   
      2DUP > 0= IF
	 2DUP < IF  R@ remember-bestmove THEN
	 R@ OVER adjust-alpha
	 NIP DUP is-cutoff? IF
	    R> is-killer EXIT
	 THEN
      ELSE DROP THEN R> DROP
   REPEAT DROP ;
0 VALUE #null
: null-move-eval  ( curr-eval -- curr-eval|better-eval )
   0 TO cutoff?
   ( disabled...) EXIT
   curr-eval beta <  think-depth think-limit < OR IF EXIT THEN
   null-move? IF
      #null 1+ TO #null
      -1 eval-move-recursive
      DUP is-cutoff? DROP
      DUP alpha MAX TO alpha
      MAX
   THEN ;
: single-move-eval  ( curr-eval -- curr-eval|better-eval )
   0 TO cutoff?
   0 set-move-eval  0 eval-move-recursive
   DUP is-cutoff? IF  0 is-killer THEN
   0 OVER adjust-alpha
   2DUP < IF  0 remember-bestmove THEN
   MAX ;
: killer-move-eval  ( curr-eval -- curr-eval|better-eval )
   0 TO cutoff?
   generate-fast-killer #moves IF
      single-move-eval
   THEN forget-moves ;
: capture-killer-move-eval  ( curr-eval -- curr-eval|better-eval )
   0 TO cutoff?
   generate-fast-strike-killer #moves IF
      single-move-eval
   THEN forget-moves ;
: target-move-eval  ( curr-eval -- curr-eval|better-eval )
   opponent-move-target
   generate-moves-to delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: capture-not-target-move-eval  ( curr-eval -- curr-eval|better-eval )
   opponent-move-target DUP opponent? 0= IF DROP 0 THEN
   generate-strike-moves-not-to delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: capture-move-eval  ( curr-eval -- curr-eval|better-eval )
   generate-strike-moves delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: full-quiescence-move-eval  ( curr-eval -- curr-eval|better-eval )
   generate-quiescence-moves delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: quiescence-capture-eval  ( curr-eval -- curr-eval|better-eval )
   generate-quiescence-captures delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: quiescence-promotion-eval  ( curr-eval -- curr-eval|better-eval )
   generate-promotions delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: all-move-eval  ( curr-eval -- curr-eval|better-eval )
   generate-moves delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: not-target-move-eval  ( curr-eval -- curr-eval|better-eval )
   opponent-move-target DUP opponent? 0= IF DROP 0 THEN
   generate-moves-not-to delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: peaceful-move-eval  ( curr-eval -- curr-eval|better-eval )
   generate-peaceful-moves delete-fast-killer eval-moves weight-moves
   best-move-eval forget-move-weights forget-moves ;
: minimum-quiescence-move-eval  ( curr-eval -- curr-eval|better-eval )
   0 TO cutoff?
   opponent-move-target generate-cheapest-move-to #moves IF
      single-move-eval
   THEN  forget-moves ;

: ?stale-mate  ( eval -- eval|stale-mate-eval)
   think-depth curr-think-limit > 0= IF
      moves-exist? 0= IF   DROP stale-mate THEN
   THEN ;
: ?check/stale-mate  ( eval -- eval|stale-mate-eval)
   moves-exist? 0= IF
      DROP curr-check? IF check-mate-eval ELSE stale-mate THEN
   THEN ;

: only-aggression-hopeful?  ( -- flag ) \ my version of futility pruning
   curr-eval alpha 128 - <
   curr-check? 0= AND   \ this is important! (else checking could save pieces)
   think-depth think-limit 2 - < 0= AND ;
: (eval-position-recursive)  ( -- eval )
   #nodes 1+ TO #nodes
   only-aggression-hopeful? IF
      \ futility pruning: if I'm bad, only try good captures and checking moves
      curr-eval capture-killer-move-eval cutoff? IF EXIT THEN
      full-quiescence-move-eval cutoff? IF EXIT THEN
      ?check/stale-mate EXIT
   THEN
   -infinity
   null-move-eval cutoff? IF EXIT THEN
   killer-move-eval cutoff? IF EXIT THEN
   target-move-eval cutoff? IF EXIT THEN
   not-target-move-eval cutoff? IF EXIT THEN   ?check/stale-mate ;
: eval>tt  ( eval1 -- eval2 ) \ convert evaluation for storing in ttable
   DUP -infinity check-mate WITHIN IF  think-depth - EXIT THEN
   DUP [ check-mate NEGATE ] LITERAL +infinity
   WITHIN IF  think-depth + EXIT THEN ;
: tt>eval  ( eval1 -- eval2 ) \ convert evaluation from ttable
   DUP -infinity 1+ check-mate WITHIN IF  think-depth + EXIT THEN
   DUP [ check-mate NEGATE ] LITERAL +infinity
   WITHIN IF  think-depth - EXIT THEN ;
: store-evaluation  ( eval -- )
   aborting? IF  DROP EXIT THEN
   tt-store ?DUP IF   >R
      horizon-distance R@ ttentry-distance !
      DUP alpha > IF DUP eval>tt ELSE -infinity THEN   R@ ttentry-low !
      DUP beta <  IF DUP eval>tt ELSE +infinity THEN   R> ttentry-up !
   THEN DROP ;
: eval-position-with-memory  ( -- eval )
   tt-retrieve ?DUP IF
      DUP ttentry-up @ undefined <>
      OVER ttentry-distance @ horizon-distance < 0= AND IF  >R
	 R@ ttentry-low @ tt>eval DUP beta < 0= IF  R> DROP EXIT THEN
	 R> ttentry-up @ tt>eval DUP alpha > 0= IF  NIP EXIT THEN
	 2DUP = IF  DROP EXIT THEN
	 alpha beta 2>R
	 beta MIN TO beta   alpha MAX TO alpha
	 (eval-position-recursive) DUP store-evaluation
	 2R> TO beta TO alpha EXIT
      THEN DROP
   THEN (eval-position-recursive) DUP store-evaluation ;
' (eval-position-recursive) IS eval-position-recursive
' eval-position-with-memory IS eval-position-recursive

: (quiescence-eval-nocheck)  ( -- eval ) \ used if not in check
   #nodes 1+ TO #nodes
   curr-eval DUP is-cutoff? IF  ?stale-mate EXIT THEN
   DUP alpha MAX TO alpha
   capture-killer-move-eval cutoff? IF  EXIT THEN
   quiescence-promotion-eval cutoff? IF  EXIT THEN 
   quiescence-capture-eval cutoff? IF  EXIT THEN
   ?stale-mate ;
: (quiescence-eval-check)  ( -- eval ) \ used if in check
   #nodes 1+ TO #nodes
   curr-eval DUP is-cutoff? IF  ?check/stale-mate EXIT THEN
   DUP alpha MAX TO alpha
   capture-killer-move-eval cutoff? IF  EXIT THEN
   quiescence-capture-eval cutoff? IF  EXIT THEN
   ?check/stale-mate ;
 
\   think-depth full-quiescence-limit 1+ < IF
\      (eval-position-recursive)	\ respawn new 1ply search to get out of check
\   ELSE
\      #nodes 1+ TO #nodes	\ else do a simple quiescence search
\      curr-eval DUP alpha MAX TO alpha
\      DUP is-cutoff? 0= IF
\ 	 think-depth simple-quiescence-limit < IF
\ 	    capture-killer-move-eval cutoff? 0= IF
\ 	       quiescence-capture-eval 
\ 	    THEN
\ 	 ELSE
\ 	    minimum-quiescence-move-eval
\ 	 THEN 
\      THEN
\   THEN ;

: (quiescence-eval-position)  ( -- eval )
   curr-check?
   IF (quiescence-eval-check) ELSE (quiescence-eval-nocheck) THEN ;
' (quiescence-eval-position) IS quiescence-eval-position

: ?abort-search  ( -- ) \ check for timeout etc
   abort-search? think-depth 1 > AND TO aborting? ;
: ?check-extension  ( -- )
   curr-check? IF
      think-depth think-limit < IF 2 ELSE 1 THEN
      curr-think-limit + think-extend MIN TO curr-think-limit
   THEN ;
: (eval-move-recursive)  ( move-index|-1 -- eval )
   check-mate-eval NEGATE 1- DUP alpha > 0= IF NIP EXIT ELSE DROP THEN
   aborting? IF  DROP -infinity EXIT THEN
   alpha beta 2>R switch-alpha-beta
   on-principal-variation? curr-think-limit 2>R
   DUP ?on-principal-variation
   ?check-extension
   DUP 0< IF
      DROP get-null-move
   ELSE DUP ?set-move-eval get-move THEN
   do-move-undo-info
   remembering-position? IF   stale-mate	\ repetition: draw
   ELSE
      +depth   terminate-principal-variation
      recurse? IF
	 ?abort-search
	 eval-position-recursive
      ELSE
	 quiescence-eval-position
      THEN -depth NEGATE
   THEN
   >R undo-move R>
   2R> TO curr-think-limit TO on-principal-variation?
   2R> TO beta TO alpha ;
' (eval-move-recursive) IS eval-move-recursive

: setup-think-limit  ( limit -- )
   DUP TO think-limit TO curr-think-limit
   think-limit 2 * TO think-extend
   ( tt-expired) ;

: show-thoughts  ( limit -- )
   setup-think-limit abort-time >R -1 TO abort-time
   -infinity TO alpha  +infinity TO beta
   0 TO aborting?
   init-killers
   generate-moves eval-moves
   #moves 0 ?DO
      0 TO think-depth
      CR I 3 .R SPACE
      clear-principal-variation
      I eval-move-recursive .
      I is-principal-move
      print-principal-variation
   LOOP
   forget-moves
   R> TO abort-time ;

0 VALUE start-time
0 VALUE root-alpha
0 VALUE root-beta

: (abort-search?)  ( -- flag )
   abort-time 0< IF  FALSE
   ELSE  secs start-time - abort-time > THEN ;
' (abort-search?) IS abort-search?
: root-search  ( -- eval )
   -infinity
   root-alpha -infinity MAX TO alpha
   root-beta +infinity MIN TO beta
   TRUE TO on-principal-variation?
   FALSE TO aborting?
   root-alpha . ." - " root-beta .
   sort-moves-by-weight
   #moves 0 ?DO  -infinity I move-weight ! LOOP
   best-move find-move-x IF  first-move THEN
   #moves 0 ?DO
      0 TO think-depth
\      I display-move SPACE
      I eval-move-recursive
      DUP beta < 0=  OVER root-beta < AND IF	\ fail high -> research
	 root-beta TO beta   I SWAP adjust-alpha
	 I eval-move-recursive
	 I OVER adjust-alpha
      ELSE DUP alpha >  OVER alpha = I 0= AND OR IF
	 I OVER adjust-alpha
      THEN THEN
      DUP root-beta < 0= IF  NIP LEAVE THEN
      DUP I move-weight !
      think-limit 2 > IF  alpha 1+ TO beta THEN
      MAX
   LOOP
   aborting? IF  ." aborted "
   ELSE  DUP . print-principal-variation SPACE THEN ;

: calculate-move  ( -- move-index )
   ?game-ended check? TO curr-check?
   total-eval TO curr-abs-eval
   generate-moves eval-moves weight-moves init-killers
   secs TO start-time
   curr-eval TO alpha
   clear-principal-variation
   max-think-limit 1+ 1 ?DO
      CR I .
      I setup-think-limit
      ?use-null-moves
      think-limit 3 < IF
	 -infinity TO root-alpha
	 +infinity TO root-beta
      ELSE
	 alpha 30 - to root-alpha
	 alpha 30 + TO root-beta
      THEN
      root-search
      TRUE TO on-principal-variation?
      DUP root-alpha > 0=  aborting? 0= AND IF
	 CR I . ." <"
	 TO root-beta
	 -infinity TO root-alpha root-search
      ELSE DUP root-beta < 0=  aborting? 0= AND IF
	 CR I . ." >"
	 TO root-alpha
	 +infinity TO root-beta root-search
      THEN THEN DROP
      aborting? IF LEAVE THEN
      save-best-move
   LOOP
   forget-move-weights forget-moves
   get-saved-best-move find-move-x 0= ABORT" Move not found??" ;

