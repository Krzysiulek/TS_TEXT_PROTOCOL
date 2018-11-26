using System;
using System.Collections.Generic;

namespace client_tcp
{
    public class history
    {
        public String operation;
        public List<String> hist;

        public history(){}

        public void add(String op)
        {
            hist.Add(op);
        }

        public void print()
        {
            foreach(String s in hist)
            {
                Console.WriteLine(s);
            }
        }
    }
}
// history dodawania = new history(dodaj)
// dodawania.add
