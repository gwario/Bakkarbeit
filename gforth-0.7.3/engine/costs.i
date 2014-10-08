{ 0, 1, 1, 1 , 0, 0,0,N_paren_docol, 1}, /* (docol) */
{ 0, 1, 1, 0 , 0, 0,0,N_paren_docon, 1}, /* (docon) */
{ 0, 1, 1, 0 , 0, 0,0,N_paren_dovar, 1}, /* (dovar) */
{ 0, 1, 1, 0 , 0, 0,0,N_paren_douser, 1}, /* (douser) */
{ 0, 0, 0, 0 , 0, 0,0,N_paren_dodefer, 1}, /* (dodefer) */
{ 1, 1, 0, 0 , 0, 0,0,N_paren_field, 1}, /* (dofield) */
{ 0, 1, 1, 0 , 0, 0,0,N_paren_doval, 1}, /* (dovalue) */
{ 0, 2, 2, 1 , 0, 0,0,N_paren_dodoes, 1}, /* (dodoes) */
{ 0, 0, 0, 0 , 0, 0,0,N_paren_does_handler, 1}, /* (does-handler) */
GROUPADD(9)
GROUP( control, 9)
{ 0, 0, 0, 0 , 0, 0,0,N_noop, 1}, /* noop */
{ 1, 1, 1, 1 , 0, 0,1,N_call, 1}, /* call */
{ 1, 0, 1, 0 , 0, 0,0,N_execute, 1}, /* execute */
{ 1, 0, 1, 0 , 0, 0,0,N_perform, 1}, /* perform */
{ 1, 0, 1, 1 , 0, 0,0,N_semis, 1}, /* ;s */
{ 2, 0, 1, 0 , 0, 0,0,N_unloop, 1}, /* unloop */
{ 1, 0, 0, 0 , 0, 0,1,N_lit_perform, 1}, /* lit-perform */
{ 1, 2, 2, 1 , 0, 0,1,N_does_exec, 1}, /* does-exec */
GROUPADD(8)
#ifdef HAS_GLOCALS
{ 2, 0, 0, 1 , 0, 0,2,N_branch_lp_plus_store_number, 1}, /* branch-lp+!# */
GROUPADD(1)
#endif
{ 1, 0, 0, 1 , 0, 0,1,N_branch, 1}, /* branch */
{ 2, 0, 1, 1 , 0, 0,1,N_question_branch, 1}, /* ?branch */
GROUPADD(2)
#ifdef HAS_GLOCALS
{ 3, 0, 1, 1 , 0, 0,2,N_question_branch_lp_plus_store_number, 1}, /* ?branch-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
{ 2, 0, 1, 1 , 0, 0,1,N_question_dupe_question_branch, 1}, /* ?dup-?branch */
{ 2, 0, 1, 1 , 0, 0,1,N_question_dupe_zero_equals_question_branch, 1}, /* ?dup-0=-?branch */
GROUPADD(2)
#endif
{ 2, 1, 0, 1 , 0, 0,1,N_paren_next, 1}, /* (next) */
GROUPADD(1)
#ifdef HAS_GLOCALS
{ 3, 1, 0, 1 , 0, 0,2,N_paren_next_lp_plus_store_number, 1}, /* (next)-lp+!# */
GROUPADD(1)
#endif
{ 3, 2, 0, 1 , 0, 0,1,N_paren_loop, 1}, /* (loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
{ 4, 2, 0, 1 , 0, 0,2,N_paren_loop_lp_plus_store_number, 1}, /* (loop)-lp+!# */
GROUPADD(1)
#endif
{ 4, 2, 1, 1 , 0, 0,1,N_paren_plus_loop, 1}, /* (+loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
{ 5, 2, 1, 1 , 0, 0,2,N_paren_plus_loop_lp_plus_store_number, 1}, /* (+loop)-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
{ 4, 2, 1, 1 , 0, 0,1,N_paren_minus_loop, 1}, /* (-loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
{ 5, 2, 1, 1 , 0, 0,2,N_paren_minus_loop_lp_plus_store_number, 1}, /* (-loop)-lp+!# */
GROUPADD(1)
#endif
{ 4, 2, 1, 1 , 0, 0,1,N_paren_symmetric_plus_loop, 1}, /* (s+loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
{ 5, 2, 1, 1 , 0, 0,2,N_paren_symmetric_plus_loop_lp_plus_store_number, 1}, /* (s+loop)-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#endif
{ 1, 2, 2, 0 , 0, 0,0,N_paren_for, 1}, /* (for) */
{ 2, 2, 2, 0 , 0, 0,0,N_paren_do, 1}, /* (do) */
{ 3, 2, 2, 1 , 0, 0,1,N_paren_question_do, 1}, /* (?do) */
GROUPADD(3)
#ifdef HAS_XCONDS
{ 3, 2, 2, 1 , 0, 0,1,N_paren_plus_do, 1}, /* (+do) */
{ 3, 2, 2, 1 , 0, 0,1,N_paren_u_plus_do, 1}, /* (u+do) */
{ 3, 2, 2, 1 , 0, 0,1,N_paren_minus_do, 1}, /* (-do) */
{ 3, 2, 2, 1 , 0, 0,1,N_paren_u_minus_do, 1}, /* (u-do) */
GROUPADD(4)
#endif
{ 1, 2, 1, 0 , 0, 0,0,N_i, 1}, /* i */
{ 2, 3, 1, 0 , 0, 0,0,N_i_tick, 1}, /* i' */
{ 3, 4, 1, 0 , 0, 0,0,N_j, 1}, /* j */
{ 5, 6, 1, 0 , 0, 0,0,N_k, 1}, /* k */
GROUPADD(4)
GROUP( strings, 44)
{ 3, 0, 1, 0 , 0, 0,0,N_move, 1}, /* move */
{ 3, 0, 1, 0 , 0, 0,0,N_c_move, 1}, /* cmove */
{ 3, 0, 1, 0 , 0, 0,0,N_c_move_up, 1}, /* cmove> */
{ 3, 0, 1, 0 , 0, 0,0,N_fill, 1}, /* fill */
{ 4, 1, 1, 0 , 0, 0,0,N_compare, 1}, /* compare */
{ 1, 1, 0, 0 , 0, 0,0,N_toupper, 1}, /* toupper */
{ 4, 1, 1, 0 , 0, 0,0,N_capscompare, 1}, /* capscompare */
{ 3, 2, 1, 0 , 0, 0,0,N_slash_string, 1}, /* /string */
GROUPADD(8)
GROUP( arith, 52)
{ 1, 1, 1, 0 , 0, 0,1,N_lit, 1}, /* lit */
{ 2, 1, 1, 0 , 0, 0,0,N_plus, 1}, /* + */
{ 2, 1, 0, 0 , 0, 0,1,N_lit_plus, 1}, /* lit+ */
{ 3, 2, 1, 0 , 0, 0,0,N_under_plus, 1}, /* under+ */
{ 2, 1, 1, 0 , 0, 0,0,N_minus, 1}, /* - */
{ 1, 1, 0, 0 , 0, 0,0,N_negate, 1}, /* negate */
{ 1, 1, 0, 0 , 0, 0,0,N_one_plus, 1}, /* 1+ */
{ 1, 1, 0, 0 , 0, 0,0,N_one_minus, 1}, /* 1- */
{ 2, 1, 1, 0 , 0, 0,0,N_max, 1}, /* max */
{ 2, 1, 1, 0 , 0, 0,0,N_min, 1}, /* min */
{ 1, 1, 0, 0 , 0, 0,0,N_abs, 1}, /* abs */
{ 2, 1, 1, 0 , 0, 0,0,N_star, 1}, /* * */
{ 2, 1, 1, 0 , 0, 0,0,N_slash, 1}, /* / */
{ 2, 1, 1, 0 , 0, 0,0,N_mod, 1}, /* mod */
{ 2, 2, 0, 0 , 0, 0,0,N_slash_mod, 1}, /* /mod */
{ 3, 2, 1, 0 , 0, 0,0,N_star_slash_mod, 1}, /* x/mod */
{ 3, 1, 1, 0 , 0, 0,0,N_star_slash, 1}, /* x/ */
{ 1, 1, 0, 0 , 0, 0,0,N_two_star, 1}, /* 2* */
{ 1, 1, 0, 0 , 0, 0,0,N_two_slash, 1}, /* 2/ */
{ 3, 2, 1, 0 , 0, 0,0,N_f_m_slash_mod, 1}, /* fm/mod */
{ 3, 2, 1, 0 , 0, 0,0,N_s_m_slash_rem, 1}, /* sm/rem */
{ 2, 2, 0, 0 , 0, 0,0,N_m_star, 1}, /* m* */
{ 2, 2, 0, 0 , 0, 0,0,N_u_m_star, 1}, /* um* */
{ 3, 2, 1, 0 , 0, 0,0,N_u_m_slash_mod, 1}, /* um/mod */
{ 3, 2, 1, 0 , 0, 0,0,N_m_plus, 1}, /* m+ */
{ 4, 2, 1, 0 , 0, 0,0,N_d_plus, 1}, /* d+ */
{ 4, 2, 1, 0 , 0, 0,0,N_d_minus, 1}, /* d- */
{ 2, 2, 0, 0 , 0, 0,0,N_d_negate, 1}, /* dnegate */
{ 2, 2, 0, 0 , 0, 0,0,N_d_two_star, 1}, /* d2* */
{ 2, 2, 0, 0 , 0, 0,0,N_d_two_slash, 1}, /* d2/ */
{ 2, 1, 1, 0 , 0, 0,0,N_and, 1}, /* and */
{ 2, 1, 1, 0 , 0, 0,0,N_or, 1}, /* or */
{ 2, 1, 1, 0 , 0, 0,0,N_x_or, 1}, /* xor */
{ 1, 1, 0, 0 , 0, 0,0,N_invert, 1}, /* invert */
{ 2, 1, 1, 0 , 0, 0,0,N_r_shift, 1}, /* rshift */
{ 2, 1, 1, 0 , 0, 0,0,N_l_shift, 1}, /* lshift */
GROUPADD(36)
GROUP( compare, 88)
{ 1, 1, 0, 0 , 0, 0,0,N_zero_equals, 1}, /* 0= */
{ 1, 1, 0, 0 , 0, 0,0,N_zero_not_equals, 1}, /* 0<> */
{ 1, 1, 0, 0 , 0, 0,0,N_zero_less_than, 1}, /* 0< */
{ 1, 1, 0, 0 , 0, 0,0,N_zero_greater_than, 1}, /* 0> */
{ 1, 1, 0, 0 , 0, 0,0,N_zero_less_or_equal, 1}, /* 0<= */
{ 1, 1, 0, 0 , 0, 0,0,N_zero_greater_or_equal, 1}, /* 0>= */
{ 2, 1, 1, 0 , 0, 0,0,N_equals, 1}, /* = */
{ 2, 1, 1, 0 , 0, 0,0,N_not_equals, 1}, /* <> */
{ 2, 1, 1, 0 , 0, 0,0,N_less_than, 1}, /* < */
{ 2, 1, 1, 0 , 0, 0,0,N_greater_than, 1}, /* > */
{ 2, 1, 1, 0 , 0, 0,0,N_less_or_equal, 1}, /* <= */
{ 2, 1, 1, 0 , 0, 0,0,N_greater_or_equal, 1}, /* >= */
{ 2, 1, 1, 0 , 0, 0,0,N_u_equals, 1}, /* u= */
{ 2, 1, 1, 0 , 0, 0,0,N_u_not_equals, 1}, /* u<> */
{ 2, 1, 1, 0 , 0, 0,0,N_u_less_than, 1}, /* u< */
{ 2, 1, 1, 0 , 0, 0,0,N_u_greater_than, 1}, /* u> */
{ 2, 1, 1, 0 , 0, 0,0,N_u_less_or_equal, 1}, /* u<= */
{ 2, 1, 1, 0 , 0, 0,0,N_u_greater_or_equal, 1}, /* u>= */
GROUPADD(18)
#ifdef HAS_DCOMPS
{ 4, 1, 1, 0 , 0, 0,0,N_d_equals, 1}, /* d= */
{ 4, 1, 1, 0 , 0, 0,0,N_d_not_equals, 1}, /* d<> */
{ 4, 1, 1, 0 , 0, 0,0,N_d_less_than, 1}, /* d< */
{ 4, 1, 1, 0 , 0, 0,0,N_d_greater_than, 1}, /* d> */
{ 4, 1, 1, 0 , 0, 0,0,N_d_less_or_equal, 1}, /* d<= */
{ 4, 1, 1, 0 , 0, 0,0,N_d_greater_or_equal, 1}, /* d>= */
{ 2, 1, 1, 0 , 0, 0,0,N_d_zero_equals, 1}, /* d0= */
{ 2, 1, 1, 0 , 0, 0,0,N_d_zero_not_equals, 1}, /* d0<> */
{ 2, 1, 1, 0 , 0, 0,0,N_d_zero_less_than, 1}, /* d0< */
{ 2, 1, 1, 0 , 0, 0,0,N_d_zero_greater_than, 1}, /* d0> */
{ 2, 1, 1, 0 , 0, 0,0,N_d_zero_less_or_equal, 1}, /* d0<= */
{ 2, 1, 1, 0 , 0, 0,0,N_d_zero_greater_or_equal, 1}, /* d0>= */
{ 4, 1, 1, 0 , 0, 0,0,N_d_u_equals, 1}, /* du= */
{ 4, 1, 1, 0 , 0, 0,0,N_d_u_not_equals, 1}, /* du<> */
{ 4, 1, 1, 0 , 0, 0,0,N_d_u_less_than, 1}, /* du< */
{ 4, 1, 1, 0 , 0, 0,0,N_d_u_greater_than, 1}, /* du> */
{ 4, 1, 1, 0 , 0, 0,0,N_d_u_less_or_equal, 1}, /* du<= */
{ 4, 1, 1, 0 , 0, 0,0,N_d_u_greater_or_equal, 1}, /* du>= */
GROUPADD(18)
#endif
{ 3, 1, 1, 0 , 0, 0,0,N_within, 1}, /* within */
GROUPADD(1)
GROUP( stack, 125)
{ 1, 1, 1, 0 , 0, 0,1,N_useraddr, 1}, /* useraddr */
{ 1, 0, 1, 0 , 0, 0,0,N_up_store, 1}, /* up! */
{ 0, 1, 1, 0 , 0, 0,0,N_sp_fetch, 1}, /* sp@ */
{ 1, 0, 1, 0 , 0, 0,0,N_sp_store, 1}, /* sp! */
{ 0, 1, 1, 0 , 0, 0,0,N_rp_fetch, 1}, /* rp@ */
{ 1, 0, 1, 0 , 0, 0,0,N_rp_store, 1}, /* rp! */
GROUPADD(6)
#ifdef HAS_FLOATING
{ 0, 1, 1, 0 , 0, 0,0,N_fp_fetch, 1}, /* fp@ */
{ 1, 0, 1, 0 , 0, 0,0,N_fp_store, 1}, /* fp! */
GROUPADD(2)
#endif
{ 1, 1, 2, 0 , 0, 0,0,N_to_r, 1}, /* >r */
{ 1, 1, 2, 0 , 0, 0,0,N_r_from, 1}, /* r> */
{ 1, 0, 1, 0 , 0, 0,0,N_rdrop, 1}, /* rdrop */
{ 2, 2, 2, 0 , 0, 0,0,N_two_to_r, 1}, /* 2>r */
{ 2, 2, 2, 0 , 0, 0,0,N_two_r_from, 1}, /* 2r> */
{ 2, 4, 1, 0 , 0, 0,0,N_two_r_fetch, 1}, /* 2r@ */
{ 2, 0, 1, 0 , 0, 0,0,N_two_r_drop, 1}, /* 2rdrop */
{ 2, 3, 1, 0 , 0, 0,0,N_over, 1}, /* over */
{ 1, 0, 1, 0 , 0, 0,0,N_drop, 1}, /* drop */
{ 2, 2, 0, 0 , 0, 0,0,N_swap, 1}, /* swap */
{ 1, 2, 1, 0 , 0, 0,0,N_dupe, 1}, /* dup */
{ 3, 3, 0, 0 , 0, 0,0,N_rote, 1}, /* rot */
{ 3, 3, 0, 0 , 0, 0,0,N_not_rote, 1}, /* -rot */
{ 2, 1, 1, 0 , 0, 0,0,N_nip, 1}, /* nip */
{ 2, 3, 1, 0 , 0, 0,0,N_tuck, 1}, /* tuck */
{ 1, 1, 0, 0 , 0, 0,0,N_question_dupe, 1}, /* ?dup */
{ 1, 1, 0, 0 , 0, 0,0,N_pick, 1}, /* pick */
{ 2, 0, 1, 0 , 0, 0,0,N_two_drop, 1}, /* 2drop */
{ 2, 4, 1, 0 , 0, 0,0,N_two_dupe, 1}, /* 2dup */
{ 4, 6, 1, 0 , 0, 0,0,N_two_over, 1}, /* 2over */
{ 4, 4, 0, 0 , 0, 0,0,N_two_swap, 1}, /* 2swap */
{ 6, 6, 0, 0 , 0, 0,0,N_two_rote, 1}, /* 2rot */
{ 4, 2, 1, 0 , 0, 0,0,N_two_nip, 1}, /* 2nip */
{ 4, 6, 1, 0 , 0, 0,0,N_two_tuck, 1}, /* 2tuck */
GROUPADD(24)
GROUP( memory, 157)
{ 1, 1, 0, 0 , 0, 0,0,N_fetch, 1}, /* @ */
{ 1, 1, 1, 0 , 0, 0,1,N_lit_fetch, 1}, /* lit@ */
{ 2, 0, 1, 0 , 0, 0,0,N_store, 1}, /* ! */
{ 2, 0, 1, 0 , 0, 0,0,N_plus_store, 1}, /* +! */
{ 1, 1, 0, 0 , 0, 0,0,N_c_fetch, 1}, /* c@ */
{ 2, 0, 1, 0 , 0, 0,0,N_c_store, 1}, /* c! */
{ 3, 0, 1, 0 , 0, 0,0,N_two_store, 1}, /* 2! */
{ 1, 2, 1, 0 , 0, 0,0,N_two_fetch, 1}, /* 2@ */
{ 1, 1, 0, 0 , 0, 0,0,N_cell_plus, 1}, /* cell+ */
{ 1, 1, 0, 0 , 0, 0,0,N_cells, 1}, /* cells */
{ 1, 1, 0, 0 , 0, 0,0,N_char_plus, 1}, /* char+ */
{ 1, 1, 0, 0 , 0, 0,0,N_paren_chars, 1}, /* (chars) */
{ 1, 2, 1, 0 , 0, 0,0,N_count, 1}, /* count */
GROUPADD(13)
GROUP( compiler, 170)
GROUPADD(0)
#ifdef HAS_F83HEADERSTRING
{ 3, 1, 1, 0 , 0, 0,0,N_paren_f83find, 1}, /* (f83find) */
GROUPADD(1)
#else /* 171 */
{ 3, 1, 1, 0 , 0, 0,0,N_paren_listlfind, 1}, /* (listlfind) */
GROUPADD(1)
#ifdef HAS_HASH
{ 3, 1, 1, 0 , 0, 0,0,N_paren_hashlfind, 1}, /* (hashlfind) */
{ 3, 1, 1, 0 , 0, 0,0,N_paren_tablelfind, 1}, /* (tablelfind) */
{ 3, 1, 1, 0 , 0, 0,0,N_paren_hashkey1, 1}, /* (hashkey1) */
GROUPADD(3)
#endif
GROUPADD(0)
#endif
{ 2, 2, 0, 0 , 0, 0,0,N_paren_parse_white, 1}, /* (parse-white) */
{ 1, 1, 0, 0 , 0, 0,0,N_aligned, 1}, /* aligned */
{ 1, 1, 0, 0 , 0, 0,0,N_f_aligned, 1}, /* faligned */
{ 0, 1, 1, 0 , 0, 0,0,N_threading_method, 1}, /* threading-method */
GROUPADD(4)
GROUP( hostos, 179)
{ 1, 1, 0, 0 , 0, 0,0,N_paren_key_file, 1}, /* key-file */
{ 1, 1, 0, 0 , 0, 0,0,N_key_q_file, 1}, /* key?-file */
{ 0, 1, 1, 0 , 0, 0,0,N_stdin, 1}, /* stdin */
{ 0, 1, 1, 0 , 0, 0,0,N_stdout, 1}, /* stdout */
{ 0, 1, 1, 0 , 0, 0,0,N_stderr, 1}, /* stderr */
GROUPADD(5)
#ifdef HAS_OS
{ 0, 2, 1, 0 , 0, 0,0,N_form, 1}, /* form */
{ 1, 1, 0, 0 , 0, 0,0,N_wcwidth, 1}, /* wcwidth */
{ 2, 0, 1, 0 , 0, 0,0,N_flush_icache, 1}, /* flush-icache */
{ 1, 0, 1, 0 , 0, 0,0,N_paren_bye, 1}, /* (bye) */
{ 2, 2, 0, 0 , 0, 0,0,N_paren_system, 1}, /* (system) */
{ 2, 2, 0, 0 , 0, 0,0,N_getenv, 1}, /* getenv */
{ 3, 2, 1, 0 , 0, 0,0,N_open_pipe, 1}, /* open-pipe */
{ 1, 2, 1, 0 , 0, 0,0,N_close_pipe, 1}, /* close-pipe */
{ 0, 6, 1, 0 , 0, 0,0,N_time_and_date, 1}, /* time&date */
{ 1, 0, 1, 0 , 0, 0,0,N_ms, 1}, /* ms */
{ 1, 2, 1, 0 , 0, 0,0,N_allocate, 1}, /* allocate */
{ 1, 1, 0, 0 , 0, 0,0,N_free, 1}, /* free */
{ 2, 2, 0, 0 , 0, 0,0,N_resize, 1}, /* resize */
{ 1, 2, 1, 0 , 0, 0,0,N_strerror, 1}, /* strerror */
{ 1, 2, 1, 0 , 0, 0,0,N_strsignal, 1}, /* strsignal */
{ 1, 0, 1, 0 , 0, 0,0,N_call_c, 1}, /* call-c */
GROUPADD(16)
#endif
GROUPADD(0)
#ifdef HAS_FILE
{ 1, 1, 0, 0 , 0, 0,0,N_close_file, 1}, /* close-file */
{ 3, 2, 1, 0 , 0, 0,0,N_open_file, 1}, /* open-file */
{ 3, 2, 1, 0 , 0, 0,0,N_create_file, 1}, /* create-file */
{ 2, 1, 1, 0 , 0, 0,0,N_delete_file, 1}, /* delete-file */
{ 4, 1, 1, 0 , 0, 0,0,N_rename_file, 1}, /* rename-file */
{ 1, 3, 1, 0 , 0, 0,0,N_file_position, 1}, /* file-position */
{ 3, 1, 1, 0 , 0, 0,0,N_reposition_file, 1}, /* reposition-file */
{ 1, 3, 1, 0 , 0, 0,0,N_file_size, 1}, /* file-size */
{ 3, 1, 1, 0 , 0, 0,0,N_resize_file, 1}, /* resize-file */
{ 3, 2, 1, 0 , 0, 0,0,N_read_file, 1}, /* read-file */
{ 3, 4, 1, 0 , 0, 0,0,N_paren_read_line, 1}, /* (read-line) */
GROUPADD(11)
#endif
{ 3, 1, 1, 0 , 0, 0,0,N_write_file, 1}, /* write-file */
{ 2, 1, 1, 0 , 0, 0,0,N_emit_file, 1}, /* emit-file */
GROUPADD(2)
#ifdef HAS_FILE
{ 1, 1, 0, 0 , 0, 0,0,N_flush_file, 1}, /* flush-file */
{ 2, 2, 0, 0 , 0, 0,0,N_file_status, 1}, /* file-status */
{ 1, 1, 0, 0 , 0, 0,0,N_file_eof_query, 1}, /* file-eof? */
{ 2, 2, 0, 0 , 0, 0,0,N_open_dir, 1}, /* open-dir */
{ 3, 3, 0, 0 , 0, 0,0,N_read_dir, 1}, /* read-dir */
{ 1, 1, 0, 0 , 0, 0,0,N_close_dir, 1}, /* close-dir */
{ 4, 1, 1, 0 , 0, 0,0,N_match_file, 1}, /* filename-match */
{ 2, 1, 1, 0 , 0, 0,0,N_set_dir, 1}, /* set-dir */
{ 2, 2, 0, 0 , 0, 0,0,N_get_dir, 1}, /* get-dir */
{ 3, 1, 1, 0 , 0, 0,0,N_equals_mkdir, 1}, /* =mkdir */
GROUPADD(10)
#endif
{ 0, 2, 1, 0 , 0, 0,0,N_newline, 1}, /* newline */
GROUPADD(1)
#ifdef HAS_OS
{ 0, 2, 1, 0 , 0, 0,0,N_utime, 1}, /* utime */
{ 0, 4, 1, 0 , 0, 0,0,N_cputime, 1}, /* cputime */
GROUPADD(2)
#endif
GROUPADD(0)
#ifdef HAS_FLOATING
GROUPADD(0)
GROUP( floating, 226)
{ 2, 1, 2, 0 , 0, 0,0,N_f_equals, 1}, /* f= */
{ 2, 1, 2, 0 , 0, 0,0,N_f_not_equals, 1}, /* f<> */
{ 2, 1, 2, 0 , 0, 0,0,N_f_less_than, 1}, /* f< */
{ 2, 1, 2, 0 , 0, 0,0,N_f_greater_than, 1}, /* f> */
{ 2, 1, 2, 0 , 0, 0,0,N_f_less_or_equal, 1}, /* f<= */
{ 2, 1, 2, 0 , 0, 0,0,N_f_greater_or_equal, 1}, /* f>= */
{ 1, 1, 2, 0 , 0, 0,0,N_f_zero_equals, 1}, /* f0= */
{ 1, 1, 2, 0 , 0, 0,0,N_f_zero_not_equals, 1}, /* f0<> */
{ 1, 1, 2, 0 , 0, 0,0,N_f_zero_less_than, 1}, /* f0< */
{ 1, 1, 2, 0 , 0, 0,0,N_f_zero_greater_than, 1}, /* f0> */
{ 1, 1, 2, 0 , 0, 0,0,N_f_zero_less_or_equal, 1}, /* f0<= */
{ 1, 1, 2, 0 , 0, 0,0,N_f_zero_greater_or_equal, 1}, /* f0>= */
{ 1, 1, 2, 0 , 0, 0,0,N_s_to_f, 1}, /* s>f */
{ 2, 1, 2, 0 , 0, 0,0,N_d_to_f, 1}, /* d>f */
{ 1, 2, 2, 0 , 0, 0,0,N_f_to_d, 1}, /* f>d */
{ 1, 1, 2, 0 , 0, 0,0,N_f_to_s, 1}, /* f>s */
{ 2, 0, 2, 0 , 0, 0,0,N_f_store, 1}, /* f! */
{ 1, 1, 2, 0 , 0, 0,0,N_f_fetch, 1}, /* f@ */
{ 1, 1, 2, 0 , 0, 0,0,N_d_f_fetch, 1}, /* df@ */
{ 2, 0, 2, 0 , 0, 0,0,N_d_f_store, 1}, /* df! */
{ 1, 1, 2, 0 , 0, 0,0,N_s_f_fetch, 1}, /* sf@ */
{ 2, 0, 2, 0 , 0, 0,0,N_s_f_store, 1}, /* sf! */
{ 2, 1, 1, 0 , 0, 0,0,N_f_plus, 1}, /* f+ */
{ 2, 1, 1, 0 , 0, 0,0,N_f_minus, 1}, /* f- */
{ 2, 1, 1, 0 , 0, 0,0,N_f_star, 1}, /* f* */
{ 2, 1, 1, 0 , 0, 0,0,N_f_slash, 1}, /* f/ */
{ 2, 1, 1, 0 , 0, 0,0,N_f_star_star, 1}, /* f** */
{ 2, 1, 1, 0 , 0, 0,0,N_fm_star, 1}, /* fm* */
{ 2, 1, 1, 0 , 0, 0,0,N_fm_slash, 1}, /* fm/ */
{ 3, 1, 1, 0 , 0, 0,0,N_fm_star_slash, 1}, /* fmx/ */
{ 1, 1, 0, 0 , 0, 0,0,N_fm_square, 1}, /* f**2 */
{ 1, 1, 0, 0 , 0, 0,0,N_f_negate, 1}, /* fnegate */
{ 1, 0, 1, 0 , 0, 0,0,N_f_drop, 1}, /* fdrop */
{ 1, 2, 1, 0 , 0, 0,0,N_f_dupe, 1}, /* fdup */
{ 2, 2, 0, 0 , 0, 0,0,N_f_swap, 1}, /* fswap */
{ 2, 3, 1, 0 , 0, 0,0,N_f_over, 1}, /* fover */
{ 3, 3, 0, 0 , 0, 0,0,N_f_rote, 1}, /* frot */
{ 2, 1, 1, 0 , 0, 0,0,N_f_nip, 1}, /* fnip */
{ 2, 3, 1, 0 , 0, 0,0,N_f_tuck, 1}, /* ftuck */
{ 1, 1, 0, 0 , 0, 0,0,N_float_plus, 1}, /* float+ */
{ 1, 1, 0, 0 , 0, 0,0,N_floats, 1}, /* floats */
{ 1, 1, 0, 0 , 0, 0,0,N_floor, 1}, /* floor */
{ 1, 1, 0, 0 , 0, 0,0,N_f_round, 1}, /* fround */
{ 2, 1, 1, 0 , 0, 0,0,N_f_max, 1}, /* fmax */
{ 2, 1, 1, 0 , 0, 0,0,N_f_min, 1}, /* fmin */
{ 3, 3, 2, 0 , 0, 0,0,N_represent, 1}, /* represent */
{ 2, 1, 1, 0 , 0, 0,0,N_to_float, 1}, /* >float */
{ 1, 1, 0, 0 , 0, 0,0,N_f_abs, 1}, /* fabs */
{ 1, 1, 0, 0 , 0, 0,0,N_f_a_cos, 1}, /* facos */
{ 1, 1, 0, 0 , 0, 0,0,N_f_a_sine, 1}, /* fasin */
{ 1, 1, 0, 0 , 0, 0,0,N_f_a_tan, 1}, /* fatan */
{ 2, 1, 1, 0 , 0, 0,0,N_f_a_tan_two, 1}, /* fatan2 */
{ 1, 1, 0, 0 , 0, 0,0,N_f_cos, 1}, /* fcos */
{ 1, 1, 0, 0 , 0, 0,0,N_f_e_x_p, 1}, /* fexp */
{ 1, 1, 0, 0 , 0, 0,0,N_f_e_x_p_m_one, 1}, /* fexpm1 */
{ 1, 1, 0, 0 , 0, 0,0,N_f_l_n, 1}, /* fln */
{ 1, 1, 0, 0 , 0, 0,0,N_f_l_n_p_one, 1}, /* flnp1 */
{ 1, 1, 0, 0 , 0, 0,0,N_f_log, 1}, /* flog */
{ 1, 1, 0, 0 , 0, 0,0,N_f_a_log, 1}, /* falog */
{ 1, 1, 0, 0 , 0, 0,0,N_f_sine, 1}, /* fsin */
{ 1, 2, 1, 0 , 0, 0,0,N_f_sine_cos, 1}, /* fsincos */
{ 1, 1, 0, 0 , 0, 0,0,N_f_square_root, 1}, /* fsqrt */
{ 1, 1, 0, 0 , 0, 0,0,N_f_tan, 1}, /* ftan */
{ 1, 1, 0, 0 , 0, 0,0,N_f_cinch, 1}, /* fsinh */
{ 1, 1, 0, 0 , 0, 0,0,N_f_cosh, 1}, /* fcosh */
{ 1, 1, 0, 0 , 0, 0,0,N_f_tan_h, 1}, /* ftanh */
{ 1, 1, 0, 0 , 0, 0,0,N_f_a_cinch, 1}, /* fasinh */
{ 1, 1, 0, 0 , 0, 0,0,N_f_a_cosh, 1}, /* facosh */
{ 1, 1, 0, 0 , 0, 0,0,N_f_a_tan_h, 1}, /* fatanh */
{ 1, 1, 0, 0 , 0, 0,0,N_s_floats, 1}, /* sfloats */
{ 1, 1, 0, 0 , 0, 0,0,N_d_floats, 1}, /* dfloats */
{ 1, 1, 0, 0 , 0, 0,0,N_s_f_aligned, 1}, /* sfaligned */
{ 1, 1, 0, 0 , 0, 0,0,N_d_f_aligned, 1}, /* dfaligned */
{ 5, 1, 2, 0 , 0, 0,0,N_v_star, 1}, /* v* */
{ 6, 0, 2, 0 , 0, 0,0,N_faxpy, 1}, /* faxpy */
GROUPADD(75)
#endif
GROUPADD(0)
#ifdef HAS_GLOCALS
GROUPADD(0)
GROUP( locals, 301)
{ 1, 1, 1, 0 , 0, 0,1,N_fetch_local_number, 1}, /* @local# */
{ 0, 1, 1, 0 , 0, 0,0,N_fetch_local_zero, 1}, /* @local0 */
{ 0, 1, 1, 0 , 0, 0,0,N_fetch_local_four, 1}, /* @local1 */
{ 0, 1, 1, 0 , 0, 0,0,N_fetch_local_eight, 1}, /* @local2 */
{ 0, 1, 1, 0 , 0, 0,0,N_fetch_local_twelve, 1}, /* @local3 */
GROUPADD(5)
#ifdef HAS_FLOATING
{ 1, 1, 1, 0 , 0, 0,1,N_f_fetch_local_number, 1}, /* f@local# */
{ 0, 1, 1, 0 , 0, 0,0,N_f_fetch_local_zero, 1}, /* f@local0 */
{ 0, 1, 1, 0 , 0, 0,0,N_f_fetch_local_eight, 1}, /* f@local1 */
GROUPADD(3)
#endif
{ 1, 1, 1, 0 , 0, 0,1,N_laddr_number, 1}, /* laddr# */
{ 1, 0, 0, 0 , 0, 0,1,N_lp_plus_store_number, 1}, /* lp+!# */
{ 0, 0, 0, 0 , 0, 0,0,N_minus_four_lp_plus_store, 1}, /* lp- */
{ 0, 0, 0, 0 , 0, 0,0,N_eight_lp_plus_store, 1}, /* lp+ */
{ 0, 0, 0, 0 , 0, 0,0,N_sixteen_lp_plus_store, 1}, /* lp+2 */
{ 1, 0, 1, 0 , 0, 0,0,N_lp_store, 1}, /* lp! */
{ 1, 0, 1, 0 , 0, 0,0,N_to_l, 1}, /* >l */
GROUPADD(7)
#ifdef HAS_FLOATING
{ 1, 0, 1, 0 , 0, 0,0,N_f_to_l, 1}, /* f>l */
{ 1, 1, 2, 0 , 0, 0,0,N_fpick, 1}, /* fpick */
GROUPADD(2)
#endif
GROUPADD(0)
#endif
GROUPADD(0)
#ifdef HAS_OS
GROUPADD(0)
GROUP( syslib, 318)
{ 2, 1, 1, 0 , 0, 0,0,N_open_lib, 1}, /* open-lib */
{ 3, 1, 1, 0 , 0, 0,0,N_lib_sym, 1}, /* lib-sym */
{ 1, 0, 1, 0 , 0, 0,0,N_wcall, 1}, /* wcall */
{ 1, 1, 0, 0 , 0, 0,0,N_u_w_fetch, 1}, /* uw@ */
{ 1, 1, 0, 0 , 0, 0,0,N_s_w_fetch, 1}, /* sw@ */
{ 2, 0, 1, 0 , 0, 0,0,N_w_store, 1}, /* w! */
{ 1, 1, 0, 0 , 0, 0,0,N_u_l_fetch, 1}, /* ul@ */
{ 1, 1, 0, 0 , 0, 0,0,N_s_l_fetch, 1}, /* sl@ */
{ 2, 0, 1, 0 , 0, 0,0,N_l_store, 1}, /* l! */
{ 0, 2, 1, 0 , 0, 0,0,N_lib_error, 1}, /* lib-error */
GROUPADD(10)
#endif
GROUPADD(0)
GROUP( peephole, 328)
GROUPADD(0)
#ifdef HAS_PEEPHOLE
{ 1, 0, 1, 0 , 0, 0,0,N_compile_prim1, 1}, /* compile-prim1 */
{ 0, 0, 0, 0 , 0, 0,0,N_finish_code, 1}, /* finish-code */
{ 1, 1, 0, 0 , 0, 0,0,N_forget_dyncode, 1}, /* forget-dyncode */
{ 1, 1, 0, 0 , 0, 0,0,N_decompile_prim, 1}, /* decompile-prim */
{ 1, 0, 0, 0 , 0, 0,1,N_set_next_code, 1}, /* set-next-code */
{ 2, 1, 1, 0 , 0, 0,2,N_call2, 1}, /* call2 */
{ 0, 1, 1, 0 , 0, 0,0,N_tag_offsets, 1}, /* tag-offsets */
GROUPADD(7)
#endif
GROUPADD(0)
GROUP( static_super, 335)
GROUPADD(0)
GROUP( end, 335)
