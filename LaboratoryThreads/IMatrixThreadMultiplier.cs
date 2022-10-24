namespace LaboratoryThreads
{
    public interface IMatrixThreadMultiplier
    {
        public double[,] ThreadMultiply(in double[,] matrix, in double[,] vector, int threadsCount);
    }
}
