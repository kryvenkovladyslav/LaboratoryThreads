namespace LaboratoryThreads
{
    public abstract class Matrix
    {
        public double[,] CurrentMatrix { get; protected set; }
        public int Columns { get; protected set; }
        public int Rows { get; protected set; }
        public abstract double this[int rowIndex, int columnIndex]
        {
            get;
            protected set;
        }
        
        public Matrix(int rows, int columns)
        { 
            (Rows, Columns) = (rows, columns);
            CurrentMatrix = new double[Rows, Columns];
        }
        public Matrix(double[,] matrix)
        {
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);

            CurrentMatrix = (double[,])matrix.Clone();
        }
        public double[,] ToArray() => CurrentMatrix;
    }
}
