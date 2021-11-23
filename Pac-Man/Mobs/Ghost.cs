using System;
using System.Collections.Generic;
using Pac_Man.BoardObjects;

namespace Pac_Man.Mobs
{
    public class Ghost:Creature
    {
        static public Ghost[] ghosts = new Ghost[4];
        private int previousstepfrom = 0;
        Dictionary<int, bool> AvailableDirrections = new Dictionary<int, bool>(4);

        public Ghost(int id):base(12 + id, 14 + id % 2)
        {
            color = ConsoleColor.Cyan;
            Lifes = -1;
            for(int i=0; i<4; i++) AvailableDirrections.Add(i, true);
            Sign = "Ä";
            Draw(Sign);
        }


        /// <summary>
        /// Get returns 2 element int array {x_axis, y_axis}
        /// </summary>
        public override int[] Position
        {
            get
            {
                int[] position = new int[2];
                position[0] = x_axis;
                position[1] = y_axis;
                return position;
            }
        }


        //Change current sign to the other one to animate
        protected override void Animate()
        {
            if (Sign != "Ä") Sign = "Ä";
            else if (Sign == "Ä") Sign = "Ÿ";
        }

        //Change current sign to the other one to animate
        protected override void Animate(string nextsign)
        {
            if (Sign != "Ä") Sign = "Ä";
            else if (Sign == "Ä") Sign = nextsign;
        }

        private void UpdateAvailableDirrections(BoardObject[] nextstep)
        {
            for(int i = 0; i < nextstep.Length; i++)
            {
                if(nextstep[i].IsPenetrable() == false && nextstep[i].GetType().ToString() != "Pac_Man.BoardObjects.GhostSpace")
                {
                    AvailableDirrections[i] = false;
                }
                else
                {
                    AvailableDirrections[i] = true;
                }
            }
        }

        public void Move(BoardObject[] nextstep)
        {
            UpdateAvailableDirrections(nextstep);

            int dir = RandomizeDirrection();

            if (nextstep[dir].GetType().ToString() == "Pac_Man.BoardObjects.Teleport")
            {
                Teleport.TpCreature(nextstep[dir], this);
            }

            Move(dir);

            switch(dir){
                case 0:
                    previousstepfrom = 1;
                    break;
                case 1:
                    previousstepfrom = 0;
                    break;
                case 2:
                    previousstepfrom = 3;
                    break;
                case 3:
                    previousstepfrom = 2;
                    break;
            }
        }


        private int RandomizeDirrection() {
            List<int> temp = new List<int>(0);
            var random = new Random();
            foreach (KeyValuePair<int, bool> element in AvailableDirrections)
            {
                if(element.Value == true && element.Key != previousstepfrom) {
                    temp.Add(element.Key);
                }
            }
            int index = random.Next(temp.Count);

            return temp[index];
        }


        protected override void Dead()
        {

        }

    }
}
