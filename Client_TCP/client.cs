using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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
        int ID = 0;

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
            serverStream = client.GetStream();
            byte[] buffer;
            //jezeli klient nie ma nadanego ID
            if (ID == 0)
            {
                string tmp = DataOperations.SetData("reqID", "cor", 0); //komunikat - żądanie o ID
                buffer = Encoding.ASCII.GetBytes(tmp); //przekształcenie komunikatu w byte[] 
                serverStream.Write(buffer, 0, buffer.Length); //wysłanie
                return;
            }
            //////////////////////////////////

            string text = SendManage(); //tu się dzieją operacje

            //jezeli nie zwrocilo null'a, no to wysyła
            if(text != null){
                byte[] outStream = Encoding.ASCII.GetBytes(text);
                serverStream.Write(outStream, 0, outStream.Length);
            }
            else{
                return;
            }

            //tutaj zamykam
            if (DataOperations.GetOP(text) == op.discon)
            {
                Close();
            }

        }

        int GetA1(){
            int A1;
            Console.WriteLine("Give me argument 1");
            try{
                A1 = Convert.ToInt32(Console.ReadLine());
                return A1;
            }
            catch(Exception e){
                Console.WriteLine("Something went wrong...");
                Console.WriteLine(e);
                return GetA1();
            }
        }

        int GetA2()
        {
            int A2;
            Console.WriteLine("Give me argument 2");
            try
            {
                A2 = Convert.ToInt32(Console.ReadLine());
                return A2;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong...");
                Console.WriteLine(e);
                return GetA2();
            }
        }

        //metoda do zwracania co ma być wysłane w zależności od wejściowego polecenia
        private string SendManage(){
            Console.WriteLine("What do you want to do: ");
            string text = Console.ReadLine();
            Console.WriteLine(text);
            //sprawdzamy jaka operacje ma wykonac
            if(text == op.pow){
                Console.WriteLine(op.pow + " A1 ^ A2");
                return DataOperations.SetData(op.pow, "cor", ID, GetA1(), GetA2());
            }
            else if(text == op.log){
                Console.WriteLine(op.log + " log A1 (A2)");
                return DataOperations.SetData(op.log, "cor", ID, GetA1(), GetA2());
            }

            else if(text == op.add){
                Console.WriteLine(op.add + " A1 + A2");
                return DataOperations.SetData(op.add, "cor", ID, GetA1(), GetA2());
            }
            else if(text == op.fac){
                Console.WriteLine(op.fac + " A1!");
                return DataOperations.SetData(op.fac, "cor", ID, GetA1());
            }
            else if(text == op.hisID){
                Console.WriteLine("All history request sent...");
                return DataOperations.SetData(op.hisID, "cor", ID);
            }
            else if(text == op.hisOP){
                Console.WriteLine("Which operation are you interested in?");
                return DataOperations.SetData(op.hisOP, "cor", ID, GetA1());
            }
            else if(text == op.discon){
                Console.WriteLine("Disconnecting...");
                return DataOperations.SetData(op.discon, "cor", ID);
            }

            else{
                Console.WriteLine("Incorrect operation. Available operations:" +
                                  "\n\t{0}" +
                                  "\n\t{1}" +
                                  "\n\t{2}" +
                                  "\n\t{3}" +
                                  "\n\t{4}" +
                                  "\n\t{5}" +
                                  "\n\t{6}",
                                  op.add,
                                  op.fac,
                                  op.log,
                                  op.pow,
                                  op.hisID,
                                  op.hisOP,
                                  op.discon);
                return null;
            }
        }

        private void ReceiveManage(string text)
        {
            //otrzymano poprawne dane
            if(DataOperations.GetST(text) == "cor"){
                //pobieranie ID
                if(DataOperations.GetOP(text) == op.setID){
                    ID = DataOperations.GetID(text);
                    Console.WriteLine(ID);
                }
                //dla POW
                else if(DataOperations.GetOP(text) == op.pow)
                {
                    Console.WriteLine("#{0} {1}: {2}^{3} = {4}",
                                      DataOperations.GetIO(text),
                                      op.pow,
                                      DataOperations.GetA1(text),
                                      DataOperations.GetA2(text),
                                      DataOperations.GetDT(text));
                }

                //dla LOG
                else if (DataOperations.GetOP(text) == op.log)
                {
                    Console.WriteLine("#{0} {1}: log{2}({3}) = {4}",
                                      DataOperations.GetIO(text),
                                      op.log,
                                      DataOperations.GetA1(text),
                                      DataOperations.GetA2(text),
                                      DataOperations.GetDT(text));
                }

                //dla ADD
                else if (DataOperations.GetOP(text) == op.add)
                {
                    Console.WriteLine("#{0} {1}: {2} + {3} = {4}",
                                      DataOperations.GetIO(text),
                                      op.add,
                                      DataOperations.GetA1(text),
                                      DataOperations.GetA2(text),
                                      DataOperations.GetDT(text));
                }

                //dla FAC
                else if (DataOperations.GetOP(text) == op.fac)
                {
                    Console.WriteLine("#{0} {1}: {2}! = {3}",
                                      DataOperations.GetIO(text),
                                      op.fac,
                                      DataOperations.GetA1(text),
                                      DataOperations.GetDT(text));
                }
                //dla hisOP
                else if (DataOperations.GetOP(text) == op.hisOP)
                {
                    Console.WriteLine("HIS ID");
                    string text2 = DataOperations.GetDT(text);
                    Console.WriteLine("#{0} OP:{1} A1={2} A2={3} Equals={4}",
                                      DataOperations.GetIO(text2),
                                      DataOperations.GetOP(text2),
                                      DataOperations.GetA1(text2),
                                      DataOperations.GetA2(text2),
                                      DataOperations.GetDT(text2));
                }

                //historiaID
                else if(DataOperations.GetOP(text) == op.hisID){
                    Console.WriteLine("HIS ID");
                    string text2 = DataOperations.GetDT(text);
                    Console.WriteLine("#{0} OP: {1}, A1:{2} A2:{3} Equals = {4}\n\n",
                                      DataOperations.GetIO(text2),
                                      DataOperations.GetOP(text2),
                                      DataOperations.GetA1(text2),
                                      DataOperations.GetA2(text2),
                                      DataOperations.GetDT(text2));
                }

            }
            //otrzymano niepoprawne dane
            else if(DataOperations.GetST(text) == "inc"){
                Console.WriteLine("Somethinh went wrong... Result Infinity(?)");
            }
        }

        //odbieranie
        public void Receive()
        {
            while (true)
            {

                serverStream = client.GetStream();
                byte[] receivedData = new byte[4096];
                if (serverStream.DataAvailable)
                {
                    serverStream.Read(receivedData, 0, receivedData.Length);
                    String text = Encoding.ASCII.GetString(receivedData); //odebrany tekst od serwera
                    text = text.Substring(0, text.IndexOf('\0'));
                    //Console.WriteLine("Server: {0}", text);
                    ReceiveManage(text);
                }
                Thread.Sleep(1000);
            }
        }

        private void Close()
        {
            client.Close();
        }
    }
}
