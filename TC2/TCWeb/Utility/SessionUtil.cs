using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace TCWeb.Utility
{
    public class SessionUtil
    {
        public static Guid LoginToken
        {
            get
            {
                 Guid guid = Guid.Empty;

                 if (!IsNotPresent("LoginToken") && Guid.TryParse(Session.Contents["LoginToken"].ToString(), out guid))
                 {
                     return guid;
                 }
                 else
                 {
                     return Guid.Empty;
                 }
            }
            set
            {
                SetSessionState("LoginToken", value);
            }
        }


        public static string Name
        {
            get
            {

                if (!IsNotPresent("UserFullName"))
                {
                    return Session.Contents["UserFullName"].ToString();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SetSessionState("UserFullName", value);
            }
        }

        #region Helpers
        static bool IsNotPresent(string valueName)
        {
            return Session.Contents[valueName] == null;
        }

        static void SetSessionState(string valueName, object value)
        {
            if (Session.Contents[valueName] != null)
            {
                Session.Contents[valueName] = value;
            }
            else
            {
                Session.Contents.Add(valueName, value);
            }
        }


        static public HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
        #endregion

        public static bool IsUserLoggedIn
        {
            get
            {
                return LoginToken != null && TCWeb.Utility.SessionUtil.LoginToken != Guid.Empty;
            }
        }
    }
}