using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC2.Common.Exceptions;

namespace PrologInterface
{
    public class Game : IDisposable
    {
        LogicServerHelper logicServer = new LogicServerHelper();
        static string xplPath = @"C:\Dev\Amzi\Workspace\TC\bin\TC.xpl";

        public Game()
        {
        //    logicServer = new LogicServer();
        //    logicServer.Init("");
        //    logicServer.Load(xplPath);
        }

        public void NewGame()
        {
            try
            {
                ExecStrWithExceptionThrown("start_new_game.", "New game could not be started");
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

        }

        #region Game Startup

        public void ChooseTrump(SuitType trump)
        {
            string result = string.Empty;

            try
            {
                int term = logicServer.ExecStr(string.Format("set_trump({0}, R).", trump.ToString()));

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on set_trump");
                }

                result = logicServer.GetStrArg(term, 2);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            if (result == "INVALIDSUIT")
            {
                throw new TCException(result, "Invalid suit supplied for trump");
            }

        }

        #endregion

        #region Game State
        public List<string> GetPlayerCards(int playerNumber)
        {
            List<string> cards = new List<string>();

            int term = logicServer.CallStr(string.Format("get_player_cards({0}, C).", playerNumber));
            int tr = 0;
            if (term != 0) tr = 1;
            while (tr != 0)
            {
                int length = logicServer.StrTermLen(term);
                string t;

                t = logicServer.GetStrArg(term, 2);

                cards.Add(t);
                tr = logicServer.Redo();
            }

            return cards;
        }

        public bool IsGameStarted()
        {
            string started = string.Empty;

            try
            {
                int term = logicServer.ExecStr("get_game_started(S).");

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on get_game_started");
                }

                started = logicServer.GetStrArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return bool.Parse(started);
         
        }


        public bool IsTrumpChosen()
        {
            string chosen = string.Empty;

            try
            {
                int term = logicServer.ExecStr("get_trump_chosen(C).");

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on get_trump_chosen");
                }

                chosen = logicServer.GetStrArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return bool.Parse(chosen);

        }

        public SuitType GetTrump()
        {
            string trump = string.Empty;

            try
            {
                int term = logicServer.ExecStr("get_trump(C).");

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on get_trump");
                }

                trump = logicServer.GetStrArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            if (!string.IsNullOrEmpty(trump))
            {
                return (SuitType)Enum.Parse(typeof(SuitType), trump);
            }
            else
            {
                return SuitType.na;
            }
        }

        public SuitType GetCurrentSuit()
        {
            string suit = string.Empty;

            try
            {
                int term = logicServer.ExecStr("get_current_suit(S).");

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on get_current_suit");
                }

                suit = logicServer.GetStrArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            if (!string.IsNullOrEmpty(suit))
            {
                return (SuitType)Enum.Parse(typeof(SuitType), suit);
            }
            else
            {
                return SuitType.na;
            }
         
        }

        public int GetHandsOnTable()
        {
            int hands = 0;

            try
            {
                int term = logicServer.ExecStr("get_hands_accumulated(N).");

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on get_hands_accumulated");
                }

                hands = logicServer.GetIntArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return hands;
        }

        public int GetTurnPlayer()
        {
            int playerNumber = 0;

            try
            {
                int term = logicServer.ExecStr("which_player_turn(N).");

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on which_player_turn");
                }

                playerNumber = logicServer.GetIntArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return playerNumber;
        }

        public string GetPlayerCardOnTable(int playerNumber)
        {
            string card = string.Empty;

            try
            {
                int term = logicServer.ExecStr(string.Format("player_card_on_table({0}, Card).", playerNumber));

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on player_card_on_table");
                }

                card = logicServer.GetStrArg(term, 2);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return card;
        }

        public int GetDominantPlayer()
        {
            int playerNumber = 0;

            try
            {
                int term = logicServer.ExecStr("get_dominant_player(N).");

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on get_dominant_player");
                }

                playerNumber = logicServer.GetIntArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return playerNumber;
        }

        public int GetPlayerScore(int playerNumber)
        {
            int score = 0;

            try
            {
                int term = logicServer.ExecStr(string.Format("get_player_score({0}, N).", playerNumber));

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on get_player_score");
                }

                score = logicServer.GetIntArg(term, 2);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return score;
        }

        public int GetWinnerNumber()
        {
            int winner = 0;

            try
            {
                int term = logicServer.ExecStr("get_winner_player(N).");

                if (term == 0)
                {
                    return 0;
                }

                winner = logicServer.GetIntArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return winner;
        }

        #endregion

        #region State building
        public void LoadPlayerCards(int playerNumber, List<string> cards)
        {


            try
            {
                
                string cardList = "[", aliasFinderList = string.Empty;
                string execStr = string.Empty;

                int a=0;
                if (cards.Count() > 0)
                {
                    foreach (string card in cards)
                    {
                        cardList = cardList + string.Format("A{0}, ", a);
                        aliasFinderList = aliasFinderList + string.Format("card_alias(A{0}, '{1}'),", a, card.ToLower());
                        a++;
                    }

                    cardList = cardList.Substring(0, cardList.Length - 2);
                    cardList = cardList + "]";

                    execStr = aliasFinderList + string.Format("assert(player_has({0}, {1})).", playerNumber, cardList);
                }
                else
                {
                    execStr = string.Format("assert(player_has({0}, []))", playerNumber);
                }

                int term = logicServer.ExecStr(execStr);

                if (term == 0)
                {
                    throw new TCException("FAILED", "Failed to get term on adding cards");
                }

            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

        }

        public void LoadScores(int player1Score, int player2Score, int player3Score, int player4Score)
        {
            try
            {
                ExecStrWithExceptionThrown(string.Format("set_score(1, {0}), set_score(2, {1}), set_score(3, {2}), set_score(4, {3})", player1Score,
                                                                player2Score, player3Score, player4Score), "Scores could not be set");
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }
        }

        public void LoadTable(string player1Card, string player2Card, string player3Card, string player4Card, int handsAccumulated, int turnPlayer, int dominantPlayer, bool gameStarted, SuitType trump, SuitType currentSuit)
        {
            try
            {
                string cards = string.Format("set_player_card_on_hand(1, '{0}'), set_player_card_on_hand(2, '{1}'), set_player_card_on_hand(3, '{2}'), set_player_card_on_hand(4, '{3}'). ", player1Card,
                                                player2Card, player3Card, player4Card);

                string execStr = cards ;

                ExecStrWithExceptionThrown(execStr, "Player cards on table could not be set");

                execStr = string.Format("set_hands_accumulated({0}).", handsAccumulated);

                ExecStrWithExceptionThrown(execStr, "Could not set hands accumulated");

                execStr = string.Format("set_turn_player({0}).", turnPlayer);

                ExecStrWithExceptionThrown(execStr, "Could not set turn player");


                execStr = string.Format("set_dominant_player({0}).", dominantPlayer);

                ExecStrWithExceptionThrown(execStr, "Could not set the dominant player");


                execStr = string.Format("set_game_started.", gameStarted);

                ExecStrWithExceptionThrown(execStr, "Could not set the game_started");


                if (trump != SuitType.na)
                {
                    execStr = string.Format("set_trump({0}, R)", trump.ToString());

                    ExecStrWithExceptionThrown(execStr, "Could not set the trump");
                }


                if (currentSuit != SuitType.na)
                {
                    execStr = string.Format("set_current_suit({0})", currentSuit.ToString());

                    ExecStrWithExceptionThrown(execStr, "Could not set the current_suit");
                }
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }
            
        }
        
        #endregion

        #region Game Movement
        public void SubmitPlayerCard(int playerNumber, string card)
        {
            string execStr = string.Format("submit_player_card({0}, '{1}').", playerNumber, card);

            ExecStrWithExceptionThrown(execStr, "Unable to submit player card");
        }

        public void SetTurnStart()
        {
            ExecStrWithExceptionThrown("assert(turn_start), assert(current_suit(hearts)).", "Unable to assert turn_start");
        }

        public bool GetReshufflingOccurred()
        {
            try
            {
                int term = logicServer.ExecStr("reshuffling_occurred.");

                if (term == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return false;
        }

        public bool GetTurnStart()
        {
            try
            {
                int term = logicServer.ExecStr("turn_start.");

                if (term == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return false;
        }


        public void SetVeryFirstHandGoneFlag()
        {
            ExecStrWithExceptionThrown("set_very_first_hand_gone_flag.", "Could not exec set_very_first_hand_gone_flag");
        }
        #endregion

        #region Utility

        void ExecStrWithExceptionThrown(string execStr, string exceptionMessage)
        {
            try
            {
                int term = logicServer.ExecStr(execStr);

                if (term == 0)
                {
                    throw new TCException("FAILED", exceptionMessage + ", last error: " + GetLastError());
                }
            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }
        }

        public string GetLastError()
        {
            try
            {
                int term = logicServer.ExecStr("get_last_error(E).");

                if (term == 0)
                {
                    throw new TCException("LASTERRORFAILED", string.Empty);
                }

                return logicServer.GetStrArg(term, 1);
            }
            catch (LSException lse)
            {
                throw new TCException("LASTERRORFAILED", lse.Message);
            }

            return string.Empty;
        }

        public string GetInfo()
        {
            try
            {
                int term = logicServer.ExecStr("get_info(I).");

                if (term != 0)
                {
                    return logicServer.GetStrArg(term, 1);
                }

            }
            catch (LSException lse)
            {
                throw new TCException("FAILED", lse.Message);
            }

            return string.Empty;
        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        {
            logicServer.Close();
        }
        #endregion
    }
}
