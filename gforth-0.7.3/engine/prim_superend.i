1, /* (docol) */
0, /* (docon) */
0, /* (dovar) */
0, /* (douser) */
1, /* (dodefer) */
0, /* (dofield) */
0, /* (dovalue) */
1, /* (dodoes) */
0, /* (does-handler) */
GROUPADD(9)
GROUP( control, 9)
0, /* noop */
1, /* call */
1, /* execute */
1, /* perform */
1, /* ;s */
0, /* unloop */
1, /* lit-perform */
1, /* does-exec */
GROUPADD(8)
#ifdef HAS_GLOCALS
1, /* branch-lp+!# */
GROUPADD(1)
#endif
1, /* branch */
1, /* ?branch */
GROUPADD(2)
#ifdef HAS_GLOCALS
1, /* ?branch-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
1, /* ?dup-?branch */
1, /* ?dup-0=-?branch */
GROUPADD(2)
#endif
1, /* (next) */
GROUPADD(1)
#ifdef HAS_GLOCALS
1, /* (next)-lp+!# */
GROUPADD(1)
#endif
1, /* (loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
1, /* (loop)-lp+!# */
GROUPADD(1)
#endif
1, /* (+loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
1, /* (+loop)-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
1, /* (-loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
1, /* (-loop)-lp+!# */
GROUPADD(1)
#endif
1, /* (s+loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
1, /* (s+loop)-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#endif
0, /* (for) */
0, /* (do) */
1, /* (?do) */
GROUPADD(3)
#ifdef HAS_XCONDS
1, /* (+do) */
1, /* (u+do) */
1, /* (-do) */
1, /* (u-do) */
GROUPADD(4)
#endif
0, /* i */
0, /* i' */
0, /* j */
0, /* k */
GROUPADD(4)
GROUP( strings, 44)
0, /* move */
0, /* cmove */
0, /* cmove> */
0, /* fill */
0, /* compare */
0, /* toupper */
0, /* capscompare */
0, /* /string */
GROUPADD(8)
GROUP( arith, 52)
0, /* lit */
0, /* + */
0, /* lit+ */
0, /* under+ */
0, /* --prim */
0, /* negate */
0, /* 1+ */
0, /* 1- */
0, /* max */
0, /* min */
0, /* abs */
0, /* * */
0, /* / */
0, /* mod */
0, /* /mod */
0, /* x/mod */
0, /* x/ */
0, /* 2* */
0, /* 2/ */
0, /* fm/mod */
0, /* sm/rem */
0, /* m* */
0, /* um* */
0, /* um/mod */
0, /* m+ */
0, /* d+ */
0, /* d- */
0, /* dnegate */
0, /* d2* */
0, /* d2/ */
0, /* and */
0, /* or */
0, /* xor */
0, /* invert */
0, /* rshift */
0, /* lshift */
GROUPADD(36)
GROUP( compare, 88)
0, /* 0= */
0, /* 0<> */
0, /* 0< */
0, /* 0> */
0, /* 0<= */
0, /* 0>= */
0, /* = */
0, /* <> */
0, /* < */
0, /* > */
0, /* <= */
0, /* >= */
0, /* u= */
0, /* u<> */
0, /* u< */
0, /* u> */
0, /* u<= */
0, /* u>= */
GROUPADD(18)
#ifdef HAS_DCOMPS
0, /* d= */
0, /* d<> */
0, /* d< */
0, /* d> */
0, /* d<= */
0, /* d>= */
0, /* d0= */
0, /* d0<> */
0, /* d0< */
0, /* d0> */
0, /* d0<= */
0, /* d0>= */
0, /* du= */
0, /* du<> */
0, /* du< */
0, /* du> */
0, /* du<= */
0, /* du>= */
GROUPADD(18)
#endif
0, /* within */
GROUPADD(1)
GROUP( stack, 125)
0, /* useraddr */
0, /* up! */
0, /* sp@ */
0, /* sp! */
0, /* rp@ */
0, /* rp! */
GROUPADD(6)
#ifdef HAS_FLOATING
0, /* fp@ */
0, /* fp! */
GROUPADD(2)
#endif
0, /* >r */
0, /* r> */
0, /* rdrop */
0, /* 2>r */
0, /* 2r> */
0, /* 2r@ */
0, /* 2rdrop */
0, /* over */
0, /* drop */
0, /* swap */
0, /* dup */
0, /* rot */
0, /* -rot */
0, /* nip */
0, /* tuck */
0, /* ?dup */
0, /* pick */
0, /* 2drop */
0, /* 2dup */
0, /* 2over */
0, /* 2swap */
0, /* 2rot */
0, /* 2nip */
0, /* 2tuck */
GROUPADD(24)
GROUP( memory, 157)
0, /* @ */
0, /* lit@ */
0, /* ! */
0, /* +! */
0, /* c@ */
0, /* c! */
0, /* 2! */
0, /* 2@ */
0, /* cell+ */
0, /* cells */
0, /* char+ */
0, /* (chars) */
0, /* count */
GROUPADD(13)
GROUP( compiler, 170)
GROUPADD(0)
#ifdef HAS_F83HEADERSTRING
0, /* (f83find) */
GROUPADD(1)
#else /* 171 */
0, /* (listlfind) */
GROUPADD(1)
#ifdef HAS_HASH
0, /* (hashlfind) */
0, /* (tablelfind) */
0, /* (hashkey1) */
GROUPADD(3)
#endif
GROUPADD(0)
#endif
0, /* (parse-white) */
0, /* aligned */
0, /* faligned */
0, /* threading-method */
GROUPADD(4)
GROUP( hostos, 179)
0, /* key-file */
0, /* key?-file */
0, /* stdin */
0, /* stdout */
0, /* stderr */
GROUPADD(5)
#ifdef HAS_OS
0, /* form */
0, /* wcwidth */
0, /* flush-icache */
1, /* (bye) */
0, /* (system) */
0, /* getenv */
0, /* open-pipe */
0, /* close-pipe */
0, /* time&date */
0, /* ms */
0, /* allocate */
0, /* free */
0, /* resize */
0, /* strerror */
0, /* strsignal */
0, /* call-c */
GROUPADD(16)
#endif
GROUPADD(0)
#ifdef HAS_FILE
0, /* close-file */
0, /* open-file */
0, /* create-file */
0, /* delete-file */
0, /* rename-file */
0, /* file-position */
0, /* reposition-file */
0, /* file-size */
0, /* resize-file */
0, /* read-file */
0, /* (read-line) */
GROUPADD(11)
#endif
0, /* write-file */
0, /* emit-file */
GROUPADD(2)
#ifdef HAS_FILE
0, /* flush-file */
0, /* file-status */
0, /* file-eof? */
0, /* open-dir */
0, /* read-dir */
0, /* close-dir */
0, /* filename-match */
0, /* set-dir */
0, /* get-dir */
0, /* =mkdir */
GROUPADD(10)
#endif
0, /* newline */
GROUPADD(1)
#ifdef HAS_OS
0, /* utime */
0, /* cputime */
GROUPADD(2)
#endif
GROUPADD(0)
#ifdef HAS_FLOATING
GROUPADD(0)
GROUP( floating, 226)
0, /* f= */
0, /* f<> */
0, /* f< */
0, /* f> */
0, /* f<= */
0, /* f>= */
0, /* f0= */
0, /* f0<> */
0, /* f0< */
0, /* f0> */
0, /* f0<= */
0, /* f0>= */
0, /* s>f */
0, /* d>f */
0, /* f>d */
0, /* f>s */
0, /* f! */
0, /* f@ */
0, /* df@ */
0, /* df! */
0, /* sf@ */
0, /* sf! */
0, /* f+ */
0, /* f- */
0, /* f* */
0, /* f/ */
0, /* f** */
0, /* fm* */
0, /* fm/ */
0, /* fmx/ */
0, /* f**2 */
0, /* fnegate */
0, /* fdrop */
0, /* fdup */
0, /* fswap */
0, /* fover */
0, /* frot */
0, /* fnip */
0, /* ftuck */
0, /* float+ */
0, /* floats */
0, /* floor */
0, /* fround */
0, /* fmax */
0, /* fmin */
0, /* represent */
0, /* >float */
0, /* fabs */
0, /* facos */
0, /* fasin */
0, /* fatan */
0, /* fatan2 */
0, /* fcos */
0, /* fexp */
0, /* fexpm1 */
0, /* fln */
0, /* flnp1 */
0, /* flog */
0, /* falog */
0, /* fsin */
0, /* fsincos */
0, /* fsqrt */
0, /* ftan */
0, /* fsinh */
0, /* fcosh */
0, /* ftanh */
0, /* fasinh */
0, /* facosh */
0, /* fatanh */
0, /* sfloats */
0, /* dfloats */
0, /* sfaligned */
0, /* dfaligned */
0, /* v* */
0, /* faxpy */
GROUPADD(75)
#endif
GROUPADD(0)
#ifdef HAS_GLOCALS
GROUPADD(0)
GROUP( locals, 301)
0, /* @local# */
0, /* @local0 */
0, /* @local1 */
0, /* @local2 */
0, /* @local3 */
GROUPADD(5)
#ifdef HAS_FLOATING
0, /* f@local# */
0, /* f@local0 */
0, /* f@local1 */
GROUPADD(3)
#endif
0, /* laddr# */
0, /* lp+!# */
0, /* lp- */
0, /* lp+ */
0, /* lp+2 */
0, /* lp! */
0, /* >l */
GROUPADD(7)
#ifdef HAS_FLOATING
0, /* f>l */
0, /* fpick */
GROUPADD(2)
#endif
GROUPADD(0)
#endif
GROUPADD(0)
#ifdef HAS_OS
GROUPADD(0)
GROUP( syslib, 318)
0, /* open-lib */
0, /* lib-sym */
0, /* wcall */
0, /* uw@ */
0, /* sw@ */
0, /* w! */
0, /* ul@ */
0, /* sl@ */
0, /* l! */
0, /* lib-error */
GROUPADD(10)
#endif
GROUPADD(0)
GROUP( peephole, 328)
GROUPADD(0)
#ifdef HAS_PEEPHOLE
0, /* compile-prim1 */
0, /* finish-code */
0, /* forget-dyncode */
0, /* decompile-prim */
0, /* set-next-code */
0, /* call2 */
0, /* tag-offsets */
GROUPADD(7)
#endif
GROUPADD(0)
GROUP( static_super, 335)
GROUPADD(0)
GROUP( end, 335)
