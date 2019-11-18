using GameEngine.Units;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            var angel = new Angel();
            var lich = new Lich();
            var shaman = new Shaman();

            var army1 = new Army(new List<UnitStack> { new UnitStack(angel, 20), new UnitStack(lich, 50), new UnitStack(shaman, 100) });
            var battleArmy1 = new BattleArmy(army1.UnitStacks.ConvertAll(s => new BattleUnitStack(s)));

            var army2 = new Army(new List<UnitStack> { new UnitStack(angel, 10), new UnitStack(lich, 100), new UnitStack(shaman, 20) });
            var battleArmy2 = new BattleArmy(army2.UnitStacks.ConvertAll(s => new BattleUnitStack(s)));

            var game = new Battle(battleArmy1, battleArmy2);

            game.Start();

            Console.WriteLine($"Winner is {game.Winner}");
        }
    }
}
