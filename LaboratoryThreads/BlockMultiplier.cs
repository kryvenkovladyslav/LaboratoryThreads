using System;
using System.Collections.Generic;
using System.Text;

namespace LaboratoryThreads
{
    internal class BlockMultiplier : IMatrixThreadMultiplier
    {
        public BlockMultiplier() { }
        public double[,] ThreadMultiply(double[,] matrix, double[,] vector,int threadsCount)
        {
            var rowsCount = matrix.GetLength(0);
            var columnsCount = matrix.GetLength(1);

            double[,] newMatrix;
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

            RefillMatrix(out newMatrix, ref matrix, newRowsCount, newColumnsCount);
            for (int i = 0; i < newMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < newMatrix.GetLength(1); j++)
                {
                    Console.Write(newMatrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            var pairs = GetPairsEqualsThreadCount(GetMultiples(newRowsCount), GetMultiples(newColumnsCount), threadsCount);
            Console.WriteLine("Pairs:");
            foreach (var pair in pairs)
            {
                Console.WriteLine(pair);
            }
            Console.WriteLine("\n\n\n");
            var norm = NormalizePairs(pairs, newRowsCount, newColumnsCount);
            Console.WriteLine(norm);

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


        private void RefillMatrix(out double[,] newMatrix, ref double[,] currentMatrix, int rows, int columns)
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

        public double[,] ThreadMultiply(double[,] matrix, int threadsCount)
        {
            throw new NotImplementedException();
        }
    }
}
