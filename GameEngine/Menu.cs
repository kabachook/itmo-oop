using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameEngine
{
    public enum MenuState
    {
        Start,
        FirstPlayerReady,
        SecondPlayerReady,
        FirstPlayerAttack,
        SecondPlayerAttack,
        Finished
    }

    public enum Player
    {
        FIRST,
        SECOND
    }

    public class TurnCounter
    {
        public int Turn { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public TurnCounter()
        {
            Turn = 1;
            CurrentPlayer = Player.FIRST;
        }

        public void NextTurn()
        {
            Turn += 1;
            NextPlayer();

        }

        public void NextPlayer()
        {
            if (CurrentPlayer == Player.FIRST) CurrentPlayer = Player.SECOND;
            else CurrentPlayer = Player.FIRST;
        }
    }

    public class Menu
    {
        private BattleArmy ArmyFirst;
        private BattleArmy ArmySecond;
        private List<Unit> Units;
        private Battle game;
        private TurnCounter counter;

        private FiniteStateMachine fsm;
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

        public static string Prompt = ">";
        public static string MenuPrefix = "[+]";
        public static string MenuPrefixError = "[x]";
        public static string MenuPrefixVerbose = "[v]";

        public static Dictionary<Player, string> PlayerPrompts = new Dictionary<Player, string> {
            { Player.FIRST, "p1"},
            { Player.SECOND, "p2"}
        };

        public Menu(IEnumerable<Unit> units)
        {
            ArmyFirst = new BattleArmy();
            ArmySecond = new BattleArmy();
            Units = units.ToList();

            game = new Battle(ArmyFirst, ArmySecond);
            counter = new TurnCounter();
            fsm = new FiniteStateMachine();

            // General commands
            AddCommand("help", new HelpCommand(commands));
            AddCommand("list", new ListUnitsCommand(Units));
            AddCommand("army", new ListArmyCommand(counter, ArmyFirst, ArmySecond));

            // Preparation commands
            AddStatefulCommand("add", new AddStackCommand(counter, Units, ArmyFirst, ArmySecond), new[] {
                (MenuState.Start, MenuState.Start),
                (MenuState.FirstPlayerReady, MenuState.FirstPlayerReady)
            });
            AddStatefulCommand("ready", new ReadyCommand(counter), new[] {
                (MenuState.Start, MenuState.FirstPlayerReady),
                (MenuState.FirstPlayerReady, MenuState.SecondPlayerReady)
            });

            // Battle commands
            AddCommand("queue", new QueueCommand(game));
            AddCommand("current", new CurrentStackCommand(game));
            AddCommand("enemy", new ListEnemyCommand(game));
            AddCommand("attack", new AttackCommand(game));
            AddCommand("defense", new DefenseCommand(game));
            AddCommand("await", new AwaitCommand(game));
            AddCommand("spell", new UseSpellCommand(game));

            // Internal transitions
            fsm.AddTransition(MenuState.SecondPlayerReady, MenuState.FirstPlayerAttack, StartCommand.GetInstance());
            fsm.AddTransitions(TurnCommand.GetInstance(), new[] {
                (MenuState.FirstPlayerAttack, MenuState.SecondPlayerAttack),
                (MenuState.SecondPlayerAttack, MenuState.FirstPlayerAttack)
            });
            fsm.AddTransitions(FinishCommand.GetInstance(), new[]
            {
                (MenuState.FirstPlayerAttack, MenuState.Finished),
                (MenuState.SecondPlayerAttack, MenuState.Finished)
            });

        }

        void AddCommand(string name, ICommand cmd)
        {
            commands.Add(name, cmd);
        }

        void AddStatefulCommand(string name, ICommand cmd, IEnumerable<(MenuState, MenuState)> transitions)
        {
            fsm.AddTransitions(cmd, transitions);
            AddCommand(name, cmd);
        }

        string InvokeCommand(ICommand cmd, List<string> args)
        {
            if (fsm.CommandExists(cmd))
            {
                fsm.MoveNext(cmd);
            }

            return cmd.Invoke(args);
        }


        void PrintPrompt(string value = "")
        {
            Console.Write($"{value}{Prompt} ");
        }

        void PrintPlayerPrompt()
        {
            PrintPrompt(PlayerPrompts[counter.CurrentPlayer]);
        }

        void Print(string value)
        {
            Console.WriteLine($"{MenuPrefix} {value}");
        }

        void PrintError(string value)
        {
            Console.WriteLine($"{MenuPrefixError} {value}");
        }

        void PrintVerbose(object value)
        {
            if (Config.VERBOSE) Console.WriteLine($"{MenuPrefixVerbose} {value.ToString()}");
        }

        string Input()
        {
            return Console.ReadLine().Trim();
        }

        void PrintHelp()
        {
            Print($"Available commands:\n{commands["help"].Invoke()}");
        }

        public void Start()
        {
            Print("Welcome to my new game!");
            PrintHelp();
            
            bool finished = false;
            while (!finished)
            {
                switch (fsm.CurrentState)
                {
                    case MenuState.SecondPlayerReady:
                        game.Start();
                        fsm.MoveNext(StartCommand.GetInstance());
                        Print("Game started!");
                        break;
                    case MenuState.FirstPlayerAttack:
                    case MenuState.SecondPlayerAttack:
                        // Automatically make turn if queue is empty
                        if (game.Queue.Count() == 0) {
                            counter.NextTurn();
                            fsm.MoveNext(TurnCommand.GetInstance());
                            game.nextMove();
                            Print("Next turn");
                        };
                        if (!game.currentArmy.isAlive)
                        {
                            counter.NextPlayer();
                            fsm.MoveNext(FinishCommand.GetInstance());
                            continue;
                        }
                        break;
                    case MenuState.Finished:
                        Print("Game finished!");
                        Print($"Winner is {counter.CurrentPlayer}");
                        break;
                }

                PrintVerbose($"State = {fsm.CurrentState}");

                try
                {
                    PrintPlayerPrompt();
                    var args = Input().Split(" ");
                    if (args.Length < 1 || !args[0].StartsWith('/')) continue;

                    var cmd = args[0].Substring(1);

                    if (commands.ContainsKey(cmd))
                    {
                        Print(InvokeCommand(commands[cmd], args.Skip(1).ToList()));
                    }
                    else
                    {
                        PrintError("Command does not exists");
                    }
                }
                catch (Exception e)
                {
                    PrintError(e.ToString());
                }

            }
        }
    }
}
