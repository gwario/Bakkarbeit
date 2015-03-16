\
\ Some DEFERs and other data for tree search, which are used before search.fs
\ is loaded
\

64 CONSTANT max-think-depth	\ absolute maximum think depth

0 VALUE think-limit		\ current think depth limit during iterating
0 VALUE curr-think-limit	\ current think limit (including extensions)
0 VALUE think-extend		\ maximum think-limit for search extension
max-think-depth VALUE max-think-limit	\ maximum think limit during iterating
10 VALUE abort-time		\ time for thinking
0 VALUE think-depth		\ current think depth (plies)
0 VALUE aborting?		\ set by time control routine to exit recursion

0 VALUE alpha
0 VALUE beta
0 VALUE on-principal-variation?

0 VALUE #nodes

DEFER eval-move-recursive  ( move-index -- eval )
DEFER quiescence-eval-position  ( move-index -- eval )
DEFER eval-position-recursive  ( -- eval )

DEFER abort-search?  ( -- flag ) \ used to implement time control

: recurse?  ( -- flag ) 	s" recurse?" print-def think-depth curr-think-limit < ;
: +depth  ( -- ) 	s" +depth" print-def think-depth 1+ TO think-depth ;
: -depth  ( -- ) 	s" -depth" print-def think-depth 1- TO think-depth ;
: horizon-distance  ( -- ) 	s" horizon-distance" print-def curr-think-limit think-depth - 0 MAX ;
