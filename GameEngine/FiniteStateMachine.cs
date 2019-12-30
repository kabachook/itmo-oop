using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public class FiniteStateMachine
    {
        public MenuState CurrentState { get; private set; }
        private readonly Dictionary<(MenuState, ICommand), MenuState> transitions;
        private readonly HashSet<ICommand> commands;

        public FiniteStateMachine()
        {
            CurrentState = MenuState.Start;
            transitions = new Dictionary<(MenuState, ICommand), MenuState>();
            commands = new HashSet<ICommand>();
        }

        public MenuState GetNext(ICommand cmd)
        {
            MenuState nextState;
            if (!transitions.TryGetValue((CurrentState, cmd), out nextState))
            {
                throw new ArgumentException($"Invalid transition: {CurrentState} -> {cmd}");
            }
            return nextState;
        }

        public MenuState MoveNext(ICommand cmd)
        {
            CurrentState = GetNext(cmd);
            return CurrentState;
        }

        public void AddTransition(MenuState from, MenuState to, ICommand cmd)
        {
            if (cmd == null) throw new ArgumentNullException("cmd can't be null");
            transitions.Add((from, cmd), to);
            commands.Add(cmd);
        }

        public void AddTransitions(ICommand cmd, IEnumerable<(MenuState, MenuState)> transitions)
        {
            foreach (var transition in transitions)
            {
                AddTransition(transition.Item1, transition.Item2, cmd);
            }
        }

        public bool CommandExists(ICommand cmd)
        {
            return commands.Contains(cmd);
        }
    }
}
