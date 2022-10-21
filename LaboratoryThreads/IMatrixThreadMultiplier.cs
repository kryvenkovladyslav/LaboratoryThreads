namespace LaboratoryThreads
{
    public interface IMatrixThreadMultiplier<TMatrix>
    {
        public INumericMatrix<TMatrix> ThreadMultiply(INumericMatrix<TMatrix> matrix, INumericMatrix<TMatrix> vector, int threadsCount);
    }
}
