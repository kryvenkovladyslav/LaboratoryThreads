using System.Text;

namespace LaboratoryThreads
{
    public sealed class NumericMatrix : Matrix
    {
        public override double this[int rowIndex, int columnIndex]
        {
            get => CurrentMatrix[rowIndex, columnIndex];
            protected set => CurrentMatrix[rowIndex, columnIndex] = value;
        }
        public NumericMatrix(int rows, int columns) : base(rows, columns) { }
        public NumericMatrix(double[,] matrix) : base(matrix) { }

        public NumericMatrix Add(NumericMatrix other)
        {
            if (!Rows.Equals(other.Rows)|| !Columns.Equals(other.Columns))
            {
                throw new MatrixDimensionException(
                    message: "Matrix dimensions do not match",
                    expectedDimension: (Rows, Columns),
                    actualDimension: (other.Rows, other.Columns));
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
                throw new MatrixDimensionException(
                   message: "Matrix dimensions do not match",
                   expectedDimension: (Columns, 1),
                   actualDimension: (other.Rows, other.Columns));
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
                throw new MatrixDimensionException(
                   message: "Matrix dimensions do not match",
                   expectedDimension: (Rows, Columns),
                   actualDimension: (other.Rows, other.Columns));
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
        public NumericMatrix TreadMultiply(IMatrixThreadMultiplier multiplier, NumericMatrix vector, int threadsCount)
        {
            if (!Columns.Equals(vector.Rows))
            {
                throw new MatrixDimensionException(
                   message: "Matrix dimensions do not match",
                   expectedDimension: (Rows, 1),
                   actualDimension: (vector.Rows, vector.Columns));
            }
            else
            {
                var result = multiplier.ThreadMultiply(CurrentMatrix, vector.CurrentMatrix, threadsCount);
                return ToNumericMatrix(result);
            }
            
        }
        public static NumericMatrix operator -(NumericMatrix first, NumericMatrix second) => first.Substract(second);
        public static NumericMatrix operator *(NumericMatrix first, NumericMatrix second) => first.Multiply(second);
        public static NumericMatrix operator +(NumericMatrix first, NumericMatrix second) => first.Add(second);
        public static NumericMatrix ToNumericMatrix(double[,] matrix) => new NumericMatrix(matrix);
        public sealed override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < CurrentMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < CurrentMatrix.GetLength(1); j++)
                {
                    stringBuilder.Append(CurrentMatrix[i, j] + "\t");
                }
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }
    }
}
