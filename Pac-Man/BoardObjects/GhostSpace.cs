namespace Pac_Man.BoardObjects
{
    public class GhostSpace:BoardObject
    {
        /// <summary>
        /// Constructor with no position given
        /// </summary>
        public GhostSpace()
        {
            sign = " ";
            penetrable = false;
        }


        /// <summary>
        /// Constructor with given position
        /// </summary>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        public GhostSpace(int x, int y) : base(x, y)
        {
            sign = " ";
            Draw();
            penetrable = false;
        }
    }
}
