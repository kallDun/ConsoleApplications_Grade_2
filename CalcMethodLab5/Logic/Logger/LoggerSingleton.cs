namespace CalcMethodLab5.Logic
{
    static class LoggerSingleton
    {
        private static LoggerManager logger;

        public static LoggerManager GetLogger()
        {
            if (logger == null)
            {
                logger = new LoggerManager();
            }
            return logger;
        }
    }
}