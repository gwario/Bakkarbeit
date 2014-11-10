create xterm s" xterm -Sab0 -g 100x5" 2,
create pkill s\" sh -c \"pkill -SIGTERM -f \"xterm -Sab0 -g 100x5\"\"" 2,

xterm 2@ w/o open-pipe throw constant visfile-id

: .status1 ( -- )
    #cr emit cr ." base= " base @ dec.
    #cr emit cr .s
    #cr emit cr f.s
    #cr emit cr order ;

: .status2 ( -- )
    outfile-id >r
    visfile-id to outfile-id
    ['] .status1 catch
    r> to outfile-id
    throw
    visfile-id flush-file throw
    defers .status ;

\ ' .status2 is .status

\ set the c variable
visfile-id gfvis-fid

: gfvis-close ( -- )
	\ visfile-id close-pipe .s \ does not work
	pkill 2@ w/o open-pipe throw drop
;

: bye ( -- )
	cr
	gfvis-close
	0 (bye)
;
