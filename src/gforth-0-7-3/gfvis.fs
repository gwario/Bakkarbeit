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
	dup ( c-addr n fid fid ) cr .s file-size ( c-addr n fid size wior wior ) cr .s throw drop ( c-addr n fid size ) cr .s
	over ( c-addr n fid size fid ) cr .s reposition-file ( c-addr n fid wior ) cr .s throw ( c-addr n fid ) cr .s
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
	610 \ TODO parse for real
;
: parse-cellwidth ( c-addr u -- cellwidth )
	70 \ TODO parse for real
;
: parse-wordcellwidth ( c-addr u -- wordcellwidth )
	50 \ TODO parse for real
;

\ copies the template's content to the trace file, parsing the cell width of stack and word as well as the initial boundingbox width
: copy-template ( -- )
	begin
		line-buffer max-line templatefile-id read-line throw
	while
		dup line-buffer swap max-line boundingbox-def 2@ starts-width? if
			dup line-buffer swap parse-boundingboxwidth boundingboxwidth !
			." BoundingBox definition found!"
		else
			dup line-buffer swap max-line cellwidth-def 2@ starts-width? if
				dup line-buffer swap parse-cellwidth cellwidth !
				." cellwidth definition found!"
			else
				dup line-buffer swap max-line wordcellwidth-def starts-width? if
					dup line-buffer swap parse-wordcellwidth wordcellwidth !
					." wordcellwidth definition found!"
				endif
			endif
		endif
		
		line-buffer swap tracefile-id write-line throw
	repeat
	drop
;

\ updates the boundingbox size: adds the cellwidth and the wordcellwidth 
: update-boundingbox ( -- )
	\ TODO reposition to the beginning
	begin
		line-buffer max-line trace-id read-line throw
	while
		dup line-buffer swap max-line s" %%BoundingBox:" starts-width? if
			\ modify line-buffer to change width by 
			line-buffer swap tracefile-id write-line throw
		enif
	repeat
	drop
	\ TODO reposition to the end
;

copy-template templatefile-id close-file throw

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

: bye ( -- )
	cr
	gfvis-close
	0 (bye)
;

: .status1 ( -- )
    #cr emit cr ." base= " base @ dec.
    #cr emit cr .s
    #cr emit cr f.s
    #cr emit cr order ;


: n-string ( n c-addr -- )
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
: write-datastack-def
	s" /datastack [ " tracefile-id write-file throw
	
	depth 0 max maxdepth-.s @ min
	dup 0
	?do
		dup i - pick
		s" (" pad place
		num$ n-string num$ count pad +place
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
	write-command
	updage-boundingbox tracefile-id throw
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
