using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LaboratoryThreads
{
    class Program
    {
        static void Main(string[] args)
        {
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

            var test = new double[,]
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 },
                {1, 2, 3, 4, 5, 6, 7, 8, 9 }
            };

           


            var thread = 10;
            /*var rows = test.GetLength(0);
            var columns = test.GetLength(1);
            Console.WriteLine("Current rows: " + rows);
            Console.WriteLine("Current columns: " + columns);

            double[,] newTestMatrix;
            int newRowsCount = rows;
            int newColumnsCount = columns;


            var threadMultiples = GetMultiples(thread);
            var rowMultiples = GetMultiples(rows);
            var columnMultiples = GetMultiples(columns);

            var specialNumber = PlusMoral(rowMultiples, columnMultiples, threadMultiples);
            Console.WriteLine("special number:" + specialNumber);

            do
            {
                newRowsCount = rows % 2 == 0 ? rows : ModifyLength(rows, specialNumber);
                newColumnsCount = columns % 2 == 0 ? columns : ModifyLength(columns, specialNumber);
                specialNumber = PlusMoral(GetMultiples(newRowsCount), GetMultiples(newColumnsCount), threadMultiples);
            } while (specialNumber != 0);

            RefillMatrix(out newTestMatrix, ref test, newRowsCount, newColumnsCount);
            for (int i = 0; i < newTestMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < newTestMatrix.GetLength(1); j++)
                {
                    Console.Write(newTestMatrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            var pairs = GetPairsEqualsThreadCount(GetMultiples(newRowsCount), GetMultiples(newColumnsCount), thread);
            Console.WriteLine("Pairs:");
            foreach (var pair in pairs)
            {
                Console.WriteLine(pair);
            }
            Console.WriteLine("\n\n\n");
            var norm = NormalizePairs(pairs, newRowsCount, newColumnsCount);
            Console.WriteLine(norm);*/

            BlockMultiplier blockMultiplier = new BlockMultiplier();
            blockMultiplier.ThreadMultiply(test, null, 4);









        }

        public static int ModifyLength(int currentLength, int specialNumber)
        {
            while (currentLength % specialNumber != 0)
            {
                currentLength++;
            }
            return currentLength;
        }

     
        public static void RefillMatrix(out double[,] newMatrix, ref double[,] currentMatrix, int rows, int columns)
        {
            newMatrix = new double[rows, columns];
            for (int i = 0; i < currentMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < currentMatrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = currentMatrix[i, j];
                }
            }
        }

        public static int PlusMoral(List<int> rowsMultiples, List<int> columnsMultiples, List<int> threadsMultiples)
        {
            int specialNumber = default;
            for (int i = threadsMultiples.Count - 1; i >= 0; i--) 
            {
                if (!(rowsMultiples.Contains(threadsMultiples[i]) || columnsMultiples.Contains(threadsMultiples[i])))
                {
                    specialNumber = threadsMultiples[i];
                    break;
                }              
            }

            return specialNumber;
        }
        public static (int, int) NormalizePairs(List<(int, int)> pairs, int rowsCount, int ColumnCount)
        {
            Dictionary<(int, int), int> normalizePairs = new Dictionary<(int, int), int>();
            for (int i = 0; i < pairs.Count; i++)
            {
                if (!(pairs[i].Item1.Equals(rowsCount) || pairs[i].Item2.Equals(ColumnCount)))
                {
                    normalizePairs.Add((pairs[i].Item1, pairs[i].Item2), Math.Abs(pairs[i].Item1 - pairs[i].Item2));
                }
            }

            int min = int.MaxValue;
            (int, int) resultKey = default;
            foreach (var key in normalizePairs.Keys)
            {
                if (normalizePairs[key] < min)
                {
                    min = normalizePairs[key];
                    resultKey = key;
                }
            }

            return resultKey;
        }
        public static List<(int, int)> GetPairsEqualsThreadCount(List<int> first, List<int> second, int threadCount)
        {
            List<(int, int)> pairs = new List<(int, int)>();

            for (int i = 0; i < first.Count; i++)
            {
                for (int j = 0; j < second.Count; j++)
                {
                    if ((first[i] * second[j]).Equals(threadCount))
                    {
                        pairs.Add((first[i], second[j]));                     
                    }
                }
            }

            return pairs;
        }
        public static List<int> GetMultiples(int number)
        {
            List<int> result = new List<int>();
            // rofl
            for (int i = number - 1; i > 1; i--) 
            {
                if (number % i == 0)
                {
                    result.Add(i);
                }
            }
            return result;
        }
    }
}
