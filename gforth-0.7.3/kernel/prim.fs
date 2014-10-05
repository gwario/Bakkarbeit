\ run-time routine headers

\ Copyright (C) 1997,1998,1999,2000,2003,2007 Free Software Foundation, Inc.

\ This file is part of Gforth.

\ Gforth is free software; you can redistribute it and/or
\ modify it under the terms of the GNU General Public License
\ as published by the Free Software Foundation, either version 3
\ of the License, or (at your option) any later version.

\ This program is distributed in the hope that it will be useful,
\ but WITHOUT ANY WARRANTY; without even the implied warranty of
\ MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
\ GNU General Public License for more details.

\ You should have received a copy of the GNU General Public License
\ along with this program. If not, see http://www.gnu.org/licenses/.

\ -2 Alias: :docol
\ -3 Alias: :docon
\ -4 Alias: :dovar
\ -5 Alias: :douser
\ -6 Alias: :dodefer
\ -7 Alias: :dofield
\ -8 Alias: :dodoes
\ -9 Alias: :doesjump
 0 [if]
 [endif]
9 groupadd
group  control
: noop  ( -- )
 ;

: perform  ( a_addr -- )
 @ execute ;

: unloop  ( R:w1 R:w2 -- )
 r> rdrop rdrop >r ;

