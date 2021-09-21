using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager.Classes.Operations.Observers
{
    class SystemObserver : IDisposable
    {
        public delegate void FolderChangedEventHandler(string path);
        public event FolderChangedEventHandler OnFolderChanged;

        private Dictionary<string, FileSystemWatcher> Watchers = new();

        int size;

        public void AddFolder(string path)
        {
            if (Watchers.ContainsKey(path)) return;

            FileSystemWatcher watcher = new();
            watcher.Path = path;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Add event handlers.
            watcher.Changed += (s, e) => OnFolderChanged.Invoke(e.FullPath);
            watcher.Created += (s, e) => OnFolderChanged.Invoke(e.FullPath);
            watcher.Deleted += (s, e) => OnFolderChanged.Invoke(e.FullPath);
            watcher.Renamed += (s, e) => OnFolderChanged.Invoke(e.FullPath);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            Watchers.Add(path, watcher);
            size = Watchers.Count();
        }
    
        public void RemoveFolder(string path)
        {
            if (!Watchers.ContainsKey(path)) return;

            var watcher = Watchers[path];
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();

            Watchers.Remove(path);
            size = Watchers.Count();
        }

        public void Dispose()
        {
            foreach (var item in Watchers)
            {
                item.Value.EnableRaisingEvents = false;
                item.Value.Dispose();
            }
        }
    }
}
