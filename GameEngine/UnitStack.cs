using System;

namespace GameEngine
{
    public class UnitStack
    {
        public Unit Type { get; }

        public ulong Count { get; }

        public UnitStack(Unit type, ulong count)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type), "Unit type should not be null");

            if (count > Config.MAX_STACK_UNITS)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, $"Unit count should not be bigger than {Config.MAX_STACK_UNITS}");
            }
            Count = count;
        }

        public UnitStack Clone()
        {
            return new UnitStack(Type, Count);
        }
    }
}