using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrologInterface
{
    public class LogicServerHelper
    {
        LogicServer logicServer = new LogicServer();
        static string xplPath = ConfigurationManager.AppSettings["logicBasePath"].ToString();

        public LogicServerHelper()
        {
            logicServer = new LogicServer();
            logicServer.Init("");
            logicServer.Load(xplPath);
        }

        public int ExecStr(string str)
        {
            Debug(str);
            return logicServer.ExecStr(str);
        }

        public string GetStrArg(int term, int argumentNumber)
        {
            return logicServer.GetStrArg(term, argumentNumber);
        }
        
        public int GetIntArg(int term, int argumentNumber)
        {
            return logicServer.GetIntArg(term, argumentNumber);
        }

        public int CallStr(string str)
        {
            Debug(str);
            return logicServer.CallStr(str);
        }

        public int Redo()
        {
            return logicServer.Redo();
        }

        public int StrTermLen(int term)
        {
            return logicServer.StrTermLen(term);
        }

        public void Close()
        {
            logicServer.Close();
        }

        public static string debugStr = string.Empty;

        void Debug(string str)
        {
            debugStr = debugStr + str.Replace(".", ",") + "\r\n";
        }
    }
}
