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
            //_currentState = new BarleyState(new byte[] { 1,2,3,4,5,6,7,8,10,0,11,14,9,15,12,13 });   // 19         
            //_currentState = new BarleyState(new byte[] { 5,1,2,4,7,3,0,8,10,6,11,14,9,15,12,13 }); // 27
            //_currentState = new BarleyState(new byte[] { 7,5,10,11,2,12,4,1,6,13,3,8,9,15,0,14 }); // 45
            _currentState = new BarleyState(new byte[] { 15,14,1,6,9,11,4,12,0,10,7,3,13,8,5,2 }); // 52
            //_currentState = new BarleyState(new byte[] { 13,7,9,15,2,14,8,10,4,5,1,0,6,12,3,11}); // 55
            //_currentState = new BarleyState(new byte[] { 13,11,14,8,7,10,2,12,9,1,15,6,5,0,3,4}); // 58
            //_currentState = new BarleyState(new byte[] { 11,10,12,0,15,4,7,8,14,1,9,6,2,3,13,5}); // 61
            //ButtonsInit(_currentState);
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            Algorothm _algorothm = new Algorothm(_currentState);
           
            _algorothm.Algorithmfinished += drawAnimationDecision;

            _algorothm.StartAlgorithm();
            watch.Stop();

            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

        }
        private static void ButtonsInit(long state)
        {
            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write((state & 15) + " ");
                    state = state >> 4;
                }

                Console.WriteLine();
            }

            Console.WriteLine();

        }
        private static void drawAnimationDecision(Dictionary<long, long> decis, long end)
        {
            
            long state = end;
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
