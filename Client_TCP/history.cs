using System;
using System.Collections.Generic;


namespace server_tcp
{
    public class history
    {
        public int i = 1;
        public String operation;
        public Dictionary<int, String> hist = new Dictionary<int, string>();

        public history() { }

        public void add(String op)
        {
            hist.Add(i, op);
            Console.WriteLine("New operation added id: {0} text: {1}", i, op);
            i++;
        }
        public void printID(int id)
        {
            foreach (KeyValuePair<int, string> entry in hist)
            {
                if (DataOperations.GetID(entry.Value) == id)
                    Console.WriteLine(entry.Value);
            }
        }

        public void printOP(int key)
        {
            foreach (KeyValuePair<int, string> entry in hist)
            {
                if (entry.Key == key)
                {
                    Console.WriteLine(entry.Value);
                    Console.WriteLine(entry.Key);
                }
            }
        }

        public Dictionary<int, String> getHist()
        {
            return hist;
        }

        public String getHistID(int i)
        {
            if (hist.Count != 0)
            {
                foreach (KeyValuePair<int, string> entry in hist)
                {
                    if (DataOperations.GetA1(entry.Value) == i)
                        return entry.Value;
                }
                return "0";
            }
                return "0";
        }
    }
}
// history dodawania = new history(dodaj)
// dodawania.add
