using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace BarelyBreak
{
    public partial class Form1 : Form
    {
        private Button[] _buttons = new Button[16];
        private System.Windows.Forms.Timer _timer;

        private DateTime _startTime = DateTime.Now;
        private TimeSpan _currentElapsedTime = TimeSpan.Zero;
        private TimeSpan _totalElapsedTime = TimeSpan.Zero;
        public Form1()
        {
            InitializeComponent();
            ButtonsArrayInit();

            ComputingThread computingThread = new ComputingThread(drawAnimationDecision, ButtonsInit);

            Thread t1 = new Thread(computingThread.StartAlgorithm);
            t1.Start();

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 10;
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            var timeSinceStartTime = DateTime.Now - _startTime;

            // The current elapsed time is the time since the start button
            // was clicked, plus the total time elapsed since the last reset
            _currentElapsedTime = timeSinceStartTime + _totalElapsedTime;

            // These are just two Label controls which display the current
            // elapsed time and total elapsed time
            textBox1.Text =
                timeSinceStartTime.Seconds
                + " : " +
                timeSinceStartTime.Milliseconds;
        }

        private void StopWatch()
        {
            _timer.Stop();
        }

        private void ButtonsArrayInit()
        {
            _buttons[0] = btn1;
            _buttons[1] = btn2;
            _buttons[2] = btn3;
            _buttons[3] = btn4;
            _buttons[4] = btn5;
            _buttons[5] = btn6;
            _buttons[6] = btn7;
            _buttons[7] = btn8;
            _buttons[8] = btn9;
            _buttons[9] = btn10;
            _buttons[10] = btn11;
            _buttons[11] = btn12;
            _buttons[12] = btn13;
            _buttons[13] = btn14;
            _buttons[14] = btn15;
            _buttons[15] = btn16;
        }

        private void ButtonsInit(long state)
        {
            if (InvokeRequired)
            {
                Action<long> del = ButtonsInit;
                Invoke(del, state);
            }
            else
            {
                for (int i = 0; i < 16; i++)
                {
                    int text = (int)(state & 15);
                    if (text == 0)
                    {
                        _buttons[i].Text = "";
                        _buttons[i].Visible = false;
                    }
                    else
                    {
                        _buttons[i].Visible = true;
                        _buttons[i].Text = text + "";
                    }

                    state = state >> 4;
                }
            }
        }
        private void drawAnimationDecision(Dictionary<long, long> decis, long end)
        {
            if (InvokeRequired)
            {
                Action<Dictionary<long, long>, long> del = drawAnimationDecision;
                Invoke(del, decis, end);
            }
            else
            {
                StopWatch();
                Stack<long> stack = new Stack<long>();
                long state = end;
                while (true)
                {
                    if (!decis.ContainsKey(state))
                    {
                        break;
                    }
                    state = decis[state];
                    stack.Push(state);

                }
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Tick += (sender, e) =>
                {
                    if (stack.Count == 0)
                    {
                        ButtonsInit(end);
                        timer.Stop();
                    }
                    else
                    {
                        ButtonsInit(stack.Pop());
                    }

                };
                timer.Interval = 500; // in miliseconds
                timer.Start();
            }


        }

    }

}



