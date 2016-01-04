using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleConsoleGame.Source;

namespace SimpleConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            new Game().Start();
            Console.WriteLine("Done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
