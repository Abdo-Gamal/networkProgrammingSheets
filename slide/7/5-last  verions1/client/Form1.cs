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
            ip = IPAddress.Parse(stringData_ip) ;
            int recv_port = sock.ReceiveFrom(data, ref ep);
            string stringData_port = Encoding.UTF8.GetString(data, 0, recv_port);
 
            port = int.Parse(stringData_port);
            sock.Close();
            txtPort.Text = port.ToString();
            txtIpAddress.Text = ip.ToString();

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

           // sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remoteEp, new AsyncCallback(MessageCallBack), buffer);
            new Thread(new ParameterizedThreadStart(ReadData)).Start(sck);

        }
        public void ReadData(object socket)
        {
            try
            {
                Socket Msoc = socket as Socket;
                NetworkStream ns = new NetworkStream(Msoc);
                 BinaryReader br = new BinaryReader(ns);
                while (true)
                {
                    string message = br.ReadString();
                    if (message == "Bye")
                    {
                        break;
                    }else if (message == "image")
                    {
                        MessageList.Items.Add("image from server " + ":");
                        recive_image(br);
                    } else
                    {
                        MessageList.Items.Add("from server " + ":");
                        MessageList.Items.Add(message + ".");
                    }
                }
                MessageBox.Show("Disconnect");
                br.Close();
                sck.Close();
            }
            catch (Exception ff)
            {
                MessageBox.Show(ff.Message);
            }
        }
       
      
         private void button1_Click(object sender, EventArgs e)
        {
            //convert string message to byte[]
            //ASCIIEncoding ascencoding = new ASCIIEncoding();
            //byte[] sendmess = new byte[1500];
            BinaryWriter br = new BinaryWriter(new NetworkStream(sck));
            br.Write(textMessage.Text);
            //sendmess = ascencoding.GetBytes(textMessage.Text);
            //sck.Send(sendmess);
            MessageList.Items.Add("You Said:" + textMessage.Text);
            textMessage.Text = "";
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
                BinaryWriter br = new BinaryWriter(new NetworkStream(sck));
                br.Write("image");
                br.Write(len);
                br.Write(byteArray);

                MessageList.Items.Add("You seed image: \n" + imageName);
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
    }
}
