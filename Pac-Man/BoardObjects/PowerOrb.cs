using System;

namespace Pac_Man.BoardObjects
{
    public class PowerOrb:BoardObject
    {
        static private int value = 50;

        /// <summary>
        /// Value of points for eat this item
        /// </summary>
        static public int Value { get => value; }

        /// <summary>
        /// Constructor with no position given
        /// </summary>
        public PowerOrb()
        {
            color = ConsoleColor.Green;
            if (OperatingSystem.IsMacOS()) sign = "⌾";
            else sign = "@";
            penetrable = true;
        }

        /// <summary>
        /// Constructor with given position
        /// </summary>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        public PowerOrb(int x, int y) : base(x, y)
        {
            color = ConsoleColor.Green;
            if (OperatingSystem.IsMacOS()) sign = "⌾";
            else sign = "@";
            penetrable = true;
            Draw();
        }
    }
}
