using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public abstract class Command : ICommand
    {
        public abstract string Help { get; }
        public abstract string Usage { get; }
        public abstract string Invoke(List<string> args = default(List<string>));
    }
}
