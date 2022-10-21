using LaboratoryThreads.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace LaboratoryThreads
{
    class Program
    {
        public static void Main(string[] args)
        {
            #region base multiplication
            /*var firstArray = new double[,]
            {
                 {1,2,3,0 },
                 {4,6,7,2 },
                 {1,2,3,0 }
            };

             var secondArray = new double[,]
             {
                 {3},
                 {4},
                 {7},
                 {1}
             };

             var watch = new Stopwatch();

             NumericMatrix first = new NumericMatrix(firstArray);
             NumericMatrix second = new NumericMatrix(secondArray);

             watch.Start();
             var result = first * second;
             watch.Stop();

             Console.WriteLine("Result:");
             Console.WriteLine(result.ToString());
             Console.WriteLine("Spent time in ms: " + watch.Elapsed.TotalMilliseconds);*/
            #endregion

            var matrix = new double[,]
            {
                {1, 2, 3, 4, 5, 6 },
                {7, 8, 9, 10 , 11, 12},
                {13, 14, 15, 16, 17, 18 },
                {19, 20, 21, 22, 23, 24}
            };

            var vector = new double[,]
            {
                {1 },
                {2 },
                {3 },
                {4 },
                {5 },
                {6 }
                
            };

            BlockMultiplier<NumericMatrix> blockMultiplier = new BlockMultiplier<NumericMatrix>(new NumericMatrix(matrix));
            blockMultiplier.OnGetPair += GetPairCallback;

            var res = blockMultiplier.ThreadMultiply(new NumericMatrix(matrix), new NumericMatrix(vector), 4);





        }
        private static void GetPairCallback(object sender, GetPairEventArgs arg)
        {
            Console.WriteLine("Got normalized pair:");
            Console.WriteLine("First: " + arg.Pair.Item1);
            Console.WriteLine("Second: " + arg.Pair.Item2);
        }
    }
}
