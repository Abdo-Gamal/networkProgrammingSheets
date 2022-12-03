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
        public Socket sock;
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = new byte[1024];

                IPAddress host = IPAddress.Parse(textBox3.Text);
                IPEndPoint hostep = new IPEndPoint(host, int.Parse(textBox4.Text));
                sock = new Socket(AddressFamily.InterNetwork,
               SocketType.Stream, ProtocolType.Tcp);
                sock.Connect(hostep);

                int bytesRec = sock.Receive(bytes);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Text += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                textBox2.Text += "   can sen and recive ):";

            }
            catch(Exception ex)
            {
                textBox1.Text = "";
                textBox2.Text = "";

                textBox2.Text += "close stream  done ):";

                textBox1.Text += ex.Message;
                sock.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "";
                textBox2.Text = "";

                textBox2.Text += "close stream  done ):";
                sock.Close();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Clear();

                textBox2.Text += "    sen and recive ):";

                byte[] bytes = new byte[1024];

                string str = textBox1.Text;
                textBox1.Clear();
                int cnt = 0;
                foreach (var line in str.Split('\n'))
                {
                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(line);
                    // Send the data through the socket.
                    int bytesSent = sock.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sock.Receive(bytes);
                    textBox1.Text += $"aknlp {cnt++} ): " + Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    textBox1.Text += "\n";
                }
            }
            catch (Exception ex)
            {

                textBox1.Text = "";
                textBox2.Text = "";

                textBox2.Text += "close stream  done ):";

                textBox1.Text += ex.Message;
                sock.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
