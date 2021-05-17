using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab_number
{
    static class Lab3
    {
        public static void Start_Lab3()
        {
            while (true)
            {
                /*bool choose = false;
                Console.WriteLine("Чувствительность к регистру: да(1) или нет (2). Нажмите подходящую цифру");
                if (int.Parse(Console.ReadLine()) == 1) choose = true;
                string str = "";
                string word = "";
                if (choose != true)
                {
                    str.ToLower();
                    word.ToLower();
                }

                Console.WriteLine(KMP("faabaabaaaabaabaaa","aabaa"));
                Console.WriteLine(KMP("Hoola-Hoola girls like hooligans", "hooligans"));
                Console.WriteLine(BoyerMoor("Hoola-Hoola girls like hooligans","hooligans"));
                */
                Console.WriteLine("Введите размерность игрового поля для генерации:");
                var size = int.Parse(Console.ReadLine());
                List<int> list = new List<int>();
                while (list.Count != size * size)
                {
                    Random rnd = new Random();
                    int next = rnd.Next(1, size * size + 1);
                    if (!list.Contains(next))
                    {
                        list.Add(next);
                    }
                }
                Game game = new Game(list.ToArray()/*new int[] { 10, 2, 4, 7, 13, 16, 1, 3, 11, 6, 12, 14, 9, 5, 15, 8 }*/);
                int[] order = game.Solve();
                //Console.WriteLine("Полученная последовательность передвижений:");
                //foreach (var item in order)
                //{
                //    Console.Write(item + ", ");
                //}
                Console.WriteLine();
                Console.WriteLine("Число перестановок: " + order.Length);
                Console.WriteLine("Нажмите любую кнопку для повторения");
                Console.ReadLine();
            }
        }
        static int KMP(string str, string word)
        {
            bool flag = true;
            var tmp = str + word;
            char ch = ' '; //начальный символ
            while (flag)
            {
                if (tmp.Contains(ch))
                {
                    ch++; //если содержится такой символ, то переходим на следующий, пока не найдем уникальный
                }
                else
                {
                    flag = false; //когда нашли уникальный символ - выходим 
                }
            }
            tmp = word + ch + str; //разделяем слово и строку уникальным символом
            var prefs = new int[tmp.Length];
            CalcPrefs(tmp);
            for (int i = word.Length; i < prefs.Length; i++) //пока не совпадут длина слова и префикса
            {
                if (prefs[i] == word.Length)
                {
                    return i - word.Length * 2; //индекс начала слова
                }
            }
            return -1;
            void CalcPrefs(string word)
            {
                /*
                 * находим максимальную длину префикса для каждого символа
                 */
                for (int i = 0, j = 1; j < word.Length; j++, i = 0)
                {
                    string tempi = word[i].ToString(), tempj = word[j].ToString();
                    int ended = j;
                    while (tempi.Length <= j)
                    {
                        if (tempi.Equals(tempj) && tempj.Length > prefs[j])
                        {
                            prefs[j] = tempj.Length;
                        }
                        i++;
                        ended--;
                        tempi += word[i].ToString();
                        tempj = word[ended].ToString() + tempj;
                    }
                }
            }
        }
        private static int BoyerMoor(string str, string word)
        {
            var alphabet = new int[256];
            for (int i = 0; i < alphabet.Length; i++)
            {
                alphabet[i] = word.Length;
            }
            string reversed = new string(word.ToCharArray().Reverse().ToArray()); //переворачиваем слово
            var chars_cost = new Dictionary<char, int>();
            for (int i = 0; i < reversed.Length; i++)
            {
                if (!chars_cost.ContainsKey(reversed[i]))
                {
                    chars_cost.Add(reversed[i], i);
                    alphabet[reversed[i]] = i;
                }
            }
            int l = word.Length - 1;
            while (l <= str.Length)
            {
                if (word[word.Length - 1] != str[l])//если последний символ не == текущему
                {
                    l += alphabet[str[l]];
                }
                else//если равен
                {
                    var flag = false;
                    for (int i = l - 1, j = word.Length - 2; j >= 0; i--, j--)
                    {
                        if (str[i] != word[j])
                        {
                            l += alphabet[str[i]];
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        return l - word.Length + 1;
                    }
                }
            }
            return -1;
        }

    }
}

