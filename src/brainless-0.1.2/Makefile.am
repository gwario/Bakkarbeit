#  no compile and link, so omit these macros
#bin_PROGRAMS=brainless
#brainless_SOURCES=

#  this gets installed
if WITH_GFORTH
bin_SCRIPTS=brainless
endif

#  list sources here instead of using the brainless_SOURCES macro
EXTRA_DIST=brainless.lsm.in brainless.spec.in brainless.in brainless.1 \
 aclocal.m4
# gpl.texinfo
# brainless.texinfo

pkgdata_DATA = \
	benchmark.fs			\
	board.fs					\
	brainless.fs					\
	drawing.fs					\
	environ.fs					\
	epd.fs						\
	eval.fs						\
	flyeval.fs					\
	hash.fs						\
	history.fs					\
	killer.fs					\
	moveconv.fs					\
	movegen.fs					\
	moves.fs					\
	null.fs						\
	options.fs					\
	profiler.fs					\
	quiescence.fs					\
	repeat.fs					\
	searchdefs.fs					\
	search.fs					\
	sglmove.fs					\
	sorting.fs					\
	string.fs					\
	tests.fs					\
	threats.fs					\
	tmovegen.fs					\
	ttable.fs					\
	tui.fs						\
	utils.fs

EXTRA_DIST += $(pkgdata_DATA)

#  if you write a self-test script named `chk', uncomment the
#  following and add `chk' to the EXTRA_DIST list
#TESTS=chk

#  build and install the .info files
#info_TEXINFOS = brainless.texinfo

#  install the man pages
man_MANS=brainless.1
