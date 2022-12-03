using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_async
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        byte[] data;
        int recv;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                server.Bind(ipep);
                server.Listen(1000);
                server.BeginAccept(new AsyncCallback(accept), server);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        void accept(IAsyncResult isr)
        {
            try
            {
                Socket server2 = isr.AsyncState as Socket;
                Socket client = server2.EndAccept(isr);
                data = Encoding.ASCII.GetBytes("welcome");
                client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(send), client);
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
                // start recievinng the image data
                data = new byte[6];

                int isize = client.Receive(data);
                isize = BitConverter.ToInt32(data, 0);
                data = new byte[isize];
                byte[] buf = new byte[1024];
                for (int i = 0; i < isize / 1024; i++)
                {
                    recv = client.Receive(buf, 0, 1024, SocketFlags.None);
                    buf.CopyTo(data, i * 1024);
                }
                buf = new byte[isize % 1024];
                if (isize % 1024 != 0)
                {
                    recv = client.Receive(buf, 0, buf.Length, SocketFlags.None);
                    buf.CopyTo(data, (isize / 1024) * 1024);
                }


                MemoryStream ms2 = new MemoryStream(data);
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
