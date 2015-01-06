
create PWD s" PWD" getenv 2,
s" pwd" system

: run-ghostview-working ( -- )
	s" gv --watch " pad place
	PWD 2@ pad +place
	s" /trace.ps" pad +place
	\ pad count system
	\ pad count sh
	pad count w/o open-pipe throw drop
;

: kill-ghostview-working ( -- )
	s\" pkill -SIGTERM -f \"gv --watch " pad place
	PWD 2@ pad +place
	s\" /trace.ps\"" pad +place
	\ pad count system
	pad count w/o open-pipe throw drop
;

: rdepth ( -- n )
    rp@ rp0 @ swap - cell /
;

: split ( str len separator len -- tokens count )
	
	here >r 2swap
	
	begin
		2dup 2,             \ save this token ( addr len )
		2over search        \ find next separator
	while
		dup negate  here 2 cells -  +!  \ adjust last token length
		2over nip /string               \ start next search past separator
	repeat
	
	2drop 2drop
	r>  here over -   ( tokens length )
	dup negate allot           \ reclaim dictionary
	2 cells /                  \ turn byte length into token count
;

: goto-eof ( fid -- )
	dup ( fid fid ) file-size ( fid size-l size-h wior ) throw ( fid size-l size-h )
	rot ( size-l size-h fid ) reposition-file ( wior ) throw
;

: goto-bof ( fid -- )
	0 0 rot ( 0 0 fid ) reposition-file ( wior ) throw
;

: goto-bol ( c-addr n-read fid -- ) \ position at the beginning of the line, the line must be on the stack
	
	rot drop swap ( fid n-read ) over ( fid n-read fid ) file-position throw ( fid n-read n-pos-l n-pos-h ) d>s ( fid n-read n-pos )
	swap ( fid n-pos n-read ) - 1- ( probably cause of newline ) ( fid n-pol ) s>d rot reposition-file throw
;

: white?  ( char -- flag )  33 u< ;		\ 2002 Is-White

: trim|  ( addr len -- addr len-i ) 		\ 2002 TRIM
(
Trim trailing whitespace from the input string.  If the input is
all whitespace, the result is the left empty string.

Note:  We prefer the name TRIM| to the Tool Belt's TRIM [the
same in 2000 and 2002], because a more natural meaning for TRIM
might be to remove both leading and trailing whitespace.  The
"|" prefix/suffix for "TRIM" was used by George T. Hawkins in
his 1987 FStrings Package:

http://www-personal.umich.edu/~williams/archive/forth/strings/fstrings/fstrings.txt

We adopt that practice.  TRIM| is equivalent to the phrase:

  BL SKIP-BACK
)
  BEGIN ( len) dup WHILE 1- 2dup chars + c@ white? 0= UNTIL 1+ THEN ;


: parse-def ( c-addr u s" ...{\s*<number>\s*}..." -- n <number> )
	s" {" split drop
	cell+ cell+ ( s" width} ..." ) \ skip the first token
	2@ s" }" split drop
	2@ bl skip trim| s>number? 2drop \ skip blanks and trim the first token - the actual number
;


: n>str ( n c-addr -- )
	>r dup >r
	( n |r: c-addr n )
	abs s>d <# #s r> sign #>
 	r@ char+ swap dup >r cmove r> r> c!
;

\ buffer used to convert numbers to strings
create num$ 64 chars allot

\ line buffer
9999 constant max-line
create line-buffer max-line 2 + allot

create boundingbox-def s" %%BoundingBox: " 2,
create cellwidth-def s" /cellwidth " 2,
create wordcellwidth-def s" /wordcellwidth " 2,

variable cellwidth
variable wordcellwidth
2variable boundingboxstart
2variable boundingboxend


: parse-boundingbox ( c-addr u -- x0 y0 x1 y1 )
	boundingbox-def 2@ nip /string ( s" x1 y1 x2 y2" )
	s"  " split drop dup
	2@ s>number? 2drop swap cell+ cell+ dup \ 2drop drops f and does d>s
	2@ s>number? 2drop swap cell+ cell+ dup
	2@ s>number? 2drop swap cell+ cell+
	2@ s>number? 2drop
;
: parse-cellwidth ( c-addr u -- cellwidth )
	( s" {width} ..." )
	parse-def
;
: parse-wordcellwidth ( c-addr u -- wordcellwidth )
	( s" {width} ..." )
	parse-def
;

\ changes the font format using ansi control characters


: color-blue ( -- )
   27 emit ." [1;34m"
;

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

: log-parsed-line ( c-addr-line u-line c-addr-comment u-comment -- )
	color-green cr type space ." -> " space
	color-blue type
	font-normal
;

: log-parsed-value ( value c-addr-comment u-comment -- )
	color-green cr type .
	font-normal
;

