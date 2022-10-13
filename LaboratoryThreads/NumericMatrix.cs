using System;
using System.Collections.Generic;
using System.Text;

namespace LaboratoryThreads
{
    internal sealed class NumericMatrix
    {
        private int rows;
        private int columns;
        private readonly double[,] matrix;

        public int Rows
        {
            get => rows;
            private set => rows = value;
        }
        public int Columns
        {
            get => columns;
            private set => columns = value;
        }
        public double this[int rowIndex, int columnIndex] 
        {
            get => matrix[rowIndex, columnIndex];
            set => matrix[rowIndex, columnIndex] = value;
        }


        public NumericMatrix(int rows, int columns)
        {
            (Rows, Columns) = (rows, columns);
            matrix = new double[Rows, Columns];
        }
        public NumericMatrix(double[,] array)
        {
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);

            matrix = (double[,])array.Clone();
        }
        public NumericMatrix ThreadMultiply(IMatrixThreadMultiplier threadMultiplier, int threadsCount)
        {
            //var result = threadMultiplier.ThreadMultiply((double[,])matrix.Clone(), threadsCount);



            return null;
        }
        private NumericMatrix Multiply(NumericMatrix other)
        {
            if (!Columns.Equals(other.Rows))
            {
                throw new ArgumentException("Current column count does not equal other matrix row count.");
            }
            else
            {
                var resultMatrix = new NumericMatrix(Rows, other.Columns);

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < other.Columns; j++)
                    {
                        for (int k = 0; k < Columns; k++)
                        {
                            resultMatrix[i, j] += matrix[i, k] * other[k, j];
                        }
                    }
                }
                return resultMatrix;
            }   
        }
        public static NumericMatrix operator *(NumericMatrix first, NumericMatrix second) => first.Multiply(second);
        public sealed override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    stringBuilder.Append(matrix[i, j] + "\t");
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }
    }
}
