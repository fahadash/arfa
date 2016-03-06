using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrologInterface;
using System.Collections.Generic;
using TC2.Controllers;
using TC2.Models;
using TC2.Database;
using System.Linq;

namespace TC2.Tests.Test
{
    
    [TestClass]
    public class PrologInterfaceTest
    {
        static string xplPath = @"C:\Dev\Amzi\Workspace\TC\bin\TC.xpl";
        [TestMethod]
        public void NewGameGetCards()
        {
            Game game = new Game();

            game.NewGame();

            List<string> cards1 = game.GetPlayerCards(1);


            List<string> cards2 = game.GetPlayerCards(2);


            List<string> cards3 = game.GetPlayerCards(3);
        }


        [TestMethod]
        public void SaveState()
        {
            UserAccountController controller = new UserAccountController();

            string token1 = "B028F9EF-06FD-4822-BE90-80DF4C84265D";
            string token2 = "B25AA3B2-C135-4FB6-BB6C-763D7484D77F";
            string token3 = "8EA5A281-9CBB-46B5-AEFA-8B37DC924142";
            string token4 = "052B3D8D-B7BA-4E6B-8190-265337C06263";

            TableModel table = TableModel.CreateTable(Guid.Parse(token1), "TestTable1024");

            TableModel.JoinTable(Guid.Parse(token2), table.TableId);
            TableModel.JoinTable(Guid.Parse(token3), table.TableId);
            TableModel.JoinTable(Guid.Parse(token4), table.TableId);

            Game game = new Game();

            game.NewGame();

            TableModel.SaveGameState(table.TableId, game);

        }

        [TestMethod]
        public void LoadState()
        {
            UserAccountController controller = new UserAccountController();

            string token1 = "B028F9EF-06FD-4822-BE90-80DF4C84265D";
            string token2 = "B25AA3B2-C135-4FB6-BB6C-763D7484D77F";
            string token3 = "8EA5A281-9CBB-46B5-AEFA-8B37DC924142";
            string token4 = "052B3D8D-B7BA-4E6B-8190-265337C06263";

            int tableId = 28;

            Game game;

            game = TableModel.BuildGameFromDB(tableId);

        }

        [TestMethod]
        public void GameTestNewGamePutBogusCard()
        {
            string token1 = "B028F9EF-06FD-4822-BE90-80DF4C84265D";
            string token2 = "B25AA3B2-C135-4FB6-BB6C-763D7484D77F";
            string token3 = "8EA5A281-9CBB-46B5-AEFA-8B37DC924142";
            string token4 = "052B3D8D-B7BA-4E6B-8190-265337C06263";

            TableModel table = TableModel.CreateTable(Guid.Parse(token1), "GameTestNewGamePutCard_TestTable0011");

            TableModel.JoinTable(Guid.Parse(token2), table.TableId);
            TableModel.JoinTable(Guid.Parse(token3), table.TableId);
            TableModel.JoinTable(Guid.Parse(token4), table.TableId);
            TableModel.BeginGame(Guid.Parse(token1), table.TableId);
            TableModel.SubmitUserCard(Guid.Parse(token1), table.TableId, "ad");
        }

        [TestMethod]
        public void GameTestNewGamePutRealCardButNoTrumpChosen()
        {
            string token1 = "B028F9EF-06FD-4822-BE90-80DF4C84265D";
            string token2 = "B25AA3B2-C135-4FB6-BB6C-763D7484D77F";
            string token3 = "8EA5A281-9CBB-46B5-AEFA-8B37DC924142";
            string token4 = "052B3D8D-B7BA-4E6B-8190-265337C06263";

            TableModel table = TableModel.CreateTable(Guid.Parse(token1), "GameTestNewGamePutRealCard_TestTable0008");

            TableModel.JoinTable(Guid.Parse(token2), table.TableId);
            TableModel.JoinTable(Guid.Parse(token3), table.TableId);
            TableModel.JoinTable(Guid.Parse(token4), table.TableId);
            UserModel user = UserModel.GetUserByToken(Guid.Parse(token4));

            Assert.IsTrue(user != null, "User login token 1 invalid");

            TableModel.BeginGame(Guid.Parse(token1), table.TableId);

            TC2DataContext ctx = TCDataContext.CreateContext();
            {
                var query = ctx.TableUsers.Where(c => c.TableId.Equals(table.TableId) &&
                                    c.UserId.Equals(user.UserId));
                TableUser tuser = query.FirstOrDefault();

                TableUserCard tucard = tuser.TableUserCards.FirstOrDefault();

                Assert.IsTrue(tucard != null, "Bad card");

                TableModel.SubmitUserCard(Guid.Parse(token4), table.TableId, tucard.Card.CardAlias.ToLower());
            }
        }


