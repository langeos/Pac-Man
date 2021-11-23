using System;
using MiniGui;

namespace Pac_Man
{
    public class Player
    {
        private double points;
        private string nickname;

        /// <summary>
        /// Get value of points, or set it unless negative value is given.
        /// </summary>
        public double Points
        {
            get => points;
            private set { if (value! < 0) points = value; }
        }


        /// <summary>
        /// Read player nickname
        /// </summary>
        public string Nickname
        {
            get => nickname;
        }

        /// <summary>
        /// Constructor sets player points to 0, and open nickname-setting window.
        /// </summary>
        public Player()
        {
            Points = 0;
            SetNickname();
        }


        /// <summary>
        /// Add to points given value
        /// </summary>
        /// <param name="add"></param>
        public void AddPoints(double add)
        {
            points += add;
        }


        /// <summary>
        /// Substract given value from point. (Unless it is already 0)
        /// </summary>
        /// <param name="sub"></param>
        public void SubstractPoints(int sub)
        {
            if (Points - sub >= 0) Points -= sub;
            else Points = 0;
        }

       
        /// <summary>
        /// Open option to set player nickname by user.
        /// </summary>
        private void SetNickname()
        {
            do
            {
                Menu.Heading("Enter your nickname", Console.WindowHeight / 2 - 2);
                Console.SetCursorPosition(1, Console.WindowHeight);
                Console.Write("Max 30 characters");
                Menu.CentercursorX(43);
                Console.SetCursorPosition(Console.CursorLeft, Console.WindowHeight / 2);
                nickname = Console.ReadLine();
            } while (nickname.Length > 30 || nickname.Length <= 0);
        }
    }
}
