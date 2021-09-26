using System;
using System.Collections.Generic;
using Priority_Queue;
namespace BarelyBreakConsoleApp
{
    class Algorothm
    {
        private BarleyState _initState;
        private FastPriorityQueue<BarleyState> _priorityQueue;
        private Dictionary<BarleyState,int> _costSoFar;
        private Dictionary<BarleyState, BarleyState> _cameTo;
        public event Action<Dictionary<BarleyState, BarleyState>, BarleyState> Algorithmfinished;
        public Algorothm(BarleyState initState)
        {
            
            _initState = initState;
            initState.Priority = 0;
            
            _priorityQueue = new FastPriorityQueue<BarleyState>(1000000);
            _costSoFar = new Dictionary<BarleyState, int>(1000000);
            _cameTo = new Dictionary<BarleyState, BarleyState>();
            
            //_priorityQueue.Enqueue(initState);
            _priorityQueue.Enqueue(initState,0);
            _costSoFar[_initState] = 0;
        }

        public void StartAlgorithm()
        {
            while (_priorityQueue.Count != 0)
            {
                BarleyState current = _priorityQueue.Dequeue();

                if (current.Heuristic == 0)
                {
                    //Algorithmfinished(_cameTo,current);
                    break;
                }

                List<BarleyState> nextSteps = current.NextStep();
                foreach (BarleyState next in nextSteps)
                {
                    int newCost = _costSoFar[current] + 1;
                    //if (!_costSoFar.ContainsKey(next) || newCost < _costSoFar[next])
                    {
                        _costSoFar[next] = newCost;
                        next.Priority = newCost + next.Heuristic;
                        _priorityQueue.Enqueue(next,next.Priority);
                        //_cameTo[next] = current;
                    }
                }
                
            }
            
        }
    }
}
