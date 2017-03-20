using arfa.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfa.Interface.Repositories
{
    public enum TableOperationResult
    {
        TableNameTaken,
        InvalidLoginToken,
        OperationSuccessful,
        InvalidTable,
        UserNotOnThisTable,
        UserAlreadyOnTable,
        UserOwnsTable,
        TableFull
    }

    public interface ITableRepository
    {
        Table CreateTable(int userId, string tableName);

        Table GetTable(int tableId);

        void JoinTable(int userId, int tableId);

        IEnumerable<Table> ListJoinableTables(int userId);

        IEnumerable<Table> ListTablesUserIsOn(int userId);

        IEnumerable<Table> ListUserTables(int userId);

        void SuspendTable(int tableId);

        TableState GetTableState(int tableId);
    }
}
