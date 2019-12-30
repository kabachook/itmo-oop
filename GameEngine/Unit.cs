using System;
using System.Collections.Generic;
using System.Collections.Immutable;

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

        public ImmutableList<ISpell> Spells = ImmutableList.Create<ISpell>();
        public ImmutableList<IPassiveEffect> PassiveEffects = ImmutableList.Create<IPassiveEffect>();

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

        public override string ToString()
        {
            return $"{Type}<HP: {HitPoints}, Attack: {Attack}, Defense: {Defense}, Damage: {Damage}, initiative: {Initiative}>";
        }
    }
}