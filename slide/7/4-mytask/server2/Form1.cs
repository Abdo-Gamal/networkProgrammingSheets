using System;
using System.Collections;
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

namespace server2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<BinaryWriter> lstSoc = new List<BinaryWriter>();//socket foreach client
        ArrayList lstID = new ArrayList();                   //
        Thread AdvertiseThread;
        Socket mainSoc;                      //tcp socket
        int PortNum = 3000;                 //tcp listen socket
        int count = 0;
        ManualResetEvent mre = new ManualResetEvent(false);
        public void Advetise()
        {
            Socket sock = new Socket(AddressFamily.InterNetwork,
            SocketType.Dgram, ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.Socket,
            SocketOptionName.Broadcast, 1);
            IPEndPoint broadCast = new IPEndPoint(IPAddress.Broadcast, 5001);//udp listen socket
            IPHostEntry ipse = Dns.GetHostByName(Dns.GetHostName());
            IPAddress myself = ipse.AddressList[0];
            //  byte[] data = Encoding.ASCII.GetBytes(myself.ToString());
            while (true)
            {
                sock.SendTo(Encoding.UTF8.GetBytes(myself.ToString()), broadCast);
                sock.SendTo(Encoding.UTF8.GetBytes(PortNum.ToString()), broadCast);
                Thread.Sleep(60);
            }
        }
      
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < lstSoc.Count; i++)
            {
                lstSoc[i].Write("Server~Bye");
            }

            Application.ExitThread();

            Environment.Exit(Environment.ExitCode);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            AdvertiseThread = new Thread(new ThreadStart(Advetise));
            AdvertiseThread.IsBackground = true;
            AdvertiseThread.Priority = ThreadPriority.Lowest;
            AdvertiseThread.Start();

            // lets do our main work, serving
            mainSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mainSoc.Bind(new IPEndPoint(IPAddress.Any, PortNum));
           mainSoc.Listen(-1);
            new Thread(new ThreadStart(StartServer)).Start();
            button1.Enabled = false;
        }
        public void StartServer()
        {

            while (true)
            {
                mre.Reset();

                mainSoc.BeginAccept(new AsyncCallback(AcceptClient), null);

                // wait to complete handling 
                mre.WaitOne();
            }
        }
        public void AcceptClient(IAsyncResult iar)
        {

            Socket client = mainSoc.EndAccept(iar);


            new Thread(new ParameterizedThreadStart(ReadData)).Start(client);

 
            //// done handling
            mre.Set();
        }
          public void ReadData(object socket)
        {
            try
            {

                Socket Msoc = socket as Socket;

                ++count;

                lstID.Add("Client #" + count.ToString());

                lstSoc.Add(new BinaryWriter(new NetworkStream(Msoc)));

                conn_client.Items.Add("Client #" + count.ToString());

                BinaryWriter bw = new BinaryWriter(new NetworkStream(Msoc));
                bw.Write( count.ToString());
                bw.Flush();
                BinaryReader reader = new BinaryReader(new NetworkStream(Msoc));

                string from, to, message, fullmessage;

                while (Msoc.Connected)
                {
                    fullmessage = reader.ReadString();

                    from = fullmessage.Split(new char[] { '~' })[0];

                    to = fullmessage.Split(new char[] { '~' })[1];

                    message = fullmessage.Split(new char[] { '~' })[2];

                    if (message == "Bye")
                    {
                        int x = Find(from);

                        lstID.RemoveAt(x);

                        lstID.TrimToSize();

                        lstSoc.RemoveAt(x);

                        conn_client.Items.Remove(from);

                        break;
                    }

                    else
                    {
                        listBox2.Items.Add(from + ":");

                        listBox2.Items.Add(message + ".");
                    }

                }
                MessageBox.Show("Disconnect");
                reader.Close();

                Msoc.Close();
            }
            catch (Exception ff)
            {
                MessageBox.Show(ff.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int x = Find(conn_client.SelectedItem.ToString());

            try
            {
                lstSoc[x].Write("Server~" + textBox1.Text);
            }
            catch (Exception ff) { MessageBox.Show(ff.Message); }
        }
        private int Find(string from)
        {
            for (int i = 0; i < lstID.Count; i++)
            {
                if (lstID[i] as string == from)
                    return i;
            }
            return -1;
        }

    }
}
