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
        history history = new history();
        int ID;

        //inicjalizacja
        public Client(string IP_, Int32 port_)
        {
            IP = IP_;
            port = port_;
            client = new TcpClient();
            client.Connect(IPAddress.Parse(IP), port);
        }

        //wysylanie
        public void Send()
        {
            if(ID == 0){
                serverStream = client.GetStream();
                serverStream.Write(Encoding.ASCII.GetBytes(DataOperations.SetData("nadID",0,0,"cor",0,"")), 0, 2);
                return;
            }
            Console.WriteLine("Write your message: ");
            serverStream = client.GetStream();
            string text = Console.ReadLine();
            text = ManageRequestsSend(text); //Ta metoda zwraca poprawną Daną którą trxeba wysłać
            byte[] outStream = Encoding.ASCII.GetBytes(text);

            serverStream.Write(outStream, 0, outStream.Length);
            Console.WriteLine("Size: {0}", outStream.Length);

            if (DataOperations.GetOP(text) == op.Disconnect)
            {
                Close();
                Console.WriteLine("Disconnected");
            }
        }

        public string ManageRequestsSend(string text){
            //dodawanie
            if(text == op.add){
                Console.WriteLine("Addition (a1 + a2)...");
                int A1, A2;
                Console.WriteLine("Give argument A1");
                A1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Give argument A2");
                A2 = Convert.ToInt32(Console.ReadLine());

                return DataOperations.SetData(op.add, A1, A2, "GR", ID, "");
            }
            //silnia z sumy
            else if(text == op.fac){
                Console.WriteLine("Sum factorial (a1 + a2)!");
                int A1, A2;
                Console.WriteLine("Give argument A1");
                A1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Give argument A2");
                A2 = Convert.ToInt32(Console.ReadLine());

                return DataOperations.SetData(op.fac, A1, A2, "GR", ID, "");
            }
            //logarytm
            else if(text == op.log){
                Console.WriteLine("Logarithm (log A1 (A2) )!");
                int A1, A2;
                Console.WriteLine("Give argument A1");
                A1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Give argument A2");
                A2 = Convert.ToInt32(Console.ReadLine());

                return DataOperations.SetData(op.log, A1, A2, "GR", ID, "");
            }
            //potegowanie
            else if(text == op.pow){
                Console.WriteLine("Power A1 ^ A2!");
                int A1, A2;
                Console.WriteLine("Give argument A1");
                A1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Give argument A2");
                A2 = Convert.ToInt32(Console.ReadLine());

                return DataOperations.SetData(op.pow, A1, A2, "GR", ID, "");
            }

            //drukowanie historii
            else if(text == op.GetHistoryID){
                Console.WriteLine("Your's ID is "+ ID + ".Your's operations:");
                history.printID(ID);

                Send();
            }
            else if (text == op.GetHistoryOP)
            {
                string OP;
                Console.WriteLine("Your history by Operations:");
                Console.WriteLine("What operation are you interested in?");
                OP = Console.ReadLine();
                history.printOP(OP);

                Send();
            }

            else if (text == op.Disconnect){
                Console.WriteLine("Disconneting...");
                return DataOperations.SetData(op.Disconnect, 0, 0, "", ID, "");
            }

            else
            {
                Console.WriteLine("Incorrect Data. Available operations:\n\t{0}\n\t{1}\n\t{2}\n\t{3}\n\t{4}\n\t{5}\n\t{6}",
                                  op.add,
                                  op.fac,
                                  op.log,
                                  op.pow,
                                  op.GetHistoryID,
                                  op.GetHistoryOP,
                                  op.Disconnect);
                Send();
            }
            return "";
        }

        //do obslugi odebranych danych
        public void ManageRequestsRecv(string text){
            history.Add(text); //dodawanie do historii

            if(DataOperations.GetST(text) == "cor"){
                Console.WriteLine("Result: OP = {0}, A1 = {1}, A2 = {2} equals = {3}",
                                  DataOperations.GetOP(text),
                                  DataOperations.GetA1(text),
                                  DataOperations.GetA2(text),
                                  DataOperations.GetDT(text));
            }
            else
            {
                Console.WriteLine("Result: OP = {0}, A1 = {1}, A2 = {2} equals = {3}",
                                  DataOperations.GetOP(text),
                                  DataOperations.GetA1(text),
                                  DataOperations.GetA2(text),
                                  "Przekroczono zakres zmiennej");
            }
        }

        //odbieranie
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
                        ManageRequestsRecv(text);
                        //Console.WriteLine("Text from server: {0}", text);
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
