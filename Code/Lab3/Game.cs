using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_number
{
    public class Game
    {
        int size;
        Cell[,] field;
        List<int> movelist;
        int[] arr;
        public Game(int[] num)
        {
            arr = num;
            size = (int)Math.Sqrt(num.Length);
            if (num.Length % size == 0)
            {

                field = new Cell[size, size];
                //инициализируется игровое поле
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        field[i, j] = new Cell(num[i * size + j], i, j, size);
                    }
                }
                MakeNeighbours(field);
                movelist = new List<int>();
            }
            else
            {
                Console.WriteLine("Недопустимое количество элементов, невозможно составить квадратное игровое поле.");
            }
        }
        /// <summary>
        /// Привязывает соседей со всех сторон для каждой клетки поля
        /// </summary>
        /// <param name="map"></param>
        void MakeNeighbours(Cell[,] map)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var temp = map[i, j]; //текущий элемент
                    var col = temp.GetCol();
                    var row = temp.GetRow();
                    if (row != 0) //если ряд не первый
                    {
                        temp.SetUp(map[row - 1, col]); //устанавливаем соседа сверху
                    }
                    else
                    {
                        temp.SetUp(null);
                    }
                    if (col != size - 1)
                    {
                        temp.SetRight(map[row, col + 1]);
                    }
                    else
                    {
                        temp.SetRight(null);
                    }
                    if (row != size - 1)
                    {
                        temp.SetDown(map[row + 1, col]);
                    }
                    else
                    {
                        temp.SetDown(null);
                    }
                    if (col != 0)
                    {
                        temp.SetLeft(map[row, col - 1]);
                    }
                    else
                    {
                        temp.SetLeft(null);
                    }
                }
            }
        }
        /// <summary>
        /// Решает заданное начальное поле
        /// </summary>
        /// <returns></returns>
        public int[] Solve()
        {
            bool solvable = CheckSolvable(arr);
            if (solvable)
            {
                PrintField(field);
                for (int i = 0; i < size - 1; i++)
                {
                    PlaceRow(i);
                    PrintField(field);

                    PlaceCol(i);
                    PrintField(field);

                }
                return movelist.ToArray();
            }
            else
            {
                Console.WriteLine("Нерешаемая комбинация.");
                return new int[] { -1 };
            }
        }
        /// <summary>
        /// устанавливает строку
        /// </summary>
        /// <param name="row"></param>
        void PlaceRow(int row)
        {
            int i;
            for (i = 0; i < size - 1; i++)
            {
                PlaceCell(row, i);
            }
            PlaceLastInRow(row);
        }
        /// <summary>
        /// устанавливает столбец 
        /// </summary>
        /// <param name="col"></param>
        void PlaceCol(int col)
        {
            int i;
            for (i = 0; i < size - 1; i++)
            {
                PlaceCell(i, col);
            }
            PlaceLastInCol(col);
        }
        /// <summary>
        /// для последнего значения в столбце, чтобы его  последнее значение создавало уголок
        /// </summary>
        /// <param name="col"></param>
        void PlaceLastInCol(int col)
        {
            Cell last = GetCell(field, 1 + col + (size - 1) * size); 
            int targRow = last.GetTargRow();
            Cell empty = GetCell(field, size * size); //пустая
            if (last.CheckPlace()) //если она уже стоит на своем месте
            {
                return;
            }
            last.SetTargCol(last.GetTargCol() + 1); //ставим целевое местоположение со смещением
            last.SetTargRow(last.GetTargRow() - 1);
            if (empty.GetRow() == size - 1 && empty.GetRight() == last) //если пустая на месте целевой, и она справа 
            {
                MoveRight(field, empty);
            }
            else
            {
                PlaceCell(targRow, col);
                SpinRight(last);
            }

            void SpinRight(Cell last) //делаем уголок
            {
                var empty = GetCell(field, size * size);
                MoveEmptyToClose(field, last);
                switch (empty.GetDirection(last))
                {
                    case Cell.Direction.Left://если 0 справа от цели
                        MoveUp(field, empty);
                        MoveLeft(field, empty);
                        goto case Cell.Direction.Down;
                    case Cell.Direction.Up://если 0 снизу цели
                        MoveRight(field, empty);
                        MoveUp(field, empty);
                        goto case Cell.Direction.Left;
                    case Cell.Direction.Down://если 0 сверху от цели
                        MoveLeft(field, empty);
                        MoveDown(field, empty);
                        MoveRight(field, empty);
                        MoveDown(field, empty);
                        MoveLeft(field, empty);
                        MoveUp(field, empty);
                        MoveUp(field, empty);
                        MoveRight(field, empty);
                        MoveRight(field, empty);
                        MoveDown(field, empty);
                        break;
                    default:
                        break;
                }
                last.SetTargCol(last.GetTargCol() - 1);
                last.SetTargRow(last.GetTargRow() + 1);
                last.CheckPlace();
            }
        }
        /// <summary>
        /// для последнего значения в строке, чтобы его  последнее значение создавало уголок
        /// </summary>
        /// <param name="row"></param>
        void PlaceLastInRow(int row)
        {
            Cell last = GetCell(field, (row + 1) * size);
            int targCol = last.GetTargCol();
            Cell empty = GetCell(field, size * size);
            if (last.CheckPlace())
            {
                return;
            }
            last.SetTargCol(last.GetTargCol() - 1);
            last.SetTargRow(last.GetTargRow() + 1);
            if (empty.GetCol() == size - 1 && empty.GetDown() == last)
            {
                MoveDown(field, empty);
            }
            else if (!(last.GetRow() == 0 && last.GetCol() == size - 1))
            {
                PlaceCell(row, targCol);
                SpinLeft(last);
            }

            void SpinLeft(Cell last)
            {
                var empty = GetCell(field, size * size);
                MoveEmptyToClose(field, last);
                switch (empty.GetDirection(last))
                {
                    case Cell.Direction.Left://если 0 справа от цели
                        MoveDown(field, empty);
                        MoveLeft(field, empty);
                        goto case Cell.Direction.Up;
                    case Cell.Direction.Up://если 0 снизу цели
                        MoveLeft(field, empty);
                        MoveUp(field, empty);
                        goto case Cell.Direction.Right;
                    case Cell.Direction.Right://если 0 слева от цели
                        MoveUp(field, empty);
                        MoveRight(field, empty);
                        MoveDown(field, empty);
                        MoveRight(field, empty);
                        MoveUp(field, empty);
                        MoveLeft(field, empty);
                        MoveLeft(field, empty);
                        MoveDown(field, empty);
                        break;
                    default:
                        break;
                }
                last.SetTargCol(last.GetTargCol() + 1);
                last.SetTargRow(last.GetTargRow() - 1);
                last.CheckPlace();
            }
        }
        /// <summary>
        /// Двигает пустую по направлению к цели, пока не станет его соседом
        /// </summary>
        /// <param name="map"></param>
        /// <param name="actual"></param>
        void MoveEmptyToClose(Cell[,] map, Cell actual)
        {
            var empty = GetCell(map, size * size);
            while (!empty.IsNear(actual))//двигаем 0 к цели пока не будем рядом
            {
                switch (empty.GetDirectionToMove(actual))
                {
                    case Cell.Direction.Down://если 0 сверху цели
                        MoveDown(map, empty);
                        break;
                    case Cell.Direction.Left://если 0 справа от цели
                        MoveLeft(map, empty);
                        break;
                    case Cell.Direction.Up://если 0 снизу цели
                        MoveUp(map, empty);
                        break;
                    case Cell.Direction.Right://если 0 слева от цели
                        MoveRight(map, empty);
                        break;
                    default:
                        break;
                }
            }
        }
        //методы, совершающие обмен между соседними
        void MoveLeft(Cell[,] map, Cell replaced)
        {
            Cell temp = replaced.GetLeft();
            movelist.Add(temp.GetValue());
            map[temp.GetRow(), temp.GetCol()] = replaced;
            map[replaced.GetRow(), replaced.GetCol()] = temp;
            temp.SetCol(temp.GetCol() + 1);
            replaced.SetCol(replaced.GetCol() - 1);
            replaced.SetPrevious(Cell.Direction.Left);
            temp.SetPrevious(Cell.Direction.Right);
            MakeNeighbours(map);
        }
        void MoveDown(Cell[,] map, Cell replaced)
        {
            Cell temp = replaced.GetDown();
            movelist.Add(temp.GetValue());
            map[temp.GetRow(), temp.GetCol()] = replaced;
            map[replaced.GetRow(), replaced.GetCol()] = temp;
            temp.SetRow(temp.GetRow() - 1);
            replaced.SetRow(replaced.GetRow() + 1);
            replaced.SetPrevious(Cell.Direction.Down);
            temp.SetPrevious(Cell.Direction.Up);
            MakeNeighbours(map);
        }
        void MoveUp(Cell[,] map, Cell replaced)
        {
            Cell temp = replaced.GetUp();
            movelist.Add(temp.GetValue());
            map[temp.GetRow(), temp.GetCol()] = replaced;
            map[replaced.GetRow(), replaced.GetCol()] = temp;
            temp.SetRow(temp.GetRow() + 1);
            replaced.SetRow(replaced.GetRow() - 1);
            replaced.SetPrevious(Cell.Direction.Up);
            temp.SetPrevious(Cell.Direction.Down);
            MakeNeighbours(map);
        }
        void MoveRight(Cell[,] map, Cell replaced)
        {
            Cell temp = replaced.GetRight();
            movelist.Add(temp.GetValue());
            map[temp.GetRow(), temp.GetCol()] = replaced;
            map[replaced.GetRow(), replaced.GetCol()] = temp;
            temp.SetCol(temp.GetCol() - 1);
            replaced.SetCol(replaced.GetCol() + 1);
            replaced.SetPrevious(Cell.Direction.Right);
            temp.SetPrevious(Cell.Direction.Left);
            MakeNeighbours(map);
        }
        void PrintField(Cell[,] field)
        {
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j].GetValue().ToString().Length == 1)
                    {
                        if (field[i, j].GetValue() == size * size)
                        {
                            Console.Write("  |");
                        }
                        else
                        {
                            Console.Write(" " + field[i, j].GetValue() + "|");
                        }

                    }
                    else
                    {
                        if (field[i, j].GetValue() == size * size)
                        {
                            Console.Write("  |");
                        }
                        else
                        {
                            Console.Write(field[i, j].GetValue() + "|");
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        /// <summary>
        /// размещение клетки 
        /// </summary>
        /// <param name="row_of_num"></param>
        /// <param name="col_of_num"></param>
        void PlaceCell(int row_of_num, int col_of_num)
        {
            Cell placed_cell = GetCell(field, row_of_num * size + col_of_num + 1);
            Cell empty = GetCell(field, size * size);
            placed_cell.CheckPlace();
            if (placed_cell.GetPlaced()) //если размещена 
            {
                return;
            }
            MoveEmptyToClose(field, placed_cell);//пока не будем рядом с целью
            while (!placed_cell.GetPlaced())//двигаем пока не поставим на своё место
            {
                Cell.Direction destination_side = placed_cell.GetDirectionToMove();
                /*
                 определяем, в каком направлении двигаться
                приоритет: лево-право, вверх-вниз.
                определяем кратчайший для передвижения с учётом уже размещённых
                 */
                switch (empty.GetDirection(placed_cell))//определяем, с какой стороны граничит с целью 0
                {
                    case Cell.Direction.Up://0 снизу
                        switch (destination_side)//определяем в каком направлении мы хотели бы передвинуть целевую клетку
                        {
                            case Cell.Direction.Up://если надо вверх
                                MoveRight(field, empty);
                                MoveUp(field, empty);
                                break;
                            case Cell.Direction.Right://надо вправо
                                MoveRight(field, empty);
                                MoveUp(field, empty);
                                break;
                            case Cell.Direction.Down://надо вниз
                                MoveUp(field, empty);
                                break;
                            case Cell.Direction.Left://надо влево
                                if (empty.CanLeft())
                                {
                                    MoveLeft(field, empty);
                                    MoveUp(field, empty);
                                }
                                else
                                {
                                    MoveRight(field, empty);
                                    MoveUp(field, empty);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case Cell.Direction.Right://0 слева
                        switch (destination_side)//определяем в каком направлении мы хотели бы передвинуть целевую клетку
                        {
                            case Cell.Direction.Up://если надо вверх
                                if (empty.CanUp())
                                {
                                    MoveUp(field, empty);
                                    MoveRight(field, empty);
                                }
                                else
                                {
                                    MoveDown(field, empty);
                                    MoveRight(field, empty);
                                }
                                break;
                            case Cell.Direction.Right://надо вправо
                                if (empty.CanUp())
                                {
                                    MoveUp(field, empty);
                                    MoveRight(field, empty);
                                }
                                else
                                {
                                    MoveDown(field, empty);
                                    MoveRight(field, empty);
                                }
                                break;
                            case Cell.Direction.Down://надо вниз
                                if (empty.CanDown())
                                {
                                    MoveDown(field, empty);
                                    MoveRight(field, empty);
                                }
                                else if (empty.CanUp())
                                {
                                    MoveUp(field, empty);
                                    MoveRight(field, empty);
                                }
                                break;
                            case Cell.Direction.Left://надо влево
                                MoveRight(field, empty);
                                break;
                            default:
                                break;
                        }
                        break;
                    case Cell.Direction.Down://0 сверху
                        switch (destination_side)//определяем в каком направлении мы хотели бы передвинуть целевую клетку
                        {
                            case Cell.Direction.Up://если надо вверх
                                MoveDown(field, empty);
                                break;
                            case Cell.Direction.Right://надо вправо
                                MoveRight(field, empty);
                                MoveDown(field, empty);
                                break;
                            case Cell.Direction.Down://надо вниз
                                MoveRight(field, empty);
                                MoveDown(field, empty);
                                break;
                            case Cell.Direction.Left://надо влево
                                if (empty.CanLeft() && placed_cell.CanLeft())//и слева не установленная клетка и целевая может идти влево
                                {
                                    MoveLeft(field, empty);
                                    MoveDown(field, empty);
                                }
                                else
                                {
                                    MoveRight(field, empty);
                                    MoveDown(field, empty);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case Cell.Direction.Left://0 справа
                        switch (destination_side)//определяем в каком направлении мы хотели бы передвинуть целевую клетку
                        {
                            case Cell.Direction.Up://если надо вверх
                                MoveUp(field, empty);
                                MoveLeft(field, empty);
                                break;
                            case Cell.Direction.Right://надо вправо
                                MoveLeft(field, empty);
                                break;
                            case Cell.Direction.Down://надо вниз
                                MoveDown(field, empty);
                                MoveLeft(field, empty);
                                break;
                            case Cell.Direction.Left://надо влево
                                if ((empty.CanDown() && empty.GetPrevious() != Cell.Direction.Up) || placed_cell.GetRow() == 0)
                                {
                                    MoveDown(field, empty);
                                    MoveLeft(field, empty);
                                }
                                else
                                {
                                    MoveUp(field, empty);
                                    MoveLeft(field, empty);
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                placed_cell.CheckPlace();
            }
        }
        /// <summary>
        /// получение клетки из массива клеток
        /// </summary>
        /// <param name="field"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        Cell GetCell(Cell[,] field, int num)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j].GetValue() == num)
                    {
                        return field[i, j];
                    }
                }
            }
            return null;
        }
        /// <sпummary>
        /// условие решаемости: если сумма беспорядков  четная, тогда решаеиая 
        /// берем текущую клетку, смотрим каждую последующую. если текущая больше, чем последующая, то беспорядок
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        bool CheckSolvable(int[] arr)
        {
            var counter = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] == size * size)
                {
                    continue;
                }
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] == size * size)
                    {
                        continue;
                    }
                    if (arr[i] > arr[j])
                    {
                        counter++;
                    }
                }
            }
            counter += (Array.IndexOf(arr, size * size) / size) + 1;
            if (counter % 2 == 1)//если нерешаемая
            {
                return false;
            }
            return true;
        }
        class Cell
        {
            int value;
            bool placed;
            int actualRow;
            int actualCol;
            int targRow;
            int targCol;
            //соседи 
            Cell up; 
            Cell right;
            Cell down;
            Cell left;
            Direction previous_move; //предыдущий ход
            public Cell(int value, int actualRow, int actualCol, int size_field)
            {
                this.value = value;
                this.actualRow = actualRow;
                this.actualCol = actualCol;
                placed = false;
                targRow = (value - 1) / size_field; 
                targCol = (value - 1) % size_field;
            }
            //блок геттеров и сеттеров для полей выше
            protected internal int GetValue()
            {
                return value;
            }
            protected internal bool GetPlaced()
            {
                return placed;
            }
            protected internal void SetPlaced(bool val)
            {
                placed = val;
            }
            protected internal int GetRow()
            {
                return actualRow;
            }
            protected internal void SetRow(int val)
            {
                actualRow = val;
            }
            protected internal int GetCol()
            {
                return actualCol;
            }
            protected internal void SetCol(int val)
            {
                actualCol = val;
            }
            protected internal int GetTargRow()
            {
                return targRow;
            }
            protected internal void SetTargRow(int val)
            {
                targRow = val;
            }
            protected internal int GetTargCol()
            {
                return targCol;
            }
            protected internal void SetTargCol(int val)
            {
                targCol = val;
            }
            protected internal Cell GetUp()
            {
                return up;
            }
            protected internal void SetUp(Cell val)
            {
                up = val;
            }
            protected internal Cell GetRight()
            {
                return right;
            }
            protected internal void SetRight(Cell val)
            {
                right = val;
            }
            protected internal Cell GetDown()
            {
                return down;
            }
            protected internal void SetDown(Cell val)
            {
                down = val;
            }
            protected internal Cell GetLeft()
            {
                return left;
            }
            protected internal void SetLeft(Cell val)
            {
                left = val;
            }
            protected internal Direction GetPrevious()
            {
                return previous_move;
            }
            protected internal void SetPrevious(Direction val)
            {
                previous_move = val;
            }
            /// <summary>
            /// проверка, размещена ли она в целевой клетке
            /// </summary>
            /// <returns></returns>
            protected internal bool CheckPlace()
            {
                if (ColIsPlaced() && RowIsPlaced())
                {
                    placed = true;
                    return true;
                }
                else
                {
                    placed = false;
                    return false;
                }
            }
            /// <summary>
            /// рядом ли она с клеткой в параметре
            /// </summary>
            /// <param name="actual"></param>
            /// <returns></returns>
            protected internal bool IsNear(Cell actual)
            {
                if (up == actual || right == actual || left == actual || down == actual)
                {
                    return true;
                }
                return false;
            }
            bool RowIsPlaced()
            {
                return actualRow == targRow;
            }
            bool ColIsPlaced()
            {
                return actualCol == targCol;
            }
            /// <summary>
            /// Определяет в каком направлении находится по отношению к цели с учётом приоритета
            /// </summary>
            /// <param name="actual">Целевая клетка</param>
            /// <returns></returns>
            internal Direction GetDirectionToMove(Cell actual)
            {
                if (GetRow() > actual.GetRow() && !up.placed)//если 0 снизу цели и над 0 неустановленная клетка
                {
                    return Direction.Up;
                }
                if (GetRow() < actual.GetRow() && !down.placed)//если 0 сверху цели и под 0 неустановленная
                {
                    return Direction.Down;
                }
                if (actualCol < actual.actualCol && !right.placed)//если 0 слева от цели и справа от 0 неустановленная
                {
                    return Direction.Right;
                }
                else//если 0 справа от цели
                {
                    return Direction.Left;
                }
            }
            /// <summary>
            /// Определяет с какой стороны от вызывающей находится переданная в параметр. Работает корректно если они действительно граничат.
            /// </summary>
            /// <param name="actual"></param>
            /// <returns></returns>
            internal Direction GetDirection(Cell actual)
            {
                if (up == actual)//если 0 снизу цели 
                {
                    return Direction.Up;
                }
                if (down == actual)//если 0 сверху цели
                {
                    return Direction.Down;
                }
                if (right == actual)//если 0 слева от цели
                {
                    return Direction.Right;
                }
                else//если 0 справа от цели
                {
                    return Direction.Left;
                }
            }
            //блок методов, определяющих приоритетное направление 
            internal bool NeedUp()
            {
                if (GetRow() > targRow)
                {
                    return true;
                }
                return false;
            }
            internal bool NeedLeft()
            {
                if (actualCol > targCol)
                {
                    return true;
                }
                return false;
            }
            internal bool NeedRight()
            {
                if (actualCol < targCol)
                {
                    return true;
                }
                return false;
            }
            //блок методов, определяющих, доступно ли какое-то направление
            internal bool CanUp()
            {
                if (up != null && !up.placed)
                {
                    return true;
                }
                return false;
            }
            internal bool CanDown()
            {
                if (down != null && !down.placed)
                {
                    return true;
                }
                return false;
            }
            internal bool CanLeft()
            {
                if (left != null && !left.placed)
                {
                    return true;
                }
                return false;
            }
            public enum Direction
            {
                Up = 0, Right = 1, Down = 2, Left = 3
            }
            /// <summary>
            /// Возвращает предпочтительное направление движения перемещаемой клетки
            /// </summary>
            /// <returns></returns>
            internal Direction GetDirectionToMove()
            {
                if (NeedLeft() && !left.placed)
                {
                    return Direction.Left;
                }
                if (NeedRight() && !right.placed)
                {
                    return Direction.Right;
                }
                if (NeedUp() && !up.placed)
                {
                    return Direction.Up;
                }
                else
                {
                    return Direction.Down;
                }
            }
        }
    }

}
