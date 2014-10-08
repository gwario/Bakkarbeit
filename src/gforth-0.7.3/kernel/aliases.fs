\ run-time routine headers

\ Copyright (C) 1997,1998,2002,2003,2006,2007 Free Software Foundation, Inc.

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

-2 Doer: :docol
-3 Doer: :docon
-4 Doer: :dovar
-5 Doer: :douser
-6 Doer: :dodefer
-7 Doer: :dofield
-8 Doer: :dovalue
-9 Doer: :dodoes
-&10 Doer: :doesjump
-&2 first-primitive \ this does not work for (at least) (DODOES),
                    \ so these routines are commented out
 0 [if]
Primitive (docol)
Primitive (docon)
Primitive (dovar)
Primitive (douser)
Primitive (dodefer)
Primitive (dofield)
Primitive (dovalue)
Primitive (dodoes)
Primitive (does-handler)
 [endif]
9 groupadd
group  control
Primitive noop
Primitive call
Primitive execute
Primitive perform
Primitive ;s
Primitive unloop
Primitive lit-perform
Primitive does-exec
8 groupadd
has? glocals [IF]
Primitive branch-lp+!#
1 groupadd
[THEN]
Primitive branch
Primitive ?branch
2 groupadd
has? glocals [IF]
Primitive ?branch-lp+!#
1 groupadd
[THEN]
0 groupadd
has? xconds [IF]
Primitive ?dup-?branch
Primitive ?dup-0=-?branch
2 groupadd
[THEN]
has? skiploopprims 0= [IF]
Primitive (next)
1 groupadd
has? glocals [IF]
Primitive (next)-lp+!#
1 groupadd
[THEN]
Primitive (loop)
1 groupadd
has? glocals [IF]
Primitive (loop)-lp+!#
1 groupadd
[THEN]
Primitive (+loop)
1 groupadd
has? glocals [IF]
Primitive (+loop)-lp+!#
1 groupadd
[THEN]
0 groupadd
has? xconds [IF]
Primitive (-loop)
1 groupadd
has? glocals [IF]
Primitive (-loop)-lp+!#
1 groupadd
[THEN]
Primitive (s+loop)
1 groupadd
has? glocals [IF]
Primitive (s+loop)-lp+!#
1 groupadd
[THEN]
0 groupadd
[THEN]
Primitive (for)
Primitive (do)
Primitive (?do)
3 groupadd
has? xconds [IF]
Primitive (+do)
Primitive (u+do)
Primitive (-do)
Primitive (u-do)
4 groupadd
[THEN]
Primitive i
Primitive i'
Primitive j
Primitive k
[THEN]
4 groupadd
group  strings
Primitive move
Primitive cmove
Primitive cmove>
Primitive fill
Primitive compare
Primitive toupper
Primitive capscompare
Primitive /string
8 groupadd
group  arith
Primitive lit
Primitive +
Primitive lit+
Primitive under+
Primitive -
Primitive negate
Primitive 1+
Primitive 1-
Primitive max
Primitive min
Primitive abs
Primitive *
Primitive /
Primitive mod
Primitive /mod
Primitive */mod
Primitive */
Primitive 2*
Primitive 2/
Primitive fm/mod
Primitive sm/rem
Primitive m*
Primitive um*
Primitive um/mod
Primitive m+
Primitive d+
Primitive d-
Primitive dnegate
Primitive d2*
Primitive d2/
Primitive and
Primitive or
Primitive xor
Primitive invert
Primitive rshift
Primitive lshift
36 groupadd
group  compare
Primitive 0=
Primitive 0<>
Primitive 0<
Primitive 0>
Primitive 0<=
Primitive 0>=
Primitive =
Primitive <>
Primitive <
Primitive >
Primitive <=
Primitive >=
Primitive u=
Primitive u<>
Primitive u<
Primitive u>
Primitive u<=
Primitive u>=
18 groupadd
has? dcomps [IF]
Primitive d=
Primitive d<>
Primitive d<
Primitive d>
Primitive d<=
Primitive d>=
Primitive d0=
Primitive d0<>
Primitive d0<
Primitive d0>
Primitive d0<=
Primitive d0>=
Primitive du=
Primitive du<>
Primitive du<
Primitive du>
Primitive du<=
Primitive du>=
18 groupadd
[THEN]
Primitive within
1 groupadd
group  stack
Primitive useraddr
Primitive up!
Primitive sp@
Primitive sp!
Primitive rp@
Primitive rp!
6 groupadd
has? floating [IF]
Primitive fp@
Primitive fp!
2 groupadd
[THEN]
Primitive >r
Primitive r>
Primitive rdrop
Primitive 2>r
Primitive 2r>
Primitive 2r@
Primitive 2rdrop
Primitive over
Primitive drop
Primitive swap
Primitive dup
Primitive rot
Primitive -rot
Primitive nip
Primitive tuck
Primitive ?dup
Primitive pick
Primitive 2drop
Primitive 2dup
Primitive 2over
Primitive 2swap
Primitive 2rot
Primitive 2nip
Primitive 2tuck
24 groupadd
group  memory
Primitive @
Primitive lit@
Primitive !
Primitive +!
Primitive c@
Primitive c!
Primitive 2!
Primitive 2@
Primitive cell+
Primitive cells
Primitive char+
Primitive (chars)
Primitive count
13 groupadd
group  compiler
0 groupadd
has? f83headerstring [IF]
Primitive (f83find)
1 groupadd
[ELSE]
Primitive (listlfind)
1 groupadd
has? hash [IF]
Primitive (hashlfind)
Primitive (tablelfind)
Primitive (hashkey1)
3 groupadd
[THEN]
0 groupadd
[THEN]
Primitive (parse-white)
Primitive aligned
Primitive faligned
has? standardthreading has? compiler and [IF]
Primitive threading-method
[THEN]
4 groupadd
group  hostos
Primitive key-file
Primitive key?-file
Primitive stdin
Primitive stdout
Primitive stderr
5 groupadd
has? os [IF]
Primitive form
Primitive wcwidth
Primitive flush-icache
Primitive (bye)
Primitive (system)
Primitive getenv
Primitive open-pipe
Primitive close-pipe
Primitive time&date
Primitive ms
Primitive allocate
Primitive free
Primitive resize
Primitive strerror
Primitive strsignal
Primitive call-c
16 groupadd
[THEN]
0 groupadd
has? file [IF]
Primitive close-file
Primitive open-file
Primitive create-file
Primitive delete-file
Primitive rename-file
Primitive file-position
Primitive reposition-file
Primitive file-size
Primitive resize-file
Primitive read-file
Primitive (read-line)
11 groupadd
[THEN]
Primitive write-file
Primitive emit-file
2 groupadd
has? file [IF]
Primitive flush-file
Primitive file-status
Primitive file-eof?
Primitive open-dir
Primitive read-dir
Primitive close-dir
Primitive filename-match
Primitive set-dir
Primitive get-dir
Primitive =mkdir
10 groupadd
[THEN]
Primitive newline
1 groupadd
has? os [IF]
Primitive utime
Primitive cputime
2 groupadd
[THEN]
0 groupadd
has? floating [IF]
0 groupadd
group  floating
Primitive f=
Primitive f<>
Primitive f<
Primitive f>
Primitive f<=
Primitive f>=
Primitive f0=
Primitive f0<>
Primitive f0<
Primitive f0>
Primitive f0<=
Primitive f0>=
Primitive s>f
Primitive d>f
Primitive f>d
Primitive f>s
Primitive f!
Primitive f@
Primitive df@
Primitive df!
Primitive sf@
Primitive sf!
Primitive f+
Primitive f-
Primitive f*
Primitive f/
Primitive f**
Primitive fm*
Primitive fm/
Primitive fm*/
Primitive f**2
Primitive fnegate
Primitive fdrop
Primitive fdup
Primitive fswap
Primitive fover
Primitive frot
Primitive fnip
Primitive ftuck
Primitive float+
Primitive floats
Primitive floor
Primitive fround
Primitive fmax
Primitive fmin
Primitive represent
Primitive >float
Primitive fabs
Primitive facos
Primitive fasin
Primitive fatan
Primitive fatan2
Primitive fcos
Primitive fexp
Primitive fexpm1
Primitive fln
Primitive flnp1
Primitive flog
Primitive falog
Primitive fsin
Primitive fsincos
Primitive fsqrt
Primitive ftan
Primitive fsinh
Primitive fcosh
Primitive ftanh
Primitive fasinh
Primitive facosh
Primitive fatanh
Primitive sfloats
Primitive dfloats
Primitive sfaligned
Primitive dfaligned
Primitive v*
Primitive faxpy
75 groupadd
[THEN]
0 groupadd
has? glocals [IF]
0 groupadd
group  locals
Primitive @local#
Primitive @local0
Primitive @local1
Primitive @local2
Primitive @local3
5 groupadd
has? floating [IF]
Primitive f@local#
Primitive f@local0
Primitive f@local1
3 groupadd
[THEN]
Primitive laddr#
Primitive lp+!#
Primitive lp-
Primitive lp+
Primitive lp+2
Primitive lp!
Primitive >l
7 groupadd
has? floating [IF]
Primitive f>l
Primitive fpick
2 groupadd
[THEN]
0 groupadd
[THEN]
0 groupadd
has? OS [IF]
0 groupadd
group  syslib
Primitive open-lib
Primitive lib-sym
Primitive wcall
Primitive uw@
Primitive sw@
Primitive w!
Primitive ul@
Primitive sl@
Primitive l!
Primitive lib-error
10 groupadd
[THEN]
0 groupadd
group  peephole
0 groupadd
has? peephole [IF]
Primitive compile-prim1
Primitive finish-code
Primitive forget-dyncode
Primitive decompile-prim
Primitive set-next-code
Primitive call2
Primitive tag-offsets
7 groupadd
[THEN]
0 groupadd
group  static_super
0 groupadd
group  end
