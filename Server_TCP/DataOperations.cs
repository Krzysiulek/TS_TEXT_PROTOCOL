using System;
using System.Text;
using System.Text.RegularExpressions;
namespace server_tcp
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
    * 
    * 
    * BRAK KONTROLI CZY INT PRZY GetA1/A2, tj. jak będzie String zamiast inta, to się wywala wyjątek
    */


    public class DataOperations
    {
        public DataOperations() { }

        public string GetOP(string data)
        {
            Match match = Regex.Match(data, @"OP=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"OP=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
            Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            return data;
        }

        public int GetA1(string data)
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

            return Convert.ToInt32(data);
        }

        public int GetA2(string data)
        {
            Match match = Regex.Match(data, @"A2=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"A2=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
            Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            return Convert.ToInt32(data);
        }

        public string GetST(string data)
        {
            Match match = Regex.Match(data, @"ST=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"ST=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
            Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            return data;
        }

        public int GetID(string data)
        {
            Match match = Regex.Match(data, @"ID=(\w*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return 0; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"ID=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
            Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            return Convert.ToInt32(data);
        }

        public string GetTS(string data)
        {
            Match match = Regex.Match(data, @"TS=(.*)\$"); //pattern do pola operacji

            if (match.Success) //jesli pattern pasuje
            {
                Console.WriteLine(match.Value);
                data = match.Value;
            }
            else return null; //jesli nie pasuje zwracamy nic

            data = Regex.Replace(data, @"TS=", ""); //wyrzuca OP=
            data = Regex.Replace(data, @"\$", ""); //wyrzuca znak $
            Console.WriteLine(data); //wypisuje tak na wszelki wypadek

            return data;
        }

        //data ustawiana automatycznie
        public string SetData(string OP, int A1, int A2, string ST, int ID)
        {

            StringBuilder data = new StringBuilder();
            data.AppendFormat("OP={0}$" +
                              "A1={1}$" +
                              "A2={2}$" +
                              "ST={3}$" +
                              "ID={4}$" +
                              "TS={5}$",
                              OP,
                              A1,
                              A2,
                              ST,
                              ID,
                              DateTime.Now.ToString()
                             );
            return data.ToString();
        }
    }

}
