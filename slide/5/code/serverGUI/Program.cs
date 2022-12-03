using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
class AsyncTcpSrvr : Form
{
    private TextBox conStatus;
    private ListBox results;
    private byte[] data = new byte[1024];
    private int size = 1024;
    private Socket server;
    public AsyncTcpSrvr()
    {
        Text = "Asynchronous TCP Server";
        Size = new Size(400, 380);
        results = new ListBox();
        results.Parent = this;
        results.Location = new Point(10, 65);
        results.Size = new Size(350, 20 * Font.Height);
        Label label1 = new Label();
        label1.Parent = this;
        label1.Text = "Text received from client:";
        label1.AutoSize = true;
        label1.Location = new Point(10, 45);
        Label label2 = new Label();
        label2.Parent = this;
        label2.Text = "Connection Status:";
        label2.AutoSize = true;
        label2.Location = new Point(10, 330);
        conStatus = new TextBox();
        conStatus.Parent = this;
        conStatus.Text = "Waiting for client...";
        conStatus.Size = new Size(200, 2 * Font.Height);
        conStatus.Location = new Point(110, 325);
        Button stopServer = new Button();
        stopServer.Parent = this;
        stopServer.Text = "Stop Server";
        stopServer.Location = new Point(260, 32);
        stopServer.Size = new Size(7 * Font.Height, 2 * Font.Height);
        stopServer.Click += new EventHandler(ButtonStopOnClick);
        ///////////////////////////////////////////////////////////////
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
        Socket client = oldserver.EndAccept(iar);

        conStatus.Text = "Connected to: " + client.RemoteEndPoint.ToString();
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
            conStatus.Text = "Waiting for client...";
            server.BeginAccept(new AsyncCallback(AcceptConn), server);
            return;
        }

        string receivedData = Encoding.ASCII.GetString(data, 0, recv);
        results.Items.Add(receivedData);
        byte[] message2 = Encoding.ASCII.GetBytes(receivedData);
        client.BeginSend(message2, 0, message2.Length, SocketFlags.None,
               new AsyncCallback(SendData), client);
    }
    void ButtonStopOnClick(object obj, EventArgs ea)
    {
        Close();
    }
   
    public static void Main()
    {
        Application.Run(new AsyncTcpSrvr());
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // AsyncTcpSrvr
            // 
            this.ClientSize = new System.Drawing.Size(493, 320);
            this.Name = "AsyncTcpSrvr";
            this.ResumeLayout(false);

    }
}

