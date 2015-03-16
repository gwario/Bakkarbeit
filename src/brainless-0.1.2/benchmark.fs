\ Brainless -- a chess-playing program written in ANS Forth
\
\ Copyright (C) 2010 David Kühling <dvdkhlng TA gmx TOD de> 
\
\ You may use this program under the GPLv3 or later.  NO WARRANTY.
\
\ Benchmark code

S" brainless.fs" INCLUDED

: millisecs  ( -- u )
   [-DEF?] cputime [IF]
      secs 1000 *
   [ELSE]
      cputime 2DROP 1000 UM/MOD NIP
   [THEN] ;

: position1  ( -- )
   CR ." Position1: initial chess board"
   CR
   init-board  ;
: position2  ( -- )
   CR ." Position2: chess board after "
   init-board
   e2 e4 m  e7 e5 m
   b1 c3 m  g8 f6 m
   g1 f3 m  b8 c6 m CR ;
: position3  ( -- )
   CR ." Position3: David&Clemens vs GForth. Berlin, 2000 ;-)"
   CR ." Black to play"
   CR 
   clear   b set-party
   w rook b1 add    w queen d1 add    w rook f1 add    w king g1 add
   w pawn a2 add    w pawn c2 add     w bishop d2 add  w pawn f2 add
   w pawn h2 add
   w pawn d3 add    w knight f3 add   w pawn g3 add    w bishop h3 add
   w knight c4 add  w pawn e4 add
   b knight c5 add  b knight c6 add   b pawn d6 add    b pawn e6 add
   b queen f6 add
   b pawn a7 add    b pawn b7 add     b pawn c7 add    b bishop e7 add
   b pawn f7 add    b pawn g7 add     b pawn h7 add
   b rook a8 add    b bishop c8 add   b rook f8 add    b king g8 add ;
: position4  ( -- )
   CR ." Position4: Study by Dr. Lasker and Reichhel" 
   CR ." White to play and win:"
   CR ."   1. Ka1-b1 Ka7-b7 2. Kb1-c1"
   CR 
   clear   w set-party
   w king a1 add
   w pawn a4 add    w pawn d4 add     w pawn f4 add    w pawn d5 add
   b king a7 add    b pawn a5 add     b pawn d6 add    b pawn f5 add ;
: position5  ( -- )
   CR ." Position5: Marco vs Maroczy. Paris, 1900"
   CR ." Black to play and win:"
   CR ."   1. ... Nb2-d3 2. Nc1-b3 Nd3-e1+ 3. Kc2-d1 Ke3-d3 4. Kd1xe1 Kd3xc3"
   CR 
   clear  b set-party
   w king c2 add    w knight c1 add   w pawn c3 add    w pawn b4 add
   w pawn d4 add
   b king e3 add    b knight b2 add   b pawn a3 add    b pawn b5 add
   b pawn c6 add    b pawn d5 add ;
: position6  ( -- )
   CR ." Position6: GForth vs GNUChess. Berlin, Nov 19 2000"
   CR ." white (GForth) to play"
   CR
   clear   w set-party
   b rook a8 add    b queen d8 add    b rook f8 add    b king g8 add
   b pawn c7 add    b pawn d7 add     b pawn f7 add    b pawn g7 add
   b pawn h7 add
   b pawn a6 add    b bishop d6 add   b knight f6 add
   w pawn d5 add
   b pawn b4 add    b pawn e4 add
   w queen d3 add   w knight f3 add
   w pawn a2 add    w pawn b2 add     w pawn c2 add    w pawn d2 add
   w pawn f2 add    w pawn g2 add     w pawn h2 add
   w rook a1 add    w bishop c1 add   w king e1 add    w rook h1 add ;
: position7  ( -- )
   CR ." Position7: GForth vs GNUChess. Berlin, Nov 19 2000 (2)"
   CR ." black(GNUChess) to play"
   CR 
   clear   b set-party
   b rook a8 add    b rook f8 add     b king g8 add
   b pawn c7 add    b pawn d7 add     b pawn f7 add    b pawn g7 add
   b pawn h7 add
   b pawn a6 add    b bishop d6 add   b knight f6 add
   w pawn d5 add
   b pawn b4 add    b queen d4 add
   w queen f3 add   w pawn h3 add
   w pawn a2 add    w pawn b2 add     w pawn c2 add    w pawn d2 add
   w pawn f2 add    w pawn g2 add
   w rook a1 add    w bishop c1 add   w king d1 add    w rook e1 add ;
: position8  ( -- )
   CR ." Position8: GForth vs GNUChess. Berlin, Nov 19 2000 (3)"
   CR ." white(GForth) to play"
   CR 
   clear   w set-party
   b rook a8 add    b king g8 add
   b pawn c7 add    b pawn d7 add    b pawn f7 add    b pawn g7 add
   b pawn h7 add
   b pawn a6 add    b bishop d6 add  b knight f6 add
   w pawn d5 add
   b pawn b4 add    b queen d4 add
   w pawn d3 add    w queen f3 add   w pawn h3 add
   w pawn a2 add    w pawn b2 add    w pawn c2 add    w pawn f2 add
   w pawn g2 add
   w rook a1 add    w bishop c1 add   w king d1 add    b rook e1 add ;
