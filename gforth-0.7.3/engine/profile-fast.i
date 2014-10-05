if (VM_IS_INST(*ip, 0)) {
  add_inst(b, "(docol)");
  ip += 1;
  return;
}
if (VM_IS_INST(*ip, 1)) {
  add_inst(b, "(docon)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 2)) {
  add_inst(b, "(dovar)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 3)) {
  add_inst(b, "(douser)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 4)) {
  add_inst(b, "(dodefer)");
  ip += 1;
  return;
}
if (VM_IS_INST(*ip, 5)) {
  add_inst(b, "(dofield)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 6)) {
  add_inst(b, "(dovalue)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 7)) {
  add_inst(b, "(dodoes)");
  ip += 1;
  return;
}
if (VM_IS_INST(*ip, 8)) {
  add_inst(b, "(does-handler)");
  ip += 1;
  goto _endif_;
}
GROUPADD(9)
GROUP( control, 9)
if (VM_IS_INST(*ip, 9)) {
  add_inst(b, "noop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 10)) {
  add_inst(b, "call");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 11)) {
  add_inst(b, "execute");
  ip += 1;
  return;
}
if (VM_IS_INST(*ip, 12)) {
  add_inst(b, "perform");
  ip += 1;
  return;
}
if (VM_IS_INST(*ip, 13)) {
  add_inst(b, ";s");
  ip += 1;
  return;
}
if (VM_IS_INST(*ip, 14)) {
  add_inst(b, "unloop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 15)) {
  add_inst(b, "lit-perform");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 16)) {
  add_inst(b, "does-exec");
  ip += 2;
  return;
}
GROUPADD(8)
#ifdef HAS_GLOCALS
if (VM_IS_INST(*ip, 17)) {
  add_inst(b, "branch-lp+!#");
  ip += 3;
  return;
}
GROUPADD(1)
#endif
if (VM_IS_INST(*ip, 18)) {
  add_inst(b, "branch");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 19)) {
  add_inst(b, "?branch");
  ip += 2;
  return;
}
GROUPADD(2)
#ifdef HAS_GLOCALS
if (VM_IS_INST(*ip, 20)) {
  add_inst(b, "?branch-lp+!#");
  ip += 3;
  return;
}
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
if (VM_IS_INST(*ip, 21)) {
  add_inst(b, "?dup-?branch");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 22)) {
  add_inst(b, "?dup-0=-?branch");
  ip += 2;
  return;
}
GROUPADD(2)
#endif
if (VM_IS_INST(*ip, 23)) {
  add_inst(b, "(next)");
  ip += 2;
  return;
}
GROUPADD(1)
#ifdef HAS_GLOCALS
if (VM_IS_INST(*ip, 24)) {
  add_inst(b, "(next)-lp+!#");
  ip += 3;
  return;
}
GROUPADD(1)
#endif
if (VM_IS_INST(*ip, 25)) {
  add_inst(b, "(loop)");
  ip += 2;
  return;
}
GROUPADD(1)
#ifdef HAS_GLOCALS
if (VM_IS_INST(*ip, 26)) {
  add_inst(b, "(loop)-lp+!#");
  ip += 3;
  return;
}
GROUPADD(1)
#endif
if (VM_IS_INST(*ip, 27)) {
  add_inst(b, "(+loop)");
  ip += 2;
  return;
}
GROUPADD(1)
#ifdef HAS_GLOCALS
if (VM_IS_INST(*ip, 28)) {
  add_inst(b, "(+loop)-lp+!#");
  ip += 3;
  return;
}
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
if (VM_IS_INST(*ip, 29)) {
  add_inst(b, "(-loop)");
  ip += 2;
  return;
}
GROUPADD(1)
#ifdef HAS_GLOCALS
if (VM_IS_INST(*ip, 30)) {
  add_inst(b, "(-loop)-lp+!#");
  ip += 3;
  return;
}
GROUPADD(1)
#endif
if (VM_IS_INST(*ip, 31)) {
  add_inst(b, "(s+loop)");
  ip += 2;
  return;
}
GROUPADD(1)
#ifdef HAS_GLOCALS
if (VM_IS_INST(*ip, 32)) {
  add_inst(b, "(s+loop)-lp+!#");
  ip += 3;
  return;
}
GROUPADD(1)
#endif
GROUPADD(0)
#endif
if (VM_IS_INST(*ip, 33)) {
  add_inst(b, "(for)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 34)) {
  add_inst(b, "(do)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 35)) {
  add_inst(b, "(?do)");
  ip += 2;
  return;
}
GROUPADD(3)
#ifdef HAS_XCONDS
if (VM_IS_INST(*ip, 36)) {
  add_inst(b, "(+do)");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 37)) {
  add_inst(b, "(u+do)");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 38)) {
  add_inst(b, "(-do)");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 39)) {
  add_inst(b, "(u-do)");
  ip += 2;
  return;
}
GROUPADD(4)
#endif
if (VM_IS_INST(*ip, 40)) {
  add_inst(b, "i");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 41)) {
  add_inst(b, "i'");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 42)) {
  add_inst(b, "j");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 43)) {
  add_inst(b, "k");
  ip += 1;
  goto _endif_;
}
GROUPADD(4)
GROUP( strings, 44)
if (VM_IS_INST(*ip, 44)) {
  add_inst(b, "move");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 45)) {
  add_inst(b, "cmove");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 46)) {
  add_inst(b, "cmove>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 47)) {
  add_inst(b, "fill");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 48)) {
  add_inst(b, "compare");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 49)) {
  add_inst(b, "toupper");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 50)) {
  add_inst(b, "capscompare");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 51)) {
  add_inst(b, "/string");
  ip += 1;
  goto _endif_;
}
GROUPADD(8)
GROUP( arith, 52)
if (VM_IS_INST(*ip, 52)) {
  add_inst(b, "lit");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 53)) {
  add_inst(b, "+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 54)) {
  add_inst(b, "lit+");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 55)) {
  add_inst(b, "under+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 56)) {
  add_inst(b, "-");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 57)) {
  add_inst(b, "negate");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 58)) {
  add_inst(b, "1+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 59)) {
  add_inst(b, "1-");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 60)) {
  add_inst(b, "max");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 61)) {
  add_inst(b, "min");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 62)) {
  add_inst(b, "abs");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 63)) {
  add_inst(b, "*");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 64)) {
  add_inst(b, "/");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 65)) {
  add_inst(b, "mod");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 66)) {
  add_inst(b, "/mod");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 67)) {
  add_inst(b, "*/mod");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 68)) {
  add_inst(b, "*/");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 69)) {
  add_inst(b, "2*");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 70)) {
  add_inst(b, "2/");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 71)) {
  add_inst(b, "fm/mod");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 72)) {
  add_inst(b, "sm/rem");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 73)) {
  add_inst(b, "m*");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 74)) {
  add_inst(b, "um*");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 75)) {
  add_inst(b, "um/mod");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 76)) {
  add_inst(b, "m+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 77)) {
  add_inst(b, "d+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 78)) {
  add_inst(b, "d-");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 79)) {
  add_inst(b, "dnegate");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 80)) {
  add_inst(b, "d2*");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 81)) {
  add_inst(b, "d2/");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 82)) {
  add_inst(b, "and");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 83)) {
  add_inst(b, "or");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 84)) {
  add_inst(b, "xor");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 85)) {
  add_inst(b, "invert");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 86)) {
  add_inst(b, "rshift");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 87)) {
  add_inst(b, "lshift");
  ip += 1;
  goto _endif_;
}
GROUPADD(36)
GROUP( compare, 88)
if (VM_IS_INST(*ip, 88)) {
  add_inst(b, "0=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 89)) {
  add_inst(b, "0<>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 90)) {
  add_inst(b, "0<");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 91)) {
  add_inst(b, "0>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 92)) {
  add_inst(b, "0<=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 93)) {
  add_inst(b, "0>=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 94)) {
  add_inst(b, "=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 95)) {
  add_inst(b, "<>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 96)) {
  add_inst(b, "<");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 97)) {
  add_inst(b, ">");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 98)) {
  add_inst(b, "<=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 99)) {
  add_inst(b, ">=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 100)) {
  add_inst(b, "u=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 101)) {
  add_inst(b, "u<>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 102)) {
  add_inst(b, "u<");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 103)) {
  add_inst(b, "u>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 104)) {
  add_inst(b, "u<=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 105)) {
  add_inst(b, "u>=");
  ip += 1;
  goto _endif_;
}
GROUPADD(18)
#ifdef HAS_DCOMPS
if (VM_IS_INST(*ip, 106)) {
  add_inst(b, "d=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 107)) {
  add_inst(b, "d<>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 108)) {
  add_inst(b, "d<");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 109)) {
  add_inst(b, "d>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 110)) {
  add_inst(b, "d<=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 111)) {
  add_inst(b, "d>=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 112)) {
  add_inst(b, "d0=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 113)) {
  add_inst(b, "d0<>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 114)) {
  add_inst(b, "d0<");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 115)) {
  add_inst(b, "d0>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 116)) {
  add_inst(b, "d0<=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 117)) {
  add_inst(b, "d0>=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 118)) {
  add_inst(b, "du=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 119)) {
  add_inst(b, "du<>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 120)) {
  add_inst(b, "du<");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 121)) {
  add_inst(b, "du>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 122)) {
  add_inst(b, "du<=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 123)) {
  add_inst(b, "du>=");
  ip += 1;
  goto _endif_;
}
GROUPADD(18)
#endif
if (VM_IS_INST(*ip, 124)) {
  add_inst(b, "within");
  ip += 1;
  goto _endif_;
}
GROUPADD(1)
GROUP( stack, 125)
if (VM_IS_INST(*ip, 125)) {
  add_inst(b, "useraddr");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 126)) {
  add_inst(b, "up!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 127)) {
  add_inst(b, "sp@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 128)) {
  add_inst(b, "sp!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 129)) {
  add_inst(b, "rp@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 130)) {
  add_inst(b, "rp!");
  ip += 1;
  goto _endif_;
}
GROUPADD(6)
#ifdef HAS_FLOATING
if (VM_IS_INST(*ip, 131)) {
  add_inst(b, "fp@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 132)) {
  add_inst(b, "fp!");
  ip += 1;
  goto _endif_;
}
GROUPADD(2)
#endif
if (VM_IS_INST(*ip, 133)) {
  add_inst(b, ">r");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 134)) {
  add_inst(b, "r>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 135)) {
  add_inst(b, "rdrop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 136)) {
  add_inst(b, "2>r");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 137)) {
  add_inst(b, "2r>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 138)) {
  add_inst(b, "2r@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 139)) {
  add_inst(b, "2rdrop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 140)) {
  add_inst(b, "over");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 141)) {
  add_inst(b, "drop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 142)) {
  add_inst(b, "swap");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 143)) {
  add_inst(b, "dup");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 144)) {
  add_inst(b, "rot");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 145)) {
  add_inst(b, "-rot");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 146)) {
  add_inst(b, "nip");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 147)) {
  add_inst(b, "tuck");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 148)) {
  add_inst(b, "?dup");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 149)) {
  add_inst(b, "pick");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 150)) {
  add_inst(b, "2drop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 151)) {
  add_inst(b, "2dup");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 152)) {
  add_inst(b, "2over");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 153)) {
  add_inst(b, "2swap");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 154)) {
  add_inst(b, "2rot");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 155)) {
  add_inst(b, "2nip");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 156)) {
  add_inst(b, "2tuck");
  ip += 1;
  goto _endif_;
}
GROUPADD(24)
GROUP( memory, 157)
if (VM_IS_INST(*ip, 157)) {
  add_inst(b, "@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 158)) {
  add_inst(b, "lit@");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 159)) {
  add_inst(b, "!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 160)) {
  add_inst(b, "+!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 161)) {
  add_inst(b, "c@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 162)) {
  add_inst(b, "c!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 163)) {
  add_inst(b, "2!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 164)) {
  add_inst(b, "2@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 165)) {
  add_inst(b, "cell+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 166)) {
  add_inst(b, "cells");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 167)) {
  add_inst(b, "char+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 168)) {
  add_inst(b, "(chars)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 169)) {
  add_inst(b, "count");
  ip += 1;
  goto _endif_;
}
GROUPADD(13)
GROUP( compiler, 170)
GROUPADD(0)
#ifdef HAS_F83HEADERSTRING
if (VM_IS_INST(*ip, 170)) {
  add_inst(b, "(f83find)");
  ip += 1;
  goto _endif_;
}
GROUPADD(1)
#else /* 171 */
if (VM_IS_INST(*ip, 171)) {
  add_inst(b, "(listlfind)");
  ip += 1;
  goto _endif_;
}
GROUPADD(1)
#ifdef HAS_HASH
if (VM_IS_INST(*ip, 172)) {
  add_inst(b, "(hashlfind)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 173)) {
  add_inst(b, "(tablelfind)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 174)) {
  add_inst(b, "(hashkey1)");
  ip += 1;
  goto _endif_;
}
GROUPADD(3)
#endif
GROUPADD(0)
#endif
if (VM_IS_INST(*ip, 175)) {
  add_inst(b, "(parse-white)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 176)) {
  add_inst(b, "aligned");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 177)) {
  add_inst(b, "faligned");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 178)) {
  add_inst(b, "threading-method");
  ip += 1;
  goto _endif_;
}
GROUPADD(4)
GROUP( hostos, 179)
if (VM_IS_INST(*ip, 179)) {
  add_inst(b, "key-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 180)) {
  add_inst(b, "key?-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 181)) {
  add_inst(b, "stdin");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 182)) {
  add_inst(b, "stdout");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 183)) {
  add_inst(b, "stderr");
  ip += 1;
  goto _endif_;
}
GROUPADD(5)
#ifdef HAS_OS
if (VM_IS_INST(*ip, 184)) {
  add_inst(b, "form");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 185)) {
  add_inst(b, "wcwidth");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 186)) {
  add_inst(b, "flush-icache");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 187)) {
  add_inst(b, "(bye)");
  ip += 1;
  return;
}
if (VM_IS_INST(*ip, 188)) {
  add_inst(b, "(system)");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 189)) {
  add_inst(b, "getenv");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 190)) {
  add_inst(b, "open-pipe");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 191)) {
  add_inst(b, "close-pipe");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 192)) {
  add_inst(b, "time&date");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 193)) {
  add_inst(b, "ms");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 194)) {
  add_inst(b, "allocate");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 195)) {
  add_inst(b, "free");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 196)) {
  add_inst(b, "resize");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 197)) {
  add_inst(b, "strerror");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 198)) {
  add_inst(b, "strsignal");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 199)) {
  add_inst(b, "call-c");
  ip += 1;
  goto _endif_;
}
GROUPADD(16)
#endif
GROUPADD(0)
#ifdef HAS_FILE
if (VM_IS_INST(*ip, 200)) {
  add_inst(b, "close-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 201)) {
  add_inst(b, "open-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 202)) {
  add_inst(b, "create-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 203)) {
  add_inst(b, "delete-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 204)) {
  add_inst(b, "rename-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 205)) {
  add_inst(b, "file-position");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 206)) {
  add_inst(b, "reposition-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 207)) {
  add_inst(b, "file-size");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 208)) {
  add_inst(b, "resize-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 209)) {
  add_inst(b, "read-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 210)) {
  add_inst(b, "(read-line)");
  ip += 1;
  goto _endif_;
}
GROUPADD(11)
#endif
if (VM_IS_INST(*ip, 211)) {
  add_inst(b, "write-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 212)) {
  add_inst(b, "emit-file");
  ip += 1;
  goto _endif_;
}
GROUPADD(2)
#ifdef HAS_FILE
if (VM_IS_INST(*ip, 213)) {
  add_inst(b, "flush-file");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 214)) {
  add_inst(b, "file-status");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 215)) {
  add_inst(b, "file-eof?");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 216)) {
  add_inst(b, "open-dir");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 217)) {
  add_inst(b, "read-dir");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 218)) {
  add_inst(b, "close-dir");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 219)) {
  add_inst(b, "filename-match");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 220)) {
  add_inst(b, "set-dir");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 221)) {
  add_inst(b, "get-dir");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 222)) {
  add_inst(b, "=mkdir");
  ip += 1;
  goto _endif_;
}
GROUPADD(10)
#endif
if (VM_IS_INST(*ip, 223)) {
  add_inst(b, "newline");
  ip += 1;
  goto _endif_;
}
GROUPADD(1)
#ifdef HAS_OS
if (VM_IS_INST(*ip, 224)) {
  add_inst(b, "utime");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 225)) {
  add_inst(b, "cputime");
  ip += 1;
  goto _endif_;
}
GROUPADD(2)
#endif
GROUPADD(0)
#ifdef HAS_FLOATING
GROUPADD(0)
GROUP( floating, 226)
if (VM_IS_INST(*ip, 226)) {
  add_inst(b, "f=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 227)) {
  add_inst(b, "f<>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 228)) {
  add_inst(b, "f<");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 229)) {
  add_inst(b, "f>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 230)) {
  add_inst(b, "f<=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 231)) {
  add_inst(b, "f>=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 232)) {
  add_inst(b, "f0=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 233)) {
  add_inst(b, "f0<>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 234)) {
  add_inst(b, "f0<");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 235)) {
  add_inst(b, "f0>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 236)) {
  add_inst(b, "f0<=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 237)) {
  add_inst(b, "f0>=");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 238)) {
  add_inst(b, "s>f");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 239)) {
  add_inst(b, "d>f");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 240)) {
  add_inst(b, "f>d");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 241)) {
  add_inst(b, "f>s");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 242)) {
  add_inst(b, "f!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 243)) {
  add_inst(b, "f@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 244)) {
  add_inst(b, "df@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 245)) {
  add_inst(b, "df!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 246)) {
  add_inst(b, "sf@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 247)) {
  add_inst(b, "sf!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 248)) {
  add_inst(b, "f+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 249)) {
  add_inst(b, "f-");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 250)) {
  add_inst(b, "f*");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 251)) {
  add_inst(b, "f/");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 252)) {
  add_inst(b, "f**");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 253)) {
  add_inst(b, "fm*");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 254)) {
  add_inst(b, "fm/");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 255)) {
  add_inst(b, "fm*/");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 256)) {
  add_inst(b, "f**2");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 257)) {
  add_inst(b, "fnegate");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 258)) {
  add_inst(b, "fdrop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 259)) {
  add_inst(b, "fdup");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 260)) {
  add_inst(b, "fswap");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 261)) {
  add_inst(b, "fover");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 262)) {
  add_inst(b, "frot");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 263)) {
  add_inst(b, "fnip");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 264)) {
  add_inst(b, "ftuck");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 265)) {
  add_inst(b, "float+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 266)) {
  add_inst(b, "floats");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 267)) {
  add_inst(b, "floor");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 268)) {
  add_inst(b, "fround");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 269)) {
  add_inst(b, "fmax");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 270)) {
  add_inst(b, "fmin");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 271)) {
  add_inst(b, "represent");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 272)) {
  add_inst(b, ">float");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 273)) {
  add_inst(b, "fabs");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 274)) {
  add_inst(b, "facos");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 275)) {
  add_inst(b, "fasin");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 276)) {
  add_inst(b, "fatan");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 277)) {
  add_inst(b, "fatan2");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 278)) {
  add_inst(b, "fcos");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 279)) {
  add_inst(b, "fexp");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 280)) {
  add_inst(b, "fexpm1");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 281)) {
  add_inst(b, "fln");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 282)) {
  add_inst(b, "flnp1");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 283)) {
  add_inst(b, "flog");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 284)) {
  add_inst(b, "falog");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 285)) {
  add_inst(b, "fsin");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 286)) {
  add_inst(b, "fsincos");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 287)) {
  add_inst(b, "fsqrt");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 288)) {
  add_inst(b, "ftan");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 289)) {
  add_inst(b, "fsinh");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 290)) {
  add_inst(b, "fcosh");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 291)) {
  add_inst(b, "ftanh");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 292)) {
  add_inst(b, "fasinh");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 293)) {
  add_inst(b, "facosh");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 294)) {
  add_inst(b, "fatanh");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 295)) {
  add_inst(b, "sfloats");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 296)) {
  add_inst(b, "dfloats");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 297)) {
  add_inst(b, "sfaligned");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 298)) {
  add_inst(b, "dfaligned");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 299)) {
  add_inst(b, "v*");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 300)) {
  add_inst(b, "faxpy");
  ip += 1;
  goto _endif_;
}
GROUPADD(75)
#endif
GROUPADD(0)
#ifdef HAS_GLOCALS
GROUPADD(0)
GROUP( locals, 301)
if (VM_IS_INST(*ip, 301)) {
  add_inst(b, "@local#");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 302)) {
  add_inst(b, "@local0");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 303)) {
  add_inst(b, "@local1");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 304)) {
  add_inst(b, "@local2");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 305)) {
  add_inst(b, "@local3");
  ip += 1;
  goto _endif_;
}
GROUPADD(5)
#ifdef HAS_FLOATING
if (VM_IS_INST(*ip, 306)) {
  add_inst(b, "f@local#");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 307)) {
  add_inst(b, "f@local0");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 308)) {
  add_inst(b, "f@local1");
  ip += 1;
  goto _endif_;
}
GROUPADD(3)
#endif
if (VM_IS_INST(*ip, 309)) {
  add_inst(b, "laddr#");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 310)) {
  add_inst(b, "lp+!#");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 311)) {
  add_inst(b, "lp-");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 312)) {
  add_inst(b, "lp+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 313)) {
  add_inst(b, "lp+2");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 314)) {
  add_inst(b, "lp!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 315)) {
  add_inst(b, ">l");
  ip += 1;
  goto _endif_;
}
GROUPADD(7)
#ifdef HAS_FLOATING
if (VM_IS_INST(*ip, 316)) {
  add_inst(b, "f>l");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 317)) {
  add_inst(b, "fpick");
  ip += 1;
  goto _endif_;
}
GROUPADD(2)
#endif
GROUPADD(0)
#endif
GROUPADD(0)
#ifdef HAS_OS
GROUPADD(0)
GROUP( syslib, 318)
if (VM_IS_INST(*ip, 318)) {
  add_inst(b, "open-lib");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 319)) {
  add_inst(b, "lib-sym");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 320)) {
  add_inst(b, "wcall");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 321)) {
  add_inst(b, "uw@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 322)) {
  add_inst(b, "sw@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 323)) {
  add_inst(b, "w!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 324)) {
  add_inst(b, "ul@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 325)) {
  add_inst(b, "sl@");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 326)) {
  add_inst(b, "l!");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 327)) {
  add_inst(b, "lib-error");
  ip += 1;
  goto _endif_;
}
GROUPADD(10)
#endif
GROUPADD(0)
GROUP( peephole, 328)
GROUPADD(0)
#ifdef HAS_PEEPHOLE
if (VM_IS_INST(*ip, 328)) {
  add_inst(b, "compile-prim1");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 329)) {
  add_inst(b, "finish-code");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 330)) {
  add_inst(b, "forget-dyncode");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 331)) {
  add_inst(b, "decompile-prim");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 332)) {
  add_inst(b, "set-next-code");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 333)) {
  add_inst(b, "call2");
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 334)) {
  add_inst(b, "tag-offsets");
  ip += 1;
  goto _endif_;
}
GROUPADD(7)
#endif
GROUPADD(0)
GROUP( static_super, 335)
if (VM_IS_INST(*ip, 335)) {
  add_inst(b, "lit");
  add_inst(b, "+");
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 336)) {
  add_inst(b, "lit");
  add_inst(b, "@");
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 337)) {
  add_inst(b, "lit");
  add_inst(b, "!");
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 338)) {
  add_inst(b, "!");
  add_inst(b, ";s");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 339)) {
  add_inst(b, "lit");
  add_inst(b, "+");
  add_inst(b, "@");
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 340)) {
  add_inst(b, "useraddr");
  add_inst(b, "@");
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 341)) {
  add_inst(b, "+");
  add_inst(b, "@");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 342)) {
  add_inst(b, "lit");
  add_inst(b, "!");
  add_inst(b, ";s");
  ip += 4;
  return;
}
if (VM_IS_INST(*ip, 343)) {
  add_inst(b, "lit");
  add_inst(b, "@");
  add_inst(b, "and");
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 344)) {
  add_inst(b, "useraddr");
  add_inst(b, "!");
  ip += 3;
  goto _endif_;
}
if (VM_IS_INST(*ip, 345)) {
  add_inst(b, "@");
  add_inst(b, ";s");
  ip += 2;
  return;
}
if (VM_IS_INST(*ip, 346)) {
  add_inst(b, "lit");
  add_inst(b, "@");
  add_inst(b, "+");
  ip += 4;
  goto _endif_;
}
if (VM_IS_INST(*ip, 347)) {
  add_inst(b, "dup");
  add_inst(b, "@");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "lit");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "lit@");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "dup");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "i");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "useraddr");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "over");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "2dup");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "drop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "drop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, ">r");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "r>");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "lp+!#");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "unloop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "lp+");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "@local0");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "nip");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "lp-");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "@local#");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "2drop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "2drop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "@local2");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "rdrop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "@local1");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "i'");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "laddr#");
  ip += 2;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "j");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "2rdrop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, ">l");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "noop");
  ip += 1;
  goto _endif_;
}
if (VM_IS_INST(*ip, 348)) {
  add_inst(b, "noop");
  ip += 1;
  goto _endif_;
}
GROUPADD(13)
GROUP( end, 348)
