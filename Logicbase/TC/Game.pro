new_game:-
assert(score(1, 0)),
assert(score(2, 0)),
assert(score(3, 0)),
assert(score(4, 0)),
distribute_cards,
assert(reshuffling_occurred).

reshuffle_game:-
date(YEAR,MON,DAY),
time(HOUR,MIN,SEC),
SEED is YEAR + MON + DAY + HOUR + MIN + SEC + cpuclock,
write(SEED),
seed_random(SEED),
retract(player_has(P1,Cards1)),
retract(player_has(P2,Cards2)),
retract(player_has(P3,Cards3)),
retract(player_has(P4,Cards4)),
retract(trump(TRUMP)),
retract(very_first_hand_gone),
retract(turn(TURNPLAYER)),
random(1, 4, Rand),
assert(turn(Rand)),
assert(reshuffling_occurred),
distribute_cards.

player_puts(N, C):-
not(turn(N)),
write_last_error('This is not this player''s turn'),nl, fail.

player_puts(N, C):-
not(trump(T)),
write_last_error('Trump not decided'), nl, fail.

player_puts(N, C):-
turn(N),
valid_card(N, C),
check_suit(C),
write('DEBUG 0'),nl,
trump(T),
write('DEBUG 1'),nl,
assert(hand(N, C)),
write('DEBUG 2'),nl,
remove_card(N, C),
write('checking complete'),nl,
check_complete.


valid_card(P, card(R, S)):-
player_has_card(P, card(R, S)).

valid_card(P, card(R, S)):-
not(player_has_card(P, card(R, S))),
write('Player '),write(P),write_last_error(' does not have this card.'),nl,fail.

player_has_card(P, card(R,S)):-
player_has(P, Cards),
member(card(R, S), Cards).

check_suit(card(R,S)):-
not(turn_start),
not(current_suit(S)),
turn(X),
current_suit(S1),
player_has_card(X, card(Y, S1)),
write(S),write_last_error(' is invalid suit for this hand'), nl,fail,!.


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
write('hand complete'),
assert(turn_start),
write('hand complete'),nl,
add_hand,
write('calling check winner'),
check_winner.

check_complete:-
not(hand_complete),
write('hand not complete, advancing turn'),nl,
advance_turn.

check_winner:-
best_card(P),
all_cards_finished,
clear_cards,
set_turn(P),
handsontable(HS),
add_score(P, HS),
clear_hands,
set_winner_player(P),
reshuffle_game,
write(P),write(' is the winner, and reshuffling occurred'),nl.

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
set_winner_player(P),
write(P),write(' is the winner'),nl.

check_winner:-
best_card(P),
on_the_win(P),
not(very_first_hand_gone),
write('2'),nl,
set_turn(P),
clear_cards,
set_very_first_hand_gone,
write(P),write(' has to make one more hand'),nl,!.

check_winner:-
set_very_first_hand_gone,
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

set_very_first_hand_gone:-
not(very_first_hand_gone),
assert(very_first_hand_gone).

set_very_first_hand_gone:-
very_first_hand_gone.


set_winner_player(P):-
not(winner_player(X)),
assert(winner_player(P)), !.

set_winner_player(P):-
winner_player(X),
retract(winner_player(X)),
assert(winner_player(P)).



all_cards_finished:-
player_has(1, []),
player_has(2, []),
player_has(3, []),
player_has(4, []).