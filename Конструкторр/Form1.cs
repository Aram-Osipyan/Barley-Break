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
namespace Конструкторр
{
    public partial class Form1 : Form
    {
        private Button[] _buttons = new Button[16];
        //private byte[] _currentState;
        private BarleyState _currentState;
        private Algorothm _algorothm;
        public Form1()
        {
            InitializeComponent();
            ButtonsArrayInit();

            _currentState = new BarleyState(new byte[]{1, 2,3,4,5,6,7,8,9,10,11,12,13,14,15,0});
            ButtonsInit(_currentState);
            _algorothm = new Algorothm(_currentState);
            
            _algorothm.Algorithmfinished += drawAnimationDecision;
            _algorothm.StartAlgorithm();
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

            foreach (var item in _buttons)
            {
                //item.Click += new System.EventHandler(this.butt_click);
            }
        }
        private void ButtonsInit(BarleyState state)
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                if (state.CurrentState[i] == 0)
                {
                    _buttons[i].Visible = false;
                }
                else
                {
                    _buttons[i].Text = state.CurrentState[i]+"";
                }
            }

        }

        private void ChangeButton(int currentButton, int anotherButton)
        {
            var t = _buttons[anotherButton].Text;
            _buttons[anotherButton].Text = _buttons[currentButton].Text;
            _buttons[currentButton].Text = t;

            _buttons[anotherButton].Visible = true;
            _buttons[currentButton].Visible = false;
        }
        private void drawAnimationDecision(Dictionary<BarleyState, BarleyState> decis)
        {
            BarleyState state = _currentState;
            while (!state.IsWin())
            {
                state = decis[state];
                System.Threading.Thread.Sleep(1000);
                ButtonsInit(state);
            }
        }
        
    }
    
}    

        

