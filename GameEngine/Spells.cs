using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    class Spells
    {
        public class PunishingStrike : ISpell
        {
            public void Apply(BattleUnitStack stack)
            {
                stack.AddEffect(new Effects.PunishingStrikeBuff(stack));
            }
        }

    }
}
