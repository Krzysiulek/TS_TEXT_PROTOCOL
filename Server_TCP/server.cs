using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

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
            Console.WriteLine("What do you want to do?");
            Console.WriteLine();
            while (true)
            {

                try
                {
                    byte[] receivedData = new byte[4096];

                    if (stream.DataAvailable)
                    {
                        stream.Read(receivedData, 0, receivedData.Length);
                        string text = Encoding.ASCII.GetString(receivedData); //odebrany tekst od klienta
                        text = text.Substring(0, text.IndexOf('\0'));
                        Console.WriteLine("Text from client: {0}", text);
                        OperateRequest(text);
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
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
            if (!clientData.getID())
            {
                Console.WriteLine("Sending ID... ");
                int id = ID();
                text = DataOperations.SetData(op.SetId, 0, 0, "cor", id, id.ToString());
                clientData.setID(id);
                History.add(text);
                Send(text);
            }
            else
            {
                string operation = DataOperations.GetOP(text);
                int a1, a2;
                int a1_, a2_;
                String op_, buff_;
                a1 = DataOperations.GetA1(text);
                a2 = DataOperations.GetA2(text);
                Dictionary<int, String> hist = new Dictionary<int, String>();
                string to_send, buff;
                Console.WriteLine("Operation {0}", operation); ;

                switch (operation)
                {
                    case "nadid":
                        Console.WriteLine("Sending ID... ");
                        int id = ID();
                        text = DataOperations.SetData(op.SetId, 0, 0, "cor", id, id.ToString());
                        clientData.setID(id);
                        History.add(text);
                        Send(text);
                        break;
                    case "dodaj":
                        buff = Operations.Add(a1, a2);
                        if (buff == "Infinity")
                        {
                            Console.WriteLine("To big number!");
                            to_send = DataOperations.SetData(op.add, a1, a2, "inc", clientData.getIDint(), buff); //incorrect
                        }
                        else
                        {
                            to_send = DataOperations.SetData(op.add, a1, a2, "cor", clientData.getIDint(), buff);//correct
                        }
                        History.add(to_send);
                        Send(to_send);
                        break;
                    case "poteguj":
                        buff = Operations.Pow(a1, a2);
                        if (buff == "Infinity")
                        {
                            Console.WriteLine("To big number!");
                            to_send = DataOperations.SetData(op.pow, a1, a2, "inc", clientData.getIDint(), buff);
                        }
                        else
                        {
                            to_send = DataOperations.SetData(op.pow, a1, a2, "cor", clientData.getIDint(), buff);
                        }
                        History.add(to_send);
                        Send(to_send);
                        break;
                    case "logarytmuj":
                        buff = Operations.Log(a1, a2);
                        if (buff == "Infinity")
                        {
                            Console.WriteLine("To big number!");
                            to_send = DataOperations.SetData(op.log, a1, a2, "inc", clientData.getIDint(), buff);
                        }
                        else
                        {
                            to_send = DataOperations.SetData(op.log, a1, a2, "cor", clientData.getIDint(), buff);
                        }
                        History.add(to_send);
                        Send(to_send);
                        break;
                    case "silnia":
                        buff = Operations.Fac(a1, a2);
                        if (buff == "Infinity")
                        {
                            Console.WriteLine("To big number!");
                            to_send = DataOperations.SetData(op.fac, a1, a2, "inc", clientData.getIDint(), buff);
                        }
                        else
                        {
                            to_send = DataOperations.SetData(op.fac, a1, a2, "cor", clientData.getIDint(), buff);
                        }
                        History.add(to_send);
                        Send(to_send);
                        break;
                    case "historyid":
                        hist = History.getHist();
                        if (hist.Count != 0)
                        {
                            foreach (KeyValuePair<int, string> entry in hist)
                            {
                                a1_ = DataOperations.GetA1(entry.Value);
                                a2_ = DataOperations.GetA1(entry.Value);
                                op_ = DataOperations.GetOP(entry.Value);
                                buff_ = DataOperations.GetDT(entry.Value);
                                to_send = DataOperations.SetData(op_, a1_, a2_, "cor", clientData.getIDint(), buff_);
                                Send(to_send);
                            }
                        }
                        else
                        {
                            to_send = DataOperations.SetData("", 0, 0, "inc", clientData.getIDint(), "empty");
                            Send(to_send);
                        }
                        break;
                    case "historyop":
                        buff = History.getHistID(a1);
                        if (buff == "0")
                        {
                            to_send = DataOperations.SetData("none", 0, 0, "inc", clientData.getIDint(), "0");
                            Send(to_send);
                        }
                        else
                        {
                            a1_ = DataOperations.GetA1(buff);
                            a2_ = DataOperations.GetA1(buff);
                            op_ = DataOperations.GetOP(buff);
                            buff_ = DataOperations.GetDT(buff);
                            to_send = DataOperations.SetData(op_, a1_, a2_, "cor", clientData.getIDint(), buff_);
                            Send(to_send);
                        }
                        break;
                    case "disconnect":
                        clientData.setConnected(false);
                        break;
                    default:
                        Console.WriteLine("Incorrect command");
                        Send("Incorrect command");
                        break;
                }
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

        private int ID()
        {
            Random rand = new Random();
            return rand.Next(255);

        }
    }
}
