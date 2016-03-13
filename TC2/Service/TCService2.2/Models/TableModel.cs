using PrologInterface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TC2.Common.Exceptions;

namespace TC2.Models
{
    using TCService2._2.Database;


    public class TableModel
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public int AvailableSlots { get; set; }
        public UserModel Owner { get; set; }

      //  private static TC2DataContext ctx = new TC2DataContext();

        public static List<TableModel> GetJoinableTables(Guid loginToken)
        {
            try
            {
                using (var ctx = new TC2DataContext())
                {
                    TC2DataContext ctxx = ctx;
                    var results = ctxx.ListJoinableTables(loginToken);




                    return results.Select(r => new TableModel()
                    {
                        TableId = r.TableId,
                        TableName = r.TableName,
                        AvailableSlots = r.SlotsAvailable.Value,
                        Owner = new UserModel
                        {
                            UserId = r.UserId,
                            UserName = r.Username
                        }
                    }).ToList();
                }
            }
            catch (SqlException sqle)
            {
                if (sqle.Message.Equals("INVALIDLOGINTOKEN"))
                {
                    throw new TCException(sqle.Message, "Invalid Login Token, make sure the user is logged in.");
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }


        public static List<TableModel> GetTablesUserIsOn(Guid loginToken)
        {
            try
            {
                using (var ctx = new TC2DataContext())
                {
                    TC2DataContext ctxx = ctx;
                    var results = ctxx.ListTablesUserIsOn(loginToken);




                    return results.Select(r => new TableModel()
                    {
                        TableId = r.TableId,
                        TableName = r.TableName,
                        AvailableSlots = r.SlotsAvailable.Value,
                        Owner = new UserModel
                        {
                            UserId = r.UserId,
                            UserName = r.Username
                        }
                    }).ToList();
                }
            }
            catch (SqlException sqle)
            {
                if (sqle.Message.Equals("INVALIDLOGINTOKEN"))
                {
                    throw new TCException(sqle.Message, "Invalid Login Token, make sure the user is logged in.");
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }


        public static List<TableModel> GetUserTables(Guid loginToken)
        {
            try
            {
                using (var ctx = new TC2DataContext())
                {
                    TC2DataContext ctxx = ctx;
                    var results = ctxx.ListUserTables(loginToken);




                    return results.Select(r => new TableModel()
                    {
                        TableId = r.TableId,
                        TableName = r.TableName,
                        AvailableSlots = r.SlotsAvailable.Value,
                        Owner = new UserModel
                        {
                            UserId = r.UserId,
                            UserName = r.Username
                        }
                    }).ToList();
                }
            }
            catch (SqlException sqle)
            {
                if (sqle.Message.Equals("INVALIDLOGINTOKEN"))
                {
                    throw new TCException(sqle.Message, "Invalid Login Token, make sure the user is logged in.");
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }

        public static TableModel CreateTable(Guid loginToken, string tableName)
        {
            try
            {
                using (var ctx = new TC2DataContext())
                {
                    TC2DataContext ctxx = ctx;
                    var results = ctxx.CreateTable(loginToken, tableName);


                    usp_CreateTableResult result = results.FirstOrDefault();

                    return new TableModel()
                    {
                        TableId = result.TableId.Value,
                        TableName = result.TableName,
                        AvailableSlots = result.AvailableSlots,
                        Owner = new UserModel
                        {
                            UserId = result.UserId,
                            UserName = result.Username
                        }
                    };
                }
            }
            catch (SqlException sqle)
            {
                if (sqle.Message.Equals("INVALIDLOGINTOKEN"))
                {
                    throw new TCException(sqle.Message, "Invalid Login Token, make sure the user is logged in.");
                }
                else if (sqle.Message.Equals("TABLENAMETAKEN"))
                {
                    throw new TCException(sqle.Message, "Table name is already in use, please choose a different one");
                }
                else
                {
                    throw new TCException(sqle.Message, "Unknown exception occurred");
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }

        public static object SuspendTable(Guid loginToken, int tableId)
        {
            try
            {
                using (var ctx = new TC2DataContext())
                {
                    TC2DataContext ctxx = ctx;
                    var results = ctxx.SuspendTable(loginToken, tableId);
                }
            }
            catch (SqlException sqle)
            {
                if (sqle.Message.Equals("INVALIDLOGINTOKEN"))
                {
                    throw TCException.INVALIDLOGINTOKEN;
                }
                else if (sqle.Message.Equals("INVALIDTABLEID"))
                {
                    throw TCException.INVALIDTABLE;
                }
                else if (sqle.Message.Equals("USERNOTOWNER"))
                {
                    throw TCException.USERNOTOWNER;
                }
                else
                {
                    throw new TCException(sqle.Message, "Unknown exception occurred");
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }


        public static object JoinTable(Guid loginToken, int tableId)
        {
            try
            {
                using (var ctx = new TC2DataContext())
                {
                    TC2DataContext ctxx = ctx;
                    var results = ctxx.JoinTable(loginToken, tableId);

                  //  var users = ctxx.TableUsers.Where(u => u.TableId.Equals(tableId)).OrderBy(o => o.TableUserId);
                    #region disabled
                    //if (users.Count() == 4)
                    //{
                    //    //Table complete, lets start game
                    //    Game game = new Game();

                    //    game.NewGame();
                    //    int i = 1;

                    //    foreach (TableUser tableUser in users)
                    //    {
                    //        List<string> playerCards = game.GetPlayerCards(i);

                    //        foreach (string cardAlias in playerCards)
                    //        {
                    //            Card card = ctxx.Cards.Where(c => c.CardAlias.Equals(cardAlias)).SingleOrDefault();

                    //            ctxx.TableUserCards.InsertOnSubmit(
                    //                new TableUserCard()
                    //                {
                    //                    Card = card,
                    //                    TableUser = tableUser
                    //                }
                    //                );
                    //        }
                                 

                    //        i++;
                    //    }

                    //    ctxx.SubmitChanges();

                    //}
                    #endregion
                }
            }
            catch (SqlException sqle)
            {
                if (sqle.Message.Equals("INVALIDLOGINTOKEN"))
                {
                    throw new TCException(sqle.Message, "Invalid Login Token, make sure the user is logged in.");
                }
                else if (sqle.Message.Equals("INVALIDTABLEID"))
                {
                    throw new TCException(sqle.Message, "Invalid table");
                }
                else if (sqle.Message.Equals("USEROWNER"))
                {
                    throw new TCException(sqle.Message, "User owns the table, which means he is on table..");
                }
                else if (sqle.Message.Equals("USERALREADYON"))
                {
                    throw new TCException(sqle.Message, "User is already on this table");
                }
                else
                {
                    throw new TCException(sqle.Message, "Unknown exception occurred");
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }

        public static object SubmitUserCard(Guid loginToken, int tableId, string card)
        {


            UserModel user = UserModel.GetUserByToken(loginToken);

            if (user == null)
            {
                throw TCException.INVALIDLOGINTOKEN;
            }

            if (!IsValidTable(tableId))
            {
                throw TCException.INVALIDTABLE;
            }


            if (!IsUserOn(user.UserId, tableId))
            {
                throw TCException.USERNOTONTABLE;
            }

            if (!IsGameStarted(tableId))
            {
                throw new TCException("GAMENOTSTARTED", "Game has not been started yet");
            }

            var cardObj = CardModel.GetCard(card);

            TCException.ThrowIfNull(cardObj, "INVALIDCARD", "Invalid card");
            
            int playerNumber = 0;

            using (var ctx = new TC2DataContext())
            {

                foreach (TableUser tUser in ctx.TableUsers.Where(u => u.TableId.Equals(tableId)))
                {
                    playerNumber++;

                    if (user.UserId == tUser.UserId)
                    {
                        break;
                    }
                }

                TableUser tableUser = ctx.TableUsers.FirstOrDefault(t => t.TableId.Equals(tableId) &&
                                        t.UserId.Equals(user.UserId));

                //check if tableUser is null
                if (ctx.TableUserCards.Count(t => t.TableUserId.Equals(tableUser.TableUserId) && t.CardId.Equals(cardObj.CardId)) == 0)
                {
                    throw new TCException("USERNOTHAVETHISCARD", "User does not have " + cardObj.Description);
                }

                Table table = ctx.Tables.SingleOrDefault(t => t.TableId.Equals(tableId) && t.Suspended.Equals(false));


                if (!IsThisUserTurn(user.UserId, tableId))
                {
                    throw new TCException("NOCURRENTUSERTURN", "It is not current user's turn");
                }

                if (string.IsNullOrEmpty(table.Trump))
                {
                    throw new TCException("NOTRUMPSELECTED", "No trump selected.");
                }


                int numSuitCards = 0;

                if (!string.IsNullOrEmpty(table.CurrentSuit))
                {
                    numSuitCards = tableUser.TableUserCards.Count(c => c.Card.CardAlias.ToLower().EndsWith(table.CurrentSuit.ToLower().Substring(0, 1)));
                }

                //Check if second clause might have to be compared with 4 not 0
                if (!string.IsNullOrEmpty(table.CurrentSuit) && table.CurrentSuit.ToLower() != cardObj.GetSuit() && ctx.TableStates.Count(t => t.TableId.Equals(tableId)) != 0
                        && numSuitCards > 0)
                {
                    throw new TCException("INVALIDSUIT", "Invalid suit");
                }
            }

            // Load game from database
            Game game = BuildGameFromDB(tableId);
            
            // Submit the card
            game.SubmitPlayerCard(playerNumber, card.ToLower());

            // Save game state
            SaveGameState(tableId, game);


            using (var ctx = new TC2DataContext())
            {
                ctx.TableHistories.InsertOnSubmit(
                    new TableHistory()
                        {
                            CardId = cardObj.CardId,
                            TableId = tableId,
                            UserId = user.UserId
                        });

                ctx.SubmitChanges();
                
                if (!ctx.TableUserCards.Any(tuc => tuc.TableUser.TableId == tableId)
                    && ctx.Tables.Any(t => t.GameStarted == true && t.TableId == tableId))
                {
                    return new { tableFinished = true };
                }
            }

            return new { tableFinished = false };
        }


        public static void BeginGame(Guid loginToken, int tableId)
        {
            UserModel user = UserModel.GetUserByToken(loginToken);


            if (user == null)
            {
                throw TCException.INVALIDLOGINTOKEN;
            }

            if (!IsValidTable(tableId))
            {
                throw TCException.INVALIDTABLE;
            }

            try
            {
                using (var ctx = new TC2DataContext())
                {

                    var userQuery = ctx.TableUsers.Where(t => t.TableId.Equals(tableId));

                    if (userQuery.Count() < 4)
                    {
                        throw new TCException("NOTENOUGHPLAYERS", "4 players are required to begin");
                    }

                    var query1 = from t in ctx.Tables
                                 where t.TableId.Equals(tableId)
                                 select t;

                    Table table = query1.FirstOrDefault();

                    if (table.OwnerUserId != user.UserId)
                    {
                        throw TCException.USERNOTOWNER;
                    }

                    if (table.GameStarted)
                    {
                        throw new TCException("GAMEALREADYSTARTED", "Game has already been started");
                    }

                    Game game = new Game();

                    game.NewGame();
                    game.SetTurnStart();
                    SaveGameState(tableId, game);
                }
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public static object ChooseTrump(Guid loginToken, int tableId, string trumpSuit)
        {
            
            UserModel user = UserModel.GetUserByToken(loginToken);

            if (user == null)
            {
                throw TCException.INVALIDLOGINTOKEN;
            }

            if (!IsValidTable(tableId))
            {
                throw TCException.INVALIDTABLE;
            }

            try
            {
                using (var ctx = new TC2DataContext())
                {
                    var query1 = from t in ctx.Tables
                                 where t.TableId.Equals(tableId)
                                 select t;

                    Table table = query1.FirstOrDefault();

                    if (!string.IsNullOrEmpty(table.Trump))
                    {
                        throw new TCException("TRUMPALREADYCHOSEN", "Trump has already been chosen");
                    }

                    if (!IsThisUserTurn(user.UserId, tableId))
                    {
                        throw TCException.NOUSERTURN;
                    }


                    if (!IsValidSuit(trumpSuit))
                    {
                        throw new TCException("INVALIDSUIT", "Suit invalid.");
                    }

                    table.Trump = trumpSuit;
                    table.Timestamp = DateTime.Now.ToString();

                    ctx.SubmitChanges();
                }
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }

            // Update trump in the database

            return null;
        }


        public static string GetTableTimestamp(Guid loginToken, int tableId)
        {
            try
            {
                using (var ctx = new TC2DataContext())
                {
                    TC2DataContext ctxx = ctx;
                    var results = ctxx.GetTableTimestamp(loginToken, tableId);

                    return results.FirstOrDefault().Timestamp;

                }
            }
            catch (SqlException sqle)
            {
                if (sqle.Message.Equals("INVALIDLOGINTOKEN"))
                {
                    throw new TCException(sqle.Message, "Invalid Login Token, make sure the user is logged in.");
                }
                else if (sqle.Message.Equals("INVALIDTABLEID"))
                {
                    throw new TCException(sqle.Message, "Invalid table");
                }
                else if (sqle.Message.Equals("USERNOTONTABLE"))
                {
                    throw new TCException(sqle.Message, "User is not on this table.");
                }
                else
                {
                    throw new TCException(sqle.Message, "Unknown exception occurred");
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
        }


        public static object GetTableUpdate(Guid loginToken, int tableId)
        {
            
            UserModel user = UserModel.GetUserByToken(loginToken);

            if (user == null)
            {
                throw TCException.INVALIDLOGINTOKEN;
            }

            if (!IsValidTable(tableId))
            {
                throw TCException.INVALIDTABLE;
            }

            if (!IsUserOn(user.UserId, tableId))
            {
                throw TCException.USERNOTONTABLE;
            }

            try
            {
                using (var tx = new TC2DataContext())
                {
                    TC2DataContext ctx = tx;
                    var query1 = from t in ctx.Tables
                                 where t.TableId.Equals(tableId)
                                 select t;
                    Table table = query1.FirstOrDefault();
                    var users = new[] { new object(), new object(), new object(), new object() };
                    int userNumber = 0;

                    var tableFinished = !ctx.TableUserCards.Any(tuc => tuc.TableUser.TableId == tableId)
                    && ctx.Tables.Any(t => t.GameStarted == true && t.TableId==tableId);

                    var previousCards =
                        ctx.TableHistories.Where(h => h.TableId.Equals(tableId))
                            .OrderByDescending(o => o.TableHistoryId)
                            .Join(
                                ctx.Cards,
                                o => o.CardId,
                                i => i.CardId,
                                (h, c) => new { UserId = h.UserId, CardId = c.CardId, CardAlias = c.CardAlias, CardName = c.CardName }).ToList();

                    var previousCardCount = previousCards.Count();

                    if (previousCardCount > 0 && previousCardCount % 4 == 0)
                    {
                        previousCards = previousCards.Take(4).ToList();
                    }
                    else
                    {
                        var query = previousCards.Skip(previousCardCount % 4);

                        if (query.Count() >= 4)
                        {
                            previousCards = query.Take(4).ToList();
                        }
                        else
                        {
                            previousCards = previousCards.Take(0).ToList();
                        }
                    }

                    bool atLeastOneCardOnTable = false;
                    foreach (TableUser tableUser in table.TableUsers)
                    {
                        var currentTableState = table.TableStates.SingleOrDefault(
                            s => s.UserId.Equals(tableUser.UserId));
                        Card currentCard = currentTableState != null ? currentTableState.Card : null;

                        atLeastOneCardOnTable = atLeastOneCardOnTable || currentCard != null;

                        bool isYou = tableUser.UserId.Equals(user.UserId);

                        object yourCards = null;
                        int yourCardsCount = 0;

                        var previousCard = previousCards.FirstOrDefault(c => c.UserId.Equals(tableUser.UserId));


                        var yourCardsQuery =
                            table.TableUsers.Where(u => u.UserId.Equals(tableUser.UserId))
                                .FirstOrDefault()
                                .TableUserCards.Select(
                                    c =>
                                    new
                                    {
                                        cardid = c.CardId,
                                        cardalias = c.Card.CardAlias,
                                        cardname = c.Card.CardName
                                    });

                        var yourCardsQueryConcrete = yourCardsQuery.ToArray();

                        if (isYou)
                        {
                            yourCards = yourCardsQueryConcrete.ToArray();
                        }

                        yourCardsCount = yourCardsQueryConcrete.Count();

                        var gameScores = table.GameScores.SingleOrDefault(s => s.UserId.Equals(tableUser.UserId));
                        users[userNumber] =
                            new
                                {
                                    userinfo =
                                        new
                                            {
                                                userid = tableUser.UserId,
                                                username = tableUser.User.Username,
                                                firstname = tableUser.User.Firstname,
                                                lastname = tableUser.User.Lastname
                                            },
                                    gamescore = gameScores != null ? gameScores.Score : 0,
                                    currentcard =
                                        currentCard != null
                                            ? new
                                                  {
                                                      cardid = currentCard.CardId,
                                                      cardalias = currentCard.CardAlias,
                                                      cardname = currentCard.CardName
                                                  }
                                            : null,
                                    previouscard =
                                        previousCard != null
                                            ? new
                                                  {
                                                      cardid = previousCard.CardId,
                                                      cardalias = previousCard.CardAlias,
                                                      cardname = previousCard.CardName
                                                  }
                                            : null,
                                    dominant = tableUser.IsDominant,
                                    lastheartbeat = tableUser.User.TokenLastHit,
                                    turn = tableUser.IsTurnPlayer,
                                    isyou = isYou,
                                    yourcards = yourCards,
                                    cardcount = yourCardsCount
                                };
                        userNumber++;
                    }

                    var tableInfo = new
                    {
                        tableid = table.TableId,
                        tablename = table.TableName,
                        gamestarted = table.GameStarted,
                        currentsuit = atLeastOneCardOnTable ? table.CurrentSuit : null,
                        trumpchosen = table.Trump != null,
                        trump = table.Trump,
                        Owner = user,
                        users = users,
                        handsaccumulated = table.HandsAccumulated,
                        tableFinished = tableFinished
                    };

                    return tableInfo;


                }
            }
            catch (TCException tce)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new TCException("FAILED", e.Message);
            }

        }

        public static Game BuildGameFromDB(int tableId)
        {
            Game game = new Game();
            try
            {
                using (var ctx = new TC2DataContext())
                {
                    Table table = ctx.Tables.Where(t => t.TableId.Equals(tableId)).FirstOrDefault();

                    string[] playerCard = new string[] {string.Empty, string.Empty, string.Empty, string.Empty};
                    int[] playerScore = new int[] { 0, 0, 0, 0 };
                    int handsAccumulated = 0;
                    int turnPlayer = 0;
                    int dominantPlayer = 0;
                    bool gameStarted = false;


                    var players = from tableUser in ctx.TableUsers
                                  where tableUser.TableId.Equals(tableId)
                                  orderby tableUser.TableUserId

                                  select tableUser;

                    #region Loading Player Cards
                    int playerNumber = 1;
                    foreach (TableUser player in players)
                    {
                        var cards = from userCard in ctx.TableUserCards
                                    join card in ctx.Cards
                                    on userCard.CardId equals card.CardId

                                    where userCard.TableUserId.Equals(player.TableUserId)

                                    select card.CardAlias;

                        game.LoadPlayerCards(playerNumber, cards.ToList());

                        if (player.IsDominant)
                        {
                            dominantPlayer = playerNumber;
                        }

                        if (player.IsTurnPlayer.HasValue && player.IsTurnPlayer.Value)
                        {
                            turnPlayer = playerNumber;
                        }

                        var tableStates = from tableState in ctx.TableStates
                        where tableState.TableId.Equals(tableId) && tableState.UserId.Equals(player.UserId)
                        select tableState;


                        var scores = from gameScore in ctx.GameScores
                                     where gameScore.UserId.Equals(player.UserId) && gameScore.TableId.Equals(tableId)
                                     select gameScore;

                        if (tableStates.Count() == 1)
                        {
                            playerCard[playerNumber - 1] = tableStates.FirstOrDefault().Card.CardAlias.ToLower();
                        }

                        if (scores.Count() == 1)
                        {
                            playerScore[playerNumber - 1] = scores.FirstOrDefault().Score;
                        }

                        playerNumber++;
                    }
                    #endregion

                    #region Loading Table Data

                    var tableUserCardsTotalQuery = ctx.TableUserCards.Where(c => c.TableUser.TableId.Equals(tableId));

                    if (tableUserCardsTotalQuery.Count() <= 48)
                    {
                        game.SetVeryFirstHandGoneFlag();
                    }

                    handsAccumulated = table.HandsAccumulated;
                    gameStarted = table.GameStarted;

                    SuitType trump = SuitType.na;
                    SuitType current_suit = SuitType.na;

                    if (!string.IsNullOrEmpty(table.Trump))
                    {
                        trump = (SuitType)Enum.Parse(typeof(SuitType), table.Trump);
                    }


                    if (!string.IsNullOrEmpty(table.CurrentSuit))
                    {
                        current_suit = (SuitType)Enum.Parse(typeof(SuitType), table.CurrentSuit);
                    }

                    if (table.TurnStart)
                    {
                        game.SetTurnStart();
                    }

                    game.LoadTable(playerCard[0], playerCard[1], playerCard[2], playerCard[3], handsAccumulated, turnPlayer, dominantPlayer, gameStarted, trump, current_suit);                    

                    #endregion

                    #region Loading Scores

                    game.LoadScores(playerScore[0], playerScore[1], playerScore[2], playerScore[3]);

                    #endregion

                }
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }


            return game;
        }

        public static void SaveGameState(int tableId, Game game)
        {
            try
            {
                string debugStr = game.GetInfo();
                Debug.Print(debugStr);

                using (var ctx = new TC2DataContext())
                {
                    Table table = ctx.Tables.Where(t => t.TableId.Equals(tableId)).FirstOrDefault();
                    int dominantPlayer = game.GetDominantPlayer(), turnPlayer = game.GetTurnPlayer();

                    var players = from tableUser in ctx.TableUsers
                                  where tableUser.TableId.Equals(tableId)
                                  orderby tableUser.TableUserId

                                  select tableUser;

                    ctx.TableUserCards.DeleteAllOnSubmit(ctx.TableUserCards.Where(c => players.Any(p => p.TableUserId.Equals(c.TableUserId))));
                    
                    ctx.TableStates.DeleteAllOnSubmit(ctx.TableStates.Where(s => s.TableId.Equals(tableId)));
                    ctx.GameScores.DeleteAllOnSubmit(ctx.GameScores.Where(s => s.TableId.Equals(tableId)));

                    foreach (TableUser tu in ctx.TableUsers.Where(t => tableId.Equals(tableId)))
                    {
                        tu.IsDominant = false;
                        tu.IsTurnPlayer = false;
                    }

                    ctx.SubmitChanges();

                    int playerNumber = 1;
                    List<TableState> tableStates = new List<TableState>();
                    List<GameScore> gameScores = new List<GameScore>();
                    int winnerPlayerNumber = game.GetWinnerNumber();

                    foreach (TableUser player in players)
                    {
                        List<string> cards = game.GetPlayerCards(playerNumber);
                        List<TableUserCard> userCards = new List<TableUserCard>();

                        foreach (string card in cards)
                        {
                            var cardO = GetCard(card);
                            userCards.Add(new TableUserCard()
                            {
                                CardId = cardO.CardId,
                                TableUserId = player.TableUserId
                            });
                            

                        }

                        ctx.TableUserCards.InsertAllOnSubmit(userCards);

                        if (playerNumber == dominantPlayer)
                        {
                            player.IsDominant = true;
                        }

                        if (playerNumber == turnPlayer)
                        {
                            player.IsTurnPlayer = true;
                        }

                        string cardAlias = game.GetPlayerCardOnTable(playerNumber);


                        if (!string.IsNullOrEmpty(cardAlias))
                        {
                            var cardOnTable = GetCard(cardAlias);

                            tableStates.Add(new TableState()
                            {
                                CardId = cardOnTable.CardId,
                                TableId = tableId,
                                UserId = player.UserId
                            });
                        }

                        gameScores.Add(new GameScore()
                        {
                            TableId = tableId,
                            UserId = player.UserId,
                            Score = game.GetPlayerScore(playerNumber),
                        });
                        
                        //Winner flag
                        if (winnerPlayerNumber != 0 && winnerPlayerNumber == playerNumber)
                        {
                            table.LastWinnerUserId = player.UserId;
                            table.LastWinnerTimestamp = DateTime.Now;
                        }

                        playerNumber++;
                    }

                    table.GameStarted = game.IsGameStarted();

                    table.HandsAccumulated = game.GetHandsOnTable();

                    table.TurnStart = game.GetTurnStart();

                    if (game.IsTrumpChosen())
                    {
                        table.Trump = game.GetTrump().ToString();
                    }
                    else
                    {
                        table.Trump = null;
                    }


                    SuitType currentSuit = game.GetCurrentSuit();

                    if (currentSuit != SuitType.na)
                    {
                        table.CurrentSuit = currentSuit.ToString();
                    }

                    table.Timestamp = DateTime.Now.ToString();

                    ctx.TableStates.InsertAllOnSubmit(tableStates);
                    ctx.GameScores.InsertAllOnSubmit(gameScores);

                    ctx.SubmitChanges();

                }
            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }

        }


        void dummy()
        {

            try
            {

            }
            catch (SqlException sqle)
            {
                throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        static bool IsValidTable(int tableId)
        {

            var ctx = new TC2DataContext();
            {
                return ctx.Tables.Any(t => t.TableId.Equals(tableId) && t.Suspended.Equals(false));
            }
        }

        static bool IsThisUserTurn(int userId, int tableId)
        {
            var ctx = new TC2DataContext();
            {
                return ctx.TableUsers.Any(t => t.TableId.Equals(tableId) && t.UserId.Equals(userId)
                                            && t.IsTurnPlayer.Equals(true));
            }
        }

        static bool IsGameStarted(int tableId)
        {
            var ctx = new TC2DataContext();
            {
                return ctx.Tables.Any(t => t.TableId.Equals(tableId) && t.GameStarted.Equals(true));
            } 
        }

        static bool IsUserOn(int userId, int tableId)
        {
            var ctx = new TC2DataContext();
            {
                return ctx.TableUsers.Any(t => t.TableId.Equals(tableId) && t.UserId.Equals(userId));
            }
        }

        public static bool IsValidSuit(string suit)
        {
            return (new List<string>() { "spades", "clubs", "hearts", "diamonds" }).Any(s => s.Equals(suit));
        }

        public static Card GetCard(string alias)
        {
            var ctx = new TC2DataContext();
            {
                return ctx.Cards.Where(c => c.CardAlias.Equals(alias)).FirstOrDefault();
            }
        }
    }
}