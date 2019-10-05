using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GameEngine.Tests
{
    [TestClass()]
    public class UnitTests
    {
        [TestMethod()]
        public void Unit_ValidArgs_Creates()
        {
            var unit = new Unit("Test", 100, 10, 5, (7, 13), 1.0);
            Assert.IsNotNull(unit);
            Assert.IsInstanceOfType(unit, typeof(Unit));
        }

        [TestMethod()]
        public void Unit_ValidArgs_Values()
        {
            var unit = new Unit("Test", 100, 10, 5, (7, 13), 1.0);
            Assert.AreEqual(unit.Type, "Test");
            Assert.AreEqual(unit.HitPoints, (uint)100);
            Assert.AreEqual(unit.Attack, (uint)10);
            Assert.AreEqual(unit.Defense, (uint)5);
            Assert.AreEqual(unit.Damage, ((uint)7, (uint)13));
            Assert.AreEqual(unit.Initiative, 1.0);
        }

        [TestMethod()]
        public void Unit_NullType_ThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Unit(null, 100, 10, 5, (7, 13), 1.0));
        }

        [TestMethod()]
        public void Unit_AttackNotInRange_ThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Unit("Test", 100, 10, 5, (15, 20), 1.0));
        }
    }
}