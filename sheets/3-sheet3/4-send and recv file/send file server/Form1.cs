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
            textBox3.Enabled = false;
            textBox4.Enabled = false;


            string data;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            newsock = new Socket(AddressFamily.InterNetwork,
           SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(ipep);
            newsock.Listen(10);
            MessageBox.Show("wait for client .....");
            client = newsock.Accept();
            MessageBox.Show("accept one  client .....");

            clientep = (IPEndPoint)client.RemoteEndPoint;
            ///////////////////////////////////////////////////////////above is constant
            ns = new NetworkStream(client);
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
            string welcome = "Welcome to my test server";
            sw.WriteLine(welcome);
            sw.Flush();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          //  ofd.InitialDirectory = "G:\fourth_year\\422\\1-network programming\abdo\\sheets\\sheet3\\3\\send file server\bin\\Debug";
            ofd.ShowDialog();
            string fileName = Path.GetFileName(ofd.FileName);
            textBox2.Text = fileName;
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] str = File.ReadAllLines(textBox2.Text);
            foreach (string str2 in str)
            {
                sw.WriteLine(str2);
                sw.Flush();
                MessageBox.Show("resend  msg ");
            }
            sw.WriteLine("exit");
            sw.Flush();

            textBox2.Text = "Disconnecting from ..." + clientep.Address;
            sw.Close();
            sr.Close();
            ns.Close();
        }
    }
  

}
