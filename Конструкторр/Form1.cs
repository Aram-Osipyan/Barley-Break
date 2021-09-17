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
        private byte[] _currentState;
        public Form1()
        {
            InitializeComponent();
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
            foreach (var item in _buttons)
            {
                item.Click += new System.EventHandler(this.butt_click);
            }
            createRand();
            
        }
        private void createRand()
        {
            Random r = new Random();
            byte[] data = new byte[16] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};

            for (int i = 15; i >= 1; --i)
            {
                int j = r.Next(0, i + 1);
                swap(ref data[j],ref data[i]);
            }

            for (int i = 0; i < 16; i++)
            {
                _buttons[i].Text = (data[i] == 0)?"":data[i] + "";
                if (data[i] == 0)
                {
                    _buttons[i].Visible = false;
                }
            }
            _currentState = data;
        }
        
        void swap<T>(ref T a,ref T b)
        {
            var t = a;
            a = b;
            b = t;
        }
        private void butt_click(object sender, EventArgs e)
        {
            int curr_ind = (sender as Button).TabIndex - 1;
            int d = curr_ind / 4;
            int m = curr_ind % 4;
            //Trace.WriteLine(curr_ind+" "+ buttons[curr_ind].Text+" "+loc[d,m]);            
            int i1 = d - 1 >= 0 ? d - 1 : d;
            int i2 = d + 1 <= 3 ? d + 1 : d;
            int j1 = m - 1 >= 0 ? m - 1 : m;
            int j2 = m + 1 <= 3 ? m + 1 : m;
            //Trace.WriteLine(buttons[i1 * 4 + m].Text + " " + buttons[4 * d + j2].Text + " " + buttons[i2 * 4 + m].Text + " " + buttons[j1 + 4 * d].Text);
            if(buttons[i1 * 4 + m].Text == "")
            {
                var t = buttons[i1 * 4 + m].Text;
                buttons[i1 * 4 + m].Text = buttons[d * 4 + m].Text;
                buttons[d * 4 + m].Text = t;
                buttons[i1 * 4 + m].Visible = true;
                buttons[d * 4 + m].Visible = false;
                return;
            }
            if (buttons[4 * d + j2].Text == "")
            {
                var t = buttons[4 * d + j2].Text;
                buttons[4 * d + j2].Text = buttons[d * 4 + m].Text;
                buttons[d * 4 + m].Text = t;
                buttons[4 * d + j2].Visible = true;
                buttons[d * 4 + m].Visible = false;
                return;
            }
            if (buttons[i2 * 4 + m].Text == "")
            {
                var t = buttons[i2 * 4 + m].Text;
                buttons[i2 * 4 + m].Text = buttons[d * 4 + m].Text;
                buttons[d * 4 + m].Text = t;

                buttons[i2 * 4 + m].Visible = true;
                buttons[d * 4 + m].Visible = false;
                return;
            }
            if (buttons[j1 + 4 * d].Text == "")
            {
                var t = buttons[j1 + 4 * d].Text;
                buttons[j1 + 4 * d].Text = buttons[d * 4 + m].Text;
                buttons[d * 4 + m].Text = t;

                buttons[j1 + 4 * d].Visible = true;
                buttons[d * 4 + m].Visible = false;
                return;
            }
        }
        bool is_win()
        {
            for (int i = 0; i < 14; i++)
            {
                if(buttons[i].Text == (i + 1) + "")
                {
                    return false;
                }
                 
            }
            return true;
        }
       
    }
}    

        

