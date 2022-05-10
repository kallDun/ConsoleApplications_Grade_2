using CalcMethodLab5.Logic.Logger;
using System.Collections.ObjectModel;

namespace CalcMethodLab5.Logic
{
    class LoggerManager
    {
        public ObservableCollection<LogEntity> Logs { get; } = new ObservableCollection<LogEntity>();
        public void WriteLog(string message) => Logs.Add(new LogEntity(message));
    }
}