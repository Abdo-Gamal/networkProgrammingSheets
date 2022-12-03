using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSserverGUI
{
    public partial class Form1 : Form
    {



        private byte[] data = new byte[1024];
        private int size = 1024;
        private Socket server;
        private Socket client;
        public Form1()
        {
            InitializeComponent();

            server = new Socket(AddressFamily.InterNetwork,
              SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
            server.Bind(iep);
            server.Listen(5);
            server.BeginAccept(new AsyncCallback(AcceptConn), server);
        }
        void AcceptConn(IAsyncResult iar)
        {
            Socket oldserver = (Socket)iar.AsyncState;
             client = oldserver.EndAccept(iar);

            conStatus1.Text = "Connected to: " + client.RemoteEndPoint.ToString();
            string stringData = "Welcome to my server";
            byte[] message1 = Encoding.ASCII.GetBytes(stringData);

            client.BeginSend(message1, 0, message1.Length, SocketFlags.None,
                  new AsyncCallback(SendData), client);
        }

        void SendData(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            int sent = client.EndSend(iar);

            client.BeginReceive(data, 0, size, SocketFlags.None,
                  new AsyncCallback(ReceiveData), client);
        }
        void ReceiveData(IAsyncResult iar)
        {
            Socket client = (Socket)iar.AsyncState;
            int recv = client.EndReceive(iar);
            if (recv == 0)
            {
                client.Close();
                conStatus1.Text = "Waiting for client...";
                server.BeginAccept(new AsyncCallback(AcceptConn), server);
                return;
            }

            ImageConverter convertData = new ImageConverter();
            Image image = (Image)convertData.ConvertFrom(data);
            pictureBox1.Image = image;
           

            //client.BeginReceive(data, 0, size, SocketFlags.None,
            //     new AsyncCallback(ReceiveData), client);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

       

      
    }
}
