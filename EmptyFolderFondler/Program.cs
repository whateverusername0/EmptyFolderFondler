using System;
using System.IO;
using System.Collections.Generic;

namespace EmptyFolderFondler
{
    internal class Program
    {
        private static string[] BigNonoFolders =
        {
            "$Recycle.Bin", "$RECYCLE.BIN",
            "C:\\Windows",
        };
        private static List<string> EmptyFolders = new List<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Begin empty folder fondling..");

            DriveInfo[] Drives = DriveInfo.GetDrives();
            Console.WriteLine($"Found {Drives.Length} drives,\n" +
                              $"Beginning traversing..");

            foreach (DriveInfo DI in Drives) Traverse(DI.RootDirectory);

            Console.WriteLine($"End traverse.\n" +
                              $"Found {EmptyFolders.Count} empty folders.\n" +
                              $"Purge ALL of em? [Y/N]");

            ConsoleKeyInfo Result = Console.ReadKey();
            if ((Result.KeyChar == 'Y') || (Result.KeyChar == 'y'))
            {
                Console.WriteLine("I'll now clean shit up.");
                foreach (string S in EmptyFolders) try { Directory.Delete(S); } catch { continue; }
                Console.WriteLine("Done. Press anything to fucking die.");
                Environment.Exit(0);
            }
            else if ((Result.KeyChar == 'N') || (Result.KeyChar == 'n'))
            {
                Console.WriteLine("I wont do anything. Press anything to fucking die.");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private static void Traverse(DirectoryInfo Dir)
        {
            FileInfo[] F = null;

            try { F = Dir.GetFiles(); }
            catch { Console.WriteLine($"Caught cringe exception at {Dir.FullName}"); }

            foreach (string BNNF in BigNonoFolders)
                if (F == null || Dir.Name == BNNF || Dir.FullName == BNNF)
                {
                    Console.WriteLine($"Big nono folder {Dir.FullName}, skipping.");
                    return;
                }

            if (F.Length == 0 && Dir.GetDirectories().Length == 0)
            {
                Console.WriteLine($"Found \"{Dir.FullName}\"");
                EmptyFolders.Add(Dir.FullName);
            }
            else if (Dir.GetDirectories().Length != 0)
                foreach (DirectoryInfo D in Dir.GetDirectories())
                    Traverse(D);
        }
    }
}
