using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Linq;

namespace GameEngine
{
    public class BattleUnitStack
    {
        private UnitStack InitialStack { get; }
        private List<IEffect> Effects;
        private int _attack;
        private int _defense;
        public Unit UnitType { get; }
        public int Count { get; private set; }
        public int Health { get; private set; }
        public int Attack
        {
            get { return _attack * Count; }
            set { _attack = value; }
        }
        public int Defense
        {
            get { return _defense * Count; }
            set { _defense = value; }
        }
        public bool isAlive => Health > 0;
        public double Initiative => Count * UnitType.Initiative;

        public void Revive()
        {
            Count = InitialStack.Count;
            Health = InitialStack.Count * InitialStack.Type.HitPoints;
        }

        public ImmutableList<ISpell> GetSpells()
        {
            return UnitType.Spells;
        }

        public void DecreaseHealth(int value)
        {
            Health = Health - value;
            if (Health < 0)
            {
                Health = 0;
            }
            Count = Health / UnitType.HitPoints;
        }

        public int OnAttack(BattleUnitStack to)
        {
            return Utils.CalculateDamage(this, to);
        }

        public int OnDefense(BattleUnitStack from, int received)
        {
            //var received = Utils.CalculateDamage(from, this);

            DecreaseHealth(received);

            // Calculate damage in return
            if (Count < 0) return 0;

            return Utils.CalculateDamage(this, from);
        }

        public void AddEffect(IEffect effect)
        {
            Effects.Add(effect);
        }

        public void ApplyEffects()
        {
            Effects.ForEach(x => { x.Apply(); x.Duration--; });
        }

        public void ClearEffects()
        {
            Effects.Where(x => x.Duration < 2).Reverse().ToList().ForEach(x => x.Revert());
        }

        public BattleUnitStack(UnitStack unitStack)
        {
            InitialStack = unitStack.Clone();

            UnitType = unitStack.Type;
            Count = unitStack.Count;
            Health = unitStack.Count * unitStack.Type.HitPoints;
            Effects = new List<IEffect>();
            _attack = unitStack.Type.Attack;
            _defense = unitStack.Type.Defense;

            // Initialize passive effects *once*
            UnitType.PassiveEffects.ForEach(effect => effect.Apply(this));
        }

        public BattleUnitStack Clone()
        {
            return new BattleUnitStack(new UnitStack(UnitType, Count));
        }

        public override string ToString()
        {
            return $"BattleUnitStack<Type: {UnitType.Type}, Count: {Count}>";
        }
    }
}
