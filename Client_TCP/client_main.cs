using System;
using System.Threading;

namespace client_tcp
{
    class MainClass
    {
        private static readonly Int32 port = 8080;
        //private static readonly string IP = "192.168.0.24";
        private static readonly string IP = "127.0.0.1";

        public static void Main(string[] args)
        {
            //string tmp = DataOperations.SetData("dodaj", 1, 2, "cor", 123, "11");
            //Console.WriteLine(DataOperations.GetDT(tmp));
            Client client = new Client(IP, port);


            while(true)
            {
                    client.Send();
                    client.Receive();
            }
        }
    }
}
/*
 * SPRAWDŹ REGEX DT
 * 
 * 
 */