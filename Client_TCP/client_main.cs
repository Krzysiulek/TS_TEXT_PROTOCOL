using System;
using System.Threading;

namespace client_tcp
{
    class MainClass
    {
        private static readonly Int32 port = 8080;
        private static readonly string IP = "127.0.0.1";

        public static void Main(string[] args)
        {
            Client client = new Client(IP, port);

            while(true)
            {
                    client.Send();
                    client.Receive();
            }
        }
    }
}
