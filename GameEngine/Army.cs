using System;
using System.Collections.Generic;

namespace GameEngine
{
    internal class Army
    {
        private List<UnitStack> unitStacks;

        public List<UnitStack> UnitStacks
        {
            get => new List<UnitStack>(unitStacks);
        }

        public Army(List<UnitStack> unitStacks)
        {
            if (unitStacks is null)
            {
                throw new ArgumentNullException(nameof(unitStacks), "Unit stacks should not be null");
            }

            if (unitStacks.Count > 6)
            {
                throw new ArgumentOutOfRangeException(nameof(unitStacks), unitStacks.Count, "Unit stacks count should be less or equal to 6");
            }

            this.unitStacks = unitStacks;
        }
    }
}