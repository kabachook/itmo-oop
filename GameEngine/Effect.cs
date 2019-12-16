using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public class Effect : IEffect
    {
        protected BattleUnitStack Stack;
        private int _duration = 1;
        public int Duration { get { return _duration; } set { _duration = value; } }
        public bool Permanent { get { return false; } }
        public virtual void Apply()
        {
            throw new NotImplementedException();
        }
        public virtual void Revert()
        {
            throw new NotImplementedException();
        }
        public Effect(BattleUnitStack stack)
        {
            Stack = stack;
        }
    }
}
