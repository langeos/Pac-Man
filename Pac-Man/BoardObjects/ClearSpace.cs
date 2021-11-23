namespace Pac_Man.BoardObjects
{
    public class ClearSpace:BoardObject
    {
        /// <summary>
        /// Constructor with no position given
        /// </summary>
        public ClearSpace()
        {
            sign = " ";
            penetrable = true;
        }


        /// <summary>
        /// Constructor with given position
        /// </summary>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        public ClearSpace(int x, int y) : base(x, y)
        {
            sign = " ";
            Draw();
            penetrable = true;
        }
    }
}
