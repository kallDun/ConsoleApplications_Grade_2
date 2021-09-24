using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Logging
{
    class Logger
    {

        ICollection<LogItem> Logs = new List<LogItem>();
        public void Log(Levels level, string Message) => Logs.Add(new LogItem(level, Message));
        public void LogDebug(string Message) => Log(Levels.Debug, Message);
        public void LogInformation(string Message) => Log(Levels.Information, Message);
        public void LogWarning(string Message) => Log(Levels.Warning, Message);
        public void LogError(string Message) => Log(Levels.Error, Message);

    }
}
