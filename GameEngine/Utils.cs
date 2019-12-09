using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    class Utils
    {
        public static int CalculateDamage(BattleUnitStack first, BattleUnitStack second) {
            var random = new Random();
            var result = first.Count * random.Next(first.UnitType.Damage.Item1, first.UnitType.Damage.Item2);
            if (first.Attack > second.Defense)
            {
                return (int)Math.Round(result * (1 + 0.05*(first.Attack - second.Defense)));
            } else
            {
                return (int)Math.Round(result * (1 + 0.05 * (second.Defense - first.Attack)));
            }
        }
    }
}
