using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class SimpleTcpSrvr
{
    public static void Main()
    {
        int recv;
        byte[] data = new byte[1024];
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        Socket newsock = new Socket(AddressFamily.InterNetwork,
        SocketType.Stream, ProtocolType.Tcp);
        newsock.Bind(ipep);
        newsock.Listen(10);

        Console.WriteLine("Waiting for a client...");
        Socket client = newsock.Accept();
        IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint; string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        client.Send(data);

        while (true)
        {
            data = new byte[1024];
            recv = client.Receive(data);
            if (recv == 0)
                break;
            Console.WriteLine(Encoding.ASCII.GetString(data, 0,
            recv));
            client.Send(data, recv, SocketFlags.None);
        }
        Console.WriteLine("Disconnected from {0}", clientep.Address);
        client.Close();
        newsock.Close();
    }
}