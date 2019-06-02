using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laba10;
using Laba11;

namespace Laba14
{
    static class MySortedDictionaryExtension
    {
        public static List<Person> GetMale(this MySortedDictionary<string, Person> dictionary)
        {
            List<Person> Males = new List<Person>();
            foreach (Person person in dictionary.Values)
            {
                if (person.gender == Gender.Male)
                {
                    Males.Add(person);
                }
            }
            return Males;
        }
        public static string GetNumberMiddle(this MySortedDictionary<string, Person> dictionary)
        {
            int i = 0;
            foreach (Person person in dictionary.Values)
            {
                if (person is Working && ((Working)person).category == Category.Middle)
                {
                    i++;
                }
            }
            return i.ToString();
        }
        public static string GetMinExp(this MySortedDictionary<string, Person> dictionary)
        {
            int minExp = 100;
            for (int i = 0; i < dictionary.Values.Count; i++)
            {
                if (dictionary.Values[i] is Administration && ((Administration)dictionary.Values[i]).Experience < minExp)
                {
                    minExp = ((Administration)dictionary.Values[i]).Experience;
                }
            }
            if (minExp != 100)
            {
                return minExp.ToString();
            }
            else
            {
                return (-1).ToString();
            }
        }
        public static string GetNumberEng(this MySortedDictionary<string, Person> dictionary)
        {
            int num = 0;
            foreach (Person person in dictionary.Values)
            {
                if (person is Engineer)
                {
                    num++;
                }
            }
            return num.ToString();
        }
        public static List<Person> GetSuernameLongerd(this MySortedDictionary<string, Person> dictionary)
        {
            List<Person> list = new List<Person>();
            foreach (Person person in dictionary.Values)
            {
                if (person.Surname.Length > 5)
                {
                    list.Add(person);
                }
            }
            return list;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MySortedDictionary<string, Person> mySortedDictionary = new MySortedDictionary<string, Person>(12);
            mySortedDictionary.Add("ИвинаАлена", new Administration("Алена", "Ивина", Gender.Female, 2));
            mySortedDictionary.Add("БетевИван", new Administration("Иван", "Бетев", Gender.Male, 5));
            mySortedDictionary.Add("ГаукДана", new Working("Дана", "Гаук", Gender.Female, Category.Middle));
            mySortedDictionary.Add("ИвановАндрей", new Engineer("Андрей", "Иванов", Gender.Male, Category.Beginner));
            mySortedDictionary.Add("ТучинСергей", new Administration("Сергей", "Тучин", Gender.Male, 4));
            mySortedDictionary.Add("ИвинСергей", new Administration("Сергей", "Ивин", Gender.Male, 3));
            mySortedDictionary.Add("ФиллиповаНастя", new Working("Настя", "Филлипова", Gender.Female, Category.Middle));
            mySortedDictionary.Add("КраснюковаКатя", new Engineer("Катя", "Краснюкова", Gender.Female, Category.Beginner));
            mySortedDictionary.Add("ЯрыжновМаксим", new Administration("Максим", "Ярыжнов", Gender.Male, 5));
            mySortedDictionary.Add("БызоваНастя", new Working("Настя", "Бызова", Gender.Female, Category.Middle));
            mySortedDictionary.Add("ВасилюкАнтон", new Engineer("Антон", "Василюк", Gender.Male, Category.God));
            mySortedDictionary.Add("ГатауллинаЭля", new Engineer("Эля", "Гатауллина", Gender.Female, Category.God));

            Console.WriteLine(mySortedDictionary);

            var Males = GetMale(mySortedDictionary);
            Console.WriteLine("GetMale:(LINQ)");
            foreach (Person person in Males)
            {
                Console.WriteLine(person.Surname + " " + person.Firstname);
            }

            Console.WriteLine("\nNumberMiddle:(LINQ) " + GetNumberMiddle(mySortedDictionary));
            Console.WriteLine("\nMinExp:(LINQ) " + GetMinExp(mySortedDictionary));
            Console.WriteLine("\nNumberEngineers:(LINQ) " + GetNumberEng(mySortedDictionary));

            var people = GetSuernameLongerd(mySortedDictionary);
            Console.WriteLine("\nGetSurnameLong:(LINQ)");
            foreach (Person person in people)
            {
                Console.WriteLine(person.Surname + " " + person.Firstname);
            }

            //==============================

            Console.WriteLine("\nGetMale:");
            foreach (Person person in mySortedDictionary.GetMale())
            {
                Console.WriteLine(person.Surname + " " + person.Firstname);
            }

            Console.WriteLine("\nNumberMiddle: " + mySortedDictionary.GetNumberMiddle());
            Console.WriteLine("\nMinExp: " + mySortedDictionary.GetMinExp());
            Console.WriteLine("\nNumberEngineers: " + mySortedDictionary.GetNumberEng());

            Console.WriteLine("\nGetSurnameLong:");
            foreach (Person person in mySortedDictionary.GetSuernameLongerd())
            {
                Console.WriteLine(person.Surname + " " + person.Firstname);
            }

            Console.ReadKey();
        }

        #region LINQ
        static IEnumerable<Person> GetMale(MySortedDictionary<string, Person> dictionary)
        {
            var listMale = dictionary.Values.Where(s => s.gender == Gender.Male);
            return listMale;
        }

        static string GetNumberMiddle(MySortedDictionary<string, Person> dictionary)
        {
            string numberMiddle = dictionary.Values.Where(s => s is Working && ((Working)s).category == Category.Middle).LongCount().ToString();
            return numberMiddle;
        }

        static string GetMinExp(MySortedDictionary<string, Person> dictionary)
        {
            string minExp = dictionary.Values.Where(s => s is Administration).Min(s => ((Administration)s).Experience).ToString();
            return minExp;
        }
        static string GetNumberEng(MySortedDictionary<string, Person> dictionary)
        {
            string numberEng = dictionary.Values.Where(s => s is Engineer).LongCount().ToString();
            return numberEng;
        }

        static IEnumerable<Person> GetSuernameLongerd(MySortedDictionary<string, Person> dictionary)
        {
            var listSurlong = dictionary.Values.Where(s => s.Surname.Length > 5);
            return listSurlong;
        }
        #endregion
    }
}
