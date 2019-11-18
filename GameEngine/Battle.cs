using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    class Battle
    {
        private BattleArmy First;
        private BattleArmy Second;

        public bool hasBattleEnded => !First.isAlive || !Second.isAlive;

        public BattleArmy Winner
        {
            get
            {
                if (!hasBattleEnded)
                {
                    throw new Exception("Battle has not finished yet!");
                }
                return First.isAlive ? First : Second;
            }
        }

        public Battle(BattleArmy firstArmy, BattleArmy secondArmy)
        {
            First = firstArmy.Clone();
            Second = secondArmy.Clone();
        }

        public void Start()
        {
            Console.WriteLine("Battle in progress");

            // Mock
            Second.Stacks.ForEach(stack => stack.OnAttack(stack.Health));
            return;
            //while (!hasBattleEnded)
            //{

            //}
        }
    }
}
