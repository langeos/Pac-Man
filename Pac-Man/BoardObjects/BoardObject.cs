using System;

namespace Pac_Man.BoardObjects
{
    public abstract class BoardObject
    {
        protected int x_axis, y_axis;
        protected bool penetrable = true;
        protected string sign;
        protected ConsoleColor color = ConsoleColor.White;

        /// <summary>
        /// Constructor with no position given
        /// </summary>
        public BoardObject()
        {
            
        }


        /// <summary>
        /// Constructor with given position
        /// </summary>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        public BoardObject(int x, int y)
        {
            x_axis = x;
            y_axis = y;
        }


        /// <summary>
        /// Property to position to make it read only
        /// </summary>
        public int[] Position
        {
            get
            {
                int[] position = new int[2];
                position[0] = x_axis;
                position[1] = y_axis;
                return (position);
            }
        }


        /// <summary>
        /// Draw board object
        /// </summary>
        public void Draw()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x_axis, y_axis);
            Console.Write(sign);
            Console.ResetColor();
        }


        /// <summary>
        /// Penetrability
        /// </summary>
        /// <returns>Whether object is penetrable</returns>
        public bool IsPenetrable() {
            return penetrable;
        }
    }
}
