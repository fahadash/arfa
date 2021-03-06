

card(card(R,S)):-
suit(S),
rank(R).


deck([]).
deck([C|Cs]):-
card(C),
deck(Cs).

new_deck(Deck):-
bagof(C, card(C), Deck).



select_nth(N, List, Element, Rest):-
length(List, Len),
N>0,
N =< Len,
nth(N, List, Element),
select_rest(N, List, Rest).


select_rest(N, List, Rest):-
setof(X, (member(X,List), not(element_position(List, X, N))), Rest).

select_rest(1,[_], []).


element_position([H|T], H, 1).
element_position([H|T], E, P):-
element_position(T,E,N),
P is N + 1.


random_element([], [], []):-
fail.

random_element(List, Element, Rest):-
length(List, Len),
Len > 0,
random(1, Len, Rand),
select_nth(Rand, List, Element, Rest).

%%%%%%%%%%%manipulation%%%%%%%%%%%%

% For last item removal %
remove_card(P, C):-
write('Debug 1'),nl,
card(C),
write('Debug 2'),nl,
player_has(P, Cards),
write('Debug 3'),nl,
[Onlyone|[]]=Cards,
retract(player_has(P,OldCards)),
write('Debug 6'),nl,
asserta(player_has(P,[])),
write('Debug 7'),nl.

% For other items removal %
remove_card(P, C):-
write('Debug 1'),nl,
card(C),
write('Debug 2'),nl,
player_has(P, Cards),
write('Debug 3'),nl,
setof(X, (member(X, Cards), not(X==C) ), NewCards),
write('Debug 4'),nl,
retract(player_has(P,OldCards)),
write('Debug 6'),nl,
asserta(player_has(P,NewCards)),
write('Debug 7'),nl.



%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


%%%%%%%%%% New Game stuff %%%%%%%%%%%%%%%%%%%%%
shuffle([], []).

shuffle(Deck0, [C|Deck]):-
random_element(Deck0, C, Deck1),
shuffle(Deck1, Deck).



distribute_cards:-
new_deck(Deck),
shuffle(Deck, Pile),
give_cards(Pile).


give_cards(Pile):-
give_player_cards(1, Pile),
give_player_cards(2, Pile),
give_player_cards(3, Pile),
give_player_cards(4, Pile).


give_player_cards(N, Pile):-
cards_for_player(N, Pile, Cards),
assert(player_has(N, Cards)).

cards_for_player(N, Pile, Cards):-
setof(C, (member(C, Pile), card_for_player(N, Pile, C)), Cards).

card_for_player(N, Pile, Cards):-
Start is 13*(N-1) + 1,
End is 13 * N,
element_position(Pile, Cards, P),
P >= Start,
P =< End.

%%%%%%%%%%%%%%%%%%%%% end new game stuff %%%%%%%%%%%%%%%%%




card_worth(card(X,Y), W):-
current_suit(Y),
number_worth(X, W).

card_worth(card(X,Y), W):-
trump(Y),
%not(current_suit(Y)),
number_worth(X, Worth),
W is Worth + 13.

card_worth(card(X,Y), 0):-
not (current_suit(Y)),
not (trump(Y)).



can_choose_trump(P, Result):-
turn(P),
not(trump(T)),
not(game_started),
Result = 'true', !.

can_choose_trump(P, Result):-
Result = 'false'.