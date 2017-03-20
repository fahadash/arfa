using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace arfaWeb.Models
{
    public class Table
    {
        public int tableId { get; set; }
        public string tableName { get; set; }
        public int slotsAvailable { get; set; }
        public User owner { get; set; }

        public static Table FromInterface(arfa.Interface.Models.Table table)
        {
            return new Table()
            {
                tableId = table.TableId,
                tableName = table.TableName,
                owner = User.FromInterface(table.Owner),
                slotsAvailable = table.AvailableSlots
            };
        }
    }
}
