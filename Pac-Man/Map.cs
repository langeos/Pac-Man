using System;
using System.IO;
using System.Threading;
using MiniGui; 
using Pac_Man.BoardObjects;

namespace Pac_Man
{
    public class Map
    {
        static private int mapwidth;
        static private int mapheight;
        public BoardObject[,] objects;

        public Map()
        {
            mapwidth = 28;
            mapheight = 32;
            objects = new BoardObject[mapheight, mapwidth];

            Console.Clear();
            Menu.CentercursorX(14);
            Menu.CentercursorY();
            Console.Write("Reading map");
            for(int i = 0; i < 5; i++)
            {
                Thread.Sleep(150);
                Console.Write(".");
                Thread.Sleep(150);
            }
            Console.Clear();
            ReadMap();
        }


        /// <summary>
        /// Draw again the map from objects array.
        /// </summary>
        /// <param name="objects"></param>
        public void DrawWholeMap()
        {
            foreach (BoardObject obj in objects)
            {
                obj.Draw();
            }
            Menu.HideCursor();
        }


        /// <summary>
        /// Read map from .txt file
        /// </summary>
        /// <returns>Array of objects with coordinates read from the map</returns>
        public BoardObject[,] ReadMap()
        {
            int counter = 0, tpcounter = 0;

            //Read from file every sign and changing it into specific objects
            foreach (string line in File.ReadLines("../../../map/map.txt"))
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < mapwidth; i++)
                {
                    switch (line[i])
                    {
                        case '#':
                            objects[counter, i] = new Wall(i, counter);
                            break;
                        case '.':
                            objects[counter, i] = new Orb(i, counter);
                            break;
                        case 'o':
                            objects[counter, i] = new PowerOrb(i, counter);
                            break;
                        case '@':
                            objects[counter, i] = Teleport.teleports[tpcounter] = new Teleport(i, counter);
                            tpcounter++;
                            break;
                        case '^':
                            objects[counter, i] = new GhostSpace(i, counter);
                            break;
                        case ' ':
                            objects[counter, i] = new ClearSpace(i, counter);
                            break;
                        default:
                            break;
                    }
                }
                counter++;
            }

            Menu.HideCursor();
            return objects;
        }
    }
}
