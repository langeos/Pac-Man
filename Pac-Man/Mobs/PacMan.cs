using System;
using System.Linq;
using MiniGui;

namespace Pac_Man.Mobs
{
    public class PacMan:Creature
    {
        private bool boosted;
        private int boost_time;
        private int killed_during_boost;

        //Signs of PacMan to any direction
        static public string sign_up = "V", sign_down = "\u15E3", sign_right = "\u15E7", sign_left =  "\u15E4";

        //Array with characters being used to animate current direction
        string[] directionalsigns = { sign_up, sign_down, sign_right, sign_left, "^", ">","<"};


        /// <summary>
        /// Put Pac-Man on the map and set his lifes to 3
        /// </summary>
        public PacMan():base(13,24)
        {
            color = ConsoleColor.Yellow;
            Lifes = 3;
            Sign = sign_right;
            Draw(Sign,"4");
        }


        /// <summary>
        /// Get returns 2 element int array {x_axis, y_axis}
        /// </summary>
        public override int[] Position
        {
            get
            {
                int[] position = new int[2];
                position[0] = x_axis;
                position[1] = y_axis;
                return position;
            }
        }

        public bool Boosted { get => boosted; private set => boosted = value; }
        public int Boost_time { get => boost_time; set => boost_time = value; }
        public int Killed_during_boost { get => killed_during_boost; set => killed_during_boost = value; }


        /// <summary>
        /// Change current sign to the other one to animate
        /// </summary>
        protected override void Animate()
        {
            if (directionalsigns.Any(Sign.Contains)) Sign = "O";
            else if (Sign == "O") Sign = sign_right;
        }


        /// <summary>
        /// Change current sign to the other one to animate
        /// </summary>
        protected override void Animate(string nextsign)
        {
            if (directionalsigns.Any(Sign.Contains)) Sign = "O";
            else if (Sign == "O") Sign = nextsign;
        }


        /// <summary>
        /// Move Pac-Man to given direction
        /// </summary>
        /// <param name="direction"></param>
        public override void Move(int direction)
        {
            switch (direction)
            {
                //move up
                case 0:
                    if (y_axis > 0)
                    {
                        Menu.Clear_Current_Area(x_axis, y_axis);
                        y_axis -= 1;
                        Draw(Sign, sign_up);
                    }
                    break;

                //move down
                case 1:
                    if (y_axis < Console.WindowHeight - 1)
                    {

                        Menu.Clear_Current_Area(x_axis, y_axis);
                        y_axis += 1;
                        Draw(Sign, sign_down);
                    }
                    break;

                //move right
                case 2:
                    if (x_axis < Console.WindowWidth - 2)
                    {
                        Menu.Clear_Current_Area(x_axis, y_axis);
                        x_axis += 1;
                        Draw(Sign, sign_right);
                    }
                    break;

                //move left
                case 3:
                    if (x_axis > 0)
                    {
                        Menu.Clear_Current_Area(x_axis, y_axis);
                        x_axis -= 1;
                        Draw(Sign, sign_left);
                    }
                    break;

                default:
                    break;
            }
            
        }

        public void DecrementLife()
        {
            Lifes -= 1;
            DeBoost();
            for (int k = 0; k < 5; k++)
            {
                Draw(" ");
                System.Threading.Thread.Sleep(150);
                Draw("O");
                System.Threading.Thread.Sleep(150);
            }
            x_axis = 13;
            y_axis = 24;
            Draw("O");
        }

        public void Boost()
        {
            Boosted = true;
            color = ConsoleColor.DarkMagenta;
            boost_time = 30;
        }

        public void DeBoost()
        {
            Boosted = false;
            color = ConsoleColor.Yellow;
            Killed_during_boost = 0;
        }


    }
}