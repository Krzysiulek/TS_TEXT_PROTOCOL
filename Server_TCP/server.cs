using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
                text = DataOperations.SetData(op.SetId , 0, 0, "cor", id, id.ToString());
                clientData.setID(id);
                History.add(text);
                Send(text);
            }
            else
            {
                string operation = DataOperations.GetOP(text);
                int a1, a2;
                a1 = DataOperations.GetA1(text);
                a2 = DataOperations.GetA2(text);
                string to_send, buff;
                Console.WriteLine("Operation {0}",operation);;

                switch (operation)
                {
                    case "dodaj":                       
                        buff = Operations.Add(a1, a2);
                        to_send = DataOperations.SetData(op.add, 0, 0, "cor", clientData.getIDint(), buff);
                        History.add(to_send);
                        Send(to_send);
                        break;
                    case "poteguj":
                        buff = Operations.Pow(a1, a2);
                        to_send = DataOperations.SetData(op.pow, 0, 0, "cor", clientData.getIDint(), buff);
                        History.add(to_send);
                        Send(to_send);
                        break;
                    case "logarytmuj":
                        buff = Operations.Log(a1, a2);
                        to_send = DataOperations.SetData(op.log, 0, 0, "cor", clientData.getIDint(), buff);
                        History.add(to_send);
                        Send(to_send);
                        break;
                    case "silnia":
                        buff = Operations.Fac(a1, a2);
                        to_send = DataOperations.SetData(op.fac, 0, 0, "cor", clientData.getIDint(), buff);
                        History.add(to_send);
                        Send(to_send);
                        break;
                    default:
                        Console.WriteLine("Incorrect command");
                        Send("Incorrect command");
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
