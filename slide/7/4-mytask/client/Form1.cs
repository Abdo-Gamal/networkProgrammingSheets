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

namespace client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Socket sck;
        EndPoint remoteEp;
        byte[] buffer;
        IPAddress ip ;
        int port ;
        int cnt;
        private void btnConnect_Click(object sender, EventArgs e)
        {

            Thread AdvertiseThread = new Thread(new ThreadStart(Rec_Advetise));
            AdvertiseThread.IsBackground = true;
            AdvertiseThread.Priority = ThreadPriority.Lowest;
            AdvertiseThread.Start();
         
        }
        public void Rec_Advetise()
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 5001);
            sock.Bind(iep);
            EndPoint ep = (EndPoint)iep;
            byte[] data = new byte[1024];
            int recv_ip = sock.ReceiveFrom(data, ref ep);
            string stringData_ip = Encoding.UTF8.GetString(data, 0, recv_ip);
            txtIpAddress.Text = stringData_ip.ToString();
            ip = IPAddress.Parse(stringData_ip) ;
            int recv_port = sock.ReceiveFrom(data, ref ep);
            string stringData_port = Encoding.UTF8.GetString(data, 0, recv_port);
 
            txtPort.Text = stringData_port;
            port = int.Parse(stringData_port);
            sock.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        private void connect_Click(object sender, EventArgs e)
        {
            remoteEp = new IPEndPoint(ip, port);
            sck.Connect(remoteEp);
            connect.Enabled = false;
            avlib_server.Enabled = false;
            buffer = new byte[1024];
            //sec client

            sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEp, new AsyncCallback(MessageCallBack), buffer);

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
                MessageList.Items.Add(response );
                cnt = int.Parse(response);
                //Loop !
                buffer = new byte[1024];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEp, new AsyncCallback(MessageCallBack), buffer);
                ////

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
      
private void button1_Click(object sender, EventArgs e)
        {
            //convert string message to byte[]
            ASCIIEncoding ascencoding = new ASCIIEncoding();
            byte[] sendmess = new byte[1500];
            BinaryWriter br = new BinaryWriter(new NetworkStream(sck));
            br.Write(textMessage.Text);
            //sendmess = ascencoding.GetBytes(textMessage.Text);
            //sck.Send(sendmess);
            MessageList.Items.Add("You Said:" + textMessage.Text);
            textMessage.Text = "";
        }

        private void showClient_Click(object sender, EventArgs e)
        {

        }
    }
}
