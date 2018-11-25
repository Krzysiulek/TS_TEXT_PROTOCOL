using System;
using System.Threading;

namespace server_tcp
{
    class MainClass
    {
        private static readonly Int32 port = 8080;
        private static readonly string IP = "127.0.0.1";

        public static void Main(string[] args)
        {
            Server server = new Server(IP, port);

            while(true)
            {
                    server.Listen();
            }
        }
    }
}
