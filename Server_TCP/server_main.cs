using System;
using System.Threading;

namespace server_tcp
{
    class MainClass
    {
        private static readonly Int32 port = 8080;
        private static readonly string IP = "127.0.0.1";
        //private static readonly string IP = "192.168.43.32";
        public static void Main(string[] args)
        {
            string tmp = Operations.Pow(999999999, 999999999);
            Console.WriteLine(tmp);


            Server server = new Server(IP, port);
            Thread t1 = new Thread(server.PrintHistory);
            t1.Start();

            while(true)
            {
                    server.Listen();
            }
        }
    }
}
