using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public interface ICommand
    {
        public string Help { get; }
        public string Usage { get; }
        public string Invoke(List<string> args = default(List<string>));
    }
}
