using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleConsoleGame.Source.Entities;

namespace SimpleConsoleGame.Source
{
    class Game
    {
        ConsoleBuffer consoleBuffer = new ConsoleBuffer(32, 32);
        ConsoleBuffer debugBuffer = new ConsoleBuffer(32, 20);
        int FPS = 0;

        public void Start()
        {
            Instance = this;

            Map map = new Map(File.ReadAllText("data/maps/level1.txt"));

            Console.CursorVisible = false;
            StartKeyListening();

            Stopwatch watch = Stopwatch.StartNew();
            int ticks = 0;

            while (true)
            {
                map.Update();
                map.Draw();
                pressedKeys.Clear();

                if (watch.ElapsedMilliseconds >= 1000)
                {
                    FPS = ticks;
                    ticks = 0;
                    watch.Restart();
                }
                ticks++;

                consoleBuffer.Blit(0, 0);

                debugBuffer.ForegroundColor = ConsoleColor.White;
                debugBuffer.BackgroundColor = ConsoleColor.DarkBlue;
                debugBuffer.Clear();
                debugBuffer.DrawString(1, 1, "Console Game Test :D");
                debugBuffer.DrawString(1, 2, $"FPS: {FPS.ToString("D3")}");
                debugBuffer.DrawString(1, 4, $"Health: {Player.Instance.health}%");
                debugBuffer.DrawString(1, 5, $"Gold: {Player.Instance.gold}");

                debugBuffer.Blit(40, 0);

                Thread.Sleep(10);
            }
        }

        List<ConsoleKey> pressedKeys = new List<ConsoleKey>();
        Thread keyListener;

        void StartKeyListening()
        {
            keyListener = new Thread(() =>
            {
                while (true)
                {
                    //Console.SetCursorPosition(0, 0);
                    pressedKeys.Add(Console.ReadKey(true).Key);
                }
            });

            keyListener.Start();
        }

        public static ConsoleBuffer ConsoleBuffer => Instance.consoleBuffer;
        public static List<ConsoleKey> PressedKeys => Instance.pressedKeys;

        public static Game Instance { get; private set; }
    }
}
