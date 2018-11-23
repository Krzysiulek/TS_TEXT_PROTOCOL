using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace server_tcp
{
    class MainClass
    {
        private static Int32 port = 8080;
        private static string IP = "127.0.0.1";

        public static void Main(string[] args)
        {

            Server server = new Server(IP, port);

            while (true)
            {
                server.Listen();
            }

        }
    }
}
