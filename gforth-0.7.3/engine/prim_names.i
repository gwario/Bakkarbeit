"(docol)",
"(docon)",
"(dovar)",
"(douser)",
"(dodefer)",
"(dofield)",
"(dovalue)",
"(dodoes)",
"(does-handler)",
GROUPADD(9)
GROUP( control, 9)
"noop",
"call",
"execute",
"perform",
";s",
"unloop",
"lit-perform",
"does-exec",
GROUPADD(8)
#ifdef HAS_GLOCALS
"branch-lp+!#",
GROUPADD(1)
#endif
"branch",
"?branch",
GROUPADD(2)
#ifdef HAS_GLOCALS
"?branch-lp+!#",
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
"?dup-?branch",
"?dup-0=-?branch",
GROUPADD(2)
#endif
"(next)",
GROUPADD(1)
#ifdef HAS_GLOCALS
"(next)-lp+!#",
GROUPADD(1)
#endif
"(loop)",
GROUPADD(1)
#ifdef HAS_GLOCALS
"(loop)-lp+!#",
GROUPADD(1)
#endif
"(+loop)",
GROUPADD(1)
#ifdef HAS_GLOCALS
"(+loop)-lp+!#",
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
"(-loop)",
GROUPADD(1)
#ifdef HAS_GLOCALS
"(-loop)-lp+!#",
GROUPADD(1)
#endif
"(s+loop)",
GROUPADD(1)
#ifdef HAS_GLOCALS
"(s+loop)-lp+!#",
GROUPADD(1)
#endif
GROUPADD(0)
#endif
"(for)",
"(do)",
"(?do)",
GROUPADD(3)
#ifdef HAS_XCONDS
"(+do)",
"(u+do)",
"(-do)",
"(u-do)",
GROUPADD(4)
#endif
"i",
"i'",
"j",
"k",
GROUPADD(4)
GROUP( strings, 44)
"move",
"cmove",
"cmove>",
"fill",
"compare",
"toupper",
"capscompare",
"/string",
GROUPADD(8)
GROUP( arith, 52)
"lit",
"+",
"lit+",
"under+",
"--prim",
"negate",
"1+",
"1-",
"max",
"min",
"abs",
"*",
"/",
"mod",
"/mod",
"*/mod",
"*/",
"2*",
"2/",
"fm/mod",
"sm/rem",
"m*",
"um*",
"um/mod",
"m+",
"d+",
"d-",
"dnegate",
"d2*",
"d2/",
"and",
"or",
"xor",
"invert",
"rshift",
"lshift",
GROUPADD(36)
GROUP( compare, 88)
"0=",
"0<>",
"0<",
"0>",
"0<=",
"0>=",
"=",
"<>",
"<",
">",
"<=",
">=",
"u=",
"u<>",
"u<",
"u>",
"u<=",
"u>=",
GROUPADD(18)
#ifdef HAS_DCOMPS
"d=",
"d<>",
"d<",
"d>",
"d<=",
"d>=",
"d0=",
"d0<>",
"d0<",
"d0>",
"d0<=",
"d0>=",
"du=",
"du<>",
"du<",
"du>",
"du<=",
"du>=",
GROUPADD(18)
#endif
"within",
GROUPADD(1)
GROUP( stack, 125)
"useraddr",
"up!",
"sp@",
"sp!",
"rp@",
"rp!",
GROUPADD(6)
#ifdef HAS_FLOATING
"fp@",
"fp!",
GROUPADD(2)
#endif
">r",
"r>",
"rdrop",
"2>r",
"2r>",
"2r@",
"2rdrop",
"over",
"drop",
"swap",
"dup",
"rot",
"-rot",
"nip",
"tuck",
"?dup",
"pick",
"2drop",
"2dup",
"2over",
"2swap",
"2rot",
"2nip",
"2tuck",
GROUPADD(24)
GROUP( memory, 157)
"@",
"lit@",
"!",
"+!",
"c@",
"c!",
"2!",
"2@",
"cell+",
"cells",
"char+",
"(chars)",
"count",
GROUPADD(13)
GROUP( compiler, 170)
GROUPADD(0)
#ifdef HAS_F83HEADERSTRING
"(f83find)",
GROUPADD(1)
#else /* 171 */
"(listlfind)",
GROUPADD(1)
#ifdef HAS_HASH
"(hashlfind)",
"(tablelfind)",
"(hashkey1)",
GROUPADD(3)
#endif
GROUPADD(0)
#endif
"(parse-white)",
"aligned",
"faligned",
"threading-method",
GROUPADD(4)
GROUP( hostos, 179)
"key-file",
"key?-file",
"stdin",
"stdout",
"stderr",
GROUPADD(5)
#ifdef HAS_OS
"form",
"wcwidth",
"flush-icache",
"(bye)",
"(system)",
"getenv",
"open-pipe",
"close-pipe",
"time&date",
"ms",
"allocate",
"free",
"resize",
"strerror",
"strsignal",
"call-c",
GROUPADD(16)
#endif
GROUPADD(0)
#ifdef HAS_FILE
"close-file",
"open-file",
"create-file",
"delete-file",
"rename-file",
"file-position",
"reposition-file",
"file-size",
"resize-file",
"read-file",
"(read-line)",
GROUPADD(11)
#endif
"write-file",
"emit-file",
GROUPADD(2)
#ifdef HAS_FILE
"flush-file",
"file-status",
"file-eof?",
"open-dir",
"read-dir",
"close-dir",
"filename-match",
"set-dir",
"get-dir",
"=mkdir",
GROUPADD(10)
#endif
"newline",
GROUPADD(1)
#ifdef HAS_OS
"utime",
"cputime",
GROUPADD(2)
#endif
GROUPADD(0)
#ifdef HAS_FLOATING
GROUPADD(0)
GROUP( floating, 226)
"f=",
"f<>",
"f<",
"f>",
"f<=",
"f>=",
"f0=",
"f0<>",
"f0<",
"f0>",
"f0<=",
"f0>=",
"s>f",
"d>f",
"f>d",
"f>s",
"f!",
"f@",
"df@",
"df!",
"sf@",
"sf!",
"f+",
"f-",
"f*",
"f/",
"f**",
"fm*",
"fm/",
"fm*/",
"f**2",
"fnegate",
"fdrop",
"fdup",
"fswap",
"fover",
"frot",
"fnip",
"ftuck",
"float+",
"floats",
"floor",
"fround",
"fmax",
"fmin",
"represent",
">float",
"fabs",
"facos",
"fasin",
"fatan",
"fatan2",
"fcos",
"fexp",
"fexpm1",
"fln",
"flnp1",
"flog",
"falog",
"fsin",
"fsincos",
"fsqrt",
"ftan",
"fsinh",
"fcosh",
"ftanh",
"fasinh",
"facosh",
"fatanh",
"sfloats",
"dfloats",
"sfaligned",
"dfaligned",
"v*",
"faxpy",
GROUPADD(75)
#endif
GROUPADD(0)
#ifdef HAS_GLOCALS
GROUPADD(0)
GROUP( locals, 301)
"@local#",
"@local0",
"@local1",
"@local2",
"@local3",
GROUPADD(5)
#ifdef HAS_FLOATING
"f@local#",
"f@local0",
"f@local1",
GROUPADD(3)
#endif
"laddr#",
"lp+!#",
"lp-",
"lp+",
"lp+2",
"lp!",
">l",
GROUPADD(7)
#ifdef HAS_FLOATING
"f>l",
"fpick",
GROUPADD(2)
#endif
GROUPADD(0)
#endif
GROUPADD(0)
#ifdef HAS_OS
GROUPADD(0)
GROUP( syslib, 318)
"open-lib",
"lib-sym",
"wcall",
"uw@",
"sw@",
"w!",
"ul@",
"sl@",
"l!",
"lib-error",
GROUPADD(10)
#endif
GROUPADD(0)
GROUP( peephole, 328)
GROUPADD(0)
#ifdef HAS_PEEPHOLE
"compile-prim1",
"finish-code",
"forget-dyncode",
"decompile-prim",
"set-next-code",
"call2",
"tag-offsets",
GROUPADD(7)
#endif
GROUPADD(0)
GROUP( static_super, 335)
GROUPADD(0)
GROUP( end, 335)
