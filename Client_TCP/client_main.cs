using System;
using System.Threading;

namespace client_tcp
{
    class MainClass
    {
        private static readonly Int32 port = 8080;
        private static readonly string IP = "127.0.0.1";
        //private static readonly string IP = "192.168.43.32";

      
        public static void Main(string[] args)
        {
            string tmp2 = DataOperations.SetData("addID", "cor", 123, 32);
            string tmp = DataOperations.SetData("addID", "cor", 123, -12, -195, 197, "1234");
            Console.WriteLine("OP: {0}\n" +
                              "ST: {1}\n" +
                              "ID: {2}\n" +
                              "TS: {3}\n" +
                              "A1: {4}\n" +
                              "A2: {5}\n" +
                              "IO: {6}\n" +
                              "DT: {7}\n",
                              DataOperations.GetOP(tmp),
                              DataOperations.GetST(tmp),
                              DataOperations.GetID(tmp),
                              DataOperations.GetTS(tmp),
                              DataOperations.GetA1(tmp),
                              DataOperations.GetA2(tmp),
                              DataOperations.GetIO(tmp),
                              DataOperations.GetDT(tmp)
                                                  );



            Client client = new Client(IP, port);
            Thread receive = new Thread(client.Receive);
            receive.Start();
            client.Send();
            Thread.Sleep(2000);

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