using System;
using System.Net;
using System.Net.Sockets;

namespace server_tcp
{
    public class ClientData
    {
        int ID;
        bool IDset = false;
        bool connected = false;

        public ClientData()
        {
            connected = true;
            IDset = false;
        }

        public bool getID()
        {
            return IDset;
        }

        public int getIDint()
        {
            return ID;
        }

        public void setID(int ID_)
        {
            ID = ID_;
            IDset = true;
        }
    }
}
