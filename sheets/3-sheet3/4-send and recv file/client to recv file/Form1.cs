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
      public  Socket server;
        public  NetworkStream ns;
        StreamReader sr;
        StreamWriter sw;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            string data;
            string input;
            int recv;
            IPEndPoint ipep = new IPEndPoint(
            IPAddress.Parse(textBox4.Text), int.Parse( textBox3.Text));
             server = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
                textBox1.Text += "connect done\n ";

            }
            catch (SocketException ex)
            {
                textBox1.Text = "";

                textBox1.Text = "Unable to connect to server.\n";
                textBox1.Text = ex.ToString();
                return;
            }
            ns = new NetworkStream(server);
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
            textBox1.Text += "start send file   \n";

            string fileName = textBox2.Text;
            using (StreamWriter fi = new StreamWriter(fileName))
            {
                while ((data = sr.ReadLine()) != "exit")
                {
                    fi.WriteLine(data);
                   
                }
            }
            textBox1.Text += " send file   done\n";
            textBox1.Text += " start close client\n";

            try
            {
                textBox1.Text = "";

                textBox1.Text = "Disconnecting from server...\n";
                sr.Close();
                sw.Close();
                ns.Close();
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                textBox1.Text += "  close client done\n";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        
        
    }
  }
