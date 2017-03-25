using arfa.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using arfa.Interface.Models;
using System.Linq;
using arfa.Interface.Exceptions;
using arfa.Interface.Enums;
using arfa.Interface.Helpers;

namespace arfa.Repository.EfSql
{
    public class TableRepository : ITableRepository
    {
        private readonly Database.arfaDBContext dbContext;

        public TableRepository(Database.arfaDBContext context)
        {
            dbContext = context;
        }

        public void BeginGame(int tableId)
        {
            var table = dbContext.Table.FirstOrDefault(t => t.TableId == tableId);
            
            table.GameStarted = true;
            
            dbContext.SaveChanges();
        }

        public Table CreateTable(int userId, string tableName)
        {
            if (dbContext.Table.Any(t => t.TableName.Equals(tableName, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new ArfaException("TABLENAMETAKEN", "This table name is already in user, choose another");
            }
            var user = dbContext.User.First(u => u.UserId == userId);

            var table = new Database.Table()
            {
                TableName = tableName,
                OwnerUser = user,
                TableUser = new[]
                {
                    new Database.TableUser()
                    {
                        User = user
                    }
                }
            };
            dbContext.Table.Add(table);
            dbContext.SaveChanges();

            return table.ToInterface(); 
        }

    public Table GetTable(int tableId)
    {
            return dbContext.Table.Select(t => t.ToInterface()).FirstOrDefault();
    }

    public TableState GetTableState(int tableId)
    {
        throw new NotImplementedException();
    }

    public void JoinTable(int userId, int tableId)
        {
            var table = dbContext.Table.FirstOrDefault(t => t.TableId == tableId);

            table.TableUser.Add(new Database.TableUser()
            {
                User = dbContext.User.FirstOrDefault(u => u.UserId == userId),
                Score = 0
            });

            dbContext.SaveChanges();
        }

    public IEnumerable<Table> ListJoinableTables(int userId)
    {
            return dbContext
                        .Table
                        .Where(t => t.OwnerUserId != userId && !t.TableUser.Any(tu => tu.UserId == userId)
                                && t.TableUser.Count() <= 3)
                        .Select(t => t.ToInterface());
    }

    public IEnumerable<Table> ListTablesUserIsOn(int userId)
        {
            return dbContext
                        .Table
                        .Where(t => t.OwnerUserId == userId || t.TableUser.Any(tu => tu.UserId == userId))
                        .Select(t => t.ToInterface());
        }

    public IEnumerable<Table> ListUserTables(int userId)
        {
            return dbContext
                        .Table
                        .Where(t => t.OwnerUserId == userId)
                        .Select(t => t.ToInterface());
        }

        public void SetTrump(int tableId, Suit suit)
        {
            var table = dbContext.Table.FirstOrDefault(t => t.TableId == tableId);

            table.Trump = suit.GetDescription();

            dbContext.SaveChanges();
        }

        public void SuspendTable(int tableId)
    {
            var table = dbContext.Table.FirstOrDefault(t => t.TableId == tableId);

            table.Suspended = true;
            dbContext.SaveChanges();
    }
}
}
