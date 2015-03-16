dnl Process this file with autoconf to produce a configure script.

AC_INIT(Brainless, 0.1.2,,brainless,http://mosquito.dyndns.tv/freesvn/trunk/brainless)
dnl Every other copy of the package version number gets its value from here
AM_INIT_AUTOMAKE([foreign])

AC_SUBST(VERSION)

ISODATE=`date +%Y-%m-%d`
AC_SUBST(ISODATE)

AC_CANONICAL_HOST

dnl Checks for programs.
AC_PROG_INSTALL

AC_ARG_WITH([gforth],
  [AS_HELP_STRING([--with-gforth ?=path?],
    [Build&install gforth-specific launcher @<:@default=check@:>@])],
  [],
  [with_gforth=check])

GFORTH_BINARY=
AS_IF([test "x$with_gforth" = xcheck || test "x$with_gforth" = xyes],
  [AC_PATH_PROGS(GFORTH_BINARY, [gforth-fast gforth gforth-itc], [no])
  ],
  [
  AC_MSG_CHECKING([Which gforth interpreter to use])
  AC_SUBST(GFORTH_BINARY, [$with_gforth])
  AC_MSG_RESULT([$GFORTH_BINARY (from --with-gforth)])
])

AC_MSG_CHECKING([Whether to build and install gforth launcher])
AM_CONDITIONAL(WITH_GFORTH, test x$GFORTH_BINARY != xno)
AS_IF([test "x$with_gforth" != xno],
	    [AC_MSG_RESULT([yes])],
	    [AC_MSG_RESULT([no])])

AC_OUTPUT(Makefile brainless.lsm brainless brainless.spec)
