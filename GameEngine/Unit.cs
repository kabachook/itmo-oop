using System;

namespace GameEngine
{
    public class Unit
    {
        public string Type { get; }

        public int HitPoints { get; }

        public int Attack { get; }

        public int Defense { get; }

        public (int, int) Damage { get; }

        public double Initiative { get; }

        public Unit(
            string type,
            int hitPoints,
            int attack,
            int defense,
            (int, int) damage,
            double initiative)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type), "Unit type shoud not be null");
            HitPoints = hitPoints;
            Attack = attack;
            Defense = defense;
            Damage = damage;
            Initiative = initiative;
        }
    }
}