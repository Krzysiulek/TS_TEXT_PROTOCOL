using System;
class OperStatus
{
    public String add = "dodaj";
    public String pow = "poteguj";
    public String log = "logarytmuj";
    public String fac = "silnia";
    public String SetId = "setId";
    public String GetHistoryID = "historyID";
    public String GetHistoryOP = "historyOP";
}

namespace server_tcp
{
    public class Operations
    {
        public Operations() { }

        public static string Pow(int a1, int a2)
        {
            double x = Math.Pow(a1, a2);
            return x.ToString();
        }

        public static string Log(int a1, int a2)
        {
            double x = Math.Log(a1, a2);
            return x.ToString();
        }

        public static string Fac(int a1, int a2)
        {
            return Factor(a1 + a2).ToString();
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