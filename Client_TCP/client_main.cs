using System;
using System.Threading;

namespace client_tcp
{
    class MainClass
    {
        private static readonly Int32 port = 8080;
        //private static readonly string IP = "127.0.0.1";
        private static readonly string IP = "192.168.0.24";

      
        public static void Main(string[] args)
        {
            Client client = new Client(IP, port);
            Thread receive = new Thread(client.Receive);
            receive.Start();
            client.Send();
            Thread.Sleep(2000); //przeczekanie aż klient odbierze ID

            while(true)
            {
                Thread.Sleep(100);
                //client.Receive();
                client.Send();
            }
        }
    }
}
/*
 * SPRAWDŹ REGEX DT
 * 
 * 
 */