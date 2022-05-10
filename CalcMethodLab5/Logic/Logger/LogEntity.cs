using System;

namespace CalcMethodLab5.Logic.Logger
{
    class LogEntity
    {
        public string Message { get; }
        public DateTime Time { get; }
        public LogEntity(string message)
        {
            Message = message;
            Time = DateTime.Now;
        }
    }
}