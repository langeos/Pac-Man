using System;
using Pac_Man.BoardObjects;
using Pac_Man.Mobs;

namespace Pac_Man
{
    public class Board
    {
        private int speed;
        private double multipler;
        public BoardObject[] nextstep = new BoardObject[4];
        Map map;

        public int Speed { get => speed; private set => speed = value; }

        
        /// <summary>
        /// Set multipler to current game by given hard level
        /// </summary>
        /// <param name="speed"></param>
        public Board(int speed)
        {

            Speed = speed;
            switch (Speed)
            {
                case 400:
                    multipler = 0.8;
                    break;

                case 200:
                    multipler = 1.0;
                    break;

                case 100:
                    multipler = 1.4;
                    break;

                case 50:
                    multipler = 2.0;
                    break;
            }
            Console.Clear();
        }



        public bool Play(Player player)
        {
            Console.Clear();
            Console.CursorVisible = false;

            //Creating map
            map = new Map();

            //Create 4 ghosts and set them at the center of the map
            for (int i = 0; i<4; i++)
            {
                Ghost.ghosts[i] = new(i);
            }

            //Create Pac-Man
            PacMan pac_man = new();

            //Things happen on the board
            int dir;
            int prevdir = 2;
            ConsoleKeyInfo choice;
            do
            {
                choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.UpArrow:
                        dir = 0;
                        prevdir = DoNextMapEvent(dir, prevdir, pac_man, player);
                        break;


                    case ConsoleKey.DownArrow:
                        dir = 1;
                        prevdir = DoNextMapEvent(dir, prevdir, pac_man, player);
                        break;


                    case ConsoleKey.RightArrow:
                        dir = 2;
                        prevdir = DoNextMapEvent(dir, prevdir, pac_man, player);
                        break;


                    case ConsoleKey.LeftArrow:
                        dir = 3;
                        prevdir = DoNextMapEvent(dir, prevdir, pac_man, player);
                        break;

                    case ConsoleKey.Spacebar:
                        MiniGui.Menu.CentercursorY();
                        MiniGui.Menu.CentercursorX();
                        MiniGui.Menu.WriteCenterOneLineLower("PAUSE");
                        break;

                    default:
                        DoNextMapEvent(prevdir, prevdir, pac_man, player);
                        break;
                }

                if (Won()) break;
                if (pac_man.Lifes == 0) break;

            } while (choice.Key != ConsoleKey.Escape);

            return Won();
        }


        /// <summary>
        /// Set next step to any direction to specific area on the map
        /// </summary>
        /// <param name="nextstep">Type of object lyng on the creature next step</param>
        /// <param name="objects">Array of objects on the map</param>
        /// <param name="creature">Creature</param>
        private void UpdateNextstep(BoardObject[] nextstep, Creature creature)
        {
            nextstep[0] = map.objects[creature.Position[1] - 1, creature.Position[0]];
            nextstep[1] = map.objects[creature.Position[1] + 1, creature.Position[0]];
            nextstep[2] = map.objects[creature.Position[1], creature.Position[0] + 1];
            nextstep[3] = map.objects[creature.Position[1], creature.Position[0] - 1];
        }


        /// <summary>
        /// Get coordinates of object lying on the next creature step
        /// </summary>
        /// <param name="creature">Creature</param>
        /// <param name="dir">Actual directoin</param>
        /// <returns></returns>
        private int[] GetNextStep(Creature creature, int dir)
        {
            int[] result = new int[2];
            switch (dir)
            {
                case 0:
                    result[0] = creature.Position[1] - 1;
                    result[1] = creature.Position[0];
                    break;
                case 1:
                    result[0] = creature.Position[1] + 1;
                    result[1] = creature.Position[0];
                    break;
                case 2:
                    result[0] = creature.Position[1];
                    result[1] = creature.Position[0] + 1;
                    break;
                case 3:
                    result[0] = creature.Position[1];
                    result[1] = creature.Position[0] - 1;
                    break;
            }
            
            return result;
        }


