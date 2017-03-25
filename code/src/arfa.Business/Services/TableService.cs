using arfa.Interface.Business;
using System;
using System.Collections.Generic;
using System.Text;
using arfa.Interface.Enums;
using arfa.Interface.Models;
using arfa.Interface.Repositories;
using arfa.Interface.Exceptions;

namespace arfa.Business.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository tableRepository;
        private readonly IUserRepository userRepository;

        public TableService(ITableRepository tableRepo, IUserRepository userRepo)
        {
            tableRepository = tableRepo;
            userRepository = userRepo;
        }

        public void BeginGame(Guid loginToken, int tableId)
        {
            throw new NotImplementedException();
        }

        public void ChooseTrump(Guid loginToken, int tableId, Suit trumpSuit)
        {
            throw new NotImplementedException();
        }

        public Table CreateTable(Guid loginToken, string tableName)
        {
            var user = VerifyTokenOrThrow(loginToken);

            var table = tableRepository.CreateTable(user.UserId, tableName);

            return table;
        }

        public IEnumerable<Table> GetJoinableTables(Guid loginToken)
        {

            var user = VerifyTokenOrThrow(loginToken);

            try
            {
                    return tableRepository.ListJoinableTables(user.UserId);
            }
            catch (Exception e)
            {
                // TODO: Add logging
                throw new ArfaException("UNKNOWN", "An unknown error has occurred");
            }
        }

        public DateTime GetTableTimestamp(Guid loginToken, int tableId)
        {
            throw new NotImplementedException();
        }

        public TableState GetTableUpdate(Guid loginToken, int tableId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Table> GetTableUserIsOn(Guid loginToken)
        {
            var user = VerifyTokenOrThrow(loginToken);

            try
            {
                return tableRepository.ListTablesUserIsOn(user.UserId);
            }
            catch (Exception e)
            {
                // TODO: Add logging
                throw new ArfaException("UNKNOWN", "An unknown error has occurred");
            }
        }

        public IEnumerable<Table> GetUserTables(Guid loginToken)
        {
            var user = VerifyTokenOrThrow(loginToken);

            try
            {
                return tableRepository.ListUserTables(user.UserId);
            }
            catch (Exception e)
            {
                // TODO: Add logging
                throw new ArfaException("UNKNOWN", "An unknown error has occurred");
            }
        }

        public void JoinTable(Guid loginToken, int tableId)
        {
            var user = VerifyTokenOrThrow(loginToken);

            try
            {
                tableRepository.JoinTable(user.UserId, tableId);
            }
            catch (Exception e)
            {
                // TODO: Add logging
                throw new ArfaException("UNKNOWN", "An unknown error has occurred");
            }
        }

        public void SubmitUserCard(Guid loginToken, int tableId, Card card)
        {
            throw new NotImplementedException();
        }

        public void SuspendTable(Guid loginToken, int tableId)
        {
            var user = VerifyTokenOrThrow(loginToken);

            try
            {
                var table = tableRepository.GetTable(tableId);
                if (table.Owner.UserId == user.UserId)
                {
                    tableRepository.SuspendTable(tableId);
                }
                else
                {
                    throw new ArfaException("USERNOTOWNER", "User does not own this table");
                }
            }
            catch (Exception e)
            {
                // TODO: Add logging
                throw new ArfaException("UNKNOWN", "An unknown error has occurred");
            }
        }

        private User VerifyTokenOrThrow(Guid loginToken)
        {
            var user = userRepository.GetUser(loginToken);

            if (user == null)
            {
                throw new ArfaException("INVALIDTOKEN", "Invalid token, make user user is logged on");
            }

            return user;
        }
    }
}
