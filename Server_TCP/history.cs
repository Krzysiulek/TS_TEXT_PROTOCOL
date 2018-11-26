using System;
using System.Collections.Generic;

namespace server_tcp
{
    public class history
    {
        public String operation;
        public List<String> hist;

        public history() { }

        public void add(String op)
        {
            hist.Add(op);
        }

        public void printID(int id)
        {
            foreach (String s in hist)
            {
                if (DataOperations.GetID(s) == id)
                    Console.WriteLine(s);
            }
        }

        public void printOP(string OP)
        {
            foreach (String s in hist)
            {
                if (DataOperations.GetOP(s) == OP)
                    Console.WriteLine(s);
            }
        }
    }
}
// history dodawania = new history(dodaj)
// dodawania.add
