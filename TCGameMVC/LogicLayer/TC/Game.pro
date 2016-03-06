new_game:-
distribute_cards.


player_puts(N, C):-
not(turn(N)),
write('This is not this player''s turn'),nl.

player_puts(N, C):-
turn(N),
valid_card(N, C),
check_suit(C),
assert(hand(N, C)),
remove_card(N, C),
write('checking complete'),nl,
check_complete.


valid_card(P, card(R, S)):-
player_has_card(P, card(R, S)).

valid_card(P, card(R, S)):-
not(player_has_card(P, card(R, S))),
write('Player '),write(P),write(' does not have this card.'),nl,fail.

player_has_card(P, card(R,S)):-
player_has(P, Cards),
member(card(R, S), Cards).

check_suit(card(R,S)):-
not(turn_start),
not(current_suit(S)),
turn(X),
current_suit(S1),
player_has_card(X, card(Y, S1)),
write(S),write(' is invalid suit for this hand'), nl,fail,!.


check_suit(card(R,S)):-
not(turn_start),
current_suit(S1),
not(S == S1),
turn(X),
not(player_has_card(X, card(R1, S1))).

check_suit(card(R,S)):-
not(turn_start),
current_suit(S).

check_suit(card(R,S)):-
turn_start,
retract(turn_start),
retract(current_suit(X)),
assert(current_suit(S)),
write('the suite now is '),write(S),nl.


advance_turn:-
turn(P),
P < 4,
retract(turn(P)),
N is P + 1,
assert(turn(N)).

advance_turn:-
turn(P),
P =:= 4,
set_turn(1).

check_complete:-
hand_complete,
assert(turn_start),
write('hand complete'),nl,
add_hand,
check_winner.

check_complete:-
not(hand_complete),
write('hand not complete, advancing turn'),nl,
advance_turn.

check_winner:-
best_card(P),
on_the_win(P),
very_first_hand_gone,
clear_cards,
set_turn(P),
retract(on_the_win(P)),
assert(on_the_win(5)),
handsontable(HS),
add_score(P, HS),
clear_hands,
write(P),write(' is the winner'),nl.

check_winner:-
best_card(P),
on_the_win(P),
not(very_first_hand_gone),
write('2'),nl,
set_turn(P),
clear_cards,
assert(very_first_hand_gone),
write(P),write(' has to make one more hand'),nl.

check_winner:-
best_card(P),
not(on_the_win(P)),
retract(on_the_win(X)),
write('1'),nl,
set_turn(P),
clear_cards,
assert(on_the_win(P)),write(P),write(' is on the win, and it is his turn.'),nl.

best_card(P2):-
setof((P,C,W), (hand(P,C), card_worth(C, W)), Q),
best_worth(Q,M),
card_worth(Card, M),
hand(P2, Card).

best_worth([(P,C,W)], W).

best_worth([(P, C, W)|T], M2):-
best_worth(T,M),
M2 is max(W, M). 




hand_complete:-
setof((X,Y), hand(X,Y), Q),
length(Q,4).


add_hand:-
handsontable(X),
write('adding hands current is '),write(X),nl,
retract(handsontable(X)),
N is X + 1,
assert(handsontable(N)).


clear_cards:-
retract(hand(1,C)),
retract(hand(2,D)),
retract(hand(3,E)),
retract(hand(4,F)).

clear_hands:-
retract(handsontable(X)),
assert(handsontable(0)).

set_turn(P):-
retract(turn(X)),
assert(turn(P)).
