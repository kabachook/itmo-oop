using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public interface ISpell
    {
        public void Apply(BattleUnitStack stack) { }
    }
}
