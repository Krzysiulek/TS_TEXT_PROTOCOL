using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client_tcp
{
    public class Client
    {
        private readonly Int32 port;
        private readonly string IP;
        public TcpClient client;

        public Client(string IP_, Int32 port_)
        {
            IP = IP_;
            port = port_;
            client = new TcpClient();
            client.Connect(IPAddress.Parse(IP), port);
        }

        public void Send()
        {

            NetworkStream serverStream = client.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(Console.ReadLine());
            serverStream.Write(outStream, 0, outStream.Length);
            Console.WriteLine("Size: {0}", outStream.Length);
            serverStream.Flush();
        }
    }
}
