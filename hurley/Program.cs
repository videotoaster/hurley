using System;
using System.Collections.Generic;
using System.IO;

// Hurley Interpreter
// created by VideoToaster
// 7/10/22

namespace hurley
{
    class Program
    {

        // This function shows help text
        static void showhelp()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Commands");
            Console.ResetColor();
            Console.WriteLine("save - Write queue as script");
            Console.WriteLine("load - Load a script");
            Console.WriteLine("dump - Print queue");
            Console.WriteLine("nuke - Clear queue");
            Console.WriteLine("help - Display this help");
            Console.WriteLine("play - Play queued beeps");
            Console.WriteLine("exit - Quit the interpreter\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Queueing");
            Console.ResetColor();
            Console.WriteLine("Pass two numbers to queue a tone:");
            Console.WriteLine("500,2000 would queue a 500hz tone for 2000 ms");
        }

        // This function just plays tones and intervals
        static void play(List<int> tones, List<int> intervals)
        {
            if (tones.Count != intervals.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tones and intervals not of equal length?");
                Console.WriteLine("Immediately save, and clear. Internal error!");
                return;
            }
            else if (tones.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Not playing an empty queue, silly :P");
                return;
            }
            for (int i=0; i<tones.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Playing " + tones[i].ToString() + "hz for " + intervals[i].ToString() + "ms");
                Console.Beep(tones[i], intervals[i]);
            }
        }

        // This function works just like play, but instead of playing, it dumps
        static string dump(List<int> tones, List<int> intervals)
        {
            if (tones.Count != intervals.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Tones and intervals not of equal length?");
                return "";
            }
            else if (tones.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Not dumping an empty queue, silly :P");
                return "";
            }
            string dumpstr = "";
            for (int i = 0; i < tones.Count; i++)
            {
                dumpstr += tones[i].ToString() + "," + intervals[i].ToString() + "\n";
            }
            return dumpstr.Trim();
        }

        // This function will ask for a path from the user
        static string readpath()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("[file]");
            Console.ResetColor();
            Console.Write(" ");
            return Console.ReadLine();
        }

        // This function saves tones and intervals
        static bool savefile(List<int> tones, List<int> intervals)
        {
            string filepath = readpath();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("[file]");
            Console.ResetColor();
            Console.Write(" ");
            string sfilepath = Console.ReadLine();
            if (File.Exists(sfilepath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nA file by this name already exists.");
                return false;
            }
            try
            {
                StreamWriter saver = File.CreateText(sfilepath);
                saver.WriteLine(dump(tones, intervals));
                saver.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nSuccessfully saved file!");
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn error occurred while saving the file.");
                return false;
            }
        }

        // This function returns tones and intervals from a text doc
        static List<int>[] loadfile()
        {
            List<int> tones = new List<int>();
            List<int> intervals = new List<int>();
            string filepath = readpath();
            if (File.Exists(filepath))
            {
                try
                {
                    string[] instructions = File.ReadAllText(filepath).Trim().Split("\n");
                    foreach (string command in instructions)
                    {
                        string[] tint = command.Trim().Split(',');
                        int tone = int.Parse(tint[0].Trim());
                        int intv = int.Parse(tint[1].Trim());
                        if (tone >= 37 && tone < 65535 && intv > 0)
                        {
                            tones.Add(tone);
                            intervals.Add(intv);
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("[load]");
                            Console.ResetColor();
                            Console.WriteLine(" added " + tone.ToString() + " hz for " + intv.ToString() + "ms");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid tone/interval!");
                            Console.ResetColor();
                            Console.WriteLine(command);
                        }
                    }
                    return new List<int>[] {tones, intervals};
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error loading file.\nIt may be in an improper format or inaccessable.");
                    Console.WriteLine("File does not exist.");
                    return null;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File does not exist.");
                return null;
            }
        }

        static void Main(string[] args)
        {
            List<int> tones = new List<int>();
            List<int> intervals = new List<int>();
            Console.WriteLine("hurley v1.0");
            // extremely basic interpreter
            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n[int]");
                Console.ResetColor();
                Console.Write(" ");
                string cmd = Console.ReadLine();
                switch (cmd.ToLower())
                {
                    case "":
                        Console.CursorTop -= 2;
                        break;
                    case "load":
                        List<int>[] ti = loadfile();
                        if (ti != null)
                        {
                            tones = ti[0];
                            intervals = ti[1];
                        }
                        break;
                    case "help":
                        showhelp();
                        break;
                    case "nuke":
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write("Are you sure? (y/n)");
                        Console.CursorTop += 1;
                        Console.ResetColor();
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            Console.CursorLeft = 0;
                            tones = new List<int>();
                            intervals = new List<int>();
                            Console.Write("Cleared all tones and intervals.\n");
                        } else
                        {
                            Console.CursorLeft = 0;
                            Console.Write("We're NOT doing that today!\n");
                        }
                        break;
                    case "play":
                        play(tones, intervals);
                        break;
                    case "dump":
                        Console.WriteLine(dump(tones, intervals));
                        break;
                    case "exit":
                        Console.WriteLine("goodbye!");
                        Environment.Exit(0);
                        break;
                    case "save":
                        savefile(tones, intervals);
                        break;
                    default:
                        try
                        {
                            string[] tint = cmd.Split(',');
                            int tone = int.Parse(tint[0]);
                            int intv = int.Parse(tint[1]);
                            if (tone >= 37 && tone < 65535 && intv > 0)
                            {
                                tones.Add(tone);
                                intervals.Add(intv);
                                Console.WriteLine("+ " + tone.ToString() + " hz for "+intv.ToString()+"ms");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Tone must be above 37hz and below 65535hz, interval must be above 0ms");
                            }
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unknown command, type help for help");
                        }
                        break;
                }
            }
        }
    }
}
