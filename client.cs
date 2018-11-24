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
        private NetworkStream serverStream;
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
            Console.WriteLine("Write your message: ");
            serverStream = client.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(Console.ReadLine());
            serverStream.Write(outStream, 0, outStream.Length);
            Console.WriteLine("Size: {0}", outStream.Length);
            serverStream.Flush();
        }

        public void Receive()
        {
            try
            {
                serverStream = client.GetStream();
                byte[] receivedData = new byte[4096];

                if (serverStream.DataAvailable)
                {
                    serverStream.Read(receivedData, 0, receivedData.Length);
                    String text = Encoding.ASCII.GetString(receivedData); //odebrany tekst od klienta
                    text = text.Substring(0, text.IndexOf('\0'));
                    Console.WriteLine("Text from server: {0}", text);
                    serverStream.Flush();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
