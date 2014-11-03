\ f+       r1 r2 – r3        float       “f-plus”
: f+
	f+
	send-fmod
;

\ f-       r1 r2 – r3        float       “f-minus”
: f-
	f-
	send-fmod
;

\ f*       r1 r2 – r3        float       “f-star”
: f*
	f*
	send-fmod
;

\ f/       r1 r2 – r3        float       “f-slash”
: f/
	f/
	send-fmod
;

\ fnegate       r1 – r2        float       “f-negate”
: fnegate
	fnegate
	send-fmod
;

\ fabs       r1 – r2        float-ext       “f-abs”
: fabs
	fabs
	send-fmod
;

\ fmax       r1 r2 – r3        float       “f-max”
: fmax
	fmax
	send-fmod
;

\ fmin       r1 r2 – r3        float       “f-min”
: fmin
	fmin
	send-fmod
;

\ floor       r1 – r2        float       “floor” Round towards the next smaller integral value, i.e., round toward negative infinity.
: floor
	floor
	send-fmod
;

\ fround       r1 – r2        float       “f-round” Round to the nearest integral value.
: fround
	fround
	send-fmod
;

\ f**       r1 r2 – r3        float-ext       “f-star-star” r3 is r1 raised to the r2th power.
: f**
	f**
	send-fmod
;

\ fsqrt       r1 – r2        float-ext       “f-square-root”
: fsqrt
	fsqrt
	send-fmod
;

\ fexp       r1 – r2        float-ext       “f-e-x-p”
: fexp
	fexp
	send-fmod
;

\ fexpm1       r1 – r2        float-ext       “f-e-x-p-m-one” r2=e**r1−1
: fexpm1
	fexpm1
	send-fmod
;

\ fln       r1 – r2        float-ext       “f-l-n”
: fln
	fln
	send-fmod
;

\ flnp1       r1 – r2        float-ext       “f-l-n-p-one” r2=ln(r1+1)
: flnp1
	flnp1
	send-fmod
;

\ flog       r1 – r2        float-ext       “f-log” The decimal logarithm.
: flog
	flog
	send-fmod
;

\ falog       r1 – r2        float-ext       “f-a-log” r2=10**r1
: falog
	falog
	send-fmod
;

\ f2*       r1 – r2         gforth       “f2*” Multiply r1 by 2.0e0
: f2*
	f2*
	send-fmod
;

\ f2/       r1 – r2         gforth       “f2/” Multiply r1 by 0.5e0
: f2/
	f2/
	send-fmod
;

\ 1/f       r1 – r2         gforth       “1/f” Divide 1.0e0 by r1.
: 1/f
	1/f
	send-fmod
;

\ precision       – u         float-ext       “precision” u is the number of significant digits currently used by F. FE. and FS.
: precision
	precision
	send-dmod
;

\ set-precision       u –         float-ext       “set-precision” Set the number of significant digits currently used by F. FE. and FS. to u.
: set-precision
	set-precision
	send-dmod
;

\ fsin       r1 – r2        float-ext       “f-sine” 
: fsin
	fsin
	send-fmod
;

\ fcos       r1 – r2        float-ext       “f-cos”
: fcos
	fcos
	send-fmod
;

\ fsincos       r1 – r2 r3        float-ext       “f-sine-cos” r2=sin(r1), r3=cos(r1)
: fsincos
	fsincos
	send-fmod
;

\ ftan       r1 – r2        float-ext       “f-tan”
: ftan
	ftan
	send-fmod
;

\ fasin       r1 – r2        float-ext       “f-a-sine”
: fasin
	fasin
	send-fmod
;

\ facos       r1 – r2        float-ext       “f-a-cos”
: facos
	facos
	send-fmod
;

\ fatan       r1 – r2        float-ext       “f-a-tan”
: fatan
	fatan
	send-fmod
;

\ fatan2       r1 r2 – r3        float-ext       “f-a-tan-two” r1/r2=tan(r3). ANS Forth does not require, but probably intends this to be the inverse of fsincos. In gforth it is.
: fatan2
	fatan2
	send-fmod
;

\ fsinh       r1 – r2        float-ext       “f-cinch”
: fsinh
	fsinh
	send-fmod
;

\ fcosh       r1 – r2        float-ext       “f-cosh”
: fcosh
	fcosh
	send-fmod
;

\ ftanh       r1 – r2        float-ext       “f-tan-h”
: ftanh
	ftanh
	send-fmod
;

\ fasinh       r1 – r2        float-ext       “f-a-cinch”
: fasinh
	fasinh
	send-fmod
;

\ facosh       r1 – r2        float-ext       “f-a-cosh”
: facosh
	facosh
	send-fmod
;

\ fatanh       r1 – r2        float-ext       “f-a-tan-h”
: fatanh
	fatanh
	send-fmod
;

\ pi       – r         gforth       “pi” Fconstant – r is the value pi; the ratio of a circle's area to its diameter.
: pi
	pi
	send-fmod
;

\ f~rel       r1 r2 r3 – flag         gforth       “f~rel” Approximate equality with relative error: |r1-r2|<r3*|r1+r2|.
: f~rel
	f~rel
	send-fmod
	send-dmod
;

\ f~abs       r1 r2 r3 – flag         gforth       “f~abs” Approximate equality with absolute error: |r1-r2|<r3.
: f~abs
	f~abs
	send-fmod
	send-dmod
;

\ f~       r1 r2 r3 – flag         float-ext       “f-proximate” ANS Forth medley for comparing r1 and r2 for equality: r3>0: f~abs; r3=0: bitwise comparison; r3<0: fnegate f~rel.
: f~
	f~
	send-fmod
	send-dmod
;

\ f=       r1 r2 – f        gforth       “f-equals”
: f=
	f=
	send-fmod
	send-dmod
;

\ f<>       r1 r2 – f        gforth       “f-not-equals”
: f<>
	f<>
	send-fmod
	send-dmod
;

\ f<       r1 r2 – f        float       “f-less-than”
: f<
	f<
	send-fmod
	send-dmod
;

\ f<=       r1 r2 – f        gforth       “f-less-or-equal”
: f<=
	f<=
	send-fmod
	send-dmod
;

\ f>       r1 r2 – f        gforth       “f-greater-than”
: f>
	f>
	send-fmod
	send-dmod
;

\ f>=       r1 r2 – f        gforth       “f-greater-or-equal”
: f>=
	f>=
	send-fmod
	send-dmod
;

\ f0<       r – f        float       “f-zero-less-than”
: f0<
	f0<
	send-fmod
	send-dmod
;

\ f0<=       r – f        gforth       “f-zero-less-or-equal”
: f0<=
	f0<=
	send-fmod
	send-dmod
;

\ f0<>       r – f        gforth       “f-zero-not-equals”
: f0<>
	f0<>
	send-fmod
	send-dmod
;

\ f0=       r – f        float       “f-zero-equals”
: f0=
	f0=
	send-fmod
	send-dmod
;

\ f0>       r – f        gforth       “f-zero-greater-than”
: f0>
	f0>
	send-fmod
	send-dmod
;

\ f0>=       r – f        gforth       “f-zero-greater-or-equal”
: f0>=
	f0>=
	send-fmod
	send-dmod
;
