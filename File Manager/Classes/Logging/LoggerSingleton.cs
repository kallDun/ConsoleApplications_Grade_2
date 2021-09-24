using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Logging
{
    class LoggerSingleton
    {
        static Logger logger;
        public Logger GetInstance()
        {
            if (logger is null) logger = new Logger();
            return logger;
        }
    }
}
