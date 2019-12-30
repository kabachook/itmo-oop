using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Army
    {
        private List<UnitStack> unitStacks;

        public List<UnitStack> UnitStacks
        {
            get => unitStacks.ConvertAll(p => p.Clone());
        }

        public Army(List<UnitStack> unitStacks)
        {
            if (unitStacks is null)
            {
                throw new ArgumentNullException(nameof(unitStacks), "Unit stacks should not be null");
            }

            if (unitStacks.Count > Config.MAX_ARMY_STACKS)
            {
                throw new ArgumentOutOfRangeException(nameof(unitStacks), unitStacks.Count, $"Unit stacks count should be less or equal to {Config.MAX_ARMY_STACKS}");
            }

            // Make a deep copy
            this.unitStacks = unitStacks.ConvertAll(p => p.Clone());
        }

        public BattleArmy ToBattleArmy()
        {
            return new BattleArmy(UnitStacks.ConvertAll(x => new BattleUnitStack(x)));
        }
    }
}