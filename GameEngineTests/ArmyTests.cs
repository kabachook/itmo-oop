using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GameEngine.Tests
{
    [TestClass()]
    public class ArmyTests
    {
        [TestMethod()]
        public void Army_ValidArgs_Creates()
        {
            var stacks = new List<UnitStack>
            {
                new UnitStack(new Unit("Test1", 100, 10, 5, (7, 13), 1.0), 100 ),
                new UnitStack(new Unit("Test2", 80, 15, 3, (10, 20), 0.8), 10)
            };
            var army = new Army(stacks);
            Assert.IsNotNull(stacks);
            Assert.IsInstanceOfType(army, typeof(Army));
        }

        [TestMethod()]
        public void Army_ValidArgs_Values()
        {
            var stacks = new List<UnitStack>
            {
                new UnitStack(new Unit("Test1", 100, 10, 5, (7, 13), 1.0), 100 ),
                new UnitStack(new Unit("Test2", 80, 15, 3, (10, 20), 0.8), 10)
            };
            var army = new Army(stacks);
            CollectionAssert.AreEqual(army.UnitStacks, stacks);
        }

        [TestMethod()]
        public void Army_NullStack_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Army(null));
        }

        [TestMethod()]
        public void Army_LargeStack_ThrowsException()
        {
            var stacks = new List<UnitStack>
            {
                new UnitStack(new Unit("Test1", 100, 10, 5, (7, 13), 1.0), 100 ),
                new UnitStack(new Unit("Test2", 80, 15, 3, (10, 20), 0.8), 10),
                new UnitStack(new Unit("Test3", 80, 15, 3, (10, 20), 0.8), 10),
                new UnitStack(new Unit("Test4", 80, 15, 3, (10, 20), 0.8), 10),
                new UnitStack(new Unit("Test5", 80, 15, 3, (10, 20), 0.8), 10),
                new UnitStack(new Unit("Test6", 80, 15, 3, (10, 20), 0.8), 10),
                new UnitStack(new Unit("Test7", 80, 15, 3, (10, 20), 0.8), 10)
            };
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Army(stacks));
            Assert.AreEqual(ex.ActualValue, stacks.Count);
        }
    }
}