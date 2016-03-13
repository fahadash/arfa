d_debug1:-
write('1'),nl,
card_alias(A0, 'ah'),assert(player_has(1, [A0])),
write('2'),nl,
card_alias(B0, 'qs'),assert(player_has(2, [B0])),
write('3'),nl,
card_alias(C0, 'ac'),assert(player_has(3, [C0])),
write('4'),nl,
card_alias(D0, '9h'),assert(player_has(4, [D0])),
write('5'),nl,
set_very_first_hand_gone_flag,
write('6'),nl,
assert(turn_start), assert(current_suit(hearts)),
write('7'),nl,
set_player_card_on_hand(1, ''),
write('8'),nl,
set_player_card_on_hand(2, ''),
write('9'),nl,
 set_player_card_on_hand(3, ''),
write('10'),nl,
set_player_card_on_hand(4, ''), 
write('11'),nl,
set_hands_accumulated(3),
write('12'),nl,
set_turn_player(3),
write('13'),nl,
set_dominant_player(0),
write('14'),nl,
set_game_started,
write('15'),nl,
set_trump(clubs, R),
write('16'),nl,
set_current_suit(diamonds),
write('17'),nl,
set_score(1, 0),
write('18'),nl,
set_score(2, 0),
write('19'),nl,
set_score(3, 7),
write('20'),nl,
set_score(4, 2),
write('F'),nl.



d_submit_debug1_card:-
submit_player_card(3, 'ac').


d_submit_debug1_card1:-
submit_player_card(4, '9h').

d_submit_debug1_card2:-
submit_player_card(1, 'ah').

d_submit_debug1_card3:-
submit_player_card(2, 'qs').

d_submit_all1:-
d_submit_debug1_card,
d_submit_debug1_card1,
d_submit_debug1_card2.
