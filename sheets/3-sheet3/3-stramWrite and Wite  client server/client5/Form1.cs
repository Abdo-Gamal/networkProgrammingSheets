using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace client1
{




    public partial class Form1 : Form
    {
        public IPEndPoint sender;
        public EndPoint Remote;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Clear();

                byte[] data = new byte[1024];
                string input, stringData;
                IPEndPoint ipep = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"), 9050);
                Socket server = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);

                string welcome = "Hello, are you there? \n";
                data = Encoding.ASCII.GetBytes(welcome);

                server.SendTo(data, data.Length, SocketFlags.None, ipep);
                MessageBox.Show("send Hello  mesg ");

                sender = new IPEndPoint(IPAddress.Any, 0);
                 Remote = (EndPoint)sender;
                data = new byte[1024]; int recv = server.ReceiveFrom(data, ref Remote);
                textBox1.Text += "Message received from : " + Remote.ToString();
                textBox1.Text += "\r\n";
                textBox1.Text += Encoding.ASCII.GetString(data, 0, recv);
                textBox1.Text += "\r\n";

                foreach (var it in textBox5.Text.Split('\n'))
                {
                    input = it;
                    if (input == "exit")
                        break;
                    server.SendTo(Encoding.ASCII.GetBytes(input), Remote);
                    data = new byte[1024];
                    recv = server.ReceiveFrom(data, ref Remote);
                    stringData = Encoding.ASCII.GetString(data, 0, recv);
                    textBox1.Text += stringData;
                    textBox1.Text += "\n";
                }
                server.SendTo(Encoding.ASCII.GetBytes("exit"), Remote);

                textBox1.Text += "\n";
                textBox1.Text += "Stopping client \n";
                server.Close();
            
    }
            catch(Exception ex)
            {
                textBox1.Text = "";
                textBox2.Text = "";

                textBox2.Text += "close stream  done ):";

                textBox1.Text += ex.Message;
            }
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
    
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
