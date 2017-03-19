using System;
using System.Collections.Generic;
using System.Text;
using arfa.Interface.Enums;
using arfa.Interface.Models;

namespace arfa.Interface.Business
{
    public interface ITableService
    {
        IEnumerable<Table> GetJoinableTables(Guid loginToken);
        IEnumerable<Table> GetTableUserIsOn(Guid loginToken);
        IEnumerable<Table> GetUserTables(Guid loginToken);
        Table CreateTable(Guid loginToken, string tableName);
        void SuspendTable(Guid loginToken, int tableId);
        void JoinTable(Guid loginToken, int tableId);
        void SubmitUserCard(Guid loginToken, int tableId, Card card);
        void BeginGame(Guid loginToken, int tableId);
        void ChooseTrump(Guid loginToken, int tableId, Suit trumpSuit);
        DateTime GetTableTimestamp(Guid loginToken, int tableId);
        TableState GetTableUpdate(Guid loginToken, int tableId);
        
    }
}
