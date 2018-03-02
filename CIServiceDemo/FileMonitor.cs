using System.IO;
using System.Management.Automation;
using CiSharedServices;

namespace CIServiceDemo
{
    internal class FileMonitor
    {
        public FileMonitor()
        {
            var watcher = new FileSystemWatcher
            {
                Path = Path.GetDirectoryName(CiTrigger.DemoTfsBuildDir),
                Filter = Path.GetFileName(CiTrigger.DemoTfsBuildFile)
            };
            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            using (var ps = PowerShell.Create())
            {
                ps.AddScript(CiTrigger.PowershellScript);
                ps.Invoke();
            }
        }
    }
}
