namespace LaboratoryThreads
{
    internal interface IMatrixThreadMultiplier
    {
        public double[,] ThreadMultiply(double[,] matrix, double[,] vector, int threadsCount);
    }
}