: copy-template { to-fid from-fid -- }

	begin
		line-buffer max-line from-fid read-line throw
		
	while ( n-read )
		
		dup line-buffer swap boundingbox-def 2@ starts-with? if ( n-read )
		
			dup line-buffer swap parse-boundingbox boundingboxend 2! boundingboxstart 2!
			dup line-buffer swap s" BoundingBox definition found!" log-parsed-line
			color-green cr ." boundingbox: " boundingboxstart 2@ . . boundingboxend 2@ . . font-normal
			
		else ( n-read )
			
			dup line-buffer swap cellwidth-def 2@ starts-with? if ( n-read )
				
				dup line-buffer swap parse-cellwidth cellwidth !
				dup line-buffer swap s" cellwidth definition found!" log-parsed-line
				cellwidth @ s" cellwidth: " log-parsed-value
				
			else ( n-read )
			
				dup line-buffer swap wordcellwidth-def 2@ starts-with? if ( n-read )
				
					dup line-buffer swap parse-wordcellwidth wordcellwidth !
					
					dup line-buffer swap s" wordcellwidth definition found!" log-parsed-line
					wordcellwidth @ s" wordcellwidth: " log-parsed-value
					
				endif ( n-read )
			endif ( n-read )
		endif ( n-read )
		
		line-buffer swap to-fid write-line throw
		
	repeat drop
;

\ updates the boundingbox size: adds the cellwidth and the wordcellwidth 
: update-boundingbox { trace-fid -- }
	
	trace-fid goto-bof
	
	begin
		line-buffer max-line trace-fid read-line throw
	while
		dup line-buffer swap boundingbox-def 2@ starts-with? if ( n-read )
		
			dup line-buffer swap trace-fid goto-bol ( n-read )
			
			boundingbox-def 2@ pad place
			
			boundingboxstart 2@ swap
			num$ n>str num$ count pad +place s"  " pad +place num$ n>str num$ count pad +place s"  " pad +place
			
			boundingboxend 2@ swap cellwidth @ + wordcellwidth @ + 2dup swap boundingboxend 2!
			num$ n>str num$ count pad +place s"  " pad +place num$ n>str num$ count pad +place
			
			pad count trace-fid write-file throw
			
		endif ( n-read ) drop
	repeat drop
	
	trace-fid goto-eof
;

\ create trace file
s" gfvis.ps" r/o open-file throw constant templatefile-id
s" trace.ps" r/w create-file throw constant tracefile-id


tracefile-id templatefile-id copy-template
templatefile-id close-file throw

\ set the c variable
\ tracefile-id set-gfvis-fid \ not yet used

\ to append to the existing content
\ tracefile-id goto-eof \ no need since we moved the trace code exclusively to trace.ps

run-ghostview-working
\ run-ghostview


: gfvis-close ( -- )
	kill-ghostview-working
	tracefile-id close-file
;

: .status1 ( -- )
    #cr emit cr ." base= " base @ dec.
    #cr emit cr .s
    #cr emit cr f.s
    #cr emit cr order ;

: escape-wordname ( c-addr n -- c-addr n )
	2dup s" \" str= if 
		2drop s" \\"
	else
		2dup s" (" str= if
			2drop s" \("
		else
			2dup s" )" str= if
				2drop s" \)"
			endif
		endif
	endif
;

: write-word-def ( -- )
	s" /wordname (" pad place
	gfvis-mrw 2@ escape-wordname pad +place
	s" ) def" pad +place
	pad count tracefile-id write-line throw
;
: write-datastack-def ( -- )
	s" /datastack [ " tracefile-id write-file throw
	
	depth 0 max \ maxdepth-.s @ min  \ not more than  maxdepth-.s
	dup 0 ?do
		dup i - pick
		s" (" pad place
		num$ n>str num$ count pad +place
		s" ) " pad +place
		pad count tracefile-id write-file throw
	loop
	drop
	
	s" ] def" tracefile-id write-line throw
;

: write-floatstack-def ( -- )
	s" /floatstack [ " tracefile-id write-file throw

	fdepth 0 max \ maxdepth-.s @ min \ not more than maxdepth-.s TODO set max depth in dependant of font height and document height / 3
	dup 0 ?DO
		dup i - 1- floats fp@ + f@
		s" (" pad place
		16 5 11 ( f.rdp does the printing, actually f>str-rdp type ) f>str-rdp pad +place
		s" ) " pad +place
		pad count tracefile-id write-file throw
	loop
	drop
	
	s" ] def" tracefile-id write-line throw
;

: n>hex-char ( n -- c-addr len )
	dup 9 > if
		dup 10 = if
			s" A"
		else
			dup 11 = if
				s" B"
			else
				dup 12 = if
					s" C"
				else
					dup 13 = if
						s" D"
					else
						dup 14 = if
							s" E"
						else
							dup 15 = if
								s" F"
							endif
						endif
					endif
				endif
			endif
		endif
		rot drop
	else
		0 <# # #>
	endif
;

: test store-backtrace ." do something" true if dobacktrace ." do if" else ." do else" endif ." die" ;

create reversebuffer 32 chars allot

