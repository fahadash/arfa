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
  