8 groupadd
has? glocals [IF]
1 groupadd
[THEN]
: branch  ( #a_target -- )
 r> @ >r ;

: ?branch  ( #a_target f -- )
 0= dup 0=          \ !f f
 r> tuck cell+      \ !f branchoffset f IP+
 and -rot @ and or  \ f&IP+|!f&branch
 >r ;

2 groupadd
has? glocals [IF]
1 groupadd
[THEN]
0 groupadd
has? xconds [IF]
2 groupadd
[THEN]
has? skiploopprims 0= [IF]
: (next)  ( #a_target R:n1 -- R:n2 )
 r> r> dup 1- >r
 IF @ >r ELSE cell+ >r THEN ;

1 groupadd
has? glocals [IF]
1 groupadd
[THEN]
: (loop)  ( #a_target R:nlimit R:n1 -- R:nlimit R:n2 )
 r> r> 1+ r> 2dup =
 IF >r 1- >r cell+ >r
 ELSE >r >r @ >r THEN ;

1 groupadd
has? glocals [IF]
1 groupadd
[THEN]
: (+loop)  ( #a_target n R:nlimit R:n1 -- R:nlimit R:n2 )
 r> swap
 r> r> 2dup - >r
 2 pick r@ + r@ xor 0< 0=
 3 pick r> xor 0< 0= or
 IF    >r + >r @ >r
 ELSE  >r >r drop cell+ >r THEN ;

1 groupadd
has? glocals [IF]
1 groupadd
[THEN]
0 groupadd
has? xconds [IF]
1 groupadd
has? glocals [IF]
1 groupadd
[THEN]
1 groupadd
has? glocals [IF]
1 groupadd
[THEN]
0 groupadd
[THEN]
: (for)  ( ncount -- R:nlimit R:ncount )
 r> swap 0 >r >r >r ;

: (do)  ( nlimit nstart -- R:nlimit R:nstart )
 r> swap rot >r >r >r ;

: (?do)  ( #a_target nlimit nstart -- R:nlimit R:nstart )
  2dup =
  IF   r> swap rot >r >r
       @ >r
  ELSE r> swap rot >r >r
       cell+ >r
  THEN ;				\ --> CORE-EXT

3 groupadd
has? xconds [IF]
: (+do)  ( #a_target nlimit nstart -- R:nlimit R:nstart )
 swap 2dup
 r> swap >r swap >r
 >=
 IF
     @
 ELSE
     cell+
 THEN  >r ;

: (u+do)  ( #a_target ulimit ustart -- R:ulimit R:ustart )
 swap 2dup
 r> swap >r swap >r
 u>=
 IF
     @
 ELSE
     cell+
 THEN  >r ;

: (-do)  ( #a_target nlimit nstart -- R:nlimit R:nstart )
 swap 2dup
 r> swap >r swap >r
 <=
 IF
     @
 ELSE
     cell+
 THEN  >r ;

: (u-do)  ( #a_target ulimit ustart -- R:ulimit R:ustart )
 swap 2dup
 r> swap >r swap >r
 u<=
 IF
     @
 ELSE
     cell+
 THEN  >r ;

4 groupadd
[THEN]
: i  ( R:n -- R:n n )
\ rp@ cell+ @ ;
  r> r> tuck >r >r ;

: i'  ( R:w R:w2 -- R:w R:w2 w )
\ rp@ cell+ cell+ @ ;
  r> r> r> dup itmp ! >r >r >r itmp @ ;
variable itmp

: j  ( R:w R:w1 R:w2 -- w R:w R:w1 R:w2 )
\ rp@ cell+ cell+ cell+ @ ;
  r> r> r> r> dup itmp ! >r >r >r >r itmp @ ;
[IFUNDEF] itmp variable itmp [THEN]

: k  ( R:w R:w1 R:w2 R:w3 R:w4 -- w R:w R:w1 R:w2 R:w3 R:w4 )
\ rp@ [ 5 cells ] Literal + @ ;
  r> r> r> r> r> r> dup itmp ! >r >r >r >r >r >r itmp @ ;
[IFUNDEF] itmp variable itmp [THEN]

[THEN]
4 groupadd
group  strings
: move  ( c_from c_to ucount -- )
 >r 2dup u< IF r> cmove> ELSE r> cmove THEN ;

: cmove  ( c_from c_to u -- )
 bounds ?DO  dup c@ I c! 1+  LOOP  drop ;

: cmove>  ( c_from c_to u -- )
 dup 0= IF  drop 2drop exit  THEN
 rot over + -rot bounds swap 1-
 DO  1- dup c@ I c!  -1 +LOOP  drop ;

: fill  ( c_addr u c -- )
 -rot bounds
 ?DO  dup I c!  LOOP  drop ;

: compare  ( c_addr1 u1 c_addr2 u2 -- n )
 rot 2dup swap - >r min swap -text dup
 IF  rdrop  ELSE  drop r> sgn  THEN ;
: -text ( c_addr1 u c_addr2 -- n )
 swap bounds
 ?DO  dup c@ I c@ = WHILE  1+  LOOP  drop 0
 ELSE  c@ I c@ - unloop  THEN  sgn ;
: sgn ( n -- -1/0/1 )
 dup 0= IF EXIT THEN  0< 2* 1+ ;

: toupper  ( c1 -- c2 )
 dup [char] a - [ char z char a - 1 + ] Literal u<  bl and - ;

: /string  ( c_addr1 u1 n -- c_addr2 u2 )
 tuck - >r + r> dup 0< IF  - 0  THEN ;

8 groupadd
group  arith
: lit  ( #w -- w )
 r> dup @ swap cell+ >r ;

: under+  ( n1 n2 n3 -- n n2 )
 rot + swap ;

: --prim  ( n1 n2 -- n )
 negate + ;

: negate  ( n1 -- n2 )
 invert 1+ ;

: 1+  ( n1 -- n2 )
 1 + ;

: 1-  ( n1 -- n2 )
 1 - ;

: max  ( n1 n2 -- n )
 2dup < IF swap THEN drop ;

: min  ( n1 n2 -- n )
 2dup > IF swap THEN drop ;

: abs  ( n -- u )
 dup 0< IF negate THEN ;

: *  ( n1 n2 -- n )
 um* drop ;

: /  ( n1 n2 -- n )
 /mod nip ;

: mod  ( n1 n2 -- n )
 /mod drop ;

: /mod  ( n1 n2 -- n3 n4 )
 >r s>d r> fm/mod ;

: */mod  ( n1 n2 n3 -- n4 n5 )
 >r m* r> fm/mod ;

: */  ( n1 n2 n3 -- n4 )
 */mod nip ;

: 2*  ( n1 -- n2 )
 dup + ;

: 2/  ( n1 -- n2 )
 dup MINI and IF 1 ELSE 0 THEN
 [ bits/char cell * 1- ] literal 
 0 DO 2* swap dup 2* >r MINI and 
     IF 1 ELSE 0 THEN or r> swap
 LOOP nip ;

: fm/mod  ( d1 n1 -- n2 n3 )
 dup >r dup 0< IF  negate >r dnegate r>  THEN
 over       0< IF  tuck + swap  THEN
 um/mod
 r> 0< IF  swap negate swap  THEN ;

: sm/rem  ( d1 n1 -- n2 n3 )
 over >r dup >r abs -rot
 dabs rot um/mod
 r> r@ xor 0< IF       negate       THEN
 r>        0< IF  swap negate swap  THEN ;

: m*  ( n1 n2 -- d )
 2dup      0< and >r
 2dup swap 0< and >r
 um* r> - r> - ;

: um*  ( u1 u2 -- ud )
   0 -rot dup [ 8 cells ] literal -
   DO
	dup 0< I' and d2*+ drop
   LOOP ;
: d2*+ ( ud n -- ud+n c )
   over MINI
   and >r >r 2dup d+ swap r> + swap r> ;

: um/mod  ( ud u1 -- u2 u3 )
   0 swap [ 8 cells 1 + ] literal 0
   ?DO /modstep
   LOOP drop swap 1 rshift or swap ;
: /modstep ( ud c R: u -- ud-?u c R: u )
   >r over r@ u< 0= or IF r@ - 1 ELSE 0 THEN  d2*+ r> ;
: d2*+ ( ud n -- ud+n c )
   over MINI
   and >r >r 2dup d+ swap r> + swap r> ;

: m+  ( d1 n -- d2 )
 s>d d+ ;

: d+  ( d1 d2 -- d )
 rot + >r tuck + swap over u> r> swap - ;

: d-  ( d1 d2 -- d )
 dnegate d+ ;

: dnegate  ( d1 -- d2 )
 invert swap negate tuck 0= - ;

: d2*  ( d1 -- d2 )
 2dup d+ ;

: d2/  ( d1 -- d2 )
 dup 1 and >r 2/ swap 2/ [ 1 8 cells 1- lshift 1- ] Literal and
 r> IF  [ 1 8 cells 1- lshift ] Literal + THEN  swap ;

: or  ( w1 w2 -- w )
 invert swap invert and invert ;

: invert  ( w1 -- w2 )
 MAXU xor ;

: rshift  ( u1 n -- u2 )
    0 ?DO 2/ MAXI and LOOP ;

: lshift  ( u1 n -- u2 )
    0 ?DO 2* LOOP ;

36 groupadd
group  compare
: 0=  ( n -- f )
    [ char 0x char 0 = [IF]
	] IF false ELSE true THEN [
    [ELSE]
	] xor 0= [
    [THEN] ] ;

: 0<>  ( n -- f )
    [ char 0x char 0 = [IF]
	] IF true ELSE false THEN [
    [ELSE]
	] xor 0<> [
    [THEN] ] ;

: 0<  ( n -- f )
    [ char 0x char 0 = [IF]
	] MINI and 0<> [
    [ELSE] char 0x char u = [IF]
	]   2dup xor 0<  IF nip ELSE - THEN 0<  [
	[ELSE]
	    ] MINI xor >r MINI xor r> u< [
	[THEN]
    [THEN] ] ;

: 0>  ( n -- f )
    [ char 0x char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    0< ;

: 0<=  ( n -- f )
    0> 0= ;

: 0>=  ( n -- f )
    [ char 0x char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    0<= ;

: =  ( n1 n2 -- f )
    [ char x char 0 = [IF]
	] IF false ELSE true THEN [
    [ELSE]
	] xor 0= [
    [THEN] ] ;

: <>  ( n1 n2 -- f )
    [ char x char 0 = [IF]
	] IF true ELSE false THEN [
    [ELSE]
	] xor 0<> [
    [THEN] ] ;

: <  ( n1 n2 -- f )
    [ char x char 0 = [IF]
	] MINI and 0<> [
    [ELSE] char x char u = [IF]
	]   2dup xor 0<  IF nip ELSE - THEN 0<  [
	[ELSE]
	    ] MINI xor >r MINI xor r> u< [
	[THEN]
    [THEN] ] ;

: >  ( n1 n2 -- f )
    [ char x char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    < ;

: <=  ( n1 n2 -- f )
    > 0= ;

: >=  ( n1 n2 -- f )
    [ char x char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    <= ;

: u=  ( u1 u2 -- f )
    [ char ux char 0 = [IF]
	] IF false ELSE true THEN [
    [ELSE]
	] xor 0= [
    [THEN] ] ;

: u<>  ( u1 u2 -- f )
    [ char ux char 0 = [IF]
	] IF true ELSE false THEN [
    [ELSE]
	] xor 0<> [
    [THEN] ] ;

: u<  ( u1 u2 -- f )
    [ char ux char 0 = [IF]
	] MINI and 0<> [
    [ELSE] char ux char u = [IF]
	]   2dup xor 0<  IF nip ELSE - THEN 0<  [
	[ELSE]
	    ] MINI xor >r MINI xor r> u< [
	[THEN]
    [THEN] ] ;

: u>  ( u1 u2 -- f )
    [ char ux char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    u< ;

: u<=  ( u1 u2 -- f )
    u> 0= ;

: u>=  ( u1 u2 -- f )
    [ char ux char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    u<= ;

18 groupadd
has? dcomps [IF]
18 groupadd
[THEN]
: within  ( u1 u2 u3 -- f )
 over - >r - r> u< ;

1 groupadd
group  stack
: up!  ( a_addr -- )
 up ! ;
Variable UP

6 groupadd
has? floating [IF]
2 groupadd
[THEN]
: >r  ( w -- R:w )
 (>r) ;
: (>r)  rp@ cell+ @ rp@ ! rp@ cell+ ! ;

: r>  ( R:w -- w )
 rp@ cell+ @ rp@ @ rp@ cell+ ! (rdrop) rp@ ! ;
Create (rdrop) ' ;s A,

: rdrop  ( R:w -- )
 r> r> drop >r ;

: 2>r  ( d -- R:d )
 swap r> swap >r swap >r >r ;

: 2r>  ( R:d -- d )
 r> r> swap r> swap >r swap ;

: 2r@  ( R:d -- R:d d )
 i' j ;

: 2rdrop  ( R:d -- )
 r> r> drop r> drop >r ;

: over  ( w1 w2 -- w1 w2 w1 )
 sp@ cell+ @ ;

: drop  ( w -- )
 IF THEN ;

: swap  ( w1 w2 -- w2 w1 )
 >r (swap) ! r> (swap) @ ;
Variable (swap)

: dup  ( w -- w w )
 sp@ @ ;

: rot  ( w1 w2 w3 -- w2 w3 w1 )
[ defined? (swap) [IF] ]
    (swap) ! (rot) ! >r (rot) @ (swap) @ r> ;
Variable (rot)
[ELSE] ]
    >r swap r> swap ;
[THEN]

: -rot  ( w1 w2 w3 -- w3 w1 w2 )
 rot rot ;

: nip  ( w1 w2 -- w2 )
 swap drop ;

: tuck  ( w1 w2 -- w2 w1 w2 )
 swap over ;

: ?dup  ( w -- S:... w )
 dup IF dup THEN ;

: pick  ( S:... u -- S:... w )
 1+ cells sp@ + @ ;

: 2drop  ( w1 w2 -- )
 drop drop ;

: 2dup  ( w1 w2 -- w1 w2 w1 w2 )
 over over ;

: 2over  ( w1 w2 w3 w4 -- w1 w2 w3 w4 w1 w2 )
 3 pick 3 pick ;

: 2swap  ( w1 w2 w3 w4 -- w3 w4 w1 w2 )
 rot >r rot r> ;

: 2rot  ( w1 w2 w3 w4 w5 w6 -- w3 w4 w5 w6 w1 w2 )
 >r >r 2swap r> r> 2swap ;

: 2nip  ( w1 w2 w3 w4 -- w3 w4 )
 2swap 2drop ;

: 2tuck  ( w1 w2 w3 w4 -- w3 w4 w1 w2 w3 w4 )
 2swap 2over ;

24 groupadd
group  memory
: +!  ( n a_addr -- )
 tuck @ + swap ! ;

: c@  ( c_addr -- c )
[ bigendian [IF] ]
    [ cell>bit 4 = [IF] ]
	dup [ 0 cell - ] Literal and @ swap 1 and
	IF  $FF and  ELSE  8>>  THEN  ;
    [ [ELSE] ]
	dup [ cell 1- ] literal and
	tuck - @ swap [ cell 1- ] literal xor
 	0 ?DO 8>> LOOP $FF and
    [ [THEN] ]
[ [ELSE] ]
    [ cell>bit 4 = [IF] ]
	dup [ 0 cell - ] Literal and @ swap 1 and
	IF  8>>  ELSE  $FF and  THEN
    [ [ELSE] ]
	dup [ cell  1- ] literal and 
	tuck - @ swap
	0 ?DO 8>> LOOP 255 and
    [ [THEN] ]
[ [THEN] ]
;
: 8>> 2/ 2/ 2/ 2/  2/ 2/ 2/ 2/ ;

: c!  ( c c_addr -- )
[ bigendian [IF] ]
    [ cell>bit 4 = [IF] ]
	tuck 1 and IF  $FF and  ELSE  8<<  THEN >r
	dup -2 and @ over 1 and cells masks + @ and
	r> or swap -2 and ! ;
	Create masks $00FF , $FF00 ,
    [ELSE] ]
	dup [ cell 1- ] literal and dup 
	[ cell 1- ] literal xor >r
	- dup @ $FF r@ 0 ?DO 8<< LOOP invert and
	rot $FF and r> 0 ?DO 8<< LOOP or swap ! ;
    [THEN]
[ELSE] ]
    [ cell>bit 4 = [IF] ]
	tuck 1 and IF  8<<  ELSE  $FF and  THEN >r
	dup -2 and @ over 1 and cells masks + @ and
	r> or swap -2 and ! ;
	Create masks $FF00 , $00FF ,
    [ELSE] ]
	dup [ cell 1- ] literal and dup >r
	- dup @ $FF r@ 0 ?DO 8<< LOOP invert and
	rot $FF and r> 0 ?DO 8<< LOOP or swap ! ;
    [THEN]
[THEN]
: 8<< 2* 2* 2* 2*  2* 2* 2* 2* ;

: 2!  ( w1 w2 a_addr -- )
 tuck ! cell+ ! ;

: 2@  ( a_addr -- w1 w2 )
 dup cell+ @ swap @ ;

: cell+  ( a_addr1 -- a_addr2 )
 cell + ;

: cells  ( n1 -- n2 )
 [ cell
 2/ dup [IF] ] 2* [ [THEN]
 2/ dup [IF] ] 2* [ [THEN]
 2/ dup [IF] ] 2* [ [THEN]
 2/ dup [IF] ] 2* [ [THEN]
 drop ] ;

: char+  ( c_addr1 -- c_addr2 )
 1+ ;

: (chars)  ( n1 -- n2 )
 ;

: count  ( c_addr1 -- c_addr2 u )
 dup 1+ swap c@ ;

13 groupadd
group  compiler
0 groupadd
has? f83headerstring [IF]
: (f83find)  ( c_addr u f83name1 -- f83name2 )
    BEGIN  dup WHILE  (find-samelen)  dup  WHILE
	>r 2dup r@ cell+ char+ capscomp  0=
	IF  2drop r>  EXIT  THEN
	r> @
    REPEAT  THEN  nip nip ;
: (find-samelen) ( u f83name1 -- u f83name2/0 )
    BEGIN  2dup cell+ c@ $1F and <> WHILE  @  dup 0= UNTIL  THEN ;
: capscomp ( c_addr1 u c_addr2 -- n )
 swap bounds
 ?DO  dup c@ I c@ <>
     IF  dup c@ toupper I c@ toupper =
     ELSE  true  THEN  WHILE  1+  LOOP  drop 0
 ELSE  c@ toupper I c@ toupper - unloop  THEN  sgn ;
: sgn ( n -- -1/0/1 )
 dup 0= IF EXIT THEN  0< 2* 1+ ;

1 groupadd
[ELSE]
: (listlfind)  ( c_addr u longname1 -- longname2 )
    BEGIN  dup WHILE  (findl-samelen)  dup  WHILE
	>r 2dup r@ cell+ cell+ capscomp  0=
	IF  2drop r>  EXIT  THEN
	r> @
    REPEAT  THEN  nip nip ;
: (findl-samelen) ( u longname1 -- u longname2/0 )
    BEGIN  2dup cell+ @ lcount-mask and <> WHILE  @  dup 0= UNTIL  THEN ;
: capscomp ( c_addr1 u c_addr2 -- n )
 swap bounds
 ?DO  dup c@ I c@ <>
     IF  dup c@ toupper I c@ toupper =
     ELSE  true  THEN  WHILE  1+  LOOP  drop 0
 ELSE  c@ toupper I c@ toupper - unloop  THEN  sgn ;
: sgn ( n -- -1/0/1 )
 dup 0= IF EXIT THEN  0< 2* 1+ ;

1 groupadd
has? hash [IF]
: (hashlfind)  ( c_addr u a_addr -- longname2 )
 BEGIN  dup  WHILE
        2@ >r >r dup r@ cell+ @ lcount-mask and =
        IF  2dup r@ cell+ cell+ capscomp 0=
	    IF  2drop r> rdrop  EXIT  THEN  THEN
	rdrop r>
 REPEAT nip nip ;

: (tablelfind)  ( c_addr u a_addr -- longname2 )
 BEGIN  dup  WHILE
        2@ >r >r dup r@ cell+ @ lcount-mask and =
        IF  2dup r@ cell+ cell+ -text 0=
	    IF  2drop r> rdrop  EXIT  THEN  THEN
	rdrop r>
 REPEAT nip nip ;
: -text ( c_addr1 u c_addr2 -- n )
 swap bounds
 ?DO  dup c@ I c@ = WHILE  1+  LOOP  drop 0
 ELSE  c@ I c@ - unloop  THEN  sgn ;
: sgn ( n -- -1/0/1 )
 dup 0= IF EXIT THEN  0< 2* 1+ ;

: (hashkey1)  ( c_addr u ubits -- ukey )
 dup rot-values + c@ over 1 swap lshift 1- >r
 tuck - 2swap r> 0 2swap bounds
 ?DO  dup 4 pick lshift swap 3 pick rshift or
      I c@ toupper xor
      over and  LOOP
 nip nip nip ;
Create rot-values
  5 c, 0 c, 1 c, 2 c, 3 c,  4 c, 5 c, 5 c, 5 c, 5 c,
  3 c, 5 c, 5 c, 5 c, 5 c,  7 c, 5 c, 5 c, 5 c, 5 c,
  7 c, 5 c, 5 c, 5 c, 5 c,  6 c, 5 c, 5 c, 5 c, 5 c,
  7 c, 5 c, 5 c,

3 groupadd
[THEN]
0 groupadd
[THEN]
: (parse-white)  ( c_addr1 u1 -- c_addr2 u2 )
 BEGIN  dup  WHILE  over c@ bl <=  WHILE  1 /string
 REPEAT  THEN  2dup
 BEGIN  dup  WHILE  over c@ bl >   WHILE  1 /string
 REPEAT  THEN  nip - ;

: aligned  ( c_addr -- a_addr )
 [ cell 1- ] Literal + [ -1 cells ] Literal and ;

: faligned  ( c_addr -- f_addr )
 [ 1 floats 1- ] Literal + [ -1 floats ] Literal and ;

has? standardthreading has? compiler and [IF]
: threading-method  ( -- n )
 1 ;

[THEN]
4 groupadd
group  hostos
5 groupadd
has? os [IF]
16 groupadd
[THEN]
0 groupadd
has? file [IF]
11 groupadd
[THEN]
2 groupadd
has? file [IF]
10 groupadd
[THEN]
: newline  ( -- c_addr u )
 "newline count ;
Create "newline e? crlf [IF] 2 c, $0D c, [ELSE] 1 c, [THEN] $0A c,

1 groupadd
has? os [IF]
2 groupadd
[THEN]
0 groupadd
has? floating [IF]
0 groupadd
group  floating
: f=  ( r1 r2 -- f )
    [ char fx char 0 = [IF]
	] IF false ELSE true THEN [
    [ELSE]
	] xor 0= [
    [THEN] ] ;

: f<>  ( r1 r2 -- f )
    [ char fx char 0 = [IF]
	] IF true ELSE false THEN [
    [ELSE]
	] xor 0<> [
    [THEN] ] ;

: f<  ( r1 r2 -- f )
    [ char fx char 0 = [IF]
	] MINI and 0<> [
    [ELSE] char fx char u = [IF]
	]   2dup xor 0<  IF nip ELSE - THEN 0<  [
	[ELSE]
	    ] MINI xor >r MINI xor r> u< [
	[THEN]
    [THEN] ] ;

: f>  ( r1 r2 -- f )
    [ char fx char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    f< ;

: f<=  ( r1 r2 -- f )
    f> 0= ;

: f>=  ( r1 r2 -- f )
    [ char fx char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    f<= ;

: f0=  ( r -- f )
    [ char f0x char 0 = [IF]
	] IF false ELSE true THEN [
    [ELSE]
	] xor 0= [
    [THEN] ] ;

: f0<>  ( r -- f )
    [ char f0x char 0 = [IF]
	] IF true ELSE false THEN [
    [ELSE]
	] xor 0<> [
    [THEN] ] ;

: f0<  ( r -- f )
    [ char f0x char 0 = [IF]
	] MINI and 0<> [
    [ELSE] char f0x char u = [IF]
	]   2dup xor 0<  IF nip ELSE - THEN 0<  [
	[ELSE]
	    ] MINI xor >r MINI xor r> u< [
	[THEN]
    [THEN] ] ;

: f0>  ( r -- f )
    [ char f0x char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    f0< ;

: f0<=  ( r -- f )
    f0> 0= ;

: f0>=  ( r -- f )
    [ char f0x char 0 = [IF] ] negate [ [ELSE] ] swap [ [THEN] ]
    f0<= ;

: ftan  ( r1 -- r2 )
 fsincos f/ ;

: fsinh  ( r1 -- r2 )
 fexpm1 fdup fdup 1. d>f f+ f/ f+ f2/ ;

: fcosh  ( r1 -- r2 )
 fexp fdup 1/f f+ f2/ ;

: ftanh  ( r1 -- r2 )
 f2* fexpm1 fdup 2. d>f f+ f/ ;

: fasinh  ( r1 -- r2 )
 fdup fdup f* 1. d>f f+ fsqrt f/ fatanh ;

: facosh  ( r1 -- r2 )
 fdup fdup f* 1. d>f f- fsqrt f+ fln ;

: fatanh  ( r1 -- r2 )
 fdup f0< >r fabs 1. d>f fover f- f/  f2* flnp1 f2/
 r> IF  fnegate  THEN ;

: sfaligned  ( c_addr -- sf_addr )
 [ 1 sfloats 1- ] Literal + [ -1 sfloats ] Literal and ;

: dfaligned  ( c_addr -- df_addr )
 [ 1 dfloats 1- ] Literal + [ -1 dfloats ] Literal and ;

: v*  ( f_addr1 nstride1 f_addr2 nstride2 ucount -- r )
 >r swap 2swap swap 0e r> 0 ?DO
     dup f@ over + 2swap dup f@ f* f+ over + 2swap
 LOOP 2drop 2drop ; 

: faxpy  ( ra f_x nstridex f_y nstridey ucount -- )
 >r swap 2swap swap r> 0 ?DO
     fdup dup f@ f* over + 2swap dup f@ f+ dup f! over + 2swap
 LOOP 2drop 2drop fdrop ;

75 groupadd
[THEN]
0 groupadd
has? glocals [IF]
0 groupadd
group  locals
5 groupadd
has? floating [IF]
3 groupadd
[THEN]
7 groupadd
has? floating [IF]
: fpick  ( f:... u -- f:... r )
 floats fp@ + f@ ;

2 groupadd
[THEN]
0 groupadd
[THEN]
0 groupadd
has? OS [IF]
0 groupadd
group  syslib
10 groupadd
[THEN]
0 groupadd
group  peephole
0 groupadd
has? peephole [IF]
7 groupadd
[THEN]
0 groupadd
group  static_super
0 groupadd
group  end