: exchange ( a1 a2 -- )
  2dup c@ swap c@ rot c! swap c! ;
: reverse ( c-addr u -- )
  1- bounds begin 2dup > while
    2dup exchange
    -1 /string
  repeat 2drop ;

: n>hex-str ( n c-addr -- )
 	swap abs \ abs ? what does hex printing do?? fuck it
	s" " reversebuffer place
	begin
		\ dup . ." / 16 = "
		16 /mod
		\ 2dup . ." R:" . cr
		( n%16 n/16 ) swap ( n/16 n%16 )
		\ 2dup . . cr
		n>hex-char
		\ 2dup ." char: " type cr
		reversebuffer +place ( n/16 )
		dup
		\ dup ." next divident: " . cr
		0=
		\ dup ." f: " . ." (true=" true . ." - break loop |false=" false . ." - restart loop)" cr 
	until ( n/16 f )
	drop
	s" $" reversebuffer +place
	reversebuffer count reverse
	reversebuffer count rot place
	\ reversebuffer count type cr
;

: append-zeros-to-reversebuffer ( count -- )
	-1 ?do
		s" 0" reversebuffer +place
	loop
;

: n>16hex-str ( n c-addr -- )
 	swap abs \ abs ? what does hex printing do?? fuck it
	s" " reversebuffer place
	begin
		16 /mod
		( n%16 n/16 ) swap ( n/16 n%16 )
		n>hex-char
		reversebuffer +place ( n/16 )
		dup
		0=
	until ( n/16 f )
	drop
	16 reversebuffer count nip - append-zeros-to-reversebuffer
	s" $" reversebuffer +place
	reversebuffer count reverse
	reversebuffer count rot place
;

: use-bt-entry ( return-stack-item -- addr count )
    cell - dup in-dictionary? over dup aligned = and
    if
	@ dup threaded>name dup if
	    nip name>string
	else
	    drop dup look if
		nip name>string
	    else
		drop body> look \ !! check for "call" in cell before?
		if
		     name>string
		else
		    drop s" ???"
		then
	    then
	then
    else
	drop  s" ???"
    then ;

: use-bt-entry-or-number ( return-stack-item -- addr count )
	cell -
	( original-value )
	dup in-dictionary? ( original-value f ) over dup ( original-value f original-value original-value ) aligned ( original-value f original-value original-aligned ) = ( original-value f f ) and ( original-value f ) if
		( original-value )
		dup 
		( original-value original-value )
		@
		( original-value ca )
		dup ( original-value ca ca ) threaded>name ( original-value ca nt ) dup ( original-value ca nt nt ) if
			( original-value ca nt )
			nip ( original-value nt )
			nip ( nt ) name>string ( addr count )
		else
			( original-value ca nt )
			drop
			( original-value ca )
			dup ( original-value ca ca ) look ( original-value ca lfa f ) if
				( original-value ca lfa )
				nip ( original-value lfa )
				nip ( lfa ) name>string ( addr count )
			else
				( original-value ca lfa )
				drop
				( original-value ca )
				body> ( original-value ca2 ) look ( original-value lfa f ) if
					( original-value lfa )
					nip ( lfa ) name>string ( original-value addr count )
				else
					( original-value lfa )
					drop
					( original-value )
					num$ n>16hex-str num$ count ( addr count )
				then
			then
		then
	else
		( original-value )
		num$ n>16hex-str num$ count ( addr count )
	then
;

: write-returnstack-def ( -- )
	s" /returnstack [ " tracefile-id write-file throw

	rdepth 0 max \ maxdepth-.s @ min \ not more than maxdepth-.s TODO set max depth in dependant of font height and document height / 3
	dup 0 ?DO
		dup i - rp@ + @
		s" (" pad place
		\ dup . cr
		\ num$ n>hex-str num$ count pad +place \ here we print the address in hex
		\ use-bt-entry pad +place \ here we try to resolve the word entry addresses
		use-bt-entry-or-number pad +place
		s" ) " pad +place
		pad count tracefile-id write-file throw
	loop
	drop
	
	s" ] def" tracefile-id write-line throw
;

: write-command
	s" printupdate" tracefile-id write-line throw
;


: .ps-update ( -- )
	write-word-def
	write-datastack-def
	write-floatstack-def
	write-returnstack-def
	write-command
	tracefile-id update-boundingbox
;


: .status2 ( -- )
    outfile-id >r
    tracefile-id to outfile-id
    ['] .status1 catch
    r> to outfile-id
    throw
    tracefile-id flush-file throw
    defers .status ;
    
: .status3 ( -- )
    ['] .ps-update catch throw
    tracefile-id flush-file throw
    defers .gfvis-status ;

' .status3 is .gfvis-status


\ \\\\\\\\\\\\\\\\\\\\\\\\\
\ \\ redegines
\ \\\\\\\\\\\\\\\\\\\\\\\\\
cr
: bye ( -- )
	cr
	gfvis-close
	0 (bye)
;
cr
true gfvis !
