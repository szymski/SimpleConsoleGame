using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleGame.Source.Entities
{
    class Bullet : Entity
    {
        public override char Character => 'o';
        public override ConsoleColor Color => ConsoleColor.Magenta;

        int dirX = 0, dirY = 0;

        public Bullet(int posX, int posY, int dirX, int dirY)
        {
            this.posX = posX;
            this.posY = posY;
            this.dirX = dirX;
            this.dirY = dirY;
        }

        Stopwatch moveStopwatch = Stopwatch.StartNew();

        public override void Update()
        {
            if (moveStopwatch.ElapsedMilliseconds >= 50)
            {
                moveStopwatch.Restart();
                Move(dirX, dirY);
                if (Map.blocks[posX + dirX, posY + dirY] != '.') // Usuwa bullet jezeli nastepny blok jest inny niz kropka
                    Destroy();
            }
        }

        public override void OnCollide(Entity other)
        {
            if (other != null && other is Enemy)
            {
                other.Destroy();
            }
        }
    }
}
