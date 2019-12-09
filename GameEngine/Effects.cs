using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    class Effects
    {
        public class DefenseBuff : IEffect
        {
            private BattleUnitStack Stack;
            private int _duration;
            public int Duration { get { return _duration; } set { _duration = value; } }
            public void Apply()
            {
                Stack.Defense = (int)Math.Round(1.3 * Stack.Defense);
            }
            public void Revert()
            {
                Stack.Defense = (int)Math.Round(Stack.Defense * (1 / 1.3));
            }
            public DefenseBuff(BattleUnitStack stack)
            {
                Stack = stack;
            }
        }
    }
}
