using System;
using System.IO;
using System.Linq;

namespace Sonaris
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args.Any() && !string.IsNullOrWhiteSpace(args[0])
                ? args[0]
                : Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            Console.WriteLine();
            Console.WriteLine("   $$$$$$\\                                          $$\\           ");
            Console.WriteLine("  $$  __$$\\                                         \\__|          ");
            Console.WriteLine("  $$ /  \\__| $$$$$$\\  $$$$$$$\\   $$$$$$\\   $$$$$$\\  $$\\  $$$$$$$\\ ");
            Console.WriteLine("  \\$$$$$$\\  $$  __$$\\ $$  __$$\\  \\____$$\\ $$  __$$\\ $$ |$$  _____|");
            Console.WriteLine("   \\____$$\\ $$ /  $$ |$$ |  $$ | $$$$$$$ |$$ |  \\__|$$ |\\$$$$$$\\  ");
            Console.WriteLine("  $$\\   $$ |$$ |  $$ |$$ |  $$ |$$  __$$ |$$ |      $$ | \\____$$\\ ");
            Console.WriteLine("  \\$$$$$$  |\\$$$$$$  |$$ |  $$ |\\$$$$$$$ |$$ |      $$ |$$$$$$$  |");
            Console.WriteLine("   \\______/  \\______/ \\__|  \\__| \\_______|\\__|      \\__|\\_______/ ");
            Console.WriteLine();
            var startTime = DateTime.Now;
            Console.WriteLine($"  <> Sonaris started at: {startTime.ToString("F")}");

            if (Directory.Exists(path))
            {
                try
                {
                    WorkDirectoryAnnounce(path);
                }
                catch (Exception e)
                {
                    Fail(e.ToString());
                }
            } else if (File.Exists(path))
            {
                try
                {
                    // ReSharper disable once AssignNullToNotNullAttribute
                    var lines = File.ReadAllLines(path);
                    foreach (var line in lines)
                    {
                        WorkDirectoryAnnounce(line);
                    }
                }
                catch (Exception e)
                {
                    Fail(e.ToString());
                }
            }
            else
            {
                Fail("Invalid path.");
            }

            Console.WriteLine();
            var endTime = DateTime.Now;
            var diff = endTime - startTime;
            Console.WriteLine($"  <> Sonaris completed at: {endTime.ToString("F")}");
            Console.WriteLine($"  <> (Total time elapsed: {diff.TotalMinutes} minutes.)");
            Console.WriteLine();
        }

        private static void WorkDirectoryAnnounce(string dir)
        {
            Console.WriteLine();
            Console.WriteLine($"  <> Starting directory enumeration: {dir}");
            Console.WriteLine();
            WorkDirectory(dir);
        }

        private static long WorkDirectory(string dir)
        {
            long totalSize = 0;
            foreach (var file in Directory.GetFiles(dir).Select(x => new FileInfo(x)))
            {
                totalSize += file.Length;
                Console.WriteLine($"{file.FullName} | {file.Length} bytes | {file.CreationTime.ToString("u")} | {file.LastWriteTime.ToString("u")}");
            }
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var directory in Directory.GetDirectories(dir))
            {
                totalSize += WorkDirectory(directory);
            }
            Console.WriteLine($"{dir} | {totalSize} bytes total");
            return totalSize;
        }

        private static void Fail(string s)
        {
            Console.WriteLine("  <> Sonaris failed to complete.");
            Console.WriteLine($"  <> ERROR: {s}");
        }
    }
}