: position9  ( -- )
   CR ." Position9: GForth vs GNUChess. Berlin, Nov 19 2000 (4)"
   CR ." white(GForth) to play"
   CR 
   clear   w set-party
   b king g8 add
   b pawn c7 add    b pawn d7 add    b pawn f7 add    b pawn g7 add
   b pawn h7 add    b pawn a6 add    b bishop d6 add  b knight f6 add
   b pawn b4 add    w queen c4 add
   w pawn d3 add    w pawn g3 add    w pawn h3 add
   w pawn a2 add    w pawn b2 add    w pawn c2 add    b rook e2 add
   w pawn f2 add    w king g2 add
   w rook a1 add    w bishop c1 add  b queen e1 add ;
: position10  ( -- )
   CR ." Position10: GForth vs GNUChess. Berlin, Nov 19 2000 (5)"
   CR ." white(GForth) to play"
   CR 
   clear   w set-party
   b king g8 add
   b pawn c7 add    b pawn d7 add    b pawn f7 add    b pawn g7 add
   b pawn h7 add    b pawn a6 add    b bishop d6 add  b knight f6 add
   b pawn b4 add    
   w pawn d3 add    b queen e3 add   w pawn g3 add    w pawn h3 add
   w pawn a2 add    w pawn b2 add    b rook c2 add
   w pawn f2 add    w king g2 add
   w rook a1 add    w bishop c1 add ;
: position11  ( -- )
   CR ." Position11: GForth vs GNUChess. Berlin, Nov 19 2000 (6)"
   CR ." black(GNUChess) to play and win in 4"
   CR 
   clear   b set-party
   b king g8 add
   b pawn d7 add    b pawn g7 add    b pawn h7 add
   b pawn a6 add    b pawn c6 add
   b knight d5 add  b pawn f5 add    w king h5 add
   b bishop d4 add  w pawn h4 add
   w pawn a3 add    b pawn b3 add    w pawn d3 add    w pawn g3 add
   w pawn b2 add    b rook f2 add
   w rook b1 add    w bishop c1 add ;
: position12  ( -- )
   CR ." Position12: GForth vs GNUChess (2). Berlin, Nov 19 2000 ()"
   CR ." white(GForth) to play"
   CR 
   clear   w set-party
   b pawn a7 add    b pawn f7 add
   w pawn f6 add
   w pawn a3 add    b pawn e3 add    b king f3 add    b pawn h3 add
   w king f1 add ;
: position13  ( -- )
   CR ." Position13: GForth vs GForth. Berlin, Dec 10 2000"
   CR ." white to play"
   S" 3r1rk1/B1p2ppp/1p6/3Np1Pn/3nP3/5P2/PP5P/3R1RK1 w - h5 bm 1; id 1;"
   epd>position ;

: <stat  ( -- evals nodes msecs )   #evals #nodes millisecs ;
: stat>  ( evals nodes msecs -- )
   CR   millisecs SWAP - DUP >R . ." ms, "
   #nodes SWAP - DUP . ." nodes (" 1000 R@ 1 MAX */ . ." Hz), "
   #evals SWAP - DUP . ." evals (" 1000 R@ 1 MAX */ . ." Hz) "
   R> DROP ;

: benchmark-eval  ( -- )
   generate-moves
   -fly-eval ." Static evaluation:"
   <stat 8000 0 DO eval-moves #moves +LOOP stat>
   +fly-eval CR ." On-the-fly single move evaluation:"
   <stat 30000 0 DO
      #moves 0 DO I get-move eval-move DROP LOOP
   #moves +LOOP stat>
   CR ." On-the-fly move-list evaluation:"
   <stat 40000 0 DO eval-moves #moves +LOOP stat>
   forget-moves ;
: benchmark-movegen  ( -- )
   <stat
   30000 0 DO
      generate-moves #moves #evals + TO #evals
      #moves forget-moves
   +LOOP
   stat> ;
   
: benchmark1  ( -- )
   small-board \ clear-killer-hist
   <stat
   -1 TO abort-time
   5 to max-think-limit position1 <stat cm stat>
   4 to max-think-limit position2 <stat cm stat>
   4 to max-think-limit position3 <stat cm stat>
   10 to max-think-limit position4 <stat cm stat>
   6 to max-think-limit position5 <stat cm stat>
   5 to max-think-limit position6 <stat cm stat>
   4 to max-think-limit position7 <stat cm stat>
   4 to max-think-limit position8 <stat cm stat>
   5 to max-think-limit position9 <stat cm stat>
   5 to max-think-limit position10 <stat cm stat>
   4 to max-think-limit position11 <stat cm stat>
   CR ." ---------------------------- " CR
   stat> ;
: benchmark2  ( -- )
   position1 benchmark-eval
   position2 benchmark-eval
   position3 benchmark-eval
   position4 benchmark-eval
   position5 benchmark-eval ;
: benchmark3  ( -- )
   <stat
   position1 benchmark-movegen
   position2 benchmark-movegen
   position3 benchmark-movegen
   position4 benchmark-movegen
   position5 benchmark-movegen
   CR ." ---------------------------- "
   stat> ;
: benchmark  ( -- )
   benchmark1 ; \ benchmark2 benchmark3 ;





