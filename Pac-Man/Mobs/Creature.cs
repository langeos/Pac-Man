using System;
using MiniGui;

namespace Pac_Man.Mobs
{
    abstract public class Creature
    {
        private int lifes;
        protected bool is_alive;
        protected int x_axis, y_axis;
        protected string sign = " ";
        protected ConsoleColor color;


        /// <summary>
        /// Set default values to object fields
        /// </summary>
        public Creature()
        {
            is_alive = true;
            x_axis = y_axis = 1;
        }


        /// <summary>
        /// Set given values to object fields
        /// </summary>
        public Creature(int x, int y) : this()
        {
            int[] temp = { x, y };
            Position = temp;
        }


        /// <summary>
        /// Get return number of lifes
        /// </summary>
        public virtual int Lifes
        {
            get
            {
                return lifes;
            }

            protected set
            {
                lifes = value;
            }
        }


        /// <summary>
        /// Get returns 2 element int array {x_axis, y_axis}
        /// Set position by giving two element int array
        /// </summary>
        public virtual int[] Position
        {
            get
            {
                int[] position = new int[2];
                position[0] = x_axis;
                position[1] = y_axis;
                return position;
            }

            set
            {
                if(value[0] >= 0 && value[1] >= 0)
                {
                    x_axis = value[0];
                    y_axis = value[1];
                }
            }
        }

        public string Sign { get => sign; protected set => sign = value; }

        public static bool operator ==(Creature x, Creature y)
        {
            return (x.Position[0] == y.Position[0] && x.Position[1] == y.Position[1]);
        }

        public static bool operator !=(Creature x, Creature y)
        {
            return (x.Position[0] != y.Position[0] || x.Position[1] != y.Position[1]);
        }


        /// <summary>
        /// Move creature to given direction
        /// </summary>
        /// <param name="direction"></param>
        virtual public void Move(int direction)
        {
            switch (direction)
            {
                //move up
                case 0:
                    if (y_axis > 0)
                    {
                        BetterCursor.Clear_Current_Area(x_axis, y_axis);
                        y_axis -= 1;
                        Draw(Sign);
                    }
                    break;

                //move down
                case 1:
                    if (y_axis < Console.WindowHeight - 1)
                    {

                        BetterCursor.Clear_Current_Area(x_axis, y_axis);
                        y_axis += 1;
                        Draw(Sign);
                    }
                    break;

                //move right
                case 2:
                    if (x_axis < Console.WindowWidth - 2)
                    {
                        BetterCursor.Clear_Current_Area(x_axis, y_axis);
                        x_axis += 1;
                        Draw(Sign);
                    }
                    break;

                //move left
                case 3:
                    if (x_axis > 0)
                    {
                        BetterCursor.Clear_Current_Area(x_axis, y_axis);
                        x_axis -= 1;
                        Draw(Sign);
                    }
                    break;

                default:
                    break;

            }
        }


        abstract protected void Animate(string nextsign);
        abstract protected void Animate();


        /// <summary>
        /// Drawing specific sign for object in cursor position
        /// </summary>
        /// <param name="sign"></param>
        virtual public void Draw(string sign, string nextsign)
        {
            PrepareToDraw(sign);
            Animate(nextsign);
        }


        /// <summary>
        /// Drawing specific sign for object in cursor position
        /// </summary>
        /// <param name="sign"></param>
        virtual public void Draw(string sign)
        {
            PrepareToDraw(sign);
            Animate();
        }


        //Prepare color, cursor position and draw sign
        private void PrepareToDraw(string sign)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x_axis, y_axis);
            Console.Write(sign);
            Console.ResetColor();
            if (OperatingSystem.IsMacOS()) BetterCursor.HideCursor();
        }


    }
}
