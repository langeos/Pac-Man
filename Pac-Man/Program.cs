using System;
using System.IO;
using MiniGui;

namespace Pac_Man
{
    class Program
    {
        static void Main()
        {
            
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
                        BetterCursor.CentercursorX(20);
                        BetterCursor.CentercursorY();
                        BetterCursor.CursorUp(5);
                        foreach (string line in File.ReadLines("../../../scoreboard/scoreboard.txt"))
                        {
                            BetterCursor.WriteCenterOneLineLower(line);
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