using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarelyBreakConsoleApp
{
    class Program
    {
        //private byte[] _currentState;
        private static BarleyState _currentState;
        
        private static Int64 CalculateHash(byte[] _currentState)
        {
            Int64 hash = 0;
            int offset = 0;
            for (int i = 0; i < _currentState.Length; i++)
            {
                long t = _currentState[i];
                hash = hash | t << offset;
                offset = (offset+ 4);
            }
            return hash;
        }
        static void Main(string[] args)
        {
            //_currentState = new BarleyState(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 15 }); //1
            //_currentState = new BarleyState(new byte[] { 1, 6, 2, 4, 5, 10, 3, 7, 0, 9, 12, 8, 13, 14, 11, 15 }); //10
            //_currentState = new BarleyState(new byte[] { 1, 7, 2, 3, 0, 6, 8, 4, 5, 9, 10, 12, 13, 14, 11, 15 }); //13
            _currentState = new BarleyState(new byte[] { 1,2,3,4,5,6,7,8,10,0,11,14,9,15,12,13 });   // 19         
            //_currentState = new BarleyState(new byte[] { 5,1,2,4,7,3,0,8,10,6,11,14,9,15,12,13 }); // 27

            //ButtonsInit(_currentState);
            Algorothm _algorothm = new Algorothm(_currentState);

            _algorothm.Algorithmfinished += drawAnimationDecision;
            _algorothm.StartAlgorithm();
            //Int64 binary = CalculateHash(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0, 15});
            //Int64 binary =  1 << 4 | 3 << 8 | 13 << 12| 2 ;
            //String bin = Convert.ToString(binary, 2);
            //Console.WriteLine(bin + " " + bin.Length);

        }
        private static void ButtonsInit(BarleyState state)
        {
            for (int i = 0; i < state.CurrentState.Length; i++)
            {
                if (i % 4 == 0)
                {
                    Console.WriteLine();
                }
                Console.Write(state.CurrentState[i] + "\t ");

            }
            Console.WriteLine();

        }
        private static void drawAnimationDecision(Dictionary<BarleyState, BarleyState> decis, BarleyState end)
        {
            BarleyState state = end;
            while (state != null)
            {
                if (!decis.ContainsKey(state))
                {
                    break;
                }
                state = decis[state];
                ButtonsInit(state);
            }
        }
    }
}
