using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameEngine
{
    class BattleUnitStack
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

        public void OnAttack()
        {

        }

        public void OnDefense(int damage)
        {
            Health = Health - damage;
            if (Health < 0)
            {
                Health = 0;
            }
            Count = Health / UnitType.HitPoints;
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
