using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TC2.Common.Exceptions;
using TC2.Models;

namespace TCService2._2.SignalR
{ 
    [HubName("arfaHub")]
    public class ArfaHub : Hub
    {
        private object ResultObject(string errorCode, string errorMessage)
        {
            return new { errorcode = errorCode, errormessage = errorMessage };
        }

        private void ValidateSubmitUserCardParams(int tableId, string cardAlias)
        {
            if (tableId < 1)
            {
                throw new TCException("INVALIDTABLEID", "Bad table.");
            }

            if (TableModel.GetCard(cardAlias) == null)
            {
                throw new TCException("INVALIDCARD", "Invalid Card");
            }
        }


        [HubMethodName("submitUserCard")]
        public object SubmitUserCard(string loginToken, int tableId, string cardAlias)
        {
            object returnValue = null;
            var user = UserModel.GetUserByToken(Guid.Parse(loginToken));
            try
            {
                ValidateSubmitUserCardParams(tableId, cardAlias);
                returnValue = TableModel.SubmitUserCard(Guid.Parse(loginToken), Convert.ToInt32(tableId), cardAlias);
            }
            catch (TCException tce)
            {
                return new
                {
                    result = tce.GetJsonException(),
                    response = new
                    {

                    }
                };
            }
            catch (Exception e)
            {
                return new { result = ResultObject("FAILED", e.Message), response = new { } };
            }

            Clients.OthersInGroup("table_group_" + tableId.ToString()).playerPuts(user.UserName, cardAlias);

            return new { result = ResultObject("SUCCESS", string.Empty), response = returnValue };
        }
  
        [HubMethodName("userJoinsTable")]
        public void UserJoinsTable(int tableId)
        {
            Groups.Add(Context.ConnectionId, "table_group_" + tableId.ToString());
        }

        [HubMethodName("userPartsTable")]
        public void UserPartsTable(int tableId)
        {
            Groups.Remove(Context.ConnectionId, "table_group_" + tableId.ToString());
        }
    }
}