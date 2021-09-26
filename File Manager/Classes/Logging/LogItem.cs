using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Logging
{
    class LogItem
    {
        public DateTime CreatedTime { get; private set; }
        public Levels Level { get; private set; }
        public string Message { get; private set; }
        public Exception Exception { get; private set; }

        public LogItem(Levels level, string message, Exception exception)
        {
            CreatedTime = DateTime.Now;
            Level = level;
            Message = message;
            Exception = exception;
        }
    }
}
