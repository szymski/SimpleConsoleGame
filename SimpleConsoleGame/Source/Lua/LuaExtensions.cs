using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua.Exceptions;

namespace SimpleConsoleGame.Source.Lua
{
    static class LuaExtensions
    {
        public static T PopType<T>(this NLua.Lua Lua)
        {
            var obj = Lua.Pop(); 
            if (!(obj is T))
            throw new LuaScriptException($"Invalid parameter, {typeof(T).Name} expected, got {obj?.GetType().Name ?? "nil"}.", "script");

            return (T)obj;
        }
    }
}
