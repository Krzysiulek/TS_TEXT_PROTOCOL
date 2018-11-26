using System;
using System.Text;
using System.Text.RegularExpressions;
namespace client_tcp
{

    /*
    * Przykładowe dane do wysłania
    * OP=dodaj$A1=123$A2=45$ST=ZLE$ID=11$TS=21.02.1992_09:45
    * 
    * OP - pole operacji
    * A1/A2 - pole argumentu1/2
    * ST - pole statusu
    * ID - pole identyfikatora
    * TS - pole znacznika czasu - Timestamp
    * DT - pole odpowiedzi
    * 
    * BRAK KONTROLI CZY INT PRZY GetA1/A2, tj. jak będzie String zamiast inta, to się wywala wyjątek
    */


    public class DataOperations
    {
        public DataOperations() { }

        public static string GetOP(string data)
        {
            Match match = Regex.Match(data, @"OP=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"OP=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek
            Console.WriteLine("Regex result: " + data);
            return data;
        }

        public static int GetA1(string data)
        {
            Match match = Regex.Match(data, @"A1=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"A1=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek
            Console.WriteLine("Regex result: " + data);
            return Convert.ToInt32(data);
        }

        public static int GetA2(string data)
        {
            Match match = Regex.Match(data, @"A2=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"A2=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            Console.WriteLine("Regex result: " + data);
            return Convert.ToInt32(data);
        }

        public static string GetST(string data)
        {
            Match match = Regex.Match(data, @"ST=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"ST=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek
            Console.WriteLine("Regex result: " + data);
            return data;
        }

        public static int GetID(string data)
        {
            Match match = Regex.Match(data, @"ID=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                //Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"ID=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
                                                   //Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            Console.WriteLine("Regex result: " + data);
            return Convert.ToInt32(data);
        }

        public static string GetTS(string data)
        {
            Match match = Regex.Match(data, @"TS=(.*)\$"); //pattern do pola operacji

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

        public static string GetDT(string data)
        {
            Match match = Regex.Match(data, @"DT=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"DT=", ""); //wyrzuca DT=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
            //Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            Console.WriteLine("Regex result: " + data);
            return data;
        }

        //data ustawiana automatycznie
        public static string SetData(string OP, int A1, int A2, string ST, int ID, string DT)
        {

            StringBuilder data = new StringBuilder();
            data.AppendFormat("OP={0}$" +
                              "A1={1}$" +
                              "A2={2}$" +
                              "ST={3}$" +
                              "ID={4}$" +
                              "DT={5}$" +
                              "TS={6}$",
                              OP,
                              A1,
                              A2,
                              ST,
                              ID,
                              DT,
                              DateTime.Now.ToString()
                             );
            return data.ToString();
        }
    }

}
