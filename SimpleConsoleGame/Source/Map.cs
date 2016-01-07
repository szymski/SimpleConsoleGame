using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleConsoleGame.Source.Entities;

namespace SimpleConsoleGame.Source
{
    class Map
    {
        public const int MAP_SIZE = 32;

        public char[,] blocks = new char[MAP_SIZE, MAP_SIZE];
        public List<Entity> entityList = new List<Entity>();

        public Map(string input)
        {
            var lines = input.Replace("\n", "").Split('\r');

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == 'X')
                    {
                        entityList.Add(new Player()
                        {
                            posX = j,
                            posY = i,
                            Map = this
                        });
                        blocks[j, i] = '.';
                        continue;
                    }
                    if (line[j] == '@')
                    {
                        entityList.Add(new Enemy()
                        {
                            posX = j,
                            posY = i,
                            Map = this
                        });
                        blocks[j, i] = '.';
                        continue;
                    }

                    blocks[j, i] = line[j];
                }
            }
        }

        public void Update()
        {
            foreach (var entity in entityList)
                entity.Update();
        }

        public bool IsWall(int x, int y)
        {
            return blocks[x, y] == '#';
        }

        public void Draw()
        {
            Game.MapBuffer.ForegroundColor = ConsoleColor.DarkGray;

            for (int y = 0; y < MAP_SIZE; y++)
            {
                for (int x = 0; x < MAP_SIZE; x++)
                {
                    if(blocks[x, y] == '.')
                        Game.MapBuffer.ForegroundColor = ConsoleColor.DarkGray;
                    else
                        Game.MapBuffer.ForegroundColor = ConsoleColor.DarkYellow;
                    Game.MapBuffer.DrawChar(x, y, blocks[x, y]);
                }
            }

            foreach (var entity in entityList)
            {
                Game.MapBuffer.ForegroundColor = entity.Color;
                Game.MapBuffer.DrawChar(entity.posX, entity.posY, entity.Character);
            }
        }
    }
}
