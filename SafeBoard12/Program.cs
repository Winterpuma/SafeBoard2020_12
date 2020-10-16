using System;
using System.Collections.Generic;

namespace SafeBoard12
{
    class Program
    {
        /// <summary>
        /// Проверяет подходит ли входная строка для дальнейшей обработки
        /// </summary>
        /// <returns>true - подходит</returns>
        private static bool IsInputOK(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
                return false;
            
            return true;
        }

        static void Main(string[] args)
        {
            // Считываем строку
            string input = Console.ReadLine();

            // Если на вход ничего не подано
            if (!IsInputOK(input))
            {
                Console.WriteLine("No data");
                return;
            }

            string[] a = input.Split(',');
            // Создаем и заполняем словарь-счетчик с ключами enum-а
            Dictionary<string, int> counter = new Dictionary<string, int>();
            foreach (string s in Enum.GetNames(typeof(DataType)))
                counter.Add(s, 0);

            // Создаем список ошибочных ключей для лога
            List<string> unknown = new List<string>();

            // Цикл анализа тектовых данных
            foreach (string word in a)
            {
                try
                {
                    if (Enum.IsDefined(typeof(DataType), word)) // если такое слово определено в enum 
                    counter[word]++;
                    else
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
                    
                }
                catch
                {
                    unknown.Add(word); // нет в enum
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
            if (unknown.Count != 0)
            {
                Console.WriteLine("Errors:");
                Console.WriteLine("Not valid input strings: " +
                    String.Join(",", unknown));
            }
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
