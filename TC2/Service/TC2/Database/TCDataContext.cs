using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC2.Database
{
    public class TCDataContext : IDisposable
    {
        private TC2DataContext _dataContext = new TC2DataContext();
         static TCDataContext _ctx = null;
         static int _refCount = 0;
         static object lockObj = new object();
	
        private TCDataContext()
        {
        }

        public static TCDataContext CreateContext()
        {
            lock (lockObj)
            {
                if (TCDataContext._ctx == null)
                {
                    _ctx = new TCDataContext();
                }

                _refCount++;
            }
            return _ctx;
        }

        public void Dispose()
        {
            lock (lockObj)
            {
                if (_refCount > 0)
                {
                    _refCount--;
                    if (_refCount == 0)
                    {
                       // _ctx.Dispose();
                       // _ctx = null;
                    }
                }
            }
        }

        public static implicit operator TC2DataContext(TCDataContext ctx)
        {
            if (ctx._dataContext.IsDead)
            {
                ctx._dataContext = new TC2DataContext();
            }
            return ctx._dataContext;
        }

    }
}