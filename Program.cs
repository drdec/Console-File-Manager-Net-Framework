using System;
using System.IO;

namespace Console_File_Manager
{
    internal class Program
    {
        private static string _path = Directory.GetCurrentDirectory();
        private const int NumOfPage = 10;

        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\tHi, welcome to my file manager ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();
            Console.Clear();

            while (true)
            {
                PrintInfo(_path);

                string command = Console.ReadLine();

                switch (ParseCommand(ref command))
                {
                    case "cd":
                        {
                            SwitchToDirectory(command);
                        }
                        break;

                    case "del":
                        {
                            DeleteCatalogOrFile(command);
                        }
                        break;

                    case "copy":
                        {

                        }
                        break;

                    case "properties":
                        {

                        }
                        break;

                    case "/":
                        {
                            BackToTheLastCatalog(_path);
                        }
                        break;

                    case "open":
                    {

                    }
                        break;

                    case "exit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Sorry, i don't know this command");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
        }

        private static string ParseCommand(ref string consoleInput)
        {
            consoleInput = consoleInput.Trim();
            string consoleCommand = "";

            for (int i = 0; i < consoleInput.Length; i++)
            {
                if (consoleInput[i] == ' ')
                {
                    consoleInput = consoleInput.Substring(consoleCommand.Length + 1);
                    return consoleCommand;
                }
                else
                {
                    consoleCommand += consoleInput[i];
                }
            }

            return consoleCommand;
        }

        private static void PrintInfo(string path)
        {
            Console.WriteLine("You are here");
            Console.WriteLine(_path);
            Console.WriteLine();

            string[] dirs = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

            foreach (var dir in dirs)
            {
                Console.WriteLine(dir);
            }

            PrintFiles(path);
        }

        private static void PrintFiles(string path)
        {
            var files = Directory.GetFiles(path, ".", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }
        }

        private static void SwitchToDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                _path = path;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, I don't know that folder.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
            }
        }

        private static void BackToTheLastCatalog(string path)
        {
            int cz = 0;

            for (int i = path.Length - 1; i >= 0; i--)
            {
                if (path[i] == '\\')
                {
                    _path = path.Remove(path.Length - cz, cz);
                    return;
                }
                else
                {
                    cz++;
                }
            }
        }

        private static void DeleteCatalogOrFile(string path)
        {
            try
            {
                Directory.Delete(path, true);
                Console.WriteLine();
                Console.WriteLine("Directory/file deleted");
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ReadKey();
        }
    }
}