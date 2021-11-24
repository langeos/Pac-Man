using System;
using System.Collections.Generic;
using System.IO;
using MiniGui;

namespace Pac_Man
{
    public class Game
    {
        private int speed;

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

            if (OperatingSystem.IsWindows()){
                Console.SetWindowSize(Console.WindowWidth, 40);
            }

            //Create board object, play on it, and check the result of the game
            Board board = new(speed);
            bool won = board.Play(player);
            Ending(won, player);
        }

        /// <summary>
        /// Read current scoreboard from file, check if current player should be on the scoreboard. If yes, put him there.
        /// </summary>
        /// <param name="player"></param>
        private void SaveScoreboard(Player player)
        {
            List<KeyValuePair<string, double>> Top10 = new List<KeyValuePair<string, double>>();
            
            foreach (string line in File.ReadLines("../../../scoreboard/scoreboard.txt"))
            {
                Top10.Add(new KeyValuePair<string, double>(line.Split()[0], double.Parse(line.Split()[1])));
            }

            Top10.Add(new KeyValuePair<string, double>(player.Nickname, player.Points));

            Top10.Sort((x, y) => x.Value.CompareTo(y.Value));
            Top10.Reverse();

            File.WriteAllText("../../../scoreboard/scoreboard.txt", string.Empty);

            for(int i = 0; i < Top10.Count && i < 10; i++)
            {
                File.AppendAllText("../../../scoreboard/scoreboard.txt", $"{Top10[i].Key} {Top10[i].Value}\n");
            }

        }

        /// <summary>
        /// End game, save scoreboard, show if player is the winner or the looser and show points.
        /// </summary>
        private void Ending(bool won, Player player)
        {

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

            SaveScoreboard(player);

            Menu.WriteCenterOneLineLower($"You got {player.Points} points");
            if (OperatingSystem.IsMacOS()) Menu.HideCursor();
            Console.ReadKey();

            Menu.Heading("LEAVING TO MAIN MENU", Console.WindowHeight / 2);
            System.Threading.Thread.Sleep(700);


        }
    }
}
