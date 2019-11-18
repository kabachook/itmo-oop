using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    class BattleUnitStack
    {
        private UnitStack InitialStack { get; }

        public Unit UnitType { get; }

        public ulong Count { get; private set; }

        public ulong Health { get; private set; }

        public bool isAlive => Health > 0;

        public void Revive()
        {
            Count = InitialStack.Count;
            Health = InitialStack.Count * InitialStack.Type.HitPoints;
        }

        public void OnAttack(ulong damage)
        {
            try
            {
                Health = checked(Health - damage);
                Count = checked(Health / UnitType.HitPoints);
            }
            catch (OverflowException)
            {
                Health = 0;
                Count = 0;
            }
        }

        public void OnDefense()
        {

        }

        public BattleUnitStack(UnitStack unitStack)
        {
            InitialStack = unitStack.Clone();

            UnitType = unitStack.Type;
            Count = unitStack.Count;
            Health = unitStack.Count * unitStack.Type.HitPoints;
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
