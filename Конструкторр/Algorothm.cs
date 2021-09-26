using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alg.PriorityQueue;

namespace Конструкторр
{
    class Algorothm
    {
        private BarleyState _initState;
        private PriorityQueue<BarleyState> _priorityQueue;
        private Dictionary<BarleyState,int> _costSoFar;
        private Dictionary<BarleyState, BarleyState> _cameTo;
        
        public event Action<Dictionary<BarleyState, BarleyState>> Algorithmfinished;
        public Algorothm(BarleyState initState)
        {
            _initState = initState;
            initState.Priority = 0;
            
            _priorityQueue = new PriorityQueue<BarleyState>();
            _costSoFar = new Dictionary<BarleyState, int>();
            _cameTo = new Dictionary<BarleyState, BarleyState>();
            
            _priorityQueue.Enqueue(initState);
            _costSoFar[_initState] = 0;
        }
        public Algorothm()
        {
            _initState = new BarleyState();
        }

        public void StartAlgorithm()
        {
            while (!_priorityQueue.IsEmpty())
            {
                BarleyState current = _priorityQueue.Dequeue();

                if (current.IsWin())
                {
                    break;
                }

                foreach (BarleyState next in current.NextStep())
                {
                    int newCost = _costSoFar[current] + 1;
                    if (!_costSoFar.ContainsKey(next) || newCost < _costSoFar[next])
                    {
                        _costSoFar[next] = newCost;
                        next.Priority = newCost + next.Heuristic();
                        _priorityQueue.Enqueue(next);
                        _cameTo[current] = next;
                    }
                }
                
            }
        }
    }
}
