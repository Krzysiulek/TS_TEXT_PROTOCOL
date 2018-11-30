using System;
class OperStatus
{
    public string reqID = "reqID";
    public string setID = "setID";
    public string pow = "potega";
    public string log = "logarytm";
    public string add = "dodawanie";
    public string fac = "silnia";
    public string hisID = "historiaID";
    public string hisOP = "historiaOP";
    public string discon = "disconnect";

}

namespace server_tcp
{
    public class Operations
    {
        public Operations() { }

        public static string Pow(int a1, int a2)
        {
            double x = Math.Pow(a1, a2);
            Console.WriteLine("x : " + x);
            return x.ToString();
        }

        public static string Log(int a1, int a2)
        {
            double x = Math.Log(a2, a1);
            return x.ToString();
        }

        public static string Fac(int a1)
        {
            return Factor(a1).ToString();
        }

        public static string Add(int a1, int a2)
        {
            return (a1 + a2).ToString();
        }

        private static double Factor(int number)
        {
            double result = 1;
            while (number != 1)
            {
                result = result * number;
                number = number - 1;
            }
            return result;
        }

    }

}