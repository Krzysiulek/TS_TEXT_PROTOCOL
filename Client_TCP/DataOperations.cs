using System;
using System.Text;
using System.Text.RegularExpressions;
    /*
    * 
    * 
        1 - string   * OP - pole operacji
        2 - string   * ST - pole statusu
        3 - int      * ID - pole identyfikatora
        4 - string   * TS - pole znacznika czasu - Timestamp
        5 - int      * A1 - pole argumentu 1
        6 - int      * A2 - pole argumentu 2
        7 - int      * IO - pole numeru identyfikatora obliczeń
        8 - string   * DT - pole odpowiedzi
    * 
    * LICZBY UJEMNE uwzglednione przy A1 i A2
    * 
    */


    public class DataOperations
    {
        public DataOperations() { }

        //GETTERS

        //NR 1
        public static string GetOP(string data)
        {
            Match match = Regex.Match(data, @"OP=(\w+)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"OP=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek
            //Console.WriteLine("Regex result: " + data);
            return data;
        }

        //NR 2
        public static string GetST(string data)
        {
            Match match = Regex.Match(data, @"ST=(\w+)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"ST=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek
                                                   //Console.WriteLine("Regex result: " + data);
            return data;
        }

        //NR 3
        public static int GetID(string data)
        {
            Match match = Regex.Match(data, @"ID=(\d+)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"ID=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            //Console.WriteLine("Regex result: " + data);
            return Convert.ToInt32(data);
        }

        //NR 4
        public static string GetTS(string data)
        {
            //                                TS=11/29/2018 8:38:14 AM$  
            Match match = Regex.Match(data, @"TS=\d+/\d+/\d+\s+\d+:\d+:\d+\s+\w{2}\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"TS=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            Console.WriteLine("Regex result: " + data);
            return data;
        }

        //NR 5 - uwzględnione liczby ujemne
        public static int GetA1(string data)
        {
            Match match = Regex.Match(data, @"A1=(-?\d+)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"A1=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek
            //Console.WriteLine("Regex result: " + data);
            return Convert.ToInt32(data);
        }

        //NR 6 - uwzględnione liczby ujemne
        public static int GetA2(string data)
        {
            Match match = Regex.Match(data, @"A2=(-?\d+)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"A2=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            //Console.WriteLine("Regex result: " + data);
            return Convert.ToInt32(data);
        }

        //NR 7
        public static int GetIO(string data)
        {
            Match match = Regex.Match(data, @"IO=(\d+)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"IO=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            //Console.WriteLine("Regex result: " + data);
            return Convert.ToInt32(data);
        }

        //NR 8
        public static string GetDT(string data)
        {
            Match match = Regex.Match(data, @"DT=(.+)*\$"); //pattern do pola DANYCH
            //match = match.NextMatch();

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = data.Remove(0, 3); //wyrzuca pierwsze trzy, czyli DT
            data = data.Remove(data.Length - 1, 1);
            //data = Regex.Replace(data, @"DT=", ""); //wyrzuca DT=
            //data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
            //Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            //Console.WriteLine("Regex result: " + data);
            return data;
        }

        //SETTERS

        //NR 1 data ustawiana automatycznie
        public static string SetData(string OP, string ST, int ID)
        {
            StringBuilder data = new StringBuilder();
            data.AppendFormat("OP={0}$" +
                              "ST={1}$" +
                              "ID={2}$" +
                              "TS={3}$",
                              OP,
                              ST,
                              ID,
                              DateTime.Now.ToString());

            return data.ToString();
        }

        //NR 2
        public static string SetData(string OP, string ST, int ID, int A1, int A2)
        {
            StringBuilder data = new StringBuilder();
            data.AppendFormat("OP={0}$" +
                              "ST={1}$" +
                              "ID={2}$" +
                              "TS={3}$" +
                              "A1={4}$" +
                              "A2={5}$",
                              OP,
                              ST,
                              ID,
                              DateTime.Now.ToString(),
                              A1,
                              A2);

            return data.ToString();
        }

        //NR 3
        public static string SetData(string OP, string ST, int ID, int A1, int A2, int IO, string DT)
        {
            StringBuilder data = new StringBuilder();
            data.AppendFormat("OP={0}$" +
                              "ST={1}$" +
                              "ID={2}$" +
                              "TS={3}$" +
                              "A1={4}$" +
                              "A2={5}$" +
                              "IO={6}$" +
                              "DT={7}$",
                              OP,
                              ST,
                              ID,
                              DateTime.Now.ToString(),
                              A1,
                              A2,
                              IO,
                              DT
                              );

            return data.ToString();
        }

        //NR 4
        public static string SetData(string OP, string ST, int ID, int A1)
        {
            StringBuilder data = new StringBuilder();
            data.AppendFormat("OP={0}$" +
                              "ST={1}$" +
                              "ID={2}$" +
                              "TS={3}$" +
                              "A1={4}$",
                              OP,
                              ST,
                              ID,
                              DateTime.Now.ToString(),
                              A1);

            return data.ToString();
        }

        //NR 5
        public static string SetData(string OP, string ST, int ID, int A1, int IO, string DT)
        {
            StringBuilder data = new StringBuilder();
            data.AppendFormat("OP={0}$" +
                              "ST={1}$" +
                              "ID={2}$" +
                              "TS={3}$" +
                              "A1={4}$" +
                              "IO={5}$" +
                              "DT={6}$",
                              OP,
                              ST,
                              ID,
                              DateTime.Now.ToString(),
                              A1,
                              IO,
                              DT
                              );

            return data.ToString();
        }

        //NR 6
        public static string SetData(string OP, string ST, int ID, string DT)
        {
            StringBuilder data = new StringBuilder();
            data.AppendFormat("OP={0}$" +
                              "ST={1}$" +
                              "ID={2}$" +
                              "TS={3}$" +
                              "DT={4}$",
                              OP,
                              ST,
                              ID,
                              DateTime.Now.ToString(),
                              DT);

            return data.ToString();
        }

    }
