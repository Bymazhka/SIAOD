using System;
using System.Collections.Generic;
using System.Text;
using static Lab_number.Lab1;
using static Lab_number.Lab2;
using static Lab_number.problems;
namespace Lab1
{
    class Start
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Номер лабораторной работы:\n1. Алгоритмы сортировки\n2. Методы поиска, хеширование\n" +
                "3. Задачи из файла problems\n");
            int num = int.Parse(Console.ReadLine());
            switch (num)
            {
                case 1:
                    Start_Lab1();
                    break;
                case 2:
                    Start_Lab2();
                    break;
                case 3:
                    Start_problems();
                    break;

                default:
                    break;
            }
        }
    }
}
