using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleConsoleGame.Source
{
    class ConsoleBuffer
    {
        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.White;
        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;

        public int Width { get; }
        public int Height { get; }

        char[,] buffer;
        ConsoleColor[,] colorBuffer;
        ConsoleColor[,] bgColorBuffer;
        bool[,] changed;

        public ConsoleBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            buffer = new char[Width, Height];
            colorBuffer = new ConsoleColor[width, height];
            bgColorBuffer = new ConsoleColor[width, height];
            changed = new bool[Width, Height];
        }

        public void Clear()
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    if (buffer[x, y] != ' ' || colorBuffer[x, y] != ForegroundColor || bgColorBuffer[x, y] != BackgroundColor)
                    {
                        buffer[x, y] = ' ';
                        colorBuffer[x, y] = ForegroundColor;
                        bgColorBuffer[x, y] = BackgroundColor;
                        changed[x, y] = true;
                    }
                }
        }

        public void DrawChar(int x, int y, char c)
        {
            if (buffer[x, y] != c || colorBuffer[x, y] != ForegroundColor || bgColorBuffer[x, y] != BackgroundColor)
            {
                buffer[x, y] = c;
                colorBuffer[x, y] = ForegroundColor;
                bgColorBuffer[x, y] = BackgroundColor;
                changed[x, y] = true;
            }
        }

        public void DrawString(int x, int y, string str)
        {
            for (int i = 0; i < str.Length; i++)
                DrawChar(x + i, y, str[i]);
        }

        public void Blit(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);

            for (int y = 0; y < Height; y++)
            {
                int lastX = 0;
                for (int x = 0; x < Width; x++)
                {
                    if (changed[x, y])
                    {
                        if (lastX != x - 1)
                            Console.SetCursorPosition(posX + x, Console.CursorTop);
                        Console.ForegroundColor = colorBuffer[x, y];
                        Console.BackgroundColor = bgColorBuffer[x, y];
                        Console.Write(buffer[x, y]);
                        changed[x, y] = false;
                        lastX = x;
                    }
                }
                Console.SetCursorPosition(posX + 1, posY + y + 1);
            }
        }
    }
}
