using System;
using System.Collections.Generic;
using System.Threading;

namespace LaboratoryThreads
{
    internal sealed class BlockMultiplier : IMatrixThreadMultiplier
    {
        private sealed class ThreadMultiplper
        {
            public AutoResetEvent AutoResetEvent { get; set; }
            public double[,] BlockMatrix { get; set; }
            public double[,] ResultTemproryVector { get; set; }

            public ThreadMultiplper(AutoResetEvent autoResetEvent, double[,] blockMatrix)
            {
                AutoResetEvent = autoResetEvent;
                BlockMatrix = blockMatrix;
                ResultTemproryVector = new double[BlockMatrix.GetLength(0), 1];
            }

            public void Multiply(object state)
            {

            }


        }
        public BlockMultiplier() { }
        public double[,] ThreadMultiply(double[,] matrix, double[,] vector,int threadsCount)
        {
            var rowsCount = matrix.GetLength(0);
            var columnsCount = matrix.GetLength(1);

            double[,] newMatrix;
            double[,] newVector;
            int newRowsCount = rowsCount;
            int newColumnsCount = columnsCount;

            var threadMultiples = GetMultiples(threadsCount);
            var rowMultiples = GetMultiples(rowsCount);
            var columnMultiples = GetMultiples(columnsCount);

            var specialNumber = PlusMoral(rowMultiples, columnMultiples, threadMultiples);

            do
            {
                newRowsCount = rowsCount % 2 == 0 ? rowsCount : ModifyLength(rowsCount, specialNumber);
                newColumnsCount = columnsCount % 2 == 0 ? columnsCount : ModifyLength(columnsCount, specialNumber);
                specialNumber = PlusMoral(GetMultiples(newRowsCount), GetMultiples(newColumnsCount), threadMultiples);
            } while (specialNumber != 0);

            RefillElements(out newMatrix, out newVector, ref matrix, ref vector, newRowsCount, newColumnsCount);

           for (int i = 0; i < newMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < newMatrix.GetLength(1); j++)
                {
                    Console.Write(newMatrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Vector:");
            for (int i = 0; i < newVector.GetLength(0); i++)
            {
                Console.WriteLine(newVector[i, 0]);
            }

            var pairs = GetPairsEqualsThreadCount(GetMultiples(newRowsCount), GetMultiples(newColumnsCount), threadsCount);
            Console.WriteLine("Pairs:");
            foreach (var pair in pairs)
            {
                Console.WriteLine(pair);
            }
            Console.WriteLine("\n\n\n");

            var normalizedPair = NormalizePairs(pairs, newRowsCount, newColumnsCount);
            Console.WriteLine(normalizedPair);


            var k = newMatrix.GetLength(0) / normalizedPair.Item1;
            var l = newMatrix.GetLength(1) / normalizedPair.Item2;

            Console.WriteLine(k);
            Console.WriteLine(l);
            List<double[,]> blocks = new List<double[,]>();
            for (int i = 0; i < newMatrix.GetLength(0); i++)
            {
                var n = 1;
                for (int j = 0; j < newMatrix.GetLength(1); j++)
                {
                    
                    if (j == (n * l) - 1) 
                    {

                    }
                }
            }

            


            


            return null;
        }
        private int ModifyLength(int currentLength, int specialNumber)
        {
            while (currentLength % specialNumber != 0)
            {
                currentLength++;
            }
            return currentLength;
        }
        private void RefillElements(out double[,] newMatrix, out double[,] newVector, ref double[,] matrix, ref double[,] vector, int rows, int columns)
        {
            newMatrix = new double[rows, columns];
            newVector = new double[columns, 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            for (int i = 0; i < vector.GetLength(0); i++)
            {
                newVector[i, 0] = vector[i, 0];
            }
        }
        private int PlusMoral(List<int> rowsMultiples, List<int> columnsMultiples, List<int> threadsMultiples)
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
        private (int, int) NormalizePairs(List<(int, int)> pairs, int rowsCount, int ColumnCount)
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
        private List<(int, int)> GetPairsEqualsThreadCount(List<int> first, List<int> second, int threadCount)
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
        private List<int> GetMultiples(int number)
        {
            List<int> result = new List<int>();

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
