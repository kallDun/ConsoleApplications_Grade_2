using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Logging.Builder
{
    class XmlLogBuilder : LogBuilder
    {
        private string GetTextLog(LogItem log) => $"<log>\n\t<CreatedTime>{log.CreatedTime}</CreatedTime>\n\t<Level>{log.Level}</Level>\n\t<Message>{log.Message}</Message>\n\t<Exception>{log.Exception}</Exception>\n</log>\n";

        public override void BuildLog(LogItem log)
        {
            product = GetTextLog(log);
        }

        public override void BuildReport(List<LogItem> logs)
        {
            product = string.Join("", logs.Select(x => GetTextLog(x)));
        }

        public override string GetResult() => product;
    }
}
