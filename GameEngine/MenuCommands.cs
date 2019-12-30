using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameEngine
{
    public class HelpCommand : ICommand
    {
        private Dictionary<string, ICommand> commands;
        public string Help => "List available commands";
        public string Usage => "";

        public HelpCommand(Dictionary<string, ICommand> commands)
        {
            this.commands = commands;
        }

        public string Invoke(List<string> args = default(List<string>))
        {
            var commandsString = new StringBuilder();
            foreach (var (name, cmd) in commands)
            {
                commandsString.Append($"/{name} {cmd.Usage} — {cmd.Help}\n");
            }
            return commandsString.ToString();
        }
    }

    public class ListUnitsCommand : ICommand
    {
        private List<Unit> Units;
        public string Help => "List available units";
        public string Usage => "";

        public ListUnitsCommand(List<Unit> units)
        {
            Units = units;
        }

        public string Invoke(List<string> args = default(List<string>))
        {
            var unitsString = new StringBuilder();

            for (int i = 0; i < Units.Count; i++)
            {
                unitsString.Append($"{i + 1}. {Units[i].ToString()}\n");
            }
            return unitsString.ToString();
        }
    }

    public class AddStackCommand : ICommand
    {
        private readonly TurnCounter counter;
        private readonly List<Unit> units;
        private readonly BattleArmy armyFirst;
        private readonly BattleArmy armySecond;

        public string Help => "Add unit stack to army";
        public string Usage => "UNIT_NUM COUNT";

        public AddStackCommand(TurnCounter counter, List<Unit> units, BattleArmy armyFirst, BattleArmy armySecond)
        {
            this.counter = counter;
            this.units = units;
            this.armyFirst = armyFirst;
            this.armySecond = armySecond;
        }

        public string Invoke(List<string> args = null)
        {
            if (args.Count != 2)
            {
                throw new ArgumentException("Not all arguments provided");
            }

            int unitNum;
            int count;

            if (!int.TryParse(args[0], out unitNum))
            {
                throw new ArgumentOutOfRangeException($"Can't parse {args[0]}");
            }

            if (!int.TryParse(args[1], out count))
            {
                throw new ArgumentOutOfRangeException($"Can't parse {args[1]}");
            }

            if (counter.CurrentPlayer == Player.FIRST)
            {
                armyFirst.AddStack(new BattleUnitStack(new UnitStack(units[unitNum - 1], count)));
            }
            else
            {
                armySecond.AddStack(new BattleUnitStack(new UnitStack(units[unitNum - 1], count)));
            }

            return "Created!";
        }
    }

    public class ReadyCommand : ICommand
    {
        private readonly TurnCounter counter;
        public string Help => "Set ready status";
        public string Usage => "";

        public ReadyCommand(TurnCounter counter)
        {
            this.counter = counter;
        }

        public string Invoke(List<string> args = null)
        {
            counter.NextPlayer();
            return "";
        }
    }

    public class ListArmyCommand : ICommand
    {
        private readonly TurnCounter counter;
        private readonly BattleArmy armyFirst;
        private readonly BattleArmy armySecond;

        public string Help => "List stacks in the army";
        public string Usage => "";

        public ListArmyCommand(TurnCounter counter, BattleArmy armyFirst, BattleArmy armySecond)
        {
            this.counter = counter;
            this.armyFirst = armyFirst;
            this.armySecond = armySecond;
        }

        public string Invoke(List<string> args = null)
        {
            var army = counter.CurrentPlayer == Player.FIRST ? armyFirst : armySecond;

            if (army is null || army.Stacks.Count == 0) return "Empty!";

            var armyString = new StringBuilder();

            for (int i = 0; i < army.Stacks.Count; i++)
            {
                armyString.Append($"{i + 1}. {army.Stacks[i].ToString()}\n");
            }
            return armyString.ToString();
        }
    }

    public class QueueCommand : ICommand
    {
        private readonly Battle game;
        public string Help => "List turn queue";
        public string Usage => "";

        public QueueCommand(Battle game)
        {
            this.game = game;
        }

        public string Invoke(List<string> args = null)
        {
            var queueString = new StringBuilder();
            var queue = game.Queue?.ToList();
            
            if (queue == null || queue.Count == 0)
            {
                return "Empty!";
            }

            foreach (var stack in queue)
            {
                queueString.Append(stack.ToString());
                queueString.Append("\n");
            }

            return queueString.ToString();
        }
    }

    public class CurrentStackCommand : ICommand
    {
        private readonly Battle game;
        public string Help => "Show current stack";
        public string Usage => "";

        public CurrentStackCommand(Battle game)
        {
            this.game = game;
        }

        public string Invoke(List<string> args = null)
        {
            return game.CurrentStack.ToString();
        }
    }

    public class ListEnemyCommand : ICommand
    {
        private readonly Battle game;
        public string Help => "List enemy army";
        public string Usage => "";

        public ListEnemyCommand(Battle game)
        {
            this.game = game;
        }
        public string Invoke(List<string> args = null)
        {
            var armyString = new StringBuilder();
            var enemy = game.Enemy;

            for (int i = 0; i < enemy.Stacks.Count; i++)
            {
                armyString.Append($"{i + 1}. {enemy.Stacks[i].ToString()}\n");
            }
            return armyString.ToString();
        }
    }

    public class AttackCommand : ICommand
    {
        private readonly Battle game;
        public string Help => "Attack enemy stack(s)";
        public string Usage => "STACK_NUM [STACK_NUM]...";

        public AttackCommand(Battle game)
        {
            this.game = game;
        }
        public string Invoke(List<string> args = null)
        {
            var stacksNum = args.Select(s => int.TryParse(s, out int n) ? n : (int?)null)
                                .Where(n => n.HasValue)
                                .Select(n => n.Value)
                                .ToList();

            var victimStacks = new List<BattleUnitStack>();

            foreach(var num in stacksNum)
            {
                if (num < 1 || num > game.Enemy.Stacks.Count) throw new ArgumentOutOfRangeException($"No such stack {num}");
                victimStacks.Add(game.Enemy.Stacks[num - 1]);
            }

            game.Attack(game.CurrentStack, victimStacks);
            game.nextStack();
            return "Attacked";
        }
    }

    public class DefenseCommand : ICommand
    {
        private readonly Battle game;
        public string Help => "Use await";
        public string Usage => "";

        public DefenseCommand(Battle game)
        {
            this.game = game;
        }
        public string Invoke(List<string> args = null)
        {
            game.Defense(game.CurrentStack);
            game.nextStack();
            return "Shields up!";
        }
    }

    public class AwaitCommand : ICommand
    {
        private readonly Battle game;
        public string Help => "Use defense";
        public string Usage => "";

        public AwaitCommand(Battle game)
        {
            this.game = game;
        }
        public string Invoke(List<string> args = null)
        {
            game.Await(game.CurrentStack);
            game.nextStack();
            return "Awaited...";
        }
    }

    public class UseSpellCommand : ICommand
    {
        private readonly Battle game;
        public string Help => "Use spell";
        public string Usage => "SPELL_NUM STACK_NUM [STACK_NUM]...";

        public UseSpellCommand(Battle game)
        {
            this.game = game;
        }
        public string Invoke(List<string> args = null)
        {
            var argsNum = args.Select(s => int.TryParse(s, out int n) ? n : (int?)null)
                                .Where(n => n.HasValue)
                                .Select(n => n.Value)
                                .ToList();
            if (argsNum.Count < 2) throw new ArgumentException("Not enough arguments");

            var spellNum = argsNum[0];
            if (spellNum > game.CurrentStack.UnitType.Spells.Count) throw new ArgumentOutOfRangeException("Spell does not exist");

            var victimStacks = new List<BattleUnitStack>();

            foreach (var num in argsNum.Skip(1))
            {
                if (num < 1 || num > game.Enemy.Stacks.Count) throw new ArgumentOutOfRangeException($"No such stack {num}");
                victimStacks.Add(game.Enemy.Stacks[num - 1]);
            }

            game.UseSpell(game.CurrentStack, victimStacks, game.CurrentStack.UnitType.Spells[spellNum - 1]);
            game.nextStack();
            return "Casted";
        }
    }

    // Internal commands for state management

    public class InternalCommand : ICommand
    {
        public string Usage => "";
        public string Help => "";

        protected InternalCommand() { }

        public string Invoke(List<string> args = null)
        {
            return "";
        }
    }

    public class StartCommand : InternalCommand
    {
        private static StartCommand _instance;
        public string Help => "Starts battle";
        
        public static StartCommand GetInstance()
        {
            if (_instance == null)
            {
                _instance = new StartCommand();
            }
            return _instance as StartCommand;
        }
    }

    public class TurnCommand : InternalCommand
    {
        private static TurnCommand _instance;
        public string Help => "Makes turn";

        public static TurnCommand GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TurnCommand();
            }
            return _instance as TurnCommand;
        }
    }

    public class FinishCommand : InternalCommand
    {
        private static FinishCommand _instance;
        public string Help => "Finish game";

        public static FinishCommand GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FinishCommand();
            }
            return _instance as FinishCommand;
        }
    }
}