        /// <summary>
        /// Main tick of the game, moving Pac-Man and Ghosts, checking if all orbs has been collected.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="prevdir"></param>
        /// <param name="pac_man"></param>
        /// <returns></returns>
        private int DoNextMapEvent(int dir, int prevdir,
            PacMan pac_man, Player player)
        {
            do
            {
                map.DrawWholeMap();
                //Check wether Pac-Man can go to designated direction, and move him. In that ifs there is also nested Game Tick
                UpdateNextstep(nextstep, pac_man);
                if (nextstep[dir].IsPenetrable())
                {

                    DoNextPacManSteps(dir, prevdir, pac_man, player);

                    DoNextGhostsSteps(player, pac_man);

                    System.Threading.Thread.Sleep(Speed);

                    prevdir = dir;

                }
                else if (nextstep[prevdir].IsPenetrable())
                {

                    DoNextPacManSteps(prevdir, prevdir, pac_man, player);

                    DoNextGhostsSteps(player, pac_man);

                    System.Threading.Thread.Sleep(Speed);

                    continue;
                }
                else
                {
                    pac_man.Draw(pac_man.Sign);

                    DoNextGhostsSteps(player, pac_man);

                    System.Threading.Thread.Sleep(Speed);
                }

                if (pac_man.Boost_time > 0) pac_man.Boost_time--;
                if (pac_man.Boost_time == 0 && pac_man.Boosted == true) pac_man.DeBoost();
                if (Won()) break;
                if (pac_man.Lifes == 0) break;

            } while (!Console.KeyAvailable);
            return prevdir;
        }


        /// <summary>
        /// Check what type of object there is in front of Pac-Man and adjust event
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="prevdir"></param>
        /// <param name="pac_man"></param>
        private void DoNextPacManSteps(int dir, int prevdir,
            PacMan pac_man, Player player)
        {
            if (nextstep[dir].GetType().ToString() == "Pac_Man.BoardObjects.Teleport")
            {
                Teleport.TpCreature(nextstep[dir], pac_man);
            }
            else if (nextstep[dir].GetType().ToString() == "Pac_Man.BoardObjects.Orb")
            {
                map.objects[GetNextStep(pac_man, dir)[0], GetNextStep(pac_man, dir)[1]] = new ClearSpace();
                player.AddPoints(Orb.Value*multipler);

            }
            else if (nextstep[prevdir].GetType().ToString() == "Pac_Man.BoardObjects.PowerOrb")
            {
                map.objects[GetNextStep(pac_man, prevdir)[0], GetNextStep(pac_man, prevdir)[1]] = new ClearSpace();
                player.AddPoints(PowerOrb.Value * multipler);
                pac_man.Boost();
            }

            pac_man.Move(dir);

            foreach (Ghost ghost in Ghost.ghosts)
            {
                if (pac_man == ghost && pac_man.Boosted == false)
                {
                    pac_man.DecrementLife();
                }
                else if (pac_man == ghost && pac_man.Boosted == true)
                {
                    pac_man.Killed_during_boost++;
                    player.AddPoints(200 * multipler * pac_man.Killed_during_boost);
                    ghost.Dead();
                }
            }

            Console.SetCursorPosition(0, 33);
            Console.WriteLine($"Score: {player.Points}");
            Console.Write($"Lifes: ");
            for (int i = 0; i < 3; i++)
            {
                if(i < pac_man.Lifes) Console.Write(" <3");
                else Console.Write("   ");

            }
        }


        private void DoNextGhostsSteps(Player player, PacMan pac_man)
        {
            foreach (Ghost ghost in Ghost.ghosts)
            {
                UpdateNextstep(nextstep, ghost);
                ghost.Move(nextstep);
                if (pac_man == ghost && pac_man.Boosted == false)
                {
                    pac_man.DecrementLife();
                }
                else if (pac_man == ghost && pac_man.Boosted == true)
                {
                    pac_man.Killed_during_boost++;
                    player.AddPoints(200 * multipler*pac_man.Killed_during_boost);
                    ghost.Dead();
                }
            }

            


            Console.SetCursorPosition(0, 33);
            Console.WriteLine($"Score: {player.Points}");
            Console.Write($"Lifes: ");
            for (int i = 0; i < pac_man.Lifes; i++) Console.Write(" <3");
        }

        /// <summary>
        /// Check if all orb has been collected
        /// </summary>
        /// <returns></returns>
        private bool Won()
        {
            int counter = 0;
            foreach (BoardObject obj in map.objects)
            {
                if(obj.GetType().ToString() == "Pac_Man.BoardObjects.Orb" || obj.GetType().ToString() == "Pac_Man.BoardObjects.PowerOrb")
                {
                    counter++;
                }
            }
            if (counter == 0) return true;
            else return false;
        }

    }
}
