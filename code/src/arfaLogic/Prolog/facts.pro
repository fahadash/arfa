

%turn(1).

%trump(spades).

%current_suit(hearts).


%on_the_win(5).

%turn_start.

%handsontable(0).
%game_started.


initialize_first_time:-
assert(turn_start), 
assert(current_suit(hearts)),
 assert(on_the_win(5)),
set_hands_accumulated(0).