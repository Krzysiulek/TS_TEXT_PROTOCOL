using System;
using System.Collections.Generic;

namespace client_tcp
{
    public class history
    {
        public string operation;
        public List<string> hist = new List<string>();

        public history() { }

        public void Add(string op)
        {
            hist.Add(op);
        }

        public void printID(int id)
        {
            foreach (string s in hist)
            {
                if (DataOperations.GetID(s) == id){
                    Console.WriteLine("Operation: {0}, A1= {1}, A2= {2}, Equals = {3}\n",
                                      DataOperations.GetOP(s),
                                      DataOperations.GetA1(s),
                                      DataOperations.GetA2(s),
                                      DataOperations.GetDT(s)
                                     );
                }

            }
        }

        public void printCalculation(string calc){
            //dorobić drukowanie dla jednego obliczenia
        }

        public void printOP(string OP)
        {
            foreach (string s in hist)
            {
                if (DataOperations.GetOP(s) == OP)
                {
                    Console.WriteLine("Operation: {0}, A1= {1}, A2= {2}, Equals = {3}\n",
                                      DataOperations.GetOP(s),
                                      DataOperations.GetA1(s),
                                      DataOperations.GetA2(s),
                                      DataOperations.GetDT(s)
                                     );
                }
            }
        }
    }
}
// history dodawania = new history(dodaj)
// dodawania.add
