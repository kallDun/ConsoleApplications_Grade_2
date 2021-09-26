using File_Manager.Classes.Logging.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Logging
{
    class Logger
    {
        List<LogItem> logs = new List<LogItem>();
        public List<LogItem> Logs { get => logs; }

        public void Log(Levels level, string Message, Exception exception = null) 
        {
            var log = new LogItem(level, Message, exception);
            logs.Add(log);
            var builder = (LogBuilder)Activator.CreateInstance(ReportDirector.ReportType);
            new ReportDirector().AddToReport(builder, log);
        }
        public void LogDebug(string Message) => Log(Levels.Debug, Message);
        public void LogInformation(string Message) => Log(Levels.Information, Message);
        public void LogWarning(string Message) => Log(Levels.Warning, Message);
        public void LogError(string Message, Exception exception) => Log(Levels.Error, Message, exception);
    }
}
