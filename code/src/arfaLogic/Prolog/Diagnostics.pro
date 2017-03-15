get_info(I):-
handsontable(H),
string_number(HS, H),

turn(P),
string_number(PS, P),

current_suit(S),
get_playertablecard(1, PC1),
get_playertablecard(2, PC2),
get_playertablecard(3, PC3),
get_playertablecard(4, PC4),
stringlist_concat(['Num hands on table ', HS, ' Turn player ', PS, ' current suit ', S, ' player 1 card ', PC1, ' Player 2 card ' , PC2, ' Player 3 Card ', PC3, ' Player 4 card ', PC4], I).


get_playertablecard(P, C):-
not(hand(P, Q)),
C='Nothing'.

get_playertablecard(P, C):-
hand(P, Q),
card_alias(Q, C).







diag_test_one:-
write('entered diag one'),nl,
card_alias(A0, 'ah'),card_alias(A1, 'ks'),card_alias(A2, 'kd'),card_alias(A3, 'js'),card_alias(A4, 'jh'),card_alias(A5, '10s'),card_alias(A6, '9s'),card_alias(A7, '9d'),card_alias(A8, '8c'),card_alias(A9, '6h'),card_alias(A10, '4c'),card_alias(A11, '4h'),assert(player_has(1, [A0, A1, A2, A3, A4, A5, A6, A7, A8, A9, A10, A11])),
write('Milestone 1'),nl,
card_alias(B0, 'qs'),card_alias(B1, '8h'),card_alias(B2, '8d'),card_alias(B3, '7s'),card_alias(B4, '6s'),card_alias(B5, '5h'),card_alias(B6, '4s'),card_alias(B7, '3h'),card_alias(B8, '3d'),card_alias(B9, '2s'),card_alias(B10, '2h'),card_alias(B11, '2d'),assert(player_has(2, [B0, B1, B2, B3, B4, B5, B6, B7, B8, B9, B10, B11])),
write('Milestone 2'),nl,
card_alias(C0, 'as'),card_alias(C1, 'ac'),card_alias(C2, 'kh'),card_alias(C3, 'qc'),card_alias(C4, 'jc'),card_alias(C5, '10c'),card_alias(C6, '10h'),card_alias(C7, '10d'),card_alias(C8, '9c'),card_alias(C9, '8s'),card_alias(C10, '7c'),card_alias(C11, '5d'),card_alias(C12, '3s'),assert(player_has(3, [C0, C1, C2, C3, C4, C5, C6, C7, C8, C9, C10, C11, C12])),
write('Milestone 3'),nl,
card_alias(D0, 'ad'),card_alias(D1, 'kc'),card_alias(D2, 'qh'),card_alias(D3, 'qd'),card_alias(D4, 'jd'),card_alias(D5, '9h'),card_alias(D6, '7h'),card_alias(D7, '7d'),card_alias(D8, '6c'),card_alias(D9, '6d'),card_alias(D10, '5s'),card_alias(D11, '3c'),assert(player_has(4, [D0, D1, D2, D3, D4, D5, D6, D7, D8, D9, D10, D11])),
write('Milestone 4'),nl,
set_player_card_on_hand(1, '2c'), set_player_card_on_hand(2, '4d'), set_player_card_on_hand(3, ''), set_player_card_on_hand(4, '5c'), 
set_hands_accumulated(0),
set_turn_player(3),
set_dominant_player(0),
write('Milestone 5'),nl,
set_game_started,
set_trump(diamonds, R),
set_current_suit(clubs),
set_score(1, 0), set_score(2, 0), set_score(3, 0), set_score(4, 0).
%submit_player_card(3, '9c').