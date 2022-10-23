using System;
using System.Text;

namespace LaboratoryThreads
{
    public sealed class Block
    {
        private double[,] Matrix { get; }

        public int Rows { get; set; }
        public int Columns { get; set; }
        public double this[int rowIndex, int columnIndex]
        {
            get => Matrix[rowIndex, columnIndex];
            set => Matrix[rowIndex, columnIndex] = value;
        }

        public Block(int rows, int columns)
        {
            (Rows, Columns) = (rows, columns);
            Matrix = new double[Rows, Columns];
        }

        public Block Add(Block other)
        {
            if (!Columns.Equals(other.Columns) || !Rows.Equals(other.Rows))
            {
                throw new Exception("Dimension does not match");
            }
            else
            {
                Block result = new Block(Rows, Columns);

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        result[i, j] = this[i, j] + other[i, j];
                    }
                }

                return result;
            }
        }
        public static Block operator +(Block first, Block second) => first.Add(second);

        public static double[,] ToArray(Block block) => block.Matrix;
        public override string ToString()
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
