test1:-
player_puts(1, card(2, clubs)),
player_puts(2, card(2, diamonds)),
player_puts(3, card(7, clubs)). 


test2:-
test1,
player_puts(4, card(5, clubs)).



test3:-
test2,
player_puts(3, card(10, hearts)),
player_puts(4, card(7, hearts)),
player_puts(1, card(6, hearts)),
player_puts(2, card(5, hearts)).


test4:-
test3,
player_puts(3,card(king, hearts)),
player_puts(4, card(9, hearts)),
player_puts(1, card(4, hearts)), %ace, hearts
player_puts(2, card(2, hearts)).



test5:-
test4,
player_puts(3,card(10, diamonds)),
player_puts(4, card(jack, diamonds)),
player_puts(1, card(9, diamonds)),
player_puts(2, card(4, diamonds)).



test6:-
test5,
player_puts(4, card(3, clubs)),
player_puts(1, card(4, clubs)),
player_puts(2, card(8, diamonds)),
player_puts(3, card(9, clubs)).


%testing trump cut
test7:-
test6,
player_puts(3, card(queen, clubs)),
player_puts(4, card(king, clubs)),
player_puts(1, card(8, clubs)),
player_puts(2, card(2, spades)).

test_int1:-
start_new_game,
initialize_first_time,
set_trump(diamonds),
submit_player_card(4, '5c'),
submit_player_card(1, '2c'),
submit_player_card(2, '4d'),
submit_player_card(3, '9c'),


submit_player_card(2, '8h'),
submit_player_card(3, '10h'),
submit_player_card(4, 'qh'),
submit_player_card(1, '4h'),


submit_player_card(4, 'ad'),
submit_player_card(1, '9d'),
submit_player_card(2, '3d'),
submit_player_card(3, '5d').


tstat:-
handsontable(H),
write('There are '),write(H),write(' hands on table.'),nl,
turn(P),
write(' Turn of player '),write(P),
current_suite(S),
write('current suit is '),write(S),nl,
setof((X,Y), hand(X,Y), Q),
length(Q, L),
write(' Player hands '),write(L), nl.


test_game_real1:-
card_alias(A0, 'jd'),assert(player_has(1, [A0])),
assert(player_has(2, [])),
assert(player_has(3, [])),
assert(player_has(4, [])),
set_very_first_hand_gone_flag,
set_player_card_on_hand(1, ''), set_player_card_on_hand(2, 'as'), set_player_card_on_hand(3, 'ah'), set_player_card_on_hand(4, 'ac'), 
set_hands_accumulated(1),
set_turn_player(1),
set_dominant_player(2),
set_game_started,
set_trump(diamonds, R),
set_current_suit(spades),
set_score(1, 0), set_score(2, 11), set_score(3, 0), set_score(4, 0).