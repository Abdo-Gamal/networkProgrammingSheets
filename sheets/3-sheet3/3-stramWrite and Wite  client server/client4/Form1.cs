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
                int recv;
                byte[] data = new byte[1024];
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
                Socket newsock = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);
                newsock.Bind(ipep);
                 sender = new IPEndPoint(IPAddress.Any, 0);
                 Remote = (EndPoint)(sender);
                MessageBox.Show("wait mesg ");
                recv = newsock.ReceiveFrom(data, ref Remote);

                textBox1.Text+="Message received from : "+Remote.ToString();
                textBox1.Text += "\n";
               textBox1.Text += Encoding.ASCII.GetString(data, 0, recv);
                textBox1.Text += "\n";

                string welcome = "Welcome to my test server \n\n";
                textBox5.Text += welcome+"\n";
                data = Encoding.ASCII.GetBytes(welcome);
                newsock.SendTo(data, data.Length, SocketFlags.None,
                Remote);
                MessageBox.Show("send wlocom  mesg ");

                while (true)
                {
                    
                    data = new byte[1024];
                    recv = newsock.ReceiveFrom(data, ref Remote);
                    if (data.ToString() == "exit")
                        break;
                    textBox5.Text +=Encoding.ASCII.GetString(data, 0,recv);
                    textBox1.Text += Encoding.ASCII.GetString(data, 0, recv);
                    textBox1.Text+="\n";
                    textBox5.Text += "\n";
                    newsock.SendTo(data, recv, SocketFlags.None, Remote);

                }
            }
            catch(Exception ex)
            {
                textBox1.Text = "";
                textBox5.Text = "";

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
