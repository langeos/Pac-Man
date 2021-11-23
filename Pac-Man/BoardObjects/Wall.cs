namespace Pac_Man.BoardObjects
{
    public class Wall:BoardObject
    {
        /// <summary>
        /// Constructor with no position given
        /// </summary>
        public Wall()
        {
            color = System.ConsoleColor.DarkGray;
            sign = "#";
            penetrable = false;
        }


        /// <summary>
        /// Constructor with given position
        /// </summary>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        public Wall(int x, int y):base(x,y)
        {
            color = System.ConsoleColor.DarkGray;
            sign = "#";
            Draw();
            penetrable = false;
        }
    }
}
