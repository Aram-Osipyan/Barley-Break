using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Конструкторр
{
    class BarleyState : IComparable<BarleyState>
    {
        private byte[] _currentState;
        
        public event Action<int, int> DOStep;

        public byte[] CurrentState
        {
            get { return _currentState; }
            
        }

        public int Priority { get; set; }

        public BarleyState()
        {
            CreateRandomField();
        }

        public BarleyState(byte[] initState)
        {
            _currentState = initState;
        }

        public override int GetHashCode()
        {
            Int64 hash = 0;
            byte offset = 0;
            foreach (byte item in _currentState)
            {
                Int64 t = item;
                hash = hash | (t << offset);
                offset += 4;
            }

            return (int) hash;
        }

        public int Heuristic()
        {
            int sum = 0;
            for (int i = 0; i < CurrentState.Length; i++)
            {
                sum += ManhattanWay(i);
            }

            return sum;
        }

        private int ManhattanWay(int index)
        {
            int d = index / 4;
            int m = index % 4;

            int d1 = CurrentState[index] / 4;
            int m1 = CurrentState[index] % 4;

            return Math.Abs(d - d1) + Math.Abs(m - m1);
        }
        public static byte[] CreateRandomField()
        {
            Random r = new Random();
            byte[] data = new byte[16] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};

            for (int i = 15; i >= 1; --i)
            {
                int j = r.Next(0, i + 1);
                swap(ref data[j], ref data[i]);
            }

            return data;
        }

        public void MakeStep(int buttonIndex)
        {
            int[] availableSteps = {4, -4, 1, -1};
            foreach (var step in availableSteps)
            {
                if (step + buttonIndex < _currentState.Length && step + buttonIndex >= 0 &&
                    _currentState[step + buttonIndex] == 0)
                {
                    ChangeButton(buttonIndex, step + buttonIndex);
                    DOStep(buttonIndex, step + buttonIndex);
                }
            }
        }

        private void ChangeButton(int currentButton, int anotherButton)
        {
            swap(ref _currentState[currentButton], ref _currentState[anotherButton]);
        }

        public List<BarleyState> NextStep()
        {
            var ans = new List<BarleyState>();
            int spaceIndex = 0;
            for (int i = 0; i < _currentState.Length; i++)
            {
                if (_currentState[i] == 0)
                {
                    spaceIndex = i;
                }
            }

            int d = spaceIndex / 4;
            int m = spaceIndex % 4;

            if (m != 3)
            {
                byte[] step = (byte[]) _currentState.Clone();
                swap(ref step[spaceIndex], ref step[spaceIndex + 1]);
                ans.Add(new BarleyState(step));
            }

            if (m != 0)
            {
                byte[] step = (byte[]) _currentState.Clone();
                swap(ref step[spaceIndex], ref step[spaceIndex - 1]);
                ans.Add(new BarleyState(step));
            }

            if (d != 0)
            {
                byte[] step = (byte[]) _currentState.Clone();
                swap(ref step[spaceIndex], ref step[spaceIndex - 4]);
                ans.Add(new BarleyState(step));
            }

            if (d != 3)
            {
                byte[] step = (byte[]) _currentState.Clone();
                swap(ref step[spaceIndex], ref step[spaceIndex + 4]);
                ans.Add(new BarleyState(step));
            }

            return ans;
        }

        public bool IsWin()
        {
            for (int i = 0; i < 16; i++)
            {
                if (_currentState[i] != i)
                {
                    return false;
                }
            }

            return true;
        }

        private static void swap(ref byte a, ref byte b)
        {
            (a, b) = (b, a);
        }

        public int CompareTo(BarleyState state)
        {
            if (Priority < state.Priority)
            {
                return -1;
            }else if(Priority > state.Priority)
            {
                return 1;
            }

            return 0;
        }
    }
}