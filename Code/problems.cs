using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_number
{
    static class problems
    {
        public static void Start_problems()
        {
            while (true)
            {
                int[] tr = {3, 6, 2, 3};
                int[] tr1 = {2, 1, 2};
                int[] tr2 = {1, 2, 1};
                int[] tr3 = { 3, 2, 3, 4 };
                Console.WriteLine("Соответсвующие периметры:" + '\n' + Triangle(tr) + '\n' + Triangle(tr1) + '\n' + 
                    Triangle(tr2) + '\n' + Triangle(tr3));

                int[] max = {3, 30, 34, 5, 9};
                int[] max1 = {34323, 3432};
                int[] max2 = {1};
                int[] max3 = {10};
                Console.WriteLine("Наибольшие числа:" + '\n' + MaxNum(max) + '\n' + MaxNum(max1)
                    + '\n' + MaxNum(max2) + '\n' + MaxNum(max3));

                Console.WriteLine("Введите количество столбцов матрицы");
                int size = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите строки матрицы через пробел");


                Console.WriteLine("Нажмите любую кнопку для повторения");
                Console.ReadLine();
            }
        }
        static int Triangle(int[] mas)
        {
            List<int> arr = new List<int>(mas);
            arr.Sort();
            arr.Reverse();
            while (arr[0] >= arr[1] + arr[2] )
            {
                if (arr.Count > 3)
                    arr.RemoveAt(0);
                else return 0;
            }
            return arr[0] + arr[1] + arr[2];
        }
        private static string MaxNum(int[] arr)
        {
            List<int> list = new List<int>(arr);
            string result = "";
            List<string> newList = list.ConvertAll<string>(i => i.ToString());
            newList.Sort(Compare);
            for (int i = 0; i < list.Count; i++)
            {
                result += newList[i];
            }
            if (result[0] == '0' && result.Length > 1)
            {
                return "0";
            }
            return result;
        }
        static int Compare(string first, string second)
        {
            string f = first + second;
            string s = second + first;
            return f.CompareTo(s) > 0 ? -1 : 1;
        }
        
    }
}
