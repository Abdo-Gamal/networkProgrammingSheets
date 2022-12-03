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
                textBox2.Text = "connect done ";
                MessageBox.Show("dsddss");
            }
            catch (SocketException ex)
            {
                textBox2.Text = "";
                textBox1.Text = "";

                textBox2.Text = "Unable to connect to server.";
                textBox1.Text = ex.ToString();
                return;
            }
            ns = new NetworkStream(server);
            sr = new StreamReader(ns);
            sw = new StreamWriter(ns);
            data = sr.ReadLine();
            textBox1.Text = data;

        }



        private void button3_Click(object sender, EventArgs e)
        {
             
            textBox2.Text = "send and recv";
            try
            {
                string data;
                string inpu;
                int recv;
                string str = textBox1.Text;
                textBox1.Text = "";
                int cnt = 0;
                foreach (var input in str.Split('\n'))
                {
                    if (input == "exit")
                        break;
                    if (ns.CanWrite)
                    {
                        sw.WriteLine(input);
                        sw.Flush();
                    }
                    data = sr.ReadLine();
                    textBox1.Text += $"akn {cnt++} : " + data;
                    textBox1.Text += $"{cnt++} \n";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = "";
                textBox1.Text = "";

                textBox2.Text = "Disconnecting from server...";
                sr.Close();
                sw.Close();
                ns.Close();
                server.Shutdown(SocketShutdown.Both);
                server.Close();
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
  }
