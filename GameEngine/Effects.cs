using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    class Effects
    {
        public class DefenseBuff : Effect
        {
            private int _duration = 1;
            public override void Apply()
            {
                Stack.Defense = (int)Math.Round(1.3 * Stack.Defense);
            }
            public override void Revert()
            {
                Stack.Defense = (int)Math.Round(Stack.Defense * (1 / 1.3));
            }
            public DefenseBuff(BattleUnitStack stack) : base(stack)
            {
            }
        }

        public class PunishingStrikeBuff : Effect
        {
            private int _duration = 1;
            public override void Apply()
            {
                Stack.Attack += 12;
            }
            public override void Revert()
            {
                Stack.Attack -= 12;
            }
            public PunishingStrikeBuff(BattleUnitStack stack) : base(stack)
            {
            }
        }

        public class PassiveRange : Effect
        {
            private int _duration = 1;
            public override void Apply()
            {
            }
            public override void Revert()
            {
            }
            public PassiveRange(BattleUnitStack stack) : base(stack)
            {
            }
        }
    }
}
