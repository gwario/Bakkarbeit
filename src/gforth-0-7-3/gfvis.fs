\ create xterm s" xterm -Sab0 -g 150x15" 2,
\ create pkillxterm s\" sh -c \"pkill -SIGTERM -f \"xterm -Sab0 -g 150x15\"\"" 2,
\ xterm 2@ w/o open-pipe throw constant visfile-id

create PWD s" PWD" getenv 2, 

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
s" trace.ps" w/o create-file throw constant tracefile-id
\ s" %!PS" tracefile-id write-line throw

9999 constant max-line
create line-buffer max-line 2 + allot

: copy-template ( -- )
	begin
		line-buffer max-line templatefile-id read-line throw
	while
		line-buffer swap tracefile-id write-line throw
	repeat
	drop
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
	s" /word (" pad place
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

: write-commands
	s" printword printstack" tracefile-id write-line throw
;


: .ps-update ( -- )
	s\" test" write-word-def
	write-datastack-def
	write-commands
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
