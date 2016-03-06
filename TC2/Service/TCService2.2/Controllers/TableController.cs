using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TC2.Common.Exceptions;
using TC2.Models;
using TC2.Common.Util;

namespace TC2.Controllers
{
    public class TableController : ApiController
    {

        #region Preliminaries
        object ResultObject(string errorCode, string errorMessage)
        {
            return new { errorcode = errorCode, errormessage = errorMessage };
        }

        public class LJTParams
        {
            public string logintoken { get; set; }
        }
        [HttpPost]
        public object ListJoinableTables(LJTParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            object tables = null;
            try
            {
                tables = TableModel.GetJoinableTables(Guid.Parse(par.logintoken));
            }
            catch (TCException tce)
            {
                return new
                {
                    result = tce.GetJsonException(),
                    response = new
                    {
                        tables = new { }
                    }
                };
            }
            catch (Exception e)
            {
                return new { result = ResultObject("FAILED", e.Message), response = new { tables = new { } } };
            }

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { tables = tables } };

        }


        [HttpPost]
        public object ListTablesIAmOn(LJTParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            object tables = null;
            try
            {
                tables = TableModel.GetTablesUserIsOn(Guid.Parse(par.logintoken));
            }
            catch (TCException tce)
            {
                return new
                {
                    result = tce.GetJsonException(),
                    response = new
                    {
                        tables = new { }
                    }
                };
            }
            catch (Exception e)
            {
                return new { result = ResultObject("FAILED", e.Message), response = new { tables = new { } } };
            }

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { tables = tables } };

        }

        [HttpPost]
        public object ListUserTables(LJTParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            object tables = null;
            try
            {
                tables = TableModel.GetUserTables(Guid.Parse(par.logintoken));
            }
            catch (TCException tce)
            {
                return new
                {
                    result = tce.GetJsonException(),
                    response = new
                    {
                        tables = new { }
                    }
                };
            }
            catch (Exception e)
            {
                return new { result = ResultObject("FAILED", e.Message), response = new { tables = new { } } };
            }

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { tables = tables } };

        }


        public class CreateTableParams
        {
            public string logintoken { get; set; }
            public string tablename { get; set; }
        }

        private void ValidateCreateParams(CreateTableParams param)
        {
            if (param.tablename.Length < 3)
            {
                throw new TCException("TABLENAMETOOSHORT", "Table name must be at least 3 characters long");
            }
        }

        [HttpPost]
        public object CreateTable(CreateTableParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            object table = null;
            try
            {
                ValidateCreateParams(par);
                table = TableModel.CreateTable(Guid.Parse(par.logintoken), par.tablename);
            }
            catch (TCException tce)
            {
                return new
                {
                    result = tce.GetJsonException(),
                    response = new
                    {
                        table = new { }
                    }
                };
            }
            catch (Exception e)
            {
                return new { result = ResultObject("FAILED", e.Message), response = new {  } };
            }

            return new { result = ResultObject("SUCCESS", string.Empty), response = table };

        }


        public class SuspendTableParams
        {
            public string logintoken { get; set; }
            public int tableid { get; set; }
        }

        private void ValidateSuspendParams(SuspendTableParams param)
        {
            if (param.tableid < 1)
            {
                throw new TCException("INVALIDTABLEID", "Bad table.");
            }
        }

        [HttpPost]
        public object SuspendTable(SuspendTableParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            object table = null;
            try
            {
                ValidateSuspendParams(par);
                TableModel.SuspendTable(Guid.Parse(par.logintoken), Convert.ToInt32(par.tableid));
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

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { } };

        }


        [HttpPost]
        public object JoinTable(SuspendTableParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            object table = null;
            try
            {
                ValidateSuspendParams(par);
                TableModel.JoinTable(Guid.Parse(par.logintoken), Convert.ToInt32(par.tableid));
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

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { } };

        }


        [HttpPost]
        public object GetTimestamp(SuspendTableParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            string timestamp;
            try
            {
                ValidateSuspendParams(par);
                timestamp = TableModel.GetTableTimestamp(Guid.Parse(par.logintoken), Convert.ToInt32(par.tableid));
                timestamp = DateTime.Parse(timestamp).ConvertToUnixTimestamp().ToString();
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
                return new { result = ResultObject("FAILED", e.Message), response = new { Timestamp = string.Empty } };
            }

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { Timestamp = timestamp } };

        }

        #endregion

        #region Begin Table

        public class BeginTableParams
        {
            public string logintoken { get; set; }
            public int tableid { get; set; }
        }

        private void ValidateBeginTableParams(BeginTableParams param)
        {
            if (param.tableid < 1)
            {
                throw new TCException("INVALIDTABLEID", "Bad table.");
            }
        }

        [HttpPost]
        public object BeginTable(BeginTableParams par)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            string timestamp;
            try
            {
                ValidateBeginTableParams(par);
                TableModel.BeginGame(Guid.Parse(par.logintoken), Convert.ToInt32(par.tableid));
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

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { } };
        }

        #endregion

        #region Choose Trump

        public class ChooseTrumpParams
        {
            public string logintoken { get; set; }
            public int tableid { get; set; }

            public string trump { get; set; }
        }

        private void ValidateChooseTrumpParams(ChooseTrumpParams param)
        {
            if (param.tableid < 1)
            {
                throw new TCException("INVALIDTABLEID", "Bad table.");
            }

            if (!TableModel.IsValidSuit(param.trump))
            {
                throw new TCException("INVALIDSUIT", "Invalid suit");
            }
        }

        [HttpPost]
        public object ChooseTrump(ChooseTrumpParams param)
        {
            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }
            
            try
            {
                ValidateChooseTrumpParams(param);
                TableModel.ChooseTrump(Guid.Parse(param.logintoken), Convert.ToInt32(param.tableid), param.trump);
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

            return new { result = ResultObject("SUCCESS", string.Empty), response = new { } };
        }
        #endregion

        #region Get Table Update
        public class GetTableUpdateParams
        {
            public string logintoken { get; set; }
            public int tableid { get; set; }
        }

        private void ValidateGetTableUpdateParams(BeginTableParams param)
        {
            if (param.tableid < 1)
            {
                throw new TCException("INVALIDTABLEID", "Bad table.");
            }
        }

        [HttpPost]
        public object GetTableUpdate(GetTableUpdateParams par)
        {
            object returnValue = null;

            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            try
            {
                returnValue = TableModel.GetTableUpdate(Guid.Parse(par.logintoken), par.tableid);
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
                return new { result = ResultObject("FAILED", e.Message), response = new {} };
            }

            return new { result = ResultObject("SUCCESS", string.Empty), response = returnValue };
        }
        #endregion

        #region Submit User Card

        public class SubmitUserCardParams
        {
            public string logintoken { get; set; }
            public int tableid { get; set; }

            public string cardalias { get; set; }
        }

        private void ValidateSubmitUserCardParams(SubmitUserCardParams param)
        {
            if (param.tableid < 1)
            {
                throw new TCException("INVALIDTABLEID", "Bad table.");
            }

            if (TableModel.GetCard(param.cardalias) == null)
            {
                throw new TCException("INVALIDCARD", "Invalid Card");
            }
        }

        [HttpPost]
        public object SubmitUserCard(SubmitUserCardParams param)
        {
            object returnValue = null;

            if (!ModelState.IsValid)
            {
                throw new ApplicationException("Invalid model state");
            }

            try
            {
                ValidateSubmitUserCardParams(param);
                returnValue = TableModel.SubmitUserCard(Guid.Parse(param.logintoken), Convert.ToInt32(param.tableid), param.cardalias);
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

            return new { result = ResultObject("SUCCESS", string.Empty), response = returnValue };
        }

        #endregion
    }
}
