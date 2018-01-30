using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cleanup
{
    class Program
    {
        static void Main(string[] args)
        {
            CrawlDir(args.Length == 0 ? "." : args.First());
            Console.WriteLine("<========================= All done =======================>");
            Console.ReadLine();
        }

        static IEnumerable<string> dirsToKill = new List<string> { "bin", "debug", "release", "obj", "node_modules", "packages" };

        private static void CrawlDir(string path)
        {
            var dirs = Directory.EnumerateDirectories(path);
            foreach (var dir in dirs)
            {
                if (!Directory.Exists(dir)) continue;
                var info = new DirectoryInfo(dir);
                if (dirsToKill.Any(s => s == info.Name.ToLower()))
                {
                    Console.WriteLine("Removing {0}", info.FullName);
                    try
                    {
                        Directory.Delete(info.FullName, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Fehler bei {0} : {1}", info.FullName, ex.Message);
                    }
                    continue;
                }

                CrawlDir(info.FullName);
            }
        }
    }
}
