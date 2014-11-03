\ changes the font format using ansi control characters
\ green bold
: color-green ( -- )
   27 emit ." [1;32m"
;

: color-red ( -- )
   27 emit ." [1;31m"
;

\ changes the font format using ansi control characters
\ resets to normal
: font-normal ( -- )
   27 emit ." [0m"
;

\ save the execution token of all words needed for "unrecognized" sending


: send-dmod
	color-green
	cr ." d:" .s
	font-normal
;


\ : send-rmod
\	color-green
\ does not work
\	cr ." r:" r.s
\	font-normal
\ ;

: send-fmod
	color-green
	cr ." f:" f.s
	font-normal
;

require redefine-f.fs
require redefine-d.fs
