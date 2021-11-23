namespace Pac_Man.BoardObjects
{
    public class Orb:BoardObject
    {
        static private int value = 10;

        /// <summary>
        /// Value of points for eat this item
        /// </summary>
        static public int Value { get => value;}


        /// <summary>
        /// Constructor with no position given
        /// </summary>
        public Orb()
        {
            color = System.ConsoleColor.Gray;
            sign = "•";
            penetrable = true;
        }


        /// <summary>
        /// Constructor with given position
        /// </summary>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        public Orb(int x, int y) : base(x, y)
        {
            color = System.ConsoleColor.Gray;
            sign = "•";
            penetrable = true;
            Draw();
        }
    }
}
