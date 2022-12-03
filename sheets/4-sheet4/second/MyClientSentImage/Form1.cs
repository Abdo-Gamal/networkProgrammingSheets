using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyClientSentImage
{
    public partial class Form1 : Form
    {
        private Socket client;
        private int size = 1024;
        private byte[] data = new byte[1024];
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conStatus1.Text = "Connecting...";
            Socket newsock = new Socket(AddressFamily.InterNetwork,
                       SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            newsock.BeginConnect(iep, new AsyncCallback(Connected), newsock);
        }

        void Connected(IAsyncResult iar)
        {
            client = (Socket)iar.AsyncState;
            try
            {
                client.EndConnect(iar);
               // conStatus1.Text = "Connected to: " + client.RemoteEndPoint.ToString();
                client.BeginReceive(data, 0, size, SocketFlags.None,
                      new AsyncCallback(ReceiveData), client);
            }
            catch (SocketException)
            {
                conStatus1.Text = "Error connecting";
            }
        }

        void ReceiveData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int recv = remote.EndReceive(iar);
            string stringData = Encoding.ASCII.GetString(data, 0, recv);
            results.Items.Add(stringData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                Bitmap bmp = new Bitmap(textBox1.Text);
                bmp.Save(ms, ImageFormat.Jpeg);
                byte[] byteArray = ms.ToArray();

                client.BeginSend(byteArray, 0,byteArray.Length,
                    SocketFlags.None,new AsyncCallback(SendData), client);
                    

            }
            catch (Exception ex)
            {

            }
        }
        
        void SendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);

            
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    openFileDialog1.ShowDialog();
        //    string path = openFileDialog1.FileName;
        //    textBox1.Text=path;
        //    //pictureBox1.Image = Image.FromFile(path);
        //    //textPath.Text = path;
        //}
    }
}
