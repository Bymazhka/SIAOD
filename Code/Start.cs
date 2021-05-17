using System;
using System.Collections.Generic;
using System.Text;
using static Lab_number.Lab1;
using static Lab_number.Lab2;
using static Lab_number.Lab3;
using static Lab_number.Lab4;
using static Lab_number.problems;
using static Lab_number.Zadachi_na_stroki;
namespace Lab1
{
    class Start
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Номер лабораторной работы:\n1. Алгоритмы сортировки\n2. Методы поиска, хеширование, шахматы\n" +
                "3. Задачи из файла problems\n" + "4. Методы поиска подстроки в строке, пятнашки\n" + "5. Реализация стека, дека\n" + 
                "6. Задачи на строки\n");
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
                case 4:
                    Start_Lab3();
                    break;
                case 5:
                    Start_Lab4();
                    break;
                case 6:
                    Start_stroki();
                    break;

                default:
                    break;
            }
        }
    }
}
