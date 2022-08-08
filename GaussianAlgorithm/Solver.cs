using System;
using System.Collections.Generic;
using System.Linq;

namespace GaussAlgorithm
{
    public class Solver
    {
        public double[] Solve(double[][] matrix, double[] freeMembers)
        {
            //�������� �������, ����� �� �������� � ������������
            matrix = CopyArray(matrix);
            freeMembers = (double[])freeMembers.Clone();
            //���������� �������� � �������� �������
            int col = matrix[0].Length;

            //������� ������� �������
            (matrix, freeMembers) = DeleteZeroRows(matrix, freeMembers);
            //���� ��� ������ ��������� �������
            if (matrix.Length == 0)
                //�� ������ ���������� ������� �������
                return new double[col];
            //���������� ����� � �������� �������
            int row = freeMembers.Length;
            col = matrix[0].Length;
            //����� �������, � ������� �������� ����� ��������
            for (int k = 0; k < row - 1; k++)
            {
                //����� ������
                for (int i = k + 1; i < row; i++)
                {
                    //���� ����� � ��������������� ������ ����� ����
                    //� �� ���������� �������� �� � ����� ������ �����������,
                    //�.� ������� ��� �������, ������������� � ����������
                    if (matrix[k][k] == 0 && !Swap(matrix, freeMembers, k, k))
                        k++;
                    //��������� �����������, � �������� �� ���� ��� ������
                    double coef = matrix[i][k] / matrix[k][k];
                    for (int j = k; j < col; j++)
                        matrix[i][j] = matrix[i][j] - matrix[k][j] * coef;
                    freeMembers[i] = freeMembers[i] - freeMembers[k] * coef;
                }
                //������� ������� ������
                (matrix, freeMembers) = DeleteZeroRows(matrix, freeMembers);
                if (matrix.Length == 0)
                    return new double[col];
                row = freeMembers.Length;
                col = matrix[0].Length;
            }
            //����� ��������� ����� ����� �����������
            //������ ����������� ������� ���������
            if (matrix[0].Length == freeMembers.Length)
                return SolveDefiniteSystem(matrix, freeMembers);
            //������ ������������� ������� ���������
            return SolveIndefineteSystem(matrix, freeMembers);
        }

        private double[] SolveDefiniteSystem(double[][] matrix, double[] freeMembers)
        {
            int row = freeMembers.Length;
            int col = matrix[0].Length;
            //������� ������� ���������
            double[] x = new double[col];
            //������� ���������� � �� ������� ��������� ��� 0
            //���� ����� ����� � ��������� �������� ������ ����������� ����������
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
            //����� ��������� ����������
            int numberOfFreeX = col - row;
            //������ ��������� ������� � ������
            int firstNotZero = 0;

            //����� ������
            for (int k = row - 1; k >= 0; k--)
            {
                //����� �������
                for (int i = 0; i < col; i++)
                {
                    //������� �� 0 � ��������� ���������
                    if (Math.Abs(matrix[k][i]) > 0.000001)
                    {
                        firstNotZero = i;
                        //���� ��� ���� ��������� ����������
                        if (numberOfFreeX > 0)
                        {
                            numberOfFreeX -= (col - 1 - i);
                            //������������ �� ��� � 0
                            for (int j = col - 1; j > i; j--)
                            {
                                if (x[j] == null)
                                    x[j] = 0;
                            }
                        }
                        break;
                    }
                }
                //��������� ������������ ����������� � ������
                double s = 0;
                for (int j = firstNotZero + 1; j < col; j++)
                    s += matrix[k][j] * (double)x[j];
                x[firstNotZero] = (freeMembers[k] - s) / matrix[k][firstNotZero];
            }
            //��� ��������, �� ������ null?, ������ �������� � double
            //� ��������� ��������
            return x.Select(s => (double)(s ?? 0)).ToArray();
        }
        //���������� ValueTuple
        private (double[][] matrix, double[] freeMembers) DeleteZeroRows(double[][] matrix, double[] freeMembers)
        {
            List<int> notZeroRows = new List<int>();
            //����� ������
            for (int i = 0; i < freeMembers.Length; i++)
            {
                bool zeroRow = true;
                //����� �������
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    //���� ���������� ���� ���� ��������� �������
                    //�� ��������� � ��������� ������
                    if (Math.Abs(matrix[i][j]) > 0.000001)
                    {
                        zeroRow = false;
                        break;
                    }
                }
                //������ �������, � ��������� ���� - ���
                if (zeroRow && Math.Abs(freeMembers[i]) > 0.000001)
                    throw new NoSolutionException("");
                //��������� ������� ������
                if (zeroRow && Math.Abs(freeMembers[i]) < 0.000001)
                    continue;
                //��������� ����� ��������� ������
                notZeroRows.Add(i);
            }

            var count = notZeroRows.Count;
            double[][] newMatrix = new double[count][];
            double[] newfreeMembers = new double[count];
            //���������� ��������� ������
            for (int i = 0; i < count; i++)
            {
                newMatrix[i] = matrix[notZeroRows[i]];
                newfreeMembers[i] = freeMembers[notZeroRows[i]];
            }
            return (newMatrix, newfreeMembers);
        }

        private bool Swap(double[][] matrix, double[] freeMembers, int row, int column)
        {
            //����� ������� ����������� ������ ��� ������� row
            //� ����� ����������� �������, � ������� �������
            //��� ������� column �� ����� 0
            //���������� true/false, ���� ����������/�� ����������
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
            //����� �������� ������ ��������
            double[][] newArr = new double[source.Length][];
            for (int i = 0; i < source.Length; i++)
                newArr[i] = (double[])source[i].Clone();

            return newArr;
        }
    }
}