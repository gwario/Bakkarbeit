\ drop       w –        core       “drop”
: drop
	drop
	send-dmod
;

\ nip       w1 w2 – w2        core-ext       “nip”
: nip
	nip
	send-dmod
;

\ dup       w – w w        core       “dupe”
: dup
	dup
	send-dmod
;

\ over       w1 w2 – w1 w2 w1        core       “over”
: over
	over
	send-dmod
;

\ tuck       w1 w2 – w2 w1 w2        core-ext       “tuck”
: tuck
	tuck
	send-dmod
;

\ swap       w1 w2 – w2 w1        core       “swap”
: swap
	swap
	send-dmod
;

\ pick       S:... u – S:... w        core-ext       “pick” Actually the stack effect is x0 ... xu u -- x0 ... xu x0 .
: pick
	pick
	send-dmod
;

\ rot       w1 w2 w3 – w2 w3 w1        core       “rote”
: rot
	rot
	send-dmod
;

\ -rot       w1 w2 w3 – w3 w1 w2        gforth       “not-rote”
: -rot
	-rot
	send-dmod
;

\ ?dup       w – S:... w        core       “question-dupe” Actually the stack effect is: ( w -- 0 | w w ). It performs a dup if w is nonzero.
: ?dup
	?dup
	send-dmod
;

\ roll       x0 x1 .. xn n – x1 .. xn x0         core-ext       “roll”
: roll
	roll
	send-dmod
;

\ 2drop       w1 w2 –        core       “two-drop”
: 2drop
	2drop
	send-dmod
;

\ 2nip       w1 w2 w3 w4 – w3 w4        gforth       “two-nip”
: 2nip
	2nip
	send-dmod
;

\ 2dup       w1 w2 – w1 w2 w1 w2        core       “two-dupe”
: 2dup
	2dup
	send-dmod
;

\ 2over       w1 w2 w3 w4 – w1 w2 w3 w4 w1 w2        core       “two-over”
: 2over
	2over
	send-dmod
;

\ 2tuck       w1 w2 w3 w4 – w3 w4 w1 w2 w3 w4        gforth       “two-tuck”
: 2tuck
	2tuck
	send-dmod
;

\ 2swap       w1 w2 w3 w4 – w3 w4 w1 w2        core       “two-swap”
: 2swap
	2swap
	send-dmod
;

\ 2rot       w1 w2 w3 w4 w5 w6 – w3 w4 w5 w6 w1 w2        double-ext       “two-rote”
: 2rot
	2rot
	send-dmod
;



\ arithmetic 

\ +       n1 n2 – n        core       “plus”
: +
	+
	send-dmod
;

\ 1+       n1 – n2        core       “one-plus”
: 1+
	1+
	send-dmod
;

\ under+       n1 n2 n3 – n n2        gforth       “under-plus” add n3 to n1 (giving n)
: under+
	under+
	send-dmod
;

\ -       n1 n2 – n        core       “minus”
: -
	-
	send-dmod
;

\ 1-       n1 – n2        core       “one-minus”
: 1-
	1-
	send-dmod
;

\ *       n1 n2 – n        core       “star”
: *
	*
	send-dmod
;

\ /       n1 n2 – n        core       “slash”
: /
	/
	send-dmod
;

\ mod       n1 n2 – n        core       “mod”
: mod
	mod
	send-dmod
;

\ /mod       n1 n2 – n3 n4        core       “slash-mod”
: /mod
	/mod
	send-dmod
;

\ negate       n1 – n2        core       “negate”
: negate
	negate
	send-dmod
;

\ abs       n – u        core       “abs”
: abs
	abs
	send-dmod
;

\ min       n1 n2 – n        core       “min”
: min
	min
	send-dmod
;

\ max       n1 n2 – n        core       “max”
: max
	max
	send-dmod
;
