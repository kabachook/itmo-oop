using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Linq;

namespace GameEngine
{
    public class UnitLoader
    {
        public static IEnumerable<Unit> LoadUnits()
        {
            if (Config.MODS_PATH is null)
            {
                throw new Exception("MODS_PATH is null, can't load units");
            }

            if (!Directory.Exists(Config.MODS_PATH))
            {
                throw new DirectoryNotFoundException($"Directory {Config.MODS_PATH} does not exists");
            }

            var units = new HashSet<Type>();

            foreach (var file in Directory.GetFiles(Config.MODS_PATH, "*.dll")){
                foreach(var type in Assembly.LoadFile(file).GetTypes())
                {
                    if (type.IsSubclassOf(typeof(Unit)))
                    {
                        units.Add(type);
                    }
                }
            }

            return units.ToList().ConvertAll(x => (Unit)Activator.CreateInstance(x));
        }
    }
}