        [TestMethod]
        public void GameTestNewGameChooseTrumpPutRealCard()
        {
            string token1 = "B028F9EF-06FD-4822-BE90-80DF4C84265D";
            string token2 = "B25AA3B2-C135-4FB6-BB6C-763D7484D77F";
            string token3 = "8EA5A281-9CBB-46B5-AEFA-8B37DC924142";
            string token4 = "052B3D8D-B7BA-4E6B-8190-265337C06263";

            TableModel table = TableModel.CreateTable(Guid.Parse(token1), "GameTestNewGameChooseTrumpPutRealCard_TestTable0010");

            TableModel.JoinTable(Guid.Parse(token2), table.TableId);
            TableModel.JoinTable(Guid.Parse(token3), table.TableId);
            TableModel.JoinTable(Guid.Parse(token4), table.TableId);
            UserModel user = UserModel.GetUserByToken(Guid.Parse(token4));

            Assert.IsTrue(user != null, "User login token 1 invalid");

            TableModel.BeginGame(Guid.Parse(token1), table.TableId);

            TC2DataContext ctx = TCDataContext.CreateContext();
            {
                var query = ctx.TableUsers.Where(c => c.TableId.Equals(table.TableId) &&
                                    c.UserId.Equals(user.UserId));
                TableUser tuser = query.FirstOrDefault();

                TableUserCard tucard = tuser.TableUserCards.FirstOrDefault();

                Assert.IsTrue(tucard != null, "Bad card");
                TableModel.ChooseTrump(Guid.Parse(token4), table.TableId, "diamonds");
                TableModel.SubmitUserCard(Guid.Parse(token4), table.TableId, tucard.Card.CardAlias.ToLower());
            }
        }

        [TestMethod]
        public void FullGameTest1()
        {
            string token1 = "B028F9EF-06FD-4822-BE90-80DF4C84265D";
            string token2 = "B25AA3B2-C135-4FB6-BB6C-763D7484D77F";
            string token3 = "8EA5A281-9CBB-46B5-AEFA-8B37DC924142";
            string token4 = "052B3D8D-B7BA-4E6B-8190-265337C06263";

            TableModel table = TableModel.CreateTable(Guid.Parse(token1), "FullGameTest1_TestTable0021");

            TableModel.JoinTable(Guid.Parse(token2), table.TableId);
            TableModel.JoinTable(Guid.Parse(token3), table.TableId);
            TableModel.JoinTable(Guid.Parse(token4), table.TableId);
            UserModel user = UserModel.GetUserByToken(Guid.Parse(token4));

            Assert.IsTrue(user != null, "User login token 1 invalid");

            TableModel.BeginGame(Guid.Parse(token1), table.TableId);

            TC2DataContext ctx = TCDataContext.CreateContext();
            {
                var query = ctx.TableUsers.Where(c => c.TableId.Equals(table.TableId) &&
                                    c.UserId.Equals(user.UserId));
                TableUser tuser = query.FirstOrDefault();

                TableUserCard tucard = tuser.TableUserCards.FirstOrDefault();

                Assert.IsTrue(tucard != null, "Bad card");
                TableModel.ChooseTrump(Guid.Parse(token4), table.TableId, "diamonds");
                TableModel.SubmitUserCard(Guid.Parse(token4), table.TableId, "5c");
                TableModel.SubmitUserCard(Guid.Parse(token1), table.TableId, "2c");
                TableModel.SubmitUserCard(Guid.Parse(token2), table.TableId, "4d");
                TableModel.SubmitUserCard(Guid.Parse(token3), table.TableId, "9c");

            }
        }


        [TestMethod]
        public void FullGame_First3HandWinner()
        {
            string token1 = "B028F9EF-06FD-4822-BE90-80DF4C84265D";
            string token2 = "B25AA3B2-C135-4FB6-BB6C-763D7484D77F";
            string token3 = "8EA5A281-9CBB-46B5-AEFA-8B37DC924142";
            string token4 = "052B3D8D-B7BA-4E6B-8190-265337C06263";

            TableModel table = TableModel.CreateTable(Guid.Parse(token1), "FullGame_First3HandWinner017");

            TableModel.JoinTable(Guid.Parse(token2), table.TableId);
            TableModel.JoinTable(Guid.Parse(token3), table.TableId);
            TableModel.JoinTable(Guid.Parse(token4), table.TableId);
            UserModel user = UserModel.GetUserByToken(Guid.Parse(token4));

            Assert.IsTrue(user != null, "User login token 1 invalid");

            TableModel.BeginGame(Guid.Parse(token1), table.TableId);

            TC2DataContext ctx = TCDataContext.CreateContext();
            {
                var query = ctx.TableUsers.Where(c => c.TableId.Equals(table.TableId) &&
                                    c.UserId.Equals(user.UserId));
                TableUser tuser = query.FirstOrDefault();

                TableUserCard tucard = tuser.TableUserCards.FirstOrDefault();

                Assert.IsTrue(tucard != null, "Bad card");
                TableModel.ChooseTrump(Guid.Parse(token4), table.TableId, "diamonds");
                TableModel.SubmitUserCard(Guid.Parse(token4), table.TableId, "5c");
                TableModel.SubmitUserCard(Guid.Parse(token1), table.TableId, "2c");
                TableModel.SubmitUserCard(Guid.Parse(token2), table.TableId, "4d");
                TableModel.SubmitUserCard(Guid.Parse(token3), table.TableId, "9c");


                TableModel.SubmitUserCard(Guid.Parse(token2), table.TableId, "8h");
                TableModel.SubmitUserCard(Guid.Parse(token3), table.TableId, "10h");
                TableModel.SubmitUserCard(Guid.Parse(token4), table.TableId, "qh");
                TableModel.SubmitUserCard(Guid.Parse(token1), table.TableId, "4h");


                TableModel.SubmitUserCard(Guid.Parse(token4), table.TableId, "ad");
                TableModel.SubmitUserCard(Guid.Parse(token1), table.TableId, "9d");
                TableModel.SubmitUserCard(Guid.Parse(token2), table.TableId, "3d");
                TableModel.SubmitUserCard(Guid.Parse(token3), table.TableId, "5d");

            }
        }
    }
}
