using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    class BattleArmy
    {
        private List<BattleUnitStack> InitialStacks { get; }

        private List<BattleUnitStack> stacks;

        public List<BattleUnitStack> Stacks
        {
            //get => stacks.ConvertAll(stack => stack.Clone());
            get => stacks;
        }

        public ulong AliveCount
        {
            get
            {
                return (ulong)stacks.Count(s => s.isAlive);
            }
        }

        public bool isAlive
        {
            get
            {
                return AliveCount > 0;
            }
        }

        public void AddStack(BattleUnitStack stack)
        {
            if (stacks.Count >= Config.MAX_BATTLE_ARMY_STACKS)
            {
                throw new ArgumentException($"Can't add stack. Stack count should be less or equal to {Config.MAX_BATTLE_ARMY_STACKS}");
            }
            stacks.Add(stack);
        }

        public void OnAttack()
        {

        }

        public void OnDefence()
        {

        }

        public BattleArmy(List<BattleUnitStack> stacks)
        {
            if (stacks.Count > Config.MAX_BATTLE_ARMY_STACKS)
            {
                throw new ArgumentException($"Can't create Battle Army. Stack count should be less or equal to {Config.MAX_BATTLE_ARMY_STACKS}");
            }

            this.stacks = stacks.ConvertAll(s => s.Clone());
        }

        public BattleArmy Clone()
        {
            return new BattleArmy(stacks);
        }

        public override string ToString()
        {
            return $"BattleArmy<Stacks: [{String.Join(',', Stacks)}]>";
        }
    }
}
