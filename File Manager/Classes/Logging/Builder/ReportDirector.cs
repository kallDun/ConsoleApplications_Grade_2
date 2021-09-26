using System;
using System.Collections.Generic;
using System.IO;

namespace File_Manager.Classes.Logging.Builder
{
    class ReportDirector
    {
        private Logger logger;
        private static string log_path = "log.txt";
        public static Type ReportType;

        public ReportDirector()
        {
            logger = LoggerSingleton.GetInstance();
        }

        public void AddToReport(LogBuilder builder, LogItem log)
        {
            builder.BuildLog(log);
            var result = builder.GetResult();
            try
            {
                File.AppendAllText(log_path, result);
            }
            catch (Exception e)
            {
                logger.LogError("Can't add log to file", e);
            }
        }

        public void FormNewReport(LogBuilder builder)
        {
            if (builder.GetType() == ReportType) return;
            ReportType = builder.GetType();

            builder.BuildReport(logger.Logs);
            var result = builder.GetResult();
            try
            {
                File.WriteAllText(log_path, result);
            }
            catch (Exception e)
            {
                logger.LogError("Can't form log report file", e);
            }
        }
    }
}
