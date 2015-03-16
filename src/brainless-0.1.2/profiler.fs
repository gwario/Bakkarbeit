
struct
   cell% field list-next
end-struct list%

list%
   cell% field def-name
   cell% field def-xt
   cell% field def-count
   double% field def-ms
end-struct def%

: next  ( a-addr1 -- a-addr2 )  list-next @ ;
: link  ( a-addr1 a-addr2 -- ) \ link a-addr1 to a-addr2
   2DUP @ SWAP list-next !   ! ;

: sort-list  ( xt a-addr -- ) \ sort list a-addr using xt
   { xt list }  
   BEGIN
      TRUE { finished? }
      list BEGIN ?DUP WHILE   { item-ptr }
	 item-ptr @ ?DUP IF
	    DUP list-next @   { item next-item }
	    next-item IF
	       item next-item xt EXECUTE IF
		  next-item item-ptr !
		  next-item list-next @   { continuation }
		  item next-item list-next !
		  continuation item list-next !
		  FALSE TO finished?
	       THEN
	    THEN
	 THEN
	 item-ptr @  list-next
      REPEAT
   finished? UNTIL ;

VARIABLE def-list	0 def-list !
0 VALUE curr-def
0 VALUE noname-def?

: new-def  ( c-addr u -- a-addr )  
   def% %allot DUP >R def-list link
   0 R@ def-count !
   0. R@ def-ms 2!
   HERE -ROT string, R@ def-name !   R> ;
: does>profile DOES> ( def-addr -- )
   @ DUP >R def-xt @
   cputime 2DROP 2>R  EXECUTE
   cputime 2DROP 2R> D-  R@ def-ms DUP >R 2@ D+  R> 2!
   1 R> def-count +! ;
: p:  ( "name" -- xt colon-sys )
   BL SWORD new-def TO curr-def  FALSE TO noname-def? :NONAME ;
: p:noname  ( -- xt colon-sys )
   S" (noname)" new-def TO curr-def  TRUE TO noname-def?  :NONAME ;
: ;p  ( colon-sys -- )
   POSTPONE ; noname-def? IF DUP THEN curr-def def-xt !
   curr-def def-name @ COUNT
   nextname CREATE curr-def , does>profile ; IMMEDIATE
: n:  ( "name" -- colon-sys )  : ;
: ;n  ( colons-sys -- )  POSTPONE ; ;

: sort-by-count  ( a-addr1 a-addr2 -- )
   def-count @ SWAP def-count @ > ;
: sort-by-ms  ( a-addr1 a-addr2 -- )
   def-ms 2@ ROT def-ms 2@ D> ;

: show-stat  ( -- )
   ['] sort-by-ms def-list sort-list
   def-list @ BEGIN ?DUP WHILE >R
      CR R@ def-name @ COUNT DUP >R TYPE   32 R> - SPACES
      R@ def-count @  8 .R
      R@ def-ms 2@ 12 D.R
      R> next
   REPEAT ;

n: :  p: ;
n: :NONAME  p:noname ; 
n: ;  POSTPONE ;p ; IMMEDIATE


   
   