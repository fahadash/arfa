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