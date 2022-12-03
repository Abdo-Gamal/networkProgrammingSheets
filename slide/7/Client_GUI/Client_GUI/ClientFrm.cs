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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_GUI
{
   
    public partial class ClientFrm : Form
    {

        Socket sck;
        EndPoint remoteEp;
        byte[] buffer;
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        int port = 5000;

        public ClientFrm()
        {
            InitializeComponent();
        }
        public int Numberclient;
        private void btnConnect_Click(object sender, EventArgs e)
        { 
            // Here U must implement the code that capture the advetise packets for the server and parse the Ip&port
            remoteEp = new IPEndPoint(IPAddress.Parse(txtIpAddress.Text), Convert.ToInt32(txtPort.Text));
            sck.Connect(remoteEp);

            //BinaryReader handelsok=  new BinaryReader(new NetworkStream(sck));
           //string response=handelsok.ReadString();
           // Numberclient = int.Parse(response);
           // avalibleClient.Items.Add(Numberclient.ToString());

            //buffer = new byte[1024];
            //sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEp, new AsyncCallback(myMesgRecvNumClientCallBack), buffer);
            //for (int i=0;i< Numberclient; i++)
            //{
            //    buffer = new byte[1024];
            //    sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEp, new AsyncCallback(myMesgRecvAvalidClientCallBack), buffer);
            //}
            buffer = new byte[1024];
            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEp, new AsyncCallback(MessageCallBack), buffer);

        }
        private void myMesgRecvNumClientCallBack(IAsyncResult ar)
        {
            try
            {
                //BinaryReader handelsok=  new BinaryReader(new NetworkStream(sck));
                //string response=handelsok.ReadString();


                byte[] receiveData = new byte[1024];
                receiveData = (byte[])ar.AsyncState;
                //Converting byte[] to string
                ASCIIEncoding ascencoding = new ASCIIEncoding();
                string response = ascencoding.GetString(receiveData);
                Numberclient = int.Parse(response);
                sck.EndReceiveFrom(ar, ref remoteEp);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void myMesgRecvAvalidClientCallBack(IAsyncResult ar)
        {
            try
            {
                //BinaryReader handelsok = new BinaryReader(new NetworkStream(sck));
                // string response = handelsok.ReadString();

                byte[] receiveData = new byte[1024];
                receiveData = (byte[])ar.AsyncState;
                //Converting byte[] to string
                ASCIIEncoding ascencoding = new ASCIIEncoding();
                string response = ascencoding.GetString(receiveData);
                //Adding message to listbox
                avalibleClient.Items.Add(response);
                sck.EndReceiveFrom(ar, ref remoteEp);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
       
        private void MessageCallBack(IAsyncResult ar)
        {
            try
            {
                byte[] receiveData = new byte[1024];
                receiveData = (byte[])ar.AsyncState;
                //Converting byte[] to string
                ASCIIEncoding ascencoding = new ASCIIEncoding();
                string response = ascencoding.GetString(receiveData);

                //Adding message to listbox
                MessageList.Items.Add(response);

                //Loop !
                buffer = new byte[1024];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEp, new AsyncCallback(MessageCallBack), buffer);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIpAddress.Text = ip.ToString();
            txtPort.Text = port.ToString();
            //setup socket
            sck = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //convert string message to byte[]
            ASCIIEncoding ascencoding = new ASCIIEncoding();
            byte[] sendmess = new byte[1500];
            sendmess = ascencoding.GetBytes(textMessage.Text);
            sck.Send(sendmess);
            MessageList.Items.Add("You Said:" + textMessage.Text);
            textMessage.Text = "";
        }

        private void MessageList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
