  length([],0).
length([H|T],N) :- length(T,M), N is M+1.
append([],X,X).
append([H|T1],X,[H|T2]) :-
  append(T1,X,T2).
  
  member(H,[H|T]).
member(X,[H|T]) :- member(X,T).

  random(Low, High, Rand):-
  date(YEAR,MON,DAY),
  time(HOUR,MIN,SEC),
  SEED is YEAR + MON + DAY + HOUR + MIN + SEC + cpuclock,
 % seed_random(SEED),
  Rand is integer(random*High + Low).
  
card_alias(card(R, S), Alias):-
rank_alias(R, RA),
suit_alias(S, SA),
atom_concat(RA, SA, Alias).


write_last_error(X):-
not(last_error(E)),
assert_last_error(X).

write_last_error(X):-
retract(last_error(E)),
assert_last_error(X).

assert_last_error(E):-
assert(last_error(E)),
write(E).

