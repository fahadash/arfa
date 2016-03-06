using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC2.Common.Exceptions
{
    public class TCException : ApplicationException
    {
        public string ErrorCode { get; set; }

        public TCException(string errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public object GetJsonException()
        {
            return new { errorcode = ErrorCode, message = Message };
        }

        public static void ThrowIfNull(object obj, string errorCode, string message = "")
        {
            if (obj == null)
            {
                throw new TCException(errorCode, message);
            }
        }

        public static TCException INVALIDLOGINTOKEN
        {
            get { return new TCException("INVALIDLOGINTOKEN", "Invalid login token, make sure the user is logged in"); }
        }


        public static TCException INVALIDTABLE
        {
            get { return new TCException("INVALIDTABLE", "Invalid Table"); }
        }

        public static TCException USERNOTOWNER
        {
            get { return new TCException("USERNOTOWNER", "User does not own this table."); }
        }

        public static TCException NOUSERTURN
        {
            get { return new TCException("NOUSERTURN", "This is not this user's turn."); }
        }

        public static Exception USERNOTONTABLE 
        {
            get
            {
                return new TCException("USERNOTONTABLE", "This user is not on this table");
            }
        }
    }
}