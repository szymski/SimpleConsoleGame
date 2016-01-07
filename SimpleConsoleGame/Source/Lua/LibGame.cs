using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeraLua;

namespace SimpleConsoleGame.Source.Lua
{
    class LibGame
    {
        static NLua.Lua Lua => GameLua.Lua;

        static int GetVersion(LuaState state)
        {
            Lua.Push("0.1");
            return 1;
        }

        static int Add(LuaState state)
        {
            Lua.Push(Lua.PopType<double>() + Lua.PopType<double>());
            return 1;
        }

        public static void Init()
        {
            Lua.NewTable("game");

            Lua["game.GetVersion"] = new LuaNativeFunction(GetVersion);
            Lua["game.Add"] = new LuaNativeFunction(Add);
        }
    }
}
