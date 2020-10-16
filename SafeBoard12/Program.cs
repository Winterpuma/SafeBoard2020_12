using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeBoard12
{
    class Program
    {
        /// <summary>
        /// Проверяет подходит ли массив для дальнейшей обработки
        /// </summary>
        /// <param name="input">Массив данных</param>
        /// <returns>true - подходит</returns>
        private static bool IsInputOK(string[] input)
        {
            if (input.Length == 0) // если массив пуст
                return false;

            bool values = false;
            foreach (string s in input)
            {
                if (s != "") // если есть непустой элемент
                {
                    values = true;
                    break;
                }
            }

            return values;
        }

        static void Main(string[] args)
        {
            // Считываем строку
            string[] a = Console.ReadLine().Split(',');

            // Если на вход ничего не подано
            if (!IsInputOK(a))
            {
                Console.WriteLine("No data");
                return;
            }
            
            // Создаем и заполняем словарь-счетчик с ключами enum-а
            Dictionary<string, int> counter = new Dictionary<string, int>();
            foreach (string s in Enum.GetNames(typeof(DataType)))
                counter.Add(s, 0);

            // Создаем список ошибочных ключей для лога
            List<string> unknown = new List<string>();

            // Цикл анализа тектовых данных
            foreach (string word in a)
            {
                if (Enum.IsDefined(typeof(DataType), word)) // если такое слово определено в enum 
                    counter[word]++;
                else
                {
                    try
                    {
                        int enumVal = Convert.ToInt32(word);
                        if (Enum.IsDefined(typeof(DataType), enumVal)) // если такое значение определено в enum
                        {
                            string name = Enum.GetName(typeof(DataType), enumVal); // еолучаем слово по значению
                            counter[name]++;
                        }
                        else
                            unknown.Add(word); // нет в enum
                    }
                    catch
                    {
                        unknown.Add(word); // нет в enum
                    }
                }
            }

            // Вывод результата
            Console.WriteLine("Input data types:");

            // Вывод подсчета по enum
            foreach (KeyValuePair<string, int> kvp in counter)
            {
                Console.WriteLine(kvp.Key + "(" + (int)Enum.Parse(typeof(DataType), kvp.Key) + ")" +
                    "-" + kvp.Value);
            }
            
            // Вывод ошибок
            Console.WriteLine("Errors:");
            Console.WriteLine("Not valid input strings: " +
                String.Join(",", unknown));
        }

        public enum DataType
        {
            None = 0,
            First = 1,
            Second = 2,
            Third = 3,
            Fourth = 4
        }
    }
}
