using System;
using System.IO;

namespace TelegrafConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Telegraf Installer for Windows Platform");
            Console.ReadLine();
            /*
            string confPath = @"C:\Program Files\Telegraf\telegraf.conf";
            if (args.Length == 0)
            {
                if (!File.Exists(confPath))
                {
                    using (var tw = new StreamWriter(confPath, true))
                    {
                        tw.WriteLine("  host = \"localhost\"");
                        tw.WriteLine("  port = 2878");
                    }
                } else
                {
                    return;
                }
            }
            */
        }
    }
}
