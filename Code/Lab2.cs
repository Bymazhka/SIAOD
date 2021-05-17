using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using static Lab_number.Lab1;
namespace Lab_number
{
    static class Lab2
    {
        public static void Start_Lab2()
        {
            while (true)
            {
                /*Console.WriteLine("Введите число элементов массива");
                int size = int.Parse(Console.ReadLine());

                Stopwatch time = new Stopwatch();
                Random rnd = new Random();
                HashSet<int> unic = new HashSet<int>();
                while (unic.Count != size)
                {
                    unic.Add(rnd.Next(-1000, 1000));
                }
                int[] array = new int[unic.Count];
                unic.CopyTo(array);

                int[] baza = array;
                Array.Sort(baza);
                for (int i = 0; i < baza.Length; i++)
                    Console.Write(" " + baza[i]);

                Console.WriteLine('\n' + "Введите нужное число для поиска");
                int num = int.Parse(Console.ReadLine());

                time.Start();
                int bin = BinarySearch(baza, num);
                time.Stop();
                Console.WriteLine("Бинарный. Число находится на позиции " + bin);
                Console.WriteLine("Время выполнения - " + time.Elapsed);
                time.Reset();

                time.Start();
                int fib = Fibonaccian(baza, num);
                time.Stop();
                Console.WriteLine('\n' + "Фибоначчиев. Число находится на позиции " + fib);
                Console.WriteLine("Время выполнения - " + time.Elapsed);
                time.Reset();

                time.Start();
                int inter = Interpolation(baza, num);
                time.Stop();
                Console.WriteLine('\n' + "Интерполяционный. Число находится на позиции " + inter);
                Console.WriteLine("Время выполнения - " + time.Elapsed);
                time.Reset();

                time.Start();
                int vstr = Array.IndexOf(baza, num);
                time.Stop();
                Console.WriteLine('\n' + "Встроенный. Число находится на позиции " + vstr);
                Console.WriteLine("Время выполнения - " + time.Elapsed);
                time.Reset();

                BinaryTree tree = new BinaryTree(new int[] { 8, 6, 10, 9, 11, 7 });
                tree.Add(6);
                time.Start();
                Console.WriteLine('\n' + "ДЕРЕВО" + '\n' + "Число добавлено в дерево " + tree.Contains(6));
                time.Stop();
                Console.WriteLine("Время выполнения на поиск - " + time.Elapsed);
                time.Reset();
                time.Start();
                tree.Delete(6);
                time.Stop();
                Console.WriteLine("Число содержится в дереве после удаления " + tree.Contains(6));
                Console.WriteLine("Время выполнения на удаление - " + time.Elapsed);
                time.Reset();


                SimpleHash hash = new SimpleHash(new int[] { 8, 6, 10, 9, 11, 7, 14, 19, 61, 24, 1 });
                Console.WriteLine('\n' + "ПРОСТОЕ ХЕШИРОВАНИЕ" + '\n' + "Число находится в таблице на позиции " + hash.Search(24));
                Console.WriteLine("Число находится в таблице на позиции " + hash.Search(10));
                time.Start();
                hash.Delete(10);
                Console.WriteLine("Число находится в таблице на позиции " + hash.Search(10));
                time.Stop();
                Console.WriteLine("Затраченное время на удаление и поиск числа = " + time.Elapsed);
                time.Reset();

                RandHash randhash = new RandHash(new int[] { 8, 6, 10, 9, 11, 7, 14, 19, 61, 24, 1 });
                Console.WriteLine('\n' + "РАНДОМНОЕ ХЕШИРОВАНИЕ" + '\n' + "Число находится в таблице на позиции " + randhash.Search(20));
                Console.WriteLine("Число находится в таблице на позиции " + randhash.Search(10));
                time.Start();
                randhash.Delete(10);
                Console.WriteLine("Число находится в таблице на позиции " + randhash.Search(10));
                time.Stop();
                Console.WriteLine("Затраченное время на удаление и поиск числа = " + time.Elapsed);
                time.Reset();

                ChainHash chainhash = new ChainHash(new int[] { 8, 6, 10, 9, 11, 7, 14, 19, 61, 24, 1 });
                Console.WriteLine('\n' + "МЕТОД ЦЕПОЧЕК" + '\n' + "Число есть в таблице - " + chainhash.HasValue(20));
                Console.WriteLine("Число есть в таблице - " + chainhash.HasValue(10));
                time.Start();
                chainhash.Delete(10);
                Console.WriteLine("Число есть в таблице - " + chainhash.HasValue(10));
                time.Stop();
                Console.WriteLine("Затраченное время на удаление и поиск числа = " + time.Elapsed);
                time.Reset();
                */
                Chess chess = new Chess();
                chess.Place();
                Console.WriteLine("Нажмите любую кнопку для повторения");
                Console.ReadLine();
            }
        }
        //Задания 1.3
        static public int BinarySearch(int[] array, int search)
        {
            /*Двоичный поиск — классический алгоритм поиска элемента в отсортированном массиве, 
             использующий дробление массива на половины. */
            int left = 0;
            int right = array.Length - 1;
            while (left <= right)
            {
                var middle = (left + right) / 2;

                if (search == array[middle])
                {
                    return middle;
                }
                else if (search < array[middle])
                {
                    //сужаем рабочую зону массива с правой стороны
                    right = middle - 1;
                }
                else
                {
                    //сужаем рабочую зону массива с левой стороны
                    left = middle + 1;
                }
            }
            return -1;
        }
        public static int min(int x, int y)
        {
            return (x <= y) ? x : y; //если да, то x, иначе y
        }
        public static int Fibonaccian(int[] arr, int x)
        {
            /* В этом поиске анализируются элементы, находящиеся в позициях, 
             * равных числам Фибоначчи. Числа Фибоначчи получаются по следующему 
             * правилу: последующее число равно сумме двух предыдущих чисел */
            int n = arr.Length;
            int fib2 = 0;
            int fib1 = 1;
            int fibM = fib2 + fib1; //следующее число
            while (fibM < n) //пока не дошло до конца массива
            {
                fib2 = fib1;
                fib1 = fibM;
                fibM = fib2 + fib1; //делаем новые следующие числа
            }
            int offset = -1; //для запоминания индекса
            while (fibM > 1)
            {
                int i = min(offset + fib2, n - 1);
                if (arr[i] < x)
                {
                    fibM = fib1;
                    fib1 = fib2;
                    fib2 = fibM - fib1;
                    offset = i;
                }
                else if (arr[i] > x)
                {
                    fibM = fib2;
                    fib1 = fib1 - fib2;
                    fib2 = fibM - fib1;
                }
                else
                    return i;
            }
            if (fib1 == 1 && arr[n - 1] == x)
                return n - 1;
            return -1;
        }
        public static int Interpolation(int[] arr, int key)
        {
            /*Поиск происходит подобно двоичному поиску, 
             * но вместо деления области поиска на две примерно равные части, 
             * интерполирующий поиск производит оценку новой области поиска по 
             * расстоянию между ключом и текущим значением элемента.
             */
            int mid;
            int i = 0;
            int j = arr.Length - 1;
            while (arr[i] < key && arr[j] > key)
            {
                if (arr[j] == arr[i]) //чтобы не было деления на 0
                    break;
                mid = i + ((key - arr[i]) * (j - i)) / (arr[j] - arr[i]);
                if (arr[mid] < key)
                    i = mid + 1;
                else if (arr[mid] > key)
                    j = mid - 1;
                else
                    return i;
            }
            if (arr[i] == key) return i;
            else if (arr[j] == key) return j;
            else return -1;
        }
        public class BinaryTree
        {
            int value;
            BinaryTree rightchild;
            BinaryTree leftchild;
            BinaryTree parent;
            public BinaryTree(int[] mas) //констурктор для массива
            {
                value = mas[0]; //первый элемент
                rightchild = null;
                leftchild = null;
                parent = null;
                for (int i = 1; i < mas.Length; i++)
                {
                    Add(mas[i]); //добавляем значения из массива в дерево
                }
            }
            public BinaryTree(int num) //конструктор для одиночного элемента
            {
                value = num; //текущий элемент
                rightchild = null;
                leftchild = null;
                parent = null;
            }
            public void Add(int num)
            {
                //temp - новый эл-т, this - вызывающий (второй)
                BinaryTree temp = new BinaryTree(num);
                if (temp.value > this.value && rightchild == null) //если идем вправо и никого нет, то привязываем
                {
                    rightchild = temp;
                    temp.parent = this; //запоминаем предудущего (родителя)
                }
                else if (temp.value > this.value && rightchild != null) //если больше и справа кто-то уже есть
                {
                    rightchild.Add(num);
                }
                else if (temp.value < this.value && leftchild == null) //если идем влево и никого нет, то привязываем
                {
                    leftchild = temp;
                    temp.parent = this;
                }
                else if (temp.value < this.value && leftchild != null) //если больше и слева кто-то уже есть
                {
                    leftchild.Add(num);
                }
            }
            public bool Contains(int num) //когда мы обращаемся к дереву, мы обращаемся к 1 элементу
            {
                if (num == value) //если равно текущему элементу
                {
                    return true;
                }
                if (num > value && rightchild != null)
                {
                    return rightchild.Contains(num);
                }
                if (num < value && leftchild != null)
                {
                    return leftchild.Contains(num);
                }
                else return false;
            }
            public void Delete(int num)
            {
                /*
                 * если элемент есть, создаем резервный массив элементами, идущими
                 * от удаляемого по нисходящей, выпиливаем удаляемый и заново запиливаем эти элементы в дерево
                 */
                if (Contains(num))
                {
                    List<int> childs = new List<int>();
                    BinaryTree deleted = GetNum(num); //временный 
                    if (deleted.leftchild != null) //если есть какие-то числа после удаляемого
                    {
                        deleted.leftchild.FindChilds(childs); //ищем детей слева
                    }
                    if (deleted.rightchild != null) //если есть какие-то числа после удаляемого
                    {
                        deleted.rightchild.FindChilds(childs);
                    }
                    if (deleted.parent.rightchild == deleted)
                    {
                        deleted.parent.rightchild = null;
                        deleted.parent = null;
                    }
                    else
                    {
                        deleted.parent.leftchild = null;
                        deleted.parent = null;
                    }
                    foreach (var item in childs)
                    {
                        Add(item);
                    }
                }
            }
            private void FindChilds(List<int> childs)
            {
                childs.Add(value);
                if (leftchild != null)
                {
                    FindChilds(childs);
                }
                if (rightchild != null)
                {
                    FindChilds(childs);
                }
            }

