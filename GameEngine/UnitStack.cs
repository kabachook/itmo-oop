using System;

namespace GameEngine
{
    public class UnitStack
    {
        public Unit Type { get; }

        public int Count { get; }

        public UnitStack(Unit type, int count)
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