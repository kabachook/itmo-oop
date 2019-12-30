using GameEngine.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Loading mods from {Environment.CurrentDirectory}");
            var unitsMod = UnitLoader.LoadUnits();
            Console.WriteLine($"Loaded {unitsMod.Count()} units: {unitsMod}");
            var devil = unitsMod.First();

            var angel = new Angel();
            var lich = new Lich();
            var shaman = new Shaman();
            var fury = new Fury();
            var cyclops = new Cyclops();

            var menu = new Menu(new List<Unit>() { devil, angel, lich, shaman, fury, cyclops });
            menu.Start();
        }
    }
}
