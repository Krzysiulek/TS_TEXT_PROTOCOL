using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;
using System.Threading;

namespace server_tcp
{
    public class Server
    {

        private readonly Int32 port;//port
        private readonly string IP;//IP 
        private NetworkStream stream;
        private TcpClient client;//klient od ktorego odbieramy
        private TcpListener listener;//serwer
        private history History = new history();
        ClientData clientData = new ClientData();
        OperStatus op = new OperStatus(); //konkretne operacje dodaj, poteguj itd

        public Server(string IP_, Int32 port_)
        {
            IP = IP_;
            port = port_;
            listener = new TcpListener(IPAddress.Parse(IP), port);
            Console.WriteLine("Listening...");
            listener.Start();//poczatek słuchania
            Console.WriteLine("Server is working!");
            Start();
        }

        private void Start()
        {
            client = listener.AcceptTcpClient(); //akceptowanie połączonego klientaTCP
            Console.WriteLine("Connection request accepted!");
            stream = client.GetStream(); //łącze z klientem, od ktorego odbieramy dane i do ktorego pozniej bedziemy wysylali dane 
        }

        public void Listen()
        {
            //Console.WriteLine("What do you want to do?");
            //Console.WriteLine();
            try
            {
                byte[] receivedData = new byte[4096];

                if (stream.DataAvailable)
                {
                    stream.Read(receivedData, 0, receivedData.Length);
                    string text = Encoding.ASCII.GetString(receivedData); //odebrany tekst od klienta
                    text = text.Substring(0, text.IndexOf('\0'));
                    Console.WriteLine("Text from client: {0}", text);
                    OperateRequest(text); //przetwarzanie odebranego komunikatu
                }
                Thread.Sleep(100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private void Send(string text) //wysyłania odpowiedzi do klienta
        {
            byte[] outStream = Encoding.ASCII.GetBytes(text);
            stream.Write(outStream, 0, outStream.Length);
            Console.WriteLine("Size: {0}", outStream.Length);
            stream.Flush();
        }

        private void OperateRequest(string text)
        {
            //nadanie ID
            if (DataOperations.GetOP(text) == op.reqID)
            {
                clientData.setID(SetID());
                string ToSend = DataOperations.SetData(op.setID, "cor", clientData.getIDint());
                Send(ToSend);
                //return;
            }
            //POTEGOWANIE
            else if (DataOperations.GetOP(text) == op.pow)
            {
                int ActualID = DataOperations.GetID(text);
                int A1 = DataOperations.GetA1(text);
                int A2 = DataOperations.GetA2(text);
                string result = Operations.Pow(A1, A2);

                if (result == "Infinity")
                {
                    string ToSend = DataOperations.SetData(op.pow, "inc", ActualID, A1, A2, History.GetI(), result);
                    History.add(ToSend);
                    Send(ToSend);
                }
                else
                {
                    string ToSend = DataOperations.SetData(op.pow, "cor", ActualID, A1, A2, History.GetI(), result);
                    History.add(ToSend);
                    Send(ToSend);
                }
            }

            //LOGARYTM
            else if (DataOperations.GetOP(text) == op.log)
            {
                int ActualID = DataOperations.GetID(text);
                int A1 = DataOperations.GetA1(text);
                int A2 = DataOperations.GetA2(text);
                string result = Operations.Log(A1, A2);

                if (result == "Infinity")
                {
                    string ToSend = DataOperations.SetData(op.log, "inc", ActualID, A1, A2, History.GetI(), result);
                    History.add(ToSend);
                    Send(ToSend);
                }
                else
                {
                    string ToSend = DataOperations.SetData(op.log, "cor", ActualID, A1, A2, History.GetI(), result);
                    History.add(ToSend);
                    Send(ToSend);
                }
            }

            //DODAWANIE
            else if (DataOperations.GetOP(text) == op.add)
            {
                int ActualID = DataOperations.GetID(text);
                int A1 = DataOperations.GetA1(text);
                int A2 = DataOperations.GetA2(text);
                string result = Operations.Add(A1, A2);

                if (result == "Infinity")
                {
                    string ToSend = DataOperations.SetData(op.add, "inc", ActualID, A1, A2, History.GetI(), result);
                    History.add(ToSend);
                    Send(ToSend);
                }
                else
                {
                    string ToSend = DataOperations.SetData(op.add, "cor", ActualID, A1, A2, History.GetI(), result);
                    History.add(ToSend);
                    Send(ToSend);
                }
            }

            //SILNIA
            else if (DataOperations.GetOP(text) == op.fac)
            {
                int ActualID = DataOperations.GetID(text);
                int A1 = DataOperations.GetA1(text);
                string result = Operations.Fac(A1);

                if (result == "Infinity")
                {
                    string ToSend = DataOperations.SetData(op.fac, "inc", ActualID, A1, 0, History.GetI(), result);
                    History.add(ToSend);
                    Send(ToSend);
                }
                else
                {
                    string ToSend = DataOperations.SetData(op.fac, "cor", ActualID, A1, 0, History.GetI(), result);
                    History.add(ToSend);
                    Send(ToSend);
                }
            }

            //historiaID
            else if (DataOperations.GetOP(text) == op.hisID)
            {
                foreach (KeyValuePair<int, string> kvp in History.hist)
                {
                    //Console.WriteLine("K: {0}, V: {1}", kvp.Key, kvp.Value);
                    string ToSend = DataOperations.SetData(op.hisID, "cor", DataOperations.GetID(text), kvp.Value);
                    Send(ToSend);
                    Console.WriteLine("To send ForEach: " + ToSend);
                    Thread.Sleep(500);
                }
            }

            //historiaOP
            else if (DataOperations.GetOP(text) == op.hisOP)
            {
                foreach (KeyValuePair<int, string> kvp in History.hist)
                {
                    if (kvp.Key == DataOperations.GetA1(text))
                    {
                        string ToSend = DataOperations.SetData(op.hisOP, "cor", DataOperations.GetID(text), kvp.Value);
                        Send(ToSend);
                        Console.WriteLine("Sending OP history");
                    }
                }
            }

            else if (DataOperations.GetOP(text) == op.discon)
            {
                Console.WriteLine("Client disconnected. Deleting history");
                History.DeleteHistory();
            }

        }


        public void PrintHistory()
        {
            string i;
            int oper;
            while (true)
            {
                i = Console.ReadLine();
                switch (i)
                {
                    case "1":
                        Console.WriteLine("History by ID..");
                        History.printID(clientData.getIDint());
                        break;
                    case "2":
                        Console.WriteLine("History by operation..");
                        Console.WriteLine("which operation do you want?");
                        oper = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("pobrano " + oper);
                        History.printOP(oper);
                        break;
                    default:
                        Console.WriteLine("No such operation..");
                        break;
                }
            }
        }

        private void Stop()
        {
            listener.Stop();
        }

        private void Close()
        {
            client.Close();
        }

        private int SetID()
        {
            Random rand = new Random();
            return rand.Next(255);
        }
    }
}
