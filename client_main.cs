using System;

namespace client_tcp
{
    class MainClass
    {
        private static Int32 port = 8080;
        private static string IP = "127.0.0.1";

        public static void Main(string[] args)
        {
            Client client = new Client(IP, port);

            while(true)
            {
                Console.WriteLine("Write your message: ");
                client.Send();
            }
        }
    }
}
