using System;
using System.Text;

namespace LaboratoryThreads
{
    internal sealed class NumericMatrix : INumericMatrix<NumericMatrix>
    {
        public double[,] Matrix { get; private set; }
        public int Columns { get; set;}
        public int Rows { get; set; }
        public double this[int rowIndex, int columnIndex]
        {
            get => Matrix[rowIndex, columnIndex];
            set => Matrix[rowIndex, columnIndex] = value;
        }
        public NumericMatrix(int rows, int columns)
        {
            (Rows, Columns) = (rows, columns);
            Matrix = new double[Rows, Columns];
        }
        public NumericMatrix(double[,] array)
        {
            Rows = array.GetLength(0);
            Columns = array.GetLength(1);

            Matrix = (double[,])array.Clone();
        }
        public NumericMatrix Add(NumericMatrix other)
        {
            if (!Rows.Equals(other.Rows)|| !Columns.Equals(other.Columns))
            {
                throw new ArgumentException("Dimensions do not match.");
            }
            else
            {
                var resultMatrix = new NumericMatrix(Rows, Columns);

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        resultMatrix[i, j] = this[i, j] + other[i, j];
                    }
                }

                return resultMatrix;
            }
        }
        public NumericMatrix Multiply(NumericMatrix other)
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
                            resultMatrix[i, j] += this[i, k] * other[k, j];
                        }
                    }
                }
                return resultMatrix;
            }   
        }
        public NumericMatrix Substract(NumericMatrix other)
        {
            if (!Rows.Equals(other.Rows) || !Columns.Equals(other.Columns))
            {
                throw new ArgumentException("Dimensions do not match.");
            }
            else
            {
                var resultMatrix = new NumericMatrix(Rows, Columns);

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        resultMatrix[i, j] = this[i, j] - other[i, j];
                    }
                }

                return resultMatrix;
            }
        }
        public static NumericMatrix operator -(NumericMatrix first, NumericMatrix second) => first.Substract(second);
        public static NumericMatrix operator *(NumericMatrix first, NumericMatrix second) => first.Multiply(second);
        public static NumericMatrix operator +(NumericMatrix first, NumericMatrix second) => first.Add(second);
        public static NumericMatrix ToMatrix(double[,] array) => new NumericMatrix(array);
        public double[,] ToArray() => Matrix;
        public sealed override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    stringBuilder.Append(Matrix[i, j] + "\t");
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }
    }
}
