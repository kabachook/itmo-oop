using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public interface IPassiveEffect
    {
        public void Apply(BattleUnitStack stack);
    }
}
