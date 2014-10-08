N_paren_docol, /* (docol) */
N_paren_docon, /* (docon) */
N_paren_dovar, /* (dovar) */
N_paren_douser, /* (douser) */
N_paren_dodefer, /* (dodefer) */
N_paren_field, /* (dofield) */
N_paren_doval, /* (dovalue) */
N_paren_dodoes, /* (dodoes) */
N_paren_does_handler, /* (does-handler) */
GROUPADD(9)
GROUP( control, 9)
N_noop, /* noop */
N_call, /* call */
N_execute, /* execute */
N_perform, /* perform */
N_semis, /* ;s */
N_unloop, /* unloop */
N_lit_perform, /* lit-perform */
N_does_exec, /* does-exec */
GROUPADD(8)
#ifdef HAS_GLOCALS
N_branch_lp_plus_store_number, /* branch-lp+!# */
GROUPADD(1)
#endif
N_branch, /* branch */
N_question_branch, /* ?branch */
GROUPADD(2)
#ifdef HAS_GLOCALS
N_question_branch_lp_plus_store_number, /* ?branch-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
N_question_dupe_question_branch, /* ?dup-?branch */
N_question_dupe_zero_equals_question_branch, /* ?dup-0=-?branch */
GROUPADD(2)
#endif
N_paren_next, /* (next) */
GROUPADD(1)
#ifdef HAS_GLOCALS
N_paren_next_lp_plus_store_number, /* (next)-lp+!# */
GROUPADD(1)
#endif
N_paren_loop, /* (loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
N_paren_loop_lp_plus_store_number, /* (loop)-lp+!# */
GROUPADD(1)
#endif
N_paren_plus_loop, /* (+loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
N_paren_plus_loop_lp_plus_store_number, /* (+loop)-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#ifdef HAS_XCONDS
N_paren_minus_loop, /* (-loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
N_paren_minus_loop_lp_plus_store_number, /* (-loop)-lp+!# */
GROUPADD(1)
#endif
N_paren_symmetric_plus_loop, /* (s+loop) */
GROUPADD(1)
#ifdef HAS_GLOCALS
N_paren_symmetric_plus_loop_lp_plus_store_number, /* (s+loop)-lp+!# */
GROUPADD(1)
#endif
GROUPADD(0)
#endif
N_paren_for, /* (for) */
N_paren_do, /* (do) */
N_paren_question_do, /* (?do) */
GROUPADD(3)
#ifdef HAS_XCONDS
N_paren_plus_do, /* (+do) */
N_paren_u_plus_do, /* (u+do) */
N_paren_minus_do, /* (-do) */
N_paren_u_minus_do, /* (u-do) */
GROUPADD(4)
#endif
N_i, /* i */
N_i_tick, /* i' */
N_j, /* j */
N_k, /* k */
GROUPADD(4)
GROUP( strings, 44)
N_move, /* move */
N_c_move, /* cmove */
N_c_move_up, /* cmove> */
N_fill, /* fill */
N_compare, /* compare */
N_toupper, /* toupper */
N_capscompare, /* capscompare */
N_slash_string, /* /string */
GROUPADD(8)
GROUP( arith, 52)
N_lit, /* lit */
N_plus, /* + */
N_lit_plus, /* lit+ */
N_under_plus, /* under+ */
N_minus, /* - */
N_negate, /* negate */
N_one_plus, /* 1+ */
N_one_minus, /* 1- */
N_max, /* max */
N_min, /* min */
N_abs, /* abs */
N_star, /* * */
N_slash, /* / */
N_mod, /* mod */
N_slash_mod, /* /mod */
N_star_slash_mod, /* x/mod */
N_star_slash, /* x/ */
N_two_star, /* 2* */
N_two_slash, /* 2/ */
N_f_m_slash_mod, /* fm/mod */
N_s_m_slash_rem, /* sm/rem */
N_m_star, /* m* */
N_u_m_star, /* um* */
N_u_m_slash_mod, /* um/mod */
N_m_plus, /* m+ */
N_d_plus, /* d+ */
N_d_minus, /* d- */
N_d_negate, /* dnegate */
N_d_two_star, /* d2* */
N_d_two_slash, /* d2/ */
N_and, /* and */
N_or, /* or */
N_x_or, /* xor */
N_invert, /* invert */
N_r_shift, /* rshift */
N_l_shift, /* lshift */
GROUPADD(36)
GROUP( compare, 88)
N_zero_equals, /* 0= */
N_zero_not_equals, /* 0<> */
N_zero_less_than, /* 0< */
N_zero_greater_than, /* 0> */
N_zero_less_or_equal, /* 0<= */
N_zero_greater_or_equal, /* 0>= */
N_equals, /* = */
N_not_equals, /* <> */
N_less_than, /* < */
N_greater_than, /* > */
N_less_or_equal, /* <= */
N_greater_or_equal, /* >= */
N_u_equals, /* u= */
N_u_not_equals, /* u<> */
N_u_less_than, /* u< */
N_u_greater_than, /* u> */
N_u_less_or_equal, /* u<= */
N_u_greater_or_equal, /* u>= */
GROUPADD(18)
#ifdef HAS_DCOMPS
N_d_equals, /* d= */
N_d_not_equals, /* d<> */
N_d_less_than, /* d< */
N_d_greater_than, /* d> */
N_d_less_or_equal, /* d<= */
N_d_greater_or_equal, /* d>= */
N_d_zero_equals, /* d0= */
N_d_zero_not_equals, /* d0<> */
N_d_zero_less_than, /* d0< */
N_d_zero_greater_than, /* d0> */
N_d_zero_less_or_equal, /* d0<= */
N_d_zero_greater_or_equal, /* d0>= */
N_d_u_equals, /* du= */
N_d_u_not_equals, /* du<> */
N_d_u_less_than, /* du< */
N_d_u_greater_than, /* du> */
N_d_u_less_or_equal, /* du<= */
N_d_u_greater_or_equal, /* du>= */
GROUPADD(18)
#endif
N_within, /* within */
GROUPADD(1)
GROUP( stack, 125)
N_useraddr, /* useraddr */
N_up_store, /* up! */
N_sp_fetch, /* sp@ */
N_sp_store, /* sp! */
N_rp_fetch, /* rp@ */
N_rp_store, /* rp! */
GROUPADD(6)
#ifdef HAS_FLOATING
N_fp_fetch, /* fp@ */
N_fp_store, /* fp! */
GROUPADD(2)
#endif
N_to_r, /* >r */
N_r_from, /* r> */
N_rdrop, /* rdrop */
N_two_to_r, /* 2>r */
N_two_r_from, /* 2r> */
N_two_r_fetch, /* 2r@ */
N_two_r_drop, /* 2rdrop */
N_over, /* over */
N_drop, /* drop */
N_swap, /* swap */
N_dupe, /* dup */
N_rote, /* rot */
N_not_rote, /* -rot */
N_nip, /* nip */
N_tuck, /* tuck */
N_question_dupe, /* ?dup */
N_pick, /* pick */
N_two_drop, /* 2drop */
N_two_dupe, /* 2dup */
N_two_over, /* 2over */
N_two_swap, /* 2swap */
N_two_rote, /* 2rot */
N_two_nip, /* 2nip */
N_two_tuck, /* 2tuck */
GROUPADD(24)
GROUP( memory, 157)
N_fetch, /* @ */
N_lit_fetch, /* lit@ */
N_store, /* ! */
N_plus_store, /* +! */
N_c_fetch, /* c@ */
N_c_store, /* c! */
N_two_store, /* 2! */
N_two_fetch, /* 2@ */
N_cell_plus, /* cell+ */
N_cells, /* cells */
N_char_plus, /* char+ */
N_paren_chars, /* (chars) */
N_count, /* count */
GROUPADD(13)
GROUP( compiler, 170)
GROUPADD(0)
#ifdef HAS_F83HEADERSTRING
N_paren_f83find, /* (f83find) */
GROUPADD(1)
#else /* 171 */
N_paren_listlfind, /* (listlfind) */
GROUPADD(1)
#ifdef HAS_HASH
N_paren_hashlfind, /* (hashlfind) */
N_paren_tablelfind, /* (tablelfind) */
N_paren_hashkey1, /* (hashkey1) */
GROUPADD(3)
#endif
GROUPADD(0)
#endif
N_paren_parse_white, /* (parse-white) */
N_aligned, /* aligned */
N_f_aligned, /* faligned */
N_threading_method, /* threading-method */
GROUPADD(4)
GROUP( hostos, 179)
N_paren_key_file, /* key-file */
N_key_q_file, /* key?-file */
N_stdin, /* stdin */
N_stdout, /* stdout */
N_stderr, /* stderr */
GROUPADD(5)
#ifdef HAS_OS
N_form, /* form */
N_wcwidth, /* wcwidth */
N_flush_icache, /* flush-icache */
N_paren_bye, /* (bye) */
N_paren_system, /* (system) */
N_getenv, /* getenv */
N_open_pipe, /* open-pipe */
N_close_pipe, /* close-pipe */
N_time_and_date, /* time&date */
N_ms, /* ms */
N_allocate, /* allocate */
N_free, /* free */
N_resize, /* resize */
N_strerror, /* strerror */
N_strsignal, /* strsignal */
N_call_c, /* call-c */
GROUPADD(16)
#endif
GROUPADD(0)
#ifdef HAS_FILE
N_close_file, /* close-file */
N_open_file, /* open-file */
N_create_file, /* create-file */
N_delete_file, /* delete-file */
N_rename_file, /* rename-file */
N_file_position, /* file-position */
N_reposition_file, /* reposition-file */
N_file_size, /* file-size */
N_resize_file, /* resize-file */
N_read_file, /* read-file */
N_paren_read_line, /* (read-line) */
GROUPADD(11)
#endif
N_write_file, /* write-file */
N_emit_file, /* emit-file */
GROUPADD(2)
#ifdef HAS_FILE
N_flush_file, /* flush-file */
N_file_status, /* file-status */
N_file_eof_query, /* file-eof? */
N_open_dir, /* open-dir */
N_read_dir, /* read-dir */
N_close_dir, /* close-dir */
N_match_file, /* filename-match */
N_set_dir, /* set-dir */
N_get_dir, /* get-dir */
N_equals_mkdir, /* =mkdir */
GROUPADD(10)
#endif
N_newline, /* newline */
GROUPADD(1)
#ifdef HAS_OS
N_utime, /* utime */
N_cputime, /* cputime */
GROUPADD(2)
#endif
GROUPADD(0)
#ifdef HAS_FLOATING
GROUPADD(0)
GROUP( floating, 226)
N_f_equals, /* f= */
N_f_not_equals, /* f<> */
N_f_less_than, /* f< */
N_f_greater_than, /* f> */
N_f_less_or_equal, /* f<= */
N_f_greater_or_equal, /* f>= */
N_f_zero_equals, /* f0= */
N_f_zero_not_equals, /* f0<> */
N_f_zero_less_than, /* f0< */
N_f_zero_greater_than, /* f0> */
N_f_zero_less_or_equal, /* f0<= */
N_f_zero_greater_or_equal, /* f0>= */
N_s_to_f, /* s>f */
N_d_to_f, /* d>f */
N_f_to_d, /* f>d */
N_f_to_s, /* f>s */
N_f_store, /* f! */
N_f_fetch, /* f@ */
N_d_f_fetch, /* df@ */
N_d_f_store, /* df! */
N_s_f_fetch, /* sf@ */
N_s_f_store, /* sf! */
N_f_plus, /* f+ */
N_f_minus, /* f- */
N_f_star, /* f* */
N_f_slash, /* f/ */
N_f_star_star, /* f** */
N_fm_star, /* fm* */
N_fm_slash, /* fm/ */
N_fm_star_slash, /* fmx/ */
N_fm_square, /* f**2 */
N_f_negate, /* fnegate */
N_f_drop, /* fdrop */
N_f_dupe, /* fdup */
N_f_swap, /* fswap */
N_f_over, /* fover */
N_f_rote, /* frot */
N_f_nip, /* fnip */
N_f_tuck, /* ftuck */
N_float_plus, /* float+ */
N_floats, /* floats */
N_floor, /* floor */
N_f_round, /* fround */
N_f_max, /* fmax */
N_f_min, /* fmin */
N_represent, /* represent */
N_to_float, /* >float */
N_f_abs, /* fabs */
N_f_a_cos, /* facos */
N_f_a_sine, /* fasin */
N_f_a_tan, /* fatan */
N_f_a_tan_two, /* fatan2 */
N_f_cos, /* fcos */
N_f_e_x_p, /* fexp */
N_f_e_x_p_m_one, /* fexpm1 */
N_f_l_n, /* fln */
N_f_l_n_p_one, /* flnp1 */
N_f_log, /* flog */
N_f_a_log, /* falog */
N_f_sine, /* fsin */
N_f_sine_cos, /* fsincos */
N_f_square_root, /* fsqrt */
N_f_tan, /* ftan */
N_f_cinch, /* fsinh */
N_f_cosh, /* fcosh */
N_f_tan_h, /* ftanh */
N_f_a_cinch, /* fasinh */
N_f_a_cosh, /* facosh */
N_f_a_tan_h, /* fatanh */
N_s_floats, /* sfloats */
N_d_floats, /* dfloats */
N_s_f_aligned, /* sfaligned */
N_d_f_aligned, /* dfaligned */
N_v_star, /* v* */
N_faxpy, /* faxpy */
GROUPADD(75)
#endif
GROUPADD(0)
#ifdef HAS_GLOCALS
GROUPADD(0)
GROUP( locals, 301)
N_fetch_local_number, /* @local# */
N_fetch_local_zero, /* @local0 */
N_fetch_local_four, /* @local1 */
N_fetch_local_eight, /* @local2 */
N_fetch_local_twelve, /* @local3 */
GROUPADD(5)
#ifdef HAS_FLOATING
N_f_fetch_local_number, /* f@local# */
N_f_fetch_local_zero, /* f@local0 */
N_f_fetch_local_eight, /* f@local1 */
GROUPADD(3)
#endif
N_laddr_number, /* laddr# */
N_lp_plus_store_number, /* lp+!# */
N_minus_four_lp_plus_store, /* lp- */
N_eight_lp_plus_store, /* lp+ */
N_sixteen_lp_plus_store, /* lp+2 */
N_lp_store, /* lp! */
N_to_l, /* >l */
GROUPADD(7)
#ifdef HAS_FLOATING
N_f_to_l, /* f>l */
N_fpick, /* fpick */
GROUPADD(2)
#endif
GROUPADD(0)
#endif
GROUPADD(0)
#ifdef HAS_OS
GROUPADD(0)
GROUP( syslib, 318)
N_open_lib, /* open-lib */
N_lib_sym, /* lib-sym */
N_wcall, /* wcall */
N_u_w_fetch, /* uw@ */
N_s_w_fetch, /* sw@ */
N_w_store, /* w! */
N_u_l_fetch, /* ul@ */
N_s_l_fetch, /* sl@ */
N_l_store, /* l! */
N_lib_error, /* lib-error */
GROUPADD(10)
#endif
GROUPADD(0)
GROUP( peephole, 328)
GROUPADD(0)
#ifdef HAS_PEEPHOLE
N_compile_prim1, /* compile-prim1 */
N_finish_code, /* finish-code */
N_forget_dyncode, /* forget-dyncode */
N_decompile_prim, /* decompile-prim */
N_set_next_code, /* set-next-code */
N_call2, /* call2 */
N_tag_offsets, /* tag-offsets */
GROUPADD(7)
#endif
GROUPADD(0)
GROUP( static_super, 335)
GROUPADD(0)
GROUP( end, 335)
