using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Logging.Builder
{
    abstract class LogBuilder
    {
        protected string product;
        public virtual void BuildLog(LogItem log) { }
        public virtual void BuildReport(List<LogItem> logs) { }
        public abstract string GetResult();
    }
}
