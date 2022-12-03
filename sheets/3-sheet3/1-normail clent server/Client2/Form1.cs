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
      public  Socket server;
        public  NetworkStream ns;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            int recv;
            IPEndPoint ipep = new IPEndPoint(
            IPAddress.Parse(textBox3.Text), int.Parse(textBox4.Text));
             server = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                textBox2.Text = "connect done ";
               

            }
            catch (SocketException ex)
            {
                textBox2.Text = "";
                textBox1.Text = "";
                textBox2.Text = "Unable to connect to server.";
                textBox2.Text = ex.ToString();
                return;
            }
            ns = new NetworkStream(server);
            recv = ns.Read(data, 0, data.Length);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            textBox1.Text += stringData;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            int recv;
             
            if (ns.CanRead)
            {
                recv = ns.Read(data, 0, data.Length);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
            else
            {
                textBox2.Text = "";
                textBox1.Text = "";
                textBox2.Text = "Error: Can't read from this socket";
                ns.Close();
                server.Close();
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "send and recv";
            byte[] data = new byte[1024];
            string inpu, stringData;
            int recv;
            string str = textBox1.Text;
            textBox1.Text="";
            int cnt = 0;
            foreach (var input in str.Split('\n'))
            {
                if (input == "exit")
                    break;
                if (ns.CanWrite)
                {
                    ns.Write(Encoding.ASCII.GetBytes(input), 0, input.Length);
                    ns.Flush();
                }
                recv = ns.Read(data, 0, data.Length);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                textBox1.Text += $"akn {cnt++} : "+stringData;
                textBox1.Text += "\n";
            }
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = "";
                textBox1.Text = "";

                textBox2.Text = "Disconnecting from server...";
                ns.Close();
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
  }
