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
            Count = count;
        }

        public UnitStack Clone()
        {
            return new UnitStack(Type, Count);
        }
    }
}