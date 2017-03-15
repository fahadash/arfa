

suit(clubs).
suit(diamonds).
suit(hearts).
suit(spades).


rank(2).
rank(3).
rank(4).
rank(5).
rank(6).
rank(7).
rank(8).
rank(9).
rank(10).
rank(jack).
rank(queen).
rank(king).
rank(ace).


number_worth(2, 2).
number_worth(3, 3).
number_worth(4, 4).
number_worth(5, 5).
number_worth(6, 6).
number_worth(7, 7).
number_worth(8, 8).
number_worth(9, 9).
number_worth(10, 10).
number_worth(jack, 11).
number_worth(queen, 12).
number_worth(king, 13).
number_worth(ace, 14).



rank_alias(ace, a).
rank_alias(king, k).
rank_alias(queen, q).
rank_alias(jack, j).
rank_alias(10, '10').
rank_alias(9, '9').
rank_alias(8, '8').
rank_alias(7, '7').
rank_alias(6, '6').
rank_alias(5, '5').
rank_alias(4, '4').
rank_alias(3, '3').
rank_alias(2, '2').

suit_alias(clubs, c).
suit_alias(diamonds, d).
suit_alias(hearts, h).
suit_alias(spades, s).