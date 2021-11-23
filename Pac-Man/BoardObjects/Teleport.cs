using MiniGui;

namespace Pac_Man.BoardObjects
{
    public class Teleport:BoardObject
    {
        /// <summary>
        /// Array containing two teleports to use it during teleporting
        /// </summary>
        static public Teleport[] teleports = new Teleport[2];

        /// <summary>
        /// Constructor with no position given
        /// </summary>
        public Teleport()
        {
            sign = " ";
            penetrable = true;
        }


        /// <summary>
        /// Constructor with given position
        /// </summary>
        /// <param name="x">X axis</param>
        /// <param name="y">Y axis</param>
        public Teleport(int x, int y) : base(x, y)
        {
            sign = " ";
            Draw();
            penetrable = true;
        }


        /// <summary>
        /// Teleport creature from one teleport to another one
        /// </summary>
        /// <param name="teleport"></param>
        /// <param name="creature"></param>
        static public void TpCreature(BoardObject teleport, Mobs.Creature creature)
        {
            Menu.Clear_Current_Area(teleport.Position[0], teleport.Position[1]);

            if (teleport == teleports[0])
            {
                int[] temp = { teleports[1].Position[0], teleports[1].Position[1] };
                creature.Position = temp;
            }
            else if(teleport == teleports[1])
            {
                int[] temp = { teleports[0].Position[0], teleports[0].Position[1] };
                creature.Position = temp;
            }
        }
    }
}
