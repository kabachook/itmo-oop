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
            var angel = new Angel();
            var lich = new Lich();
            var shaman = new Shaman();
            var fury = new Fury();
            var cyclops = new Cyclops();

            var army1 = new Army(new List<UnitStack> { new UnitStack(angel, 20), new UnitStack(lich, 50), new UnitStack(shaman, 100) });
            var battleArmy1 = new BattleArmy(army1.UnitStacks.ConvertAll(s => new BattleUnitStack(s)));

            var army2 = new Army(new List<UnitStack> { new UnitStack(angel, 10), new UnitStack(lich, 100), new UnitStack(fury, 20), new UnitStack(cyclops,100) });
            var battleArmy2 = new BattleArmy(army2.UnitStacks.ConvertAll(s => new BattleUnitStack(s)));

            var game = new Battle(battleArmy1, battleArmy2);

            game.Start();

            game.nextMove();
            Console.WriteLine("Army 1 move");
            Console.WriteLine(game.currentArmy);
            Console.WriteLine();
            while (game.Queue.Count() > 0)
            {
                var stack = game.nextStack();
                Console.WriteLine($"Move: {stack}");
                Console.WriteLine($"Stack spells: [{String.Join(',',stack.GetSpells())}]");
                if (stack.GetSpells().Count > 0)
                {
                    var spell = stack.GetSpells().First();
                    game.UseSpell(stack, stack, spell);
                }
                game.Attack(stack, game.Enemy().Stacks);
                Console.WriteLine();
            }
            

            game.nextMove();
            Console.WriteLine("Army 2 move");
            Console.WriteLine(game.currentArmy);
            Console.WriteLine();
            while (game.Queue.Count() > 0)
            {
                var stack = game.nextStack();
                game.Attack(stack, game.Enemy().Stacks);
            }

            Console.WriteLine($"Winner is {game.Winner}");
        }
    }
}
