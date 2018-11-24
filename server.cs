using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server_tcp
{
    public class Server
    {

        private readonly Int32 port;//port
        private readonly string IP;//IP 
        private NetworkStream stream;
        private TcpClient client;//klient od ktorego odbieramy
        private TcpListener listener;//serwer



        public Server(string IP_, Int32 port_)
        {
            this.IP = IP_;
            this.port = port_;
            this.listener = new TcpListener(IPAddress.Parse(IP), port);
            this.client = default(TcpClient);

            Start();
        }
        private void Start()
        {
            listener.Start();//poczatek słuchania
            Console.WriteLine("Server is working!");
            this.client = listener.AcceptTcpClient(); //akceptowanie połączonego klientaTCP
            Console.WriteLine("Connection request accepted!");
        }

        public void Listen()
        {
            try
            {
                stream = client.GetStream(); //łącze z klientem, od ktorego odbieramy dane i do ktorego pozniej bedziemy wysylali dane 
                byte[] receivedData = new byte[4096];

                if (stream.DataAvailable)
                {
                    stream.Read(receivedData, 0, receivedData.Length);
                    String text = Encoding.ASCII.GetString(receivedData); //odebrany tekst od klienta
                    text = text.Substring(0, text.IndexOf('\0'));
                    Console.WriteLine("Text from client: {0}", text);
                    stream.Flush();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public void Send()
        {
            Console.WriteLine("Write your message: ");
            stream = client.GetStream(); //łącze z klientem, od ktorego odbieramy dane i do ktorego pozniej bedziemy wysylali dane 
            byte[] outStream = Encoding.ASCII.GetBytes(Console.ReadLine());
            stream.Write(outStream, 0, outStream.Length);
            Console.WriteLine("Size: {0}", outStream.Length);
            stream.Flush();
        }
    }
}
