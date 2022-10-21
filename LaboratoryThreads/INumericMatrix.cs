namespace LaboratoryThreads
{
    public interface INumericMatrix<TMatrix>
    {
        public double[,] Matrix { get; }
        public int Columns { get; }
        public int Rows { get; }
        public double this[int rowIndex, int columnIndex]
        {
            get;
            set;
        }
        public TMatrix Add(TMatrix other);
        public TMatrix Multiply(TMatrix other);
        public TMatrix Substract(TMatrix other);
        public double[,] ToArray();
    }
}
