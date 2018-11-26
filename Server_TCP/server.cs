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
        ClientData clientData = new ClientData();

        DataOperations dataOperations = new DataOperations(); //operacje, dzieki ktorym tworzymy ciag znakow
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
                text = dataOperations.SetData(op.SetId , 0, 0, "cor", id, id.ToString());
                clientData.setID(id);
                Send(text);
            }
            else
            {
                string operation = dataOperations.GetOP(text);
                int a1, a2;
                a1 = dataOperations.GetA1(text);
                a2 = dataOperations.GetA2(text);
                string to_send, buff;
                Console.WriteLine("Operation {0}",operation);;

                switch (operation)
                {
                    case "Dodaj":                       
                        buff = Operations.add(a1, a2);
                        to_send = dataOperations.SetData(op.add, 0, 0, "cor", clientData.getIDint(), buff);
                        Send(to_send);
                        break;
                    case "Poteguj":
                        break;
                    case "Logarytmuj":
                        break;
                    case "Silnia":
                        break;
                    case "HistoryID":
                        break;
                    case "HistoryOP":
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
