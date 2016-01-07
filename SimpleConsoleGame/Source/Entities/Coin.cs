using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleGame.Source.Entities
{
    class Coin : Entity
    {
        public override char Character => 'C';
        public override ConsoleColor Color => ConsoleColor.Yellow;

        public override void OnCollide(Entity other)
        {
            if (other != null && other is Player)
            {
                ((Player) other).gold++;
                Destroy();
            }
        }
    }
}
