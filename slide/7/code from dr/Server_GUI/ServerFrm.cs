using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Server_GUI
{
    public partial class ServerFrm : Form
    {
        Socket mainSoc;

        List<BinaryWriter> lstSoc = new List<BinaryWriter>();

        ArrayList lstID = new ArrayList();

        int count = 0;

        // I used this MRE to synch the asynch calls
        ManualResetEvent mre = new ManualResetEvent(false);

        public ServerFrm()
        {
            InitializeComponent();
        }

        Thread AdvertiseThread;
        int PortNum = 5000;
        public void Advetise()
        {
            // Advertise for a server operating on port 5000, note that the advetisments themselves are 
            //disseminated on port 5001 using the udp socket
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // I used my loopback address beacuse I'm tring offline in home
            //If U R trying in FCI use a real broadcast Address
            IPEndPoint broadCast = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5001);

            s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            //every 10 sec
            while (true)
            {
                s.SendTo(Encoding.UTF8.GetBytes(Dns.GetHostName() + ":" + PortNum.ToString()), broadCast);
                Thread.Sleep(100);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Let it go listen

            AdvertiseThread = new Thread(new ThreadStart(Advetise));

            AdvertiseThread.Priority = ThreadPriority.Lowest;

            AdvertiseThread.Start();


            // lets do our main work, serving
            mainSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            mainSoc.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), PortNum));

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

            Socket client = mainSoc.EndAccept(iar);//handel socket

            new Thread(new ParameterizedThreadStart(ReadData)).Start(client);

            // done handling
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

                listBox1.Items.Add("Client #" + count.ToString());

                new BinaryWriter(new NetworkStream(Msoc)).Write("Client #" + count.ToString());

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

                        listBox1.Items.Remove(from);

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

        private int Find(string from)
        {
            for (int i = 0; i < lstID.Count; i++)
            {
                if (lstID[i] as string == from)
                    return i;
            }
            return -1;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < lstSoc.Count; i++)
            {
                lstSoc[i].Write("Server~Bye");
            }

            Application.ExitThread();

            Environment.Exit(Environment.ExitCode);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int x = Find(listBox1.SelectedItem.ToString());

            try
            {
                lstSoc[x].Write("Server~" + textBox1.Text);
            }
            catch (Exception ff) { MessageBox.Show(ff.Message); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}