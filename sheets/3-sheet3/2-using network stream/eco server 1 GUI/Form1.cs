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
using System.IO;

namespace Client2
{
    public partial class Form1 : Form
    {
      public  Socket newsock;
        Socket client;
        public NetworkStream ns;
        public StreamReader sr;
        public StreamWriter sw;
        public  IPEndPoint clientep;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Enabled=false;
            textBox4.Enabled = false;


            string data;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
             newsock = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            MessageBox.Show("wait for client .....");
             client = newsock.Accept();

             clientep = (IPEndPoint)client.RemoteEndPoint;
            ///////////////////////////////////////////////////////////above is constant
             ns = new NetworkStream(client);
             sr = new StreamReader(ns);
             sw = new StreamWriter(ns);
            string welcome = "Welcome to my test server";
            sw.WriteLine(welcome);
            sw.Flush();


            while (true)
            {

                try
                {
                    MessageBox.Show("wait for msg .....");
                    data = sr.ReadLine();
                    MessageBox.Show("recv msg ");

                }
                catch (IOException ex) { MessageBox.Show(ex.ToString()); break; }
                textBox1.Text+=data;
                textBox1.Text += "\r\n";

                sw.WriteLine(data);
                MessageBox.Show("recv msg ");

                sw.Flush();
                MessageBox.Show("resend  msg ");

            }
        
            textBox1.Clear();
            textBox1.Text += "\r\n";

            
        }

     
       

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = "";
                textBox1.Text = "";

                textBox2.Text = "Disconnecting from ..."+ clientep.Address;
                sw.Close();
                sr.Close();
                ns.Close();
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
