>r       w – R:w        core       “to-r”
r>       R:w – w        core       “r-from”
r@       – w ; R: w – w         core       “r-fetch”
rdrop       R:w –        gforth       “rdrop”
2>r       d – R:d        core-ext       “two-to-r”
2r>       R:d – d        core-ext       “two-r-from”
2r@       R:d – R:d d        core-ext       “two-r-fetch”
2rdrop       R:d –        gforth       “two-r-drop”

: >r
	>r
	' 
