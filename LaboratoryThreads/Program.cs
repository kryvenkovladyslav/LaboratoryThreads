using System;
using System.Diagnostics;

namespace LaboratoryThreads
{
    class Program
    {
        static void Main(string[] args)
        {
           var firstArray = new double[,]
            {
                {1,2,3,0 },
                {4,6,7,2 },
                {1,2,3,0 }
            };

            var secondArray = new double[,]
            {
                {3,2 },
                {4,2 },
                {7,2 },
                {1,2 }
            };

            var watch = new Stopwatch();

            NumericMatrix first = new NumericMatrix(firstArray);
            NumericMatrix second = new NumericMatrix(secondArray);

            watch.Start();
            var result = first * second;
            watch.Stop();

            Console.WriteLine("Result:");
            Console.WriteLine(result.ToString());
            Console.WriteLine("Spent time in ms: " + watch.Elapsed.TotalMilliseconds);

        


          

      




        }
    }
}
