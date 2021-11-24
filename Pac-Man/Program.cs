using System;
using System.IO;
using MiniGui;

namespace Pac_Man
{
    class Program
    {
        static void Main(string[] args)
        {
            //If OS is Windows set console window size
            if(OperatingSystem.IsWindows()) Console.SetWindowSize(50, 30);
            
            Menu start = new();
            start.Configure(new string[] { "Play","Scoreboard","Exit"});

            int choice;
            do
            {
                choice = start.Open();
                switch (choice)
                {
                    case 0:
                        Game game = new();
                        int speed = game.Configure();
                        if (speed == 0) break;
                        game.Play(speed);
                        break;

                    case 1:
                        Menu.Heading("Scoreboard");
                        Menu.CentercursorX(20);
                        Menu.CentercursorY();
                        Menu.CursorUp(5);
                        foreach (string line in File.ReadLines("../../../scoreboard/scoreboard.txt"))
                        {
                            Menu.WriteCenterOneLineLower(line);
                        }
                        Console.ReadKey();
                        break;

                    case 2:
                        Menu.Heading("SEE YOU NEXT TIME", Console.WindowHeight / 2);
                        System.Threading.Thread.Sleep(500);
                        break;
                }

            } while (choice != -1 && choice !=2);
        }
    }
}