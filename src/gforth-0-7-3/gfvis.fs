\ create xterm s" xterm -Sab0 -g 150x15" 2,
\ create pkillxterm s\" sh -c \"pkill -SIGTERM -f \"xterm -Sab0 -g 150x15\"\"" 2,
\ xterm 2@ w/o open-pipe throw constant visfile-id

create PWD s" PWD" getenv 2,
s" pwd" system

: run-ghostview ( -- )
	s" gv --ad=" pad place
	PWD 2@ pad +place
	s" /gfvis.ps" pad +place
	s"  --watch " pad +place
	PWD 2@ pad +place
	s" /trace.ps" pad +place
	pad count system
;

: kill-ghostview ( -- )
	s\" pkill -SIGTERM -f \"gv --ad=" pad place
	PWD 2@ pad +place
	s\" /gfvis.ps" pad +place
	s\" --watch "
	PWD 2@ pad +place
	s\" /trace.ps\"" pad +place
	pad count system
;


: run-ghostview-working ( -- )
	s" gv --watch " pad +place
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

\ what's wrong?
: goto-eof ( fid -- )
	dup ( fid fid ) file-size ( fid size-l size-h wior ) throw ( fid size-l size-h )
	rot ( size-l size-h fid ) reposition-file ( wior ) throw
;



\ create trace file
s" gfvis.ps" r/o open-file throw constant templatefile-id
s" trace.ps" r/w create-file throw constant tracefile-id
\ s" %!PS" tracefile-id write-line throw

9999 constant max-line
create line-buffer max-line 2 + allot

create boundingbox-def s" %%BoundingBox:" 2,
create cellwidth-def s" /cellwidth " 2,
create wordcellwidth-def s" /wordcellwidth " 2,

variable cellwidth
variable wordcellwidth
variable boundingboxwidth

: parse-boundingboxwidth ( c-addr u -- boundingboxwidth )
	2drop
	610 \ TODO parse for real
;
: parse-cellwidth ( c-addr u -- cellwidth )
	2drop
	70 \ TODO parse for real
;
: parse-wordcellwidth ( c-addr u -- wordcellwidth )
	2drop
	50 \ TODO parse for real
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
	color-blue cr type space ." -> " space
	color-green type
	font-normal
;

\ copies the template's content to the trace file, parsing the cell width of stack and word as well as the initial boundingbox width
: copy-template ( -- )
	begin
		line-buffer max-line templatefile-id read-line throw
		
	while ( n-read )
	
		dup line-buffer swap boundingbox-def 2@ starts-with? if ( n-read )
		
			dup line-buffer swap parse-boundingboxwidth boundingboxwidth !
			dup line-buffer swap s" BoundingBox definition found!" log-parsed-line
			
		else ( n-read )
		
			dup line-buffer swap cellwidth-def 2@ starts-with? if ( n-read )
			
				dup line-buffer swap parse-cellwidth cellwidth !
				dup line-buffer swap s" cellwidth definition found!" log-parsed-line
				
			else ( n-read )
			
				dup line-buffer swap wordcellwidth-def 2@ starts-with? if ( n-read )
				
					dup line-buffer swap parse-wordcellwidth wordcellwidth !
					dup line-buffer swap s" wordcellwidth definition found!" log-parsed-line
					
				endif ( n-read )
			endif ( n-read )
		endif ( n-read )
		
		line-buffer swap tracefile-id write-line throw
	repeat
	drop
;

\ updates the boundingbox size: adds the cellwidth and the wordcellwidth 
: update-boundingbox ( -- )
	\ TODO reposition to the beginning
	begin
		line-buffer max-line tracefile-id read-line throw
	while
		dup line-buffer swap max-line s" %%BoundingBox:" starts-with? if
			\ modify line-buffer to change width by 
			line-buffer swap tracefile-id write-line throw
		endif
	repeat
	drop
	
	goto-eof
;

copy-template
templatefile-id close-file throw

\ set the c variable
\ tracefile-id set-gfvis-fid \ not yet used

\ to append to the existing content
\ tracefile-id goto-eof \ no need since we moved the trace code exclusively to trace.ps

run-ghostview-working
\ run-ghostview


: gfvis-close ( -- )
	\ pkillxterm 2@ w/o open-pipe throw drop
	\ visfile-id close-file
	kill-ghostview-working
	\ kill-ghostview
	tracefile-id close-file
;

: .status1 ( -- )
    #cr emit cr ." base= " base @ dec.
    #cr emit cr .s
    #cr emit cr f.s
    #cr emit cr order ;


: n>str ( n c-addr -- )
	>r dup >r
	( n |r: c-addr n )
	abs s>d <# #s r> sign #>
 	r@ char+ swap dup >r cmove r> r> c!
;

create num$ 64 chars allot

: write-word-def ( word -- )
	s" /wordname (" pad place
	pad +place
	s" ) def" pad +place
	pad count tracefile-id write-line throw
;
: write-datastack-def ( -- )
	s" /datastack [ " tracefile-id write-file throw
	
	depth 0 max maxdepth-.s @ min  \ not more than  maxdepth-.s
	dup 0 ?do
		dup i - pick
		s" (" pad place
		num$ n>str  num$ count  pad +place
		s" ) " pad +place
		pad count tracefile-id write-file throw
	loop
	drop
	
	s" ] def" tracefile-id write-line throw
;

: write-floatstack-def ( -- )
	s" /floatstack [ " tracefile-id write-file throw

	fdepth 0 max maxdepth-.s @ min \ not more than maxdepth-.s
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

: write-command
	s" printupdate" tracefile-id write-line throw
;


: .ps-update ( -- )
	s\" test" write-word-def
	write-datastack-def
	write-floatstack-def
	write-command
	\ update-boundingbox throw
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
    defers .status ;

' .status3 is .status


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
