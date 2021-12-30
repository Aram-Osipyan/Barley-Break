using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
namespace BarelyBreak
{
    class BarleyState : FastPriorityQueueNode, IComparable<BarleyState>
    {
        private static List<int>[] _nextSteps;
        private byte[] _currentState;
        private int _heuristic;
        private byte _spaceIndex;

        public event Action<int, int> DOStep;

        public byte[] CurrentState
        {
            get { return _currentState; }
        }

        public int Priority { get; set; }
        public int Heuristic { get { return _heuristic; } }

        public long HashCode { private set; get; }
        static BarleyState()
        {
            _nextSteps = new List<int>[16];
            for (int i = 0; i < _nextSteps.Length; i++)
            {
                _nextSteps[i] = new List<int>();

                int d = i / 4;
                int m = i % 4;

                if (m != 3)
                {
                    _nextSteps[i].Add(i + 1);
                }

                if (m != 0)
                {
                    _nextSteps[i].Add(i - 1);
                }

                if (d != 0)
                {
                    _nextSteps[i].Add(i - 4);
                }

                if (d != 3)
                {
                    _nextSteps[i].Add(i + 4);
                }
            }
        }
        public BarleyState(byte[] initState)
        {
            _currentState = initState;
            _heuristic = CalculateHeuristic();
            byte spaceIndex = 0;
            for (byte i = 0; i < _currentState.Length; i++)
            {
                if (_currentState[i] == 0)
                {
                    spaceIndex = i;
                }
            }
            _spaceIndex = spaceIndex;
            HashCode = CalculateHash();
        }
        public BarleyState(byte[] initState, int heuristic)
        {
            _currentState = initState;
            _heuristic = heuristic;
            byte spaceIndex = 0;
            for (byte i = 0; i < _currentState.Length; i++)
            {
                if (_currentState[i] == 0)
                {
                    spaceIndex = i;
                }
            }
            _spaceIndex = spaceIndex;
            HashCode = CalculateHash();
        }
        public BarleyState(byte[] initState, int heuristic, byte spaceIndex)
        {
            _currentState = initState;
            _heuristic = heuristic;
            _spaceIndex = spaceIndex;
            HashCode = CalculateHash();
        }
        private long CalculateHash()
        {
            long hash = 0;
            int offset = 0;
            for (int i = 0; i < _currentState.Length; i++)
            {
                long t = _currentState[i];
                hash = hash | t << offset;
                offset += 4;
            }
            return hash;
        }
        public override int GetHashCode()
        {
            return (int)HashCode;
        }

        public int CalculateHeuristic()
        {
            int sum = 0;
            for (int i = 0; i < CurrentState.Length; i++)
            {
                sum += ManhattanWay(i);
            }

            return sum;
        }
        private int SonHeuristic(int spaceIndex, int swapIndex)
        {
            return _heuristic - ManhattanWay(swapIndex, _currentState[swapIndex])
                + ManhattanWay(spaceIndex, _currentState[swapIndex]);
        }
        private int ManhattanWay(int index, int currentValue)
        {
            int d = index / 4;
            int m = index % 4;
            if (currentValue == 0)
            {
                return 0;
            }

            int d1 = (currentValue - 1) / 4;
            int m1 = (currentValue - 1) % 4;

            return Math.Abs(d - d1) + Math.Abs(m - m1);
        }
        private int ManhattanWay(int index)
        {
            return ManhattanWay(index, CurrentState[index]);
        }

        private void ChangeButton(int currentButton, int anotherButton)
        {
            Swap(ref _currentState[currentButton], ref _currentState[anotherButton]);
        }

        public List<BarleyState> NextStep()
        {
            var ans = new List<BarleyState>(4);
            int spaceIndex = _spaceIndex;

            for (int i = 0; i < _nextSteps[spaceIndex].Count; i++)
            {
                byte[] step = Clone(_currentState);
                Swap(ref step[spaceIndex], ref step[_nextSteps[spaceIndex][i]]);
                ans.Add(new BarleyState(step, SonHeuristic(spaceIndex, _nextSteps[spaceIndex][i]), (byte)_nextSteps[spaceIndex][i]));
            }
            return ans;
        }

        private static void Swap(ref byte a, ref byte b)
        {
            byte t = a;
            a = b;
            b = t;
        }

        private static byte[] Clone(byte[] state)
        {
            byte[] clone = new byte[16];
            for (int i = 0; i < clone.Length; ++i)
            {
                clone[i] = state[i];
            }
            return clone;
        }

        public int CompareTo(BarleyState state)
        {
            if (Priority < state.Priority)
            {
                return -1;
            }
            else if (Priority > state.Priority)
            {
                return 1;
            }

            return 0;
        }
    }
}