using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleGame.Source.Entities
{
    class Player : Entity
    {
        public override char Character => 'X';
        public override ConsoleColor Color => ConsoleColor.Red;

        public int health = 100;
        public long gold = 0;

        public Player()
        {
            Instance = this;
        }

        public override void Update()
        {
            if (Game.PressedKeys.Contains(ConsoleKey.D) || Game.PressedKeys.Contains(ConsoleKey.RightArrow))
                Move(1, 0);
            else if (Game.PressedKeys.Contains(ConsoleKey.A) || Game.PressedKeys.Contains(ConsoleKey.LeftArrow))
                Move(-1, 0);

            if (Game.PressedKeys.Contains(ConsoleKey.W) || Game.PressedKeys.Contains(ConsoleKey.UpArrow))
                Move(0, -1);
            else if (Game.PressedKeys.Contains(ConsoleKey.S) || Game.PressedKeys.Contains(ConsoleKey.DownArrow))
                Move(0, 1);

            if (Game.PressedKeys.Contains(ConsoleKey.Spacebar))
                new Bullet(posX, posY, 1, 0).Spawn();

        }

        public static Player Instance { get; private set; }
    }
}
