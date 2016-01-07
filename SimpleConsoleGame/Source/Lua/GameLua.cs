using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KeraLua;
using NLua;

namespace SimpleConsoleGame.Source.Lua
{
    class GameLua
    {
        public static NLua.Lua Lua { get; } = new NLua.Lua();

        public static void Init()
        {
            LibGame.Init();
        }
    }
}
