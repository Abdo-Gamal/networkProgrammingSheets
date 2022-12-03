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


namespace Client2
{
    public partial class Form1 : Form
    {
      public  Socket newsock;
        Socket client;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
             newsock = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            MessageBox.Show("wait for client .....");
             client = newsock.Accept();

            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;

            string welcome = "Welcome to my test server";
            textBox2.Text = welcome;
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);


            while (true)
            {
                data = new byte[1024];
                MessageBox.Show("wait for msg .....");

                recv = client.Receive(data);
                MessageBox.Show("recv msg ");

                if (recv == 0)
                    break;
                textBox1.Text += (Encoding.ASCII.GetString(data, 0, recv));
                textBox1.Text+="\r\n";

                client.Send(data, recv, SocketFlags.None);
                MessageBox.Show("resend  msg ");

            }
            textBox1.Clear();
            textBox1.Text += "Disconnected from  "+clientep.Address;
            textBox1.Text += "\r\n";

            
        }

     
       

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = "";
                textBox1.Text = "";

                textBox2.Text = "Disconnecting from server...";
                client.Close();
                newsock.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
  }
