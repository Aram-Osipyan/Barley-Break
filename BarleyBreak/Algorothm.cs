using System;
using System.Collections.Generic;
using Priority_Queue;
namespace BarelyBreak
{
    class Algorothm
    {
        private BarleyState _initState;
        private FastPriorityQueue<BarleyState> _priorityQueue;
        private Dictionary<long, int> _costSoFar;
        private Dictionary<long, long> _cameTo;
        public event Action<Dictionary<long, long>, long> Algorithmfinished;
        public Algorothm(BarleyState initState)
        {

            _initState = initState;
            initState.Priority = 0;

            _priorityQueue = new FastPriorityQueue<BarleyState>(100000000);
            _costSoFar = new Dictionary<long, int>(10000000);
            _cameTo = new Dictionary<long, long>();

            _priorityQueue.Enqueue(initState, 0);
            _costSoFar[_initState.HashCode] = 0;
        }

        public void StartAlgorithm()
        {
            while (_priorityQueue.Count != 0)
            {
                BarleyState current = _priorityQueue.Dequeue();

                if (current.Heuristic == 0)
                {
                    Algorithmfinished(_cameTo, current.HashCode);
                    break;
                }

                List<BarleyState> nextSteps = current.NextStep();
                foreach (BarleyState next in nextSteps)
                {
                    int newCost = _costSoFar[current.HashCode] + 1;
                    if (!_costSoFar.ContainsKey(next.HashCode) || newCost < _costSoFar[next.HashCode])
                    {
                        _costSoFar[next.HashCode] = newCost;
                        next.Priority = newCost + next.Heuristic;
                        _priorityQueue.Enqueue(next, next.Priority);
                        _cameTo[next.HashCode] = current.HashCode;
                    }
                }

            }

        }
    }
}
