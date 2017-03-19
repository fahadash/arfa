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
        Table CreateTable(Guid loginToken, string Tablename);

        Table GetTable(int tableId);

        void JoinTable(Guid loginToken, int tableId);

        IEnumerable<Table> ListJoinableTables(Guid loginToken);

        IEnumerable<Table> ListTablesUserIsOn(Guid loginToken);

        IEnumerable<Table> ListUserTables(Guid loginToken);

        void SuspendTable(int TableId);
    }
}
