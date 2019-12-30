using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public class Battle
    {
        private BattleArmy winner = null;
        private BattleState state;
        public BattleArmy First;
        public BattleArmy Second;
        public BattleArmy currentArmy;
        public BattleQueue Queue;
        public BattleUnitStack CurrentStack { get; private set; }
        public BattleArmy Enemy
        {
            get {
                if (currentArmy.Equals(First))
                {
                    return Second;
                }
                else
                {
                    return First;
                }
            }
            
        }

        public bool hasBattleEnded => !First.isAlive || !Second.isAlive || state == BattleState.FINISHED;

        public BattleArmy Winner
        {
            get
            {
                if (!hasBattleEnded)
                {
                    throw new Exception("Battle has not finished yet!");
                }
                return winner ?? (First.isAlive ? First : Second);
            }
        }

        public Battle(BattleArmy firstArmy, BattleArmy secondArmy)
        {
            First = firstArmy;
            Second = secondArmy;
            state = BattleState.STOPPED;
        }

        

        private void onMove()
        {
            if (currentArmy == null || currentArmy == Second)
            {
                currentArmy = First;
            }
            else
            {
                currentArmy = Second;
            }
            Queue = new BattleQueue(currentArmy.Stacks);
            CurrentStack = Queue?.First();
        }

        public void nextMove()
        {
            // Clear previous effects
            First.Stacks.ForEach(stack => stack.ClearEffects());
            Second.Stacks.ForEach(stack => stack.ClearEffects());
            onMove();
            // Apply new effects
            First.Stacks.ForEach(stack => stack.ApplyEffects());
            Second.Stacks.ForEach(stack => stack.ApplyEffects());
        }

        public BattleUnitStack nextStack()
        {
            CurrentStack = Queue.Next();
            return CurrentStack;
        }

        public void Await(BattleUnitStack stack)
        {
            Queue.Await(stack);
        }

        public void Escape()
        {
            state = BattleState.FINISHED;
            if (currentArmy.Equals(Second))
            {
                winner = First;
            }
            else
            {
                winner = Second;
            }
        }

        public void Attack(BattleUnitStack from, List<BattleUnitStack> to)
        {
            to.ForEach(stack =>
            {
                var dealt = from.OnAttack(stack);
                var inReturn = stack.OnDefense(from, dealt);
                from.DecreaseHealth(inReturn);
            });
        }

        public void Defense(BattleUnitStack stack)
        {
            stack.AddEffect(new Effects.DefenseBuff(stack));
        }

        public void UseSpell(BattleUnitStack from, List<BattleUnitStack> to, ISpell spell)
        {
            if (!from.GetSpells().Contains(spell)) throw new ArgumentOutOfRangeException("From stack does not contain provided spell");
            Console.WriteLine($"Using spell {spell}");
            to.ForEach(stack => spell.Apply(stack));
        }

        public void Start()
        {
            state = BattleState.RUNNING;
            onMove();
        }
    }
}
