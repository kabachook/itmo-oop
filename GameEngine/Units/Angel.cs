namespace GameEngine.Units
{
    public class Angel : Unit
    {
        //public static string Type = "angel";

        //public static ulong HitPoints = 180;

        //public static ulong Attack = 27;

        //public static ulong Defense = 27;

        //public static (ulong, ulong) Damage = (45, 45);

        //public static double Initiative = 11;

        private static Angel instance;

        public Angel() : base("angel", 180, 27, 27, (45, 45), 11) { }

        public static Angel getInstance()
        {
            if (instance == null)
            {
                instance = new Angel();
            }
            return instance;
        }
    }
}
