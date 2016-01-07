using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLua;
using SimpleConsoleGame.Source.Entities;
using SimpleConsoleGame.Source.Lua;

namespace SimpleConsoleGame.Source
{
    class Game
    {
        // ConsoleBuffer is a class I made to optimize console drawing. It's a buffer that holds everything and draws only the changes.
        ConsoleBuffer mapBuffer = new ConsoleBuffer(32, 32); // Used for drawing the map
        ConsoleBuffer debugBuffer = new ConsoleBuffer(32, 20); // Used for drawing some info, like FPS

        int FPS = 0;

        public void Start()
        {
            Instance = this; // Set the instance, so we can access the class everywhere

            Map map = new Map(File.ReadAllText("data/maps/level1.txt")); // Load the map from a file

            Console.CursorVisible = false;
            StartKeyListening(); // Starts a new thread for getting console input as Console.ReadKey is synchronic

            // Used for FPS counting
            Stopwatch watch = Stopwatch.StartNew();
            int ticks = 0;

            // Main game loop
            while (true)
            {
                map.Update();
                map.Draw();
                pressedKeys.Clear();

                // FPS counting
                if (watch.ElapsedMilliseconds >= 1000)
                {
                    FPS = ticks;
                    ticks = 0;
                    watch.Restart();
                }
                ticks++;

                mapBuffer.DrawToScreen(0, 0); // Show the buffer

                // Setup debug buffer
                debugBuffer.ForegroundColor = ConsoleColor.White;
                debugBuffer.BackgroundColor = ConsoleColor.DarkBlue;
                debugBuffer.Clear();
                debugBuffer.DrawString(1, 1, "Console Game Test :D");
                debugBuffer.DrawString(1, 2, $"FPS: {FPS.ToString("D3")}");
                debugBuffer.DrawString(1, 4, $"Health: {Player.Instance.health}%");
                debugBuffer.DrawString(1, 5, $"Gold: {Player.Instance.gold}");

                debugBuffer.DrawToScreen(40, 0); // Show the buffer

                Thread.Sleep(10); // Sleep for 10ms, so it doesn't it 100% of CPU
            }
        }

        List<ConsoleKey> pressedKeys = new List<ConsoleKey>(); // A list with currently pressed keys
        Thread keyListener; // Thread object of key listener

        void StartKeyListening()
        {
            // Prepare a new thread
            keyListener = new Thread(() =>
            {
                while (true)
                    pressedKeys.Add(Console.ReadKey(true).Key); // Waits for a key to be pressed and adds it to the pressedKeys list
            });

            keyListener.Start(); // Start the thread
        }

        // Static methods for fast access to useful variables
        public static ConsoleBuffer MapBuffer => Instance.mapBuffer;
        public static List<ConsoleKey> PressedKeys => Instance.pressedKeys;

        public static Game Instance { get; private set; }
    }
}
