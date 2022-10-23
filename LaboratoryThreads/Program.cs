using LaboratoryThreads.Events;
using System;

namespace LaboratoryThreads
{
    class Program
    {
        public static void Main(string[] args)
        {
            var matrix = new double[,]
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
            };

            var vector = new double[,]
            {
                {1 },
                {2 },
                {3 },
                {4 },
                {5 },
                {6 },
                {7 },
                {8 },
                {9 }
            };


            NumericMatrix first = new NumericMatrix(matrix);
            NumericMatrix second = new NumericMatrix(vector);
            var defaultMultiplication = first * second;
            Console.WriteLine(defaultMultiplication);


            BlockMultiplier blockMultiplier = new BlockMultiplier();
            NumericMatrix numericMatrix = new NumericMatrix(matrix);
            var threadMultiplication = numericMatrix.TreadMultiply(blockMultiplier, new NumericMatrix(vector), 6);
            Console.WriteLine(threadMultiplication);




        }
        private static void GetPairCallback(object sender, NormalizedPairEventArgs arg)
        {
            Console.WriteLine("Got normalized pair:");
            Console.WriteLine("First: " + arg.Pair.Item1);
            Console.WriteLine("Second: " + arg.Pair.Item2);
        }
    }
}
