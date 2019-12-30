using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public class BattleQueue
    {
        private readonly Queue<BattleUnitStack> firstQueue;
        private readonly Stack<BattleUnitStack> awaitQueue;

        public BattleQueue(IEnumerable<BattleUnitStack> stacks)
        {
            firstQueue = new Queue<BattleUnitStack>(
                stacks.Where(x => x.isAlive)
                    .OrderByDescending(x => x.UnitType.Initiative)
            );
            awaitQueue = new Stack<BattleUnitStack>();
        }

        public BattleUnitStack Next()
        {
            if (firstQueue.Count != 0)
                return firstQueue.Dequeue();
            if (awaitQueue.Count == 0) throw new Exception("Nothing in stacks!");
            var leastStack = awaitQueue.Pop();
            return leastStack;
        }

        public void Await(BattleUnitStack stack) {
            if (firstQueue.Count == 0)
            {
                throw new Exception("Queue is empty");
            }
            if (!firstQueue.Contains(stack))
            {
                throw new Exception("Stack is not in the queue!");
            }
            if (!firstQueue.Peek().Equals(stack))
            {
                throw new Exception("Stack is not first");
            }
            var toAwait = firstQueue.Dequeue();
            awaitQueue.Push(toAwait);
        }
        public int Count() => firstQueue.Count + awaitQueue.Count;

        public BattleUnitStack First()
        {
            return firstQueue.Count > 0 ? firstQueue.First() : (awaitQueue.Count > 0 ? awaitQueue.First() : null);
        }

        public List<BattleUnitStack> ToList()
        {
            var queue = new List<BattleUnitStack>();

            foreach(var stack in firstQueue)
            {
                queue.Add(stack);
            }

            foreach (var stack in awaitQueue)
            {
                queue.Add(stack);
            }

            return queue;
        }
    }
}
