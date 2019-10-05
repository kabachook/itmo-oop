using System;

namespace GameEngine
{
    public class Unit
    {
        public string Type { get; }

        public ulong HitPoints { get; }

        public ulong Attack { get; }

        public ulong Defense { get; }

        public (ulong, ulong) Damage { get; }

        public double Initiative { get; }

        public Unit(
            string type,
            ulong hitPoints,
            ulong attack,
            ulong defense,
            (ulong, ulong) damage,
            double initiative)
        {
            if (!(damage.Item1 <= attack && attack <= damage.Item2))
            {
                throw new ArgumentOutOfRangeException(nameof(attack), attack, $"Attack should be in range of damage [{damage.Item1}, {damage.Item2}]");
            };
            Type = type ?? throw new ArgumentNullException(nameof(type), "Unit type shoud not be null"); ;
            HitPoints = hitPoints;
            Attack = attack;
            Defense = defense;
            Damage = damage;
            Initiative = initiative;
        }
    }
}