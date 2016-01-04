using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleGame.Source.Entities
{
    class Enemy : Entity
    {
        public override char Character => '@';
        public override ConsoleColor Color => ConsoleColor.Cyan;

        public int health = 100;

        static Random rand = new Random();

        public override void Update()
        {
            int r = rand.Next(0, 50);

            if (r == 0)
                Move(1, 0);
            else if (r == 1)
                Move(-1, 0);
            else if (r == 2)
                Move(0, 1);
            else if (r == 3)
                Move(0, -1);
        }

        public override void OnCollide(Entity other)
        {
            if (other != null && other is Player)
            {
                ((Player) other).health--;
            }
        }
    }
}
