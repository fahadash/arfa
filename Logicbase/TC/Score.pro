

%score(1,0).
%score(2,0).
%score(3,0).
%score(4,0).



add_score(P, S):-
score(P,X),
N is X + S,
retract(score(P,X)),
assert(score(P,N)).