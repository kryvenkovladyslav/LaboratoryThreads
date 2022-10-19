using System.Text;

namespace LaboratoryThreads
{
    public sealed class Block
    {
        public double[,] matrix { get; set; }

        public Block(int rows, int columns) => matrix = new double[rows, columns];

        public override string ToString()
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
