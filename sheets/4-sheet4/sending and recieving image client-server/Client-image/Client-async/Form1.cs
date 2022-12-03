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

namespace Client_async
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        byte[] data;
        int recv;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                client.BeginConnect(ipep, new AsyncCallback(conect), client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        void conect(IAsyncResult isr)
        {
            try
            {
                client.EndConnect(isr);
                data = new byte[1024];
                client.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(recvfunc), client);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
         
        }
        void recvfunc(IAsyncResult isr)
        {
            try
            {
                recv = client.EndReceive(isr);
                listBox1.Items.Add(Encoding.UTF8.GetString(data, 0, recv));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        void send(IAsyncResult isr)
        {
            try
            {
                Socket client = isr.AsyncState as Socket;
                client.EndSend(isr);
                data = new byte[1024];
                client.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(recvfunc), client);
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
                openFileDialog1.ShowDialog();
               string imageName = openFileDialog1.FileName;


                MemoryStream ms = new MemoryStream();
                Bitmap bmp = new Bitmap(imageName);
                bmp.Save(ms, ImageFormat.Jpeg);
                byte[] byteArray = ms.ToArray();

                byte[] len = BitConverter.GetBytes(byteArray.Length);
                //length
                client.Send(len, 0, len.Length, SocketFlags.None);
                //image
                client.Send(byteArray, 0, byteArray.Length, SocketFlags.None);
                
              
                MemoryStream ms2 = new MemoryStream(byteArray);
                Image image1 = Image.FromStream(ms2);
                pictureBox1.Image = image1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }
    }
}
