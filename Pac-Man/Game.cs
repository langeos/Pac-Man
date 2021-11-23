using System;
using MiniGui;

namespace Pac_Man
{
    public class Game
    {
        private int speed;

        public Game()
        {

        }

        /// <summary>
        /// Choosing difficulty level
        /// </summary>
        /// <returns>Speed of one game tick</returns>
        public int Configure()
        {
            Menu level = new();

            Menu.Heading("Choose level");
            level.Configure(new string[] { "Easy", "Normal", "Hard", "Nightmare" });
            int choice;
            choice = level.Open();
            switch (choice)
            {
                case 0:
                    speed = 400; //8
                    break;

                case 1:
                    speed = 200; //10
                    break;

                case 2:
                    speed = 100; //14
                    break;

                case 3:
                    speed = 50; //20
                    break;

                default:
                    speed = 0;
                    break;

            }
            return speed;
        }


        public void Play(int speed)
        {
            //Create new player
            Player player = new();


            //Show heading for beginning of the game
            Menu.Heading("LETS DESTROY SOME GHOSTS", Console.WindowHeight / 2);
            System.Threading.Thread.Sleep(1000);

            //If OS is Windows set console window size
            if (OperatingSystem.IsWindows()) Console.SetWindowSize(30, 35);

            //Create board object, play on it, and check the result of the game
            Board board = new(speed);
            bool won = board.Play(player);
            Ending(won, player);
        }


        private void Ending(bool won, Player player)
        {
            if (OperatingSystem.IsWindows()) { Console.SetWindowSize(50, 30); }

            if (won == true)
            {
                Menu.Heading("WINNER", Console.WindowHeight / 2);
                Menu.CentercursorY();
                Menu.WriteCenterOneLineLower($"Congratulations {player.Nickname}!");
            }
            else
            {
                Menu.Heading("LOOSER", Console.WindowHeight / 2);
                Menu.CentercursorY();
                Menu.WriteCenterOneLineLower($"Good luck next time {player.Nickname}!");
            }

            Menu.WriteCenterOneLineLower($"You got {player.Points} points");
            if (OperatingSystem.IsMacOS()) Menu.HideCursor();
            Console.ReadKey();

            Menu.Heading("LEAVING TO MAIN MENU", Console.WindowHeight / 2);
            System.Threading.Thread.Sleep(700);
        }
    }
}
