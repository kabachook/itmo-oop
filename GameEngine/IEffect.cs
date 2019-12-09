using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public interface IEffect
    {
        public int Duration { get; set; }
        public void Apply();
        public void Revert();
    }
}
