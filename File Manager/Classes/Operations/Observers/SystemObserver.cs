using File_Manager.Classes.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_Manager.Classes.Operations.Observers
{
    class SystemObserver : IDisposable
    {
        private Logger logger;
        public SystemObserver()
        {
            logger = LoggerSingleton.GetInstance();
        }

        public delegate void FolderChangedEventHandler(string path);
        public event FolderChangedEventHandler OnFolderChanged;

        private Dictionary<string, FileSystemWatcher> Watchers = new();

        public void CallPathChangedEvent(string path) => OnFolderChanged.Invoke(path);

        public void AddFolder(string path)
        {
            if (Watchers.ContainsKey(path)) return;

            FileSystemWatcher watcher = new();
            watcher.Path = path;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Add event handlers.
            watcher.Changed += (s, e) => OnFolderChanged.Invoke(e.FullPath);
            watcher.Created += (s, e) => OnFolderChanged.Invoke(e.FullPath);
            watcher.Deleted += (s, e) => OnFolderChanged.Invoke(e.FullPath);
            watcher.Renamed += (s, e) => OnFolderChanged.Invoke(e.FullPath);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            Watchers.Add(path, watcher);
            logger.LogDebug($"Add watcher to {path}. Count is {Watchers.Count}");
        }
    
        public void RemoveFolder(string path)
        {
            if (!Watchers.ContainsKey(path)) return;

            var watcher = Watchers[path];
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();

            Watchers.Remove(path);
            logger.LogDebug($"Remove watcher from {path}. Count is {Watchers.Count}");
        }

        public void Dispose()
        {
            foreach (var item in Watchers)
            {
                item.Value.EnableRaisingEvents = false;
                item.Value.Dispose();
            }
            Watchers.Clear();
        }
    }
}
