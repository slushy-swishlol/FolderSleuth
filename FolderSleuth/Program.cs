using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FolderSleuth
{
    class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("Enter the designated directory to search: ");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                string dir = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Red;

                if (!Directory.Exists(dir))
                {
                    Console.WriteLine("\nThis directory does not exist.\n");
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("Enter the designated directory to copy the files to: ");

                Console.ForegroundColor = ConsoleColor.DarkGreen;

                string target = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Red;

                if (!Directory.Exists(target))
                {
                    Console.WriteLine("\nThis directory does not exist.\n");
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;

                Console.Write("Separate file types with commas and omit spaces.\n");
                Console.Write("Enter a file type to filter files if needed (Type 0 if not): ");

                Console.ForegroundColor = ConsoleColor.DarkGreen;

                string input = Console.ReadLine();
                List<string> filters = new List<string>();
                if (!input.Contains(','))
                {
                    filters.Add(input);
                }
                else
                {
                    filters = input.Split(',').ToList();
                }

                Console.WriteLine("Beginning operation...\n");
                Copy(dir, target, filters);

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine($"\nOperation completed. {count} files copied.\n");
            }
        }

        static void Copy(string dir, string target, List<string> filters)
        {
            if (Directory.GetDirectories(dir).Length > 0)
            {
                string[] dirs = Directory.GetDirectories(dir);
                foreach (string dir_ in dirs)
                {
                    Copy(dir_, target, filters);
                }
            }
            else
            { 
                string[] files = Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    string filetype = file.Split('.')[file.Split('.').Length - 1];
                    if (!filters.ElementAt<string>(0).Equals("0"))
                    {
                        bool flag = false;
                        foreach (string filter in filters)
                        {
                            if (filter == filetype)
                            {
                                flag = true;
                            }
                        }
                        if (flag != true) continue;
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"Copying {file}");

                    //th cjim
                    string filename = file.Split('\\')[file.Split('\\').Length - 1];
                    File.Copy(file, target + $"/{filename}", true); 
                    count++;
                }
            }
        }
    }
}
