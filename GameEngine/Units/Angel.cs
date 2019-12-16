using System;
using System.Collections.Immutable;

namespace GameEngine.Units
{
    public class Angel : Unit
    {
        public Angel() : base("angel", 180, 27, 27, (45, 45), 11)
        {
            Spells = ImmutableList.Create<ISpell>(new Spells.PunishingStrike());
        }

    }
}