            private BinaryTree GetNum(int num) //возвращаам сам объект
            {
                if (num == value) //если равно текущему элементу
                {
                    return this;
                }
                if (num > value && rightchild != null)
                {
                    return rightchild.GetNum(num);
                }
                if (num < value && leftchild != null)
                {
                    return leftchild.GetNum(num);
                }
                else return null;
            }
        }

        //Задания 1.2
        class SimpleHash
        {
            int?[] table;
            public SimpleHash(int[] mas)
            {
                table = new int?[mas.Length];
                foreach (var item in mas)
                {
                    HashFunction(item);
                }
            }
            public void HashFunction(int value)
            {
                int i = 0;
                while (true)
                {
                    int seat = (Math.Abs(value) + i) % table.Length;
                    if (table[seat] == null)
                    {
                        table[seat] = value;
                        return;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            public int Search(int value)
            {
                return Array.IndexOf(table, value);
            }
            public void Delete(int value)
            {
                int i = 0;
                if (Search(value) == -1) return;
                while (true)
                {
                    int seat = (Math.Abs(value) + i) % table.Length;
                    if (table[seat] == value)
                    {
                        table[seat] = null;
                        return;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
        class RandHash
        {
            int?[] table;
            public RandHash(int[] mas)
            {
                table = new int?[mas.Length];
                foreach (var item in mas)
                {
                    HashFunction(item);
                }
            }
            public void HashFunction(int value)
            {
                Random i = new Random();
                while (true)
                {
                    int seat = (Math.Abs(value) + i.Next(0, table.Length)) % table.Length;
                    if (table[seat] == null)
                    {
                        table[seat] = value;
                        return;
                    }
                }
            }
            public int Search(int value)
            {
                return Array.IndexOf(table, value);
            }
            public void Delete(int value)
            {
                Random i = new Random();
                if (Search(value) == -1) return;
                while (true)
                {
                    int seat = (Math.Abs(value) + i.Next(0, table.Length)) % table.Length;
                    if (table[seat] == value)
                    {
                        table[seat] = null;
                        return;
                    }
                }
            }
        }
        class ChainHash
        {
            List<int>[] links;

            public ChainHash(int[] arr)
            {
                links = new List<int>[arr.Length];
                foreach (var item in arr)
                {
                    AddItem(item);
                }
            }
            public void Delete(int item)
            {

                if (HasValue(item))
                {
                    var hash = Hash(item);
                    var index = links[hash].IndexOf(item);
                    links[hash].RemoveAt(index);
                }
            }
            public bool HasValue(int item)
            {
                var res = links[Hash(item)]?.Exists(i => i == item);
                return res ?? false;
            }

            void AddItem(int item)
            {
                var index = Hash(item);
                if (links[index] == null)
                {
                    links[index] = new List<int>();
                    links[index].Add(item);
                }
                else
                {
                    links[index].Add(item);
                }
            }
            int Hash(int item)
            {
                return Math.Abs(item % links.Length);
            }
        }

        //Задание 2
        class Chess
        {
            /*
             * создаем bool матрицу 8*8, она по умолчанию будет заполнена false 
             * делаем метод, который будет расставлять фигуры 
             * при расстановке после постановки одного ферзя, все значения в матрице, находящиеся по диагонали, вертикали и горизонтали
             * будут принимать значение true 
             * и ставим мы фигуры только туда, где есть значение false
             * рандомно ставим по одной фигуре в столбец, если когда-то происходит коллизия, то все сбрасываем и начинаем с самого начала
             */
            bool[,] board;
            char[,] b_out; //для вывода доски

            public Chess()
            {
                board = new bool[8, 8];
                b_out = new char[8, 8];
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        b_out[i, j] = 'o';
                    }
                }
            }
            public void Place()
            {
                int counter = 0;
                bool flag;
                while (counter != 8) //пока не поставили все 8 ферзей
                {
                    FreeField(); //очищаем поле
                    counter = 0;
                    for (int i = 0; i < 8; i++) //цикл для 8 ферзей
                    {
                        flag = Rand_Queen(i); //если поставлена фигура
                        if (!flag) //если не поставилась фигура
                        {
                            break; //то выходим
                        }
                        counter++;
                    }
                }
                Print();
            }
            /// <summary>
            /// Выводим результат в виде доски и закрашиваем ферзи красным цветом
            /// </summary>
            void Print() 
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (b_out[i, j] == 'i')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(b_out[i, j]);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("|");
                        }
                        else
                        {
                            Console.Write(b_out[i, j] + "|");
                        }
                    }
                Console.WriteLine();
                }
            }
            /// <summary>
            /// Ставит ферзя на какое-то определенное место
            /// </summary>
            /// <param name="col">столбец</param>
            /// <returns></returns>
            private bool Rand_Queen(int col)
            {
                Random rnd = new Random();
                int row = rnd.Next(0, 8); //берём любую строчку 
                int counter = 0;
                while (counter != 8) //пока не было 8 попыток поставить фигуру
                {
                    if (!board[row, col]) //если клетка = false 
                    {
                        board[row, col] = true; //делаем ее true
                        b_out[row, col] = 'i'; //на поле для вывода ставим "фигуру"
                        FillDiags(row, col); //заполняем все идущие от нее диагонали,
                        FillHorisVert(row, col); //вертигали и горизонтали значениями true
                        return true;
                    }
                    row = rnd.Next(0, 8);
                    counter++;
                }
                return false;
            }
            /// <summary>
            /// Если не получилось поставить какого-то очередного ферзя, то возвращаемся в изначальное положение
            /// </summary>
            void FreeField()
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        board[i, j] = false;
                        b_out[i, j] = 'o';
                    }
                }
            }
            /// <summary>
            /// заполняем горизонтали и вертикали относительно центра клетки
            /// </summary>
            /// <param name="row">строки</param>
            /// <param name="col">столбцы</param>
            void FillHorisVert(int row, int col)
            {
                for (int i = 0; i < 8; i++)
                {
                    board[i, col] = true;
                    board[row, i] = true;
                }
            }
            /// <summary>
            /// заполняем 4 диагонали относительно центра клетки
            /// </summary>
            /// <param name="row">строки</param>
            /// <param name="col">столбцы</param>
            void FillDiags(int row, int col)
            {
                int row_now = row, col_now = col; 
                while (row_now >= 0 && row_now < 8 && col_now >= 0 && col_now < 8)
                {
                    board[row_now, col_now] = true;
                    --col_now;
                    --row_now;
                }
                row_now = row;
                col_now = col;
                while (row_now >= 0 && row_now < 8 && col_now >= 0 && col_now < 8)
                {
                    board[row_now, col_now] = true;
                    ++col_now;
                    ++row_now;
                }
                row_now = row;
                col_now = col;
                while (row_now >= 0 && row_now < 8 && col_now >= 0 && col_now < 8)
                {
                    board[row_now, col_now] = true;
                    ++row_now;
                    --col_now;
                }
                row_now = row;
                col_now = col;
                while (row_now >= 0 && row_now < 8 && col_now >= 0 && col_now < 8)
                {
                    board[row_now, col_now] = true;
                    --row_now;
                    ++col_now;
                }
            }
        }
    }
}
