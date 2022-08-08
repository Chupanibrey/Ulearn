using System;
using System.Collections.Generic;
using System.Linq;

namespace GaussAlgorithm
{
    public class Solver
    {
        public double[] Solve(double[][] matrix, double[] freeMembers)
        {
            //копируем матрицу, чтобы не работать с оригинальной
            matrix = CopyArray(matrix);
            freeMembers = (double[])freeMembers.Clone();
            //количество столбцов в основной матрице
            int col = matrix[0].Length;

            //удаляем нулевые строчки
            (matrix, freeMembers) = DeleteZeroRows(matrix, freeMembers);
            //если все строки оказались удалены
            if (matrix.Length == 0)
                //то просто возвращаем нулевое решение
                return new double[col];
            //количество строк в основной матрице
            int row = freeMembers.Length;
            col = matrix[0].Length;
            //номер столбца, с помощью которого будем занулять
            for (int k = 0; k < row - 1; k++)
            {
                //номер строки
                for (int i = k + 1; i < row; i++)
                {
                    //если число в рассматриваемой строке равно нулю
                    //и не получилось поменять ее с какой нибудь нижестоящей,
                    //т.е столбец уже занулен, передвигаемся к следующему
                    if (matrix[k][k] == 0 && !Swap(matrix, freeMembers, k, k))
                        k++;
                    //подбираем коэффициент, и умножаем на него всю строку
                    double coef = matrix[i][k] / matrix[k][k];
                    for (int j = k; j < col; j++)
                        matrix[i][j] = matrix[i][j] - matrix[k][j] * coef;
                    freeMembers[i] = freeMembers[i] - freeMembers[k] * coef;
                }
                //удаляем нулевые строки
                (matrix, freeMembers) = DeleteZeroRows(matrix, freeMembers);
                if (matrix.Length == 0)
                    return new double[col];
                row = freeMembers.Length;
                col = matrix[0].Length;
            }
            //число уравнений равно числу неизвестных
            //решаем определнную систему уравнений
            if (matrix[0].Length == freeMembers.Length)
                return SolveDefiniteSystem(matrix, freeMembers);
            //решаем неопределнную систему уравнений
            return SolveIndefineteSystem(matrix, freeMembers);
        }

        private double[] SolveDefiniteSystem(double[][] matrix, double[] freeMembers)
        {
            int row = freeMembers.Length;
            int col = matrix[0].Length;
            //частное решение уравнения
            double[] x = new double[col];
            //матрица квадратная и на главной диагонали нет 0
            //идем снизу вверх и вычисляем значение каждой неизвестной переменной
            for (int k = row - 1; k >= 0; k--)
            {
                double s = 0;
                for (int j = k; j < col; j++)
                    s += matrix[k][j] * x[j];

                x[k] = (freeMembers[k] - s) / matrix[k][k];
            }

            return x;
        }

        private double[] SolveIndefineteSystem(double[][] matrix, double[] freeMembers)
        {
            int row = freeMembers.Length;
            int col = matrix[0].Length;
            //nullable
            double?[] x = new double?[col];
            //число свободных переменных
            int numberOfFreeX = col - row;
            //первый ненулевой элемент в строке
            int firstNotZero = 0;

            //номер строки
            for (int k = row - 1; k >= 0; k--)
            {
                //номер столбца
                for (int i = 0; i < col; i++)
                {
                    //прверка на 0 с некоторой точностью
                    if (Math.Abs(matrix[k][i]) > 0.000001)
                    {
                        firstNotZero = i;
                        //если еще есть свободные переменные
                        if (numberOfFreeX > 0)
                        {
                            numberOfFreeX -= (col - 1 - i);
                            //приравниваем их все к 0
                            for (int j = col - 1; j > i; j--)
                            {
                                if (x[j] == null)
                                    x[j] = 0;
                            }
                        }
                        break;
                    }
                }
                //вычисляем единственную неизвестную в строке
                double s = 0;
                for (int j = firstNotZero + 1; j < col; j++)
                    s += matrix[k][j] * (double)x[j];
                x[firstNotZero] = (freeMembers[k] - s) / matrix[k][firstNotZero];
            }
            //все элементы, не равные null?, просто приводим к double
            //а остальные зануляем
            return x.Select(s => (double)(s ?? 0)).ToArray();
        }
        //возврвщаем ValueTuple
        private (double[][] matrix, double[] freeMembers) DeleteZeroRows(double[][] matrix, double[] freeMembers)
        {
            List<int> notZeroRows = new List<int>();
            //номер строки
            for (int i = 0; i < freeMembers.Length; i++)
            {
                bool zeroRow = true;
                //номер столбца
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    //если встретился хоть один ненулевой элемент
                    //то переходим к следующей строке
                    if (Math.Abs(matrix[i][j]) > 0.000001)
                    {
                        zeroRow = false;
                        break;
                    }
                }
                //строка нулевая, а свободный член - нет
                if (zeroRow && Math.Abs(freeMembers[i]) > 0.000001)
                    throw new NoSolutionException("");
                //полностью нулевая строка
                if (zeroRow && Math.Abs(freeMembers[i]) < 0.000001)
                    continue;
                //добавляем номер ненулевой строки
                notZeroRows.Add(i);
            }

            var count = notZeroRows.Count;
            double[][] newMatrix = new double[count][];
            double[] newfreeMembers = new double[count];
            //записываем ненулевые строки
            for (int i = 0; i < count; i++)
            {
                newMatrix[i] = matrix[notZeroRows[i]];
                newfreeMembers[i] = freeMembers[notZeroRows[i]];
            }
            return (newMatrix, newfreeMembers);
        }

        private bool Swap(double[][] matrix, double[] freeMembers, int row, int column)
        {
            //метод пробует переставить строку под номером row
            //с любой нижестоящей строкой, в которой элемент
            //под номером column не равен 0
            //возвращает true/false, если получилось/не получилось
            bool swapped = false;
            for (int i = matrix.Length - 1; i > row; i--)
            {
                if (matrix[i][column] != 0)
                {
                    double[] temp = new double[matrix[0].Length];
                    temp = matrix[i];
                    double tempMemb = freeMembers[i];
                    matrix[i] = matrix[row];
                    freeMembers[i] = freeMembers[row];
                    matrix[row] = temp;
                    freeMembers[row] = tempMemb;
                    swapped = true;
                    break;
                }
            }
            return swapped;
        }

        private double[][] CopyArray(double[][] source)
        {
            //метод копирует массив массивов
            double[][] newArr = new double[source.Length][];
            for (int i = 0; i < source.Length; i++)
                newArr[i] = (double[])source[i].Clone();

            return newArr;
        }
    }
}