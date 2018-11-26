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
        Operations op = new Operations();
        int ID;

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
            string text = Console.ReadLine();
            text = ManageRequests(text); //Ta metoda zwraca poprawną Daną którą trxeba wysłać
            byte[] outStream = Encoding.ASCII.GetBytes(text);

            serverStream.Write(outStream, 0, outStream.Length);
            Console.WriteLine("Size: {0}", outStream.Length);
        }

        public string ManageRequests(string text){
            //dodawanie
            if(text == op.add){
                Console.WriteLine("Dodawanie (a1 + a2)...");
                int A1, A2;
                Console.WriteLine("Podaj argument A1");
                A1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj argument A2");
                A2 = Convert.ToInt32(Console.ReadLine());

                return DataOperations.SetData(op.add, A1, A2, "GR", ID, "");
            }
            //silnia z sumy
            else if(text == op.fac){
                Console.WriteLine("Silnia z sumy (a1 + a2)!");
                int A1, A2;
                Console.WriteLine("Podaj argument A1");
                A1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj argument A2");
                A2 = Convert.ToInt32(Console.ReadLine());

                return DataOperations.SetData(op.fac, A1, A2, "GR", ID, "");
            }
            //logarytm
            else if(text == op.log){
                Console.WriteLine("Logarytm (log A1 (A2) )!");
                int A1, A2;
                Console.WriteLine("Podaj argument A1");
                A1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj argument A2");
                A2 = Convert.ToInt32(Console.ReadLine());

                return DataOperations.SetData(op.log, A1, A2, "GR", ID, "");
            }
            else if(text == op.pow){
                Console.WriteLine("Potega A1 ^ A2!");
                int A1, A2;
                Console.WriteLine("Podaj argument A1");
                A1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Podaj argument A2");
                A2 = Convert.ToInt32(Console.ReadLine());

                return DataOperations.SetData(op.pow, A1, A2, "GR", ID, "");
            }
            else{
                Console.WriteLine("Niepoprawne dane. Mozliwe operacje to:\n{0}\n{1}\n{2}\n{3}",
                                  op.add,
                                  op.fac,
                                  op.log,
                                  op.pow);
                Send();
            }
            return "";
        }

        public void Receive()
        {
            while (true)
            {
                try
                {
                    serverStream = client.GetStream();
                    byte[] receivedData = new byte[4096];

                    if (serverStream.DataAvailable)
                    {
                        serverStream.Read(receivedData, 0, receivedData.Length);
                        String text = Encoding.ASCII.GetString(receivedData); //odebrany tekst od klienta
                        ID = DataOperations.GetID(text); //odświeżam ID
                        //text = text.Substring(0, text.IndexOf('\0'));
                        Console.WriteLine("Text from server: {0}", text);
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void Close()
        {
            client.Close();
        }
    }
}
