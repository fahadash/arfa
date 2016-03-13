
% tableid(1).


start_new_game:-
date(YEAR,MON,DAY),
time(HOUR,MIN,SEC),
SEED is YEAR + MON + DAY + HOUR + MIN + SEC + cpuclock,
write(SEED),
seed_random(SEED),
not(game_started),
new_game,
random(1, 4, Rand),
assert(turn(Rand)),
assert(game_started).


get_player_cards(PlayerNum, Card):-
player_has_card(PlayerNum, C),
card_alias(C, Card).


get_hands_accumulated(N):-
handsontable(N).

get_hands_accumulated(N):-
not(handsontable(X)),
N is 0.


which_player_turn(N):-
turn(N).

which_player_turn(N):-
not(turn(X)),
N is 0.

player_card_on_table(N, Card):-
hand(N, C),
card_alias(C, Card).

player_card_on_table(N, Card):-
not(hand(N,C)),
Card = ''.

get_dominant_player(N):-
on_the_win(N).

get_dominant_player(N):-
not(on_the_win(X)),
N is 0.

get_trump(S):-
trump(S).

get_player_score(P, S):-
score(P, S).

get_player_score(P, S):-
not(score(P, X)),
S is 0.

get_current_suit(X):-
current_suit(X).	

get_current_suit(X):-
not(current_suit(Y)),
X = ''.

set_trump(T, Result):-
suit(T),
assert(trump(T)),
Result = 'SUCCESS'.

set_trump(T, Result):-
not(suit(T)),
Result = 'INVALIDSUIT', fail.

get_game_started(S):-
game_started,
S = 'true', !.

get_game_started(S):-
S = 'false'.


get_trump_chosen(C):-
not(trump(T)),
C = 'false'.

get_trump_chosen(C):-
trump(T),
C = 'true'.

set_score(P, S):- 
retract(score(P,X)),
assert(score(P, S)), !.

set_score(P, S):-
not(score(P, X)),
assert(score(P,S)).

set_player_card_on_hand(P, Alias):-
card_alias(C, Alias),
set_player_card_on_table(P, C).

set_player_card_on_hand(P, Alias):-
Alias == ''.

set_player_card_on_table(P, Card):-
retract(hand(P, X)),
assert(hand(P,Card)), !.

set_player_card_on_table(P, Card):-
assert(hand(P, Card)).

set_turn_player(P):-
retract(turn(X)),
assert(turn(P)),!.

set_turn_player(P):-
assert(turn(P)).


set_hands_accumulated(N):-
retract(handsontable(X)),
assert(handsontable(N)), !.

set_hands_accumulated(N):-
assert(handsontable(N)).

set_dominant_player(P):-
retract(on_the_win(X)),
assert(on_the_win(P)), !.

set_dominant_player(P):-
assert(on_the_win(P)).


set_player_turn(P):-
retract(turn(X)),
assert(turn(P)), !.

set_player_turn(P):-
assert(turn(P)).

set_game_started:-
retract(game_started),
assert(game_started), !.

set_game_started:-
assert(game_started).

set_trump(T):-
retract(trump(X)),
assert(trump(T)), !.

set_trump(T):-
assert(trump(T)).

set_current_suit(S):-
retract(current_suit(X)),
assert(current_suit(S)), !.

set_current_suit(S):-
assert(current_suit(S)).


submit_player_card(P, Alias):-
card_alias(Card, Alias),
player_puts(P, Card).



get_last_error(E):-
last_error(E).

get_last_error(E):-
not(last_error(X)),
E='Nothing'.


set_turn_start:-
not(turn_start),
assert(turn_start), !.

set_turn_start:-
turn_start.

get_turn_start(X):-
turn_start,
X = 'true'.

get_turn_start(X):-
not(turn_start),
X = 'false'.

set_very_first_hand_gone_flag:-
set_very_first_hand_gone.


get_winner_player(X):-
winner_player(X).



