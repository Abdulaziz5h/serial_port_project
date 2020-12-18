using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com_project_server
{
    public partial class Form1 : Form
    {
        public string play_Messamge = "";       
        public int myPlay = -1;
        public int hisPlay = -1;
        public bool Game_open = false;
        public bool Is_Reset_Game_open = false;
        String resualt ="";
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex == 1)
            {
              //  label9.Enabled = true;
                textBox1.Enabled = true;

            }
            else
            {

              //  label9.Visible = false;
                textBox1.Enabled = false;
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            String[] port;
            port = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Items.Clear();

            for (int i = 0; i < port.Length - 1; i++)
                comboBox1.Items.Add(port[i]);
            comboBox1.SelectedIndex = 0;

            // dataBits Value 
            comboBox2.SelectedIndex = 3;

            //Buad rate value
            comboBox3.SelectedIndex = 0;

            // stop bit Value
            comboBox4.SelectedIndex = 0;

            // parity  Value
            comboBox5.SelectedIndex = 0;
                
        }
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            resualt = serialPort1.ReadLine();
            if(!Game_open) {
                richTextBox1.Invoke(new Action(() => { richTextBox1.Text = resualt.ToString(); })) ;
            } else
            {
                hisPlay = int.Parse(resualt);
                if (myPlay != -1)
                {
                    getWinner();
                } else
                {
                    play_Messamge = "player tow waiting you";
                }
                if (int.Parse(resualt) == -2)
                {
                    Is_Reset_Game_open = true;
                }
            }
        }
        private void start_connect_Click(object sender, EventArgs e)
        {
            int temp = 0;
            string str;
            if (serialPort1.IsOpen == false)
            {
                // set port name Value
                serialPort1.PortName = (comboBox1.SelectedItem).ToString();

                // set dataBits Value 
                temp = Convert.ToInt32(comboBox2.SelectedItem);
                serialPort1.DataBits = temp;

                //set Buad rate value
                temp = Convert.ToInt32(comboBox3.SelectedItem);
                serialPort1.BaudRate = temp;

                // set stop bit Value
                str = "";
                str = comboBox4.SelectedItem.ToString().ToLower();
                if (str == "one")
                    serialPort1.StopBits = System.IO.Ports.StopBits.One;
                else
                    if (str == "two")
                    serialPort1.StopBits = System.IO.Ports.StopBits.Two;
                else
                        if (str == "onefivepoint")
                    serialPort1.StopBits = System.IO.Ports.StopBits.OnePointFive;

                // set parity  Value
                str = comboBox5.SelectedItem.ToString().ToLower();
                if (str == "none")
                    serialPort1.Parity = System.IO.Ports.Parity.None;
                else
                    if (str == "even")
                    serialPort1.Parity = System.IO.Ports.Parity.Even;
                else
                        if (str == "odd")
                    serialPort1.Parity = System.IO.Ports.Parity.Odd;
                else
                            if (str == "mark")
                    serialPort1.Parity = System.IO.Ports.Parity.Mark;
                else
                                if (str == "space")
                    serialPort1.Parity = System.IO.Ports.Parity.Space;

                try
                {
                    serialPort1.Open();
                    label2.Text = "connected";
                }
                catch
                {
                    label2.Text = "failed to connect";
                }
            }
        }
        private void setAsDefault_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM4";
            serialPort1.DataBits = 8;
            serialPort1.BaudRate = 9600;
            serialPort1.StopBits = System.IO.Ports.StopBits.One;
            serialPort1.Parity = System.IO.Ports.Parity.None;
            comboBox1.Text = "Com1";
            comboBox2.Text = "8";
            comboBox3.Text = "9600";
            comboBox4.Text = "One";
            comboBox5.Text = "None";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex == 1)
            {
                serialPort1.WriteLine("1");

                serialPort1.WriteLine(@"D:/aaa");

            }
            else
            {
                serialPort1.WriteLine(comboBox6.SelectedIndex.ToString());
            }
        }
        private void stop_connect_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            label2.Text = "not connect";
        }
        
        #region game
        private void Go_To_Game_Click(object sender, EventArgs e)
        {
            Game_open = true;
            Form2 frm = new Form2();
            frm.ParentForm = this;
            frm.Show();

        }
        public void send_Game_play(int play)
        {
            serialPort1.WriteLine(play.ToString());
        }
        public string set_play_status()
        {
            return play_Messamge;
        }
        public void getWinner()
        {
            //stone == 11
            //paper == 12
            //Scsstion == 13
            if (myPlay == hisPlay)
            {
                play_Messamge = "Equlation";
            } else if(myPlay == 11 && hisPlay == 12 || myPlay == 13 && hisPlay == 11 || myPlay == 12 && hisPlay == 13)
            {
                play_Messamge = "client win";
            }
            else if (myPlay == 11 && hisPlay == 13 || myPlay == 12 && hisPlay == 11 || myPlay == 13 && hisPlay == 12)
            {
                play_Messamge = "server win";
            }
        }
        #endregion
    }
}
