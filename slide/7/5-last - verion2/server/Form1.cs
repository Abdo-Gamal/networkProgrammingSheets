using System;
using System.Collections;
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
        List<BinaryWriter> lstSoc = new List<BinaryWriter>();
        ArrayList lstID = new ArrayList();

        Thread AdvertiseThread;
        Socket mainSoc;                      //tcp socket
        int PortNum = 3000;                 //tcp listen socket
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
           
            Application.ExitThread();

            Environment.Exit(Environment.ExitCode);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AdvertiseThread = new Thread(new ThreadStart(Advetise));
            AdvertiseThread.IsBackground = true;
            AdvertiseThread.Priority = ThreadPriority.Lowest;
            AdvertiseThread.Start();

            // lets do our main work, serving
            mainSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mainSoc.Bind(new IPEndPoint(IPAddress.Any, PortNum));
            mainSoc.Listen(10);
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
        int count = 0;
          public void ReadData(object socket)
          {
            try
            {
                Socket Msoc = socket as Socket;

                ++count;
                lstSoc.Add(new BinaryWriter(new NetworkStream(Msoc)));
                lstID.Add("Client #" + count.ToString());

                conn_client.Items.Add("Client #" + count.ToString());
                conn_client.SelectedIndex = 0;

                lstSoc[count-1].Write(" you are Client #" + count.ToString());
                ///////////////////
                lstSoc[count - 1].Write("allClient");
                lstSoc[count - 1].Write(count);
                for (int i = 0; i < count; i++)
                {
                    lstSoc[count - 1].Write("Client #" +(i+1).ToString());

                }
               // lstSoc[count - 1].Write("Client #2" );

                ///////////////////

                NetworkStream ns = new NetworkStream(Msoc);
                BinaryReader br = new BinaryReader(ns);
                while (Msoc.Connected)
                {
                   string message = br.ReadString();
                    if (message == "Bye")
                    {
                        conn_client.Items.Remove(0);
                        break;
                    }else if (message == "image")
                    {
                        listBox2.Items.Add("image from client " + (count ).ToString() + ":");
                     //   int x = Find(message);
                        recive_image(br);
                    }
                    else
                    {
                        int x=Find(message);
                        message = br.ReadString();
                        lstSoc[x].Write(message);
                        listBox2.Items.Add("from client " + x.ToString());
                        listBox2.Items.Add(message + ".");
                    }
                }
                MessageBox.Show("Disconnect");
                br.Close();
                Msoc.Close();
            }
            catch (Exception ff)
            {
                MessageBox.Show(ff.Message);
            }
          }
        void recive_image(BinaryReader br)
        {
            try
            {
             
                // start recievinng the image data
               // data = new byte[6];

                int len = br.ReadInt32();
               // isize = BitConverter.ToInt32(data, 0);
                byte[] data = new byte[len];
                byte[] buf = new byte[1024];
                for (int i = 0; i < len / 1024; i++)
                {
                     int recv = br.Read(buf, 0, 1024);
                    buf.CopyTo(data, i * 1024);
                }
                buf = new byte[len % 1024];
                if (len % 1024 != 0)
                {
                    int recv = br.Read(buf, 0, buf.Length);
                    buf.CopyTo(data, (len / 1024) * 1024);
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
        private void button2_Click(object sender, EventArgs e)
        {
            int x = Find(conn_client.SelectedItem.ToString());

            listBox2.Items.Add("Server ~ " + textBox1.Text);

            lstSoc[x].Write(textBox1.Text);

            textBox1.Text = "";
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
                //////////////////////////////////////////////////////send image
                //BinaryWriter br = new BinaryWriter(new NetworkStream(sck));
                lstSoc[count - 1].Write("image");
                lstSoc[count - 1].Write(len);
                lstSoc[count - 1].Write(byteArray);

                listBox2.Items.Add("You send image: \n" + imageName);
                //length
                //  sck.Send(len, 0, len.Length, SocketFlags.None);
                //image
                //sck.Send(byteArray, 0, byteArray.Length, SocketFlags.None);
                ////////////////////////////////////////////////////

                MemoryStream ms2 = new MemoryStream(byteArray);
                Image image1 = Image.FromStream(ms2);
                pictureBox1.Image = image1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int Find(string from)
        {
            from = from.Trim();

            for (int i = 0; i < lstID.Count; i++)
            {
                if (lstID[i] as string == from)
                    return i;
            }
            return -1;
        }
    }
}
