using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDotNet.Data
{
    /// <summary>
    /// Logger
    /// Implements chain of responsability
    /// </summary>
    abstract class Logger
    {
        Logger _next;

        public Logger(Logger next)
        {
            _next = next;
        }

        protected bool CanHandle(string log)
        {
            return true;
        }

        protected abstract void Handle(string log);

        public void Log(string log)
        {
            if (CanHandle(log) || _next == null)
                Handle(log);
            else
                _next.Handle(log);
        }
    }
}
