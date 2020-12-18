using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com_project_client
{
    public partial class Form2 : Form
    {
        public Form1 ParentForm { get; set; }
        public Form2()
        {
            InitializeComponent();
            check_play_status.Enabled = true;
        }
        public void restart_Play()
        {
            label6.Text = "_ _ _";
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            ParentForm.play_Messamge = "";
            ParentForm.myPlay = -1;
            ParentForm.hisPlay = -1;
            button1.BackColor = Color.Gray;
            button2.BackColor = Color.Gray;
            button3.BackColor = Color.Gray;
            button4.BackColor = Color.IndianRed;
            button5.BackColor = Color.IndianRed;
            button6.BackColor = Color.IndianRed;
        }


        public void play(int p)
        {
            if (ParentForm != null)
            {
                ParentForm.send_Game_play(p);
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                if (ParentForm.set_play_status() == "")
                {
                    label6.Text = "wait player tow";
                }
                else
                {
                    ParentForm.getWinner();
                    if (ParentForm.hisPlay == 11){
                        button6.BackColor = Color.LimeGreen;
                    }
                    else if (ParentForm.hisPlay == 12) {
                        button4.BackColor = Color.LimeGreen;
                    }
                    else if (ParentForm.hisPlay == 13) {
                        button5.BackColor = Color.LimeGreen;
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ParentForm.myPlay = 11;
            play(ParentForm.myPlay);
            button1.BackColor = Color.LimeGreen;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParentForm.myPlay = 12;
            play(ParentForm.myPlay);
            button3.BackColor = Color.LimeGreen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParentForm.myPlay = 13;
            play(ParentForm.myPlay);
            button2.BackColor = Color.LimeGreen;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            restart_Play();
            ParentForm.send_Game_play(-2);
        }


        private void check_play_status_Tick(object sender, EventArgs e)
        {
            string message = ParentForm.set_play_status();
            if (message != "")
            {
                label6.Text = message;
            }
            if (ParentForm.hisPlay != -1 && ParentForm.myPlay != -1)
            {
                if (ParentForm.hisPlay == 11)
                {
                    button6.BackColor = Color.LimeGreen;
                }
                else if (ParentForm.hisPlay == 12)
                {
                    button4.BackColor = Color.LimeGreen;
                }
                else if (ParentForm.hisPlay == 13)
                {
                    button5.BackColor = Color.LimeGreen;
                }
            }
            if (ParentForm.Is_Reset_Game_open)
            {
                restart_Play();
                ParentForm.Is_Reset_Game_open = false;
            }
        }

    }
}
