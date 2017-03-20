using arfa.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using arfa.Interface.Models;
using System.Linq;
using arfa.Interface.Exceptions;

namespace arfa.Repository.EfSql
{
    public class TableRepository : ITableRepository
    {
        private readonly Database.arfaDBContext dbContext;

        public TableRepository(Database.arfaDBContext context)
        {
            dbContext = context;
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
        throw new NotImplementedException();
    }

    public TableState GetTableState(int tableId)
    {
        throw new NotImplementedException();
    }

    public void JoinTable(int userId, int tableId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Table> ListJoinableTables(int userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Table> ListTablesUserIsOn(int userId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Table> ListUserTables(int userId)
    {
        throw new NotImplementedException();
    }

    public void SuspendTable(int tableId)
    {
        throw new NotImplementedException();
    }
}
}
