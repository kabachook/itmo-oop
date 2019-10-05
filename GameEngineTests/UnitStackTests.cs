using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameEngine.Tests
{
    [TestClass()]
    public class UnitStackTests
    {
        [TestMethod()]
        public void UnitStack_ValidArgs_Creates()
        {
            var unit = new Unit("Test", 100, 10, 5, (7, 13), 1.0);
            var stack = new UnitStack(unit, 100);
            Assert.IsNotNull(stack);
            Assert.IsInstanceOfType(stack, typeof(UnitStack));
        }

        [TestMethod()]
        public void UnitStack_ValidArgs_Values()
        {
            var unit = new Unit("Test", 100, 10, 5, (7, 13), 1.0);
            var stack = new UnitStack(unit, 100);
            Assert.AreEqual(stack.Type, unit);
            Assert.AreEqual(stack.Count, (uint)100);
        }

        [TestMethod()]
        public void UnitStack_NullType_ThrowsExcepion()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new UnitStack(null, 1));
        }
    }
}