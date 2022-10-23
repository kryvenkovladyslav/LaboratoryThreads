using LaboratoryThreads.Events;
using System;
using System.Collections.Generic;
using System.Threading;

namespace LaboratoryThreads
{
    public sealed class BlockMultiplier<TMatrix> : IMatrixThreadMultiplier<TMatrix>
    {
        public event EventHandler<NormalizedPairEventArgs> OnNormalizedPair;

        private sealed class ThreadMultiplier
        {
            public AutoResetEvent AutoResetEvent { get; set; }
            private Block Block { get; set; }
            private Block Vector { get; set; }
            public Block ResultBlockVector { get; set; }

            public ThreadMultiplier(AutoResetEvent autoResetEvent, Block block, Block vector)
            {
                ResultBlockVector = new Block(block.Rows, vector.Columns);
                AutoResetEvent = autoResetEvent;
                Vector = vector;
                Block = block;
            }

            public void Perform()
            {
                
                ThreadPool.QueueUserWorkItem(callBack => 
                {
                    for (int i = 0; i < Block.Rows; i++)
                    {
                        for (int j = 0; j < Vector.Columns; j++)
                        {
                            for (int k = 0; k < Block.Columns; k++)
                            {
                                ResultBlockVector[i, j] += Block[i, k] * Vector[k, j];
                            }
                        }
                    }
                    AutoResetEvent.Set();
                });
            }
        }

        private Block[,] blocks;
        private Block[,] blockVectors;
        private Block[,] resultBlockVectors;
      
        public BlockMultiplier(INumericMatrix<TMatrix> matrix) { }
       
        public INumericMatrix<TMatrix> ThreadMultiply(INumericMatrix<TMatrix> matrix, INumericMatrix<TMatrix> vector, int threadsCount)
        {
            var rowsCount = matrix.Rows;
            var columnsCount = matrix.Columns;
            var resultVector = new double[rowsCount, vector.Columns];

            double[,] newMatrix;
            double[,] newVector;
            double[,] currentMatrix = matrix.ToArray();
            double[,] currentVecotor = vector.ToArray();
            var newRowsCount = rowsCount;
            var newColumnsCount = columnsCount;

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

            RefillElements(out newMatrix, out newVector, ref currentMatrix, ref currentVecotor, newRowsCount, newColumnsCount);

            var pairs = GetPairsEqualsThreadCount(GetMultiples(newRowsCount), GetMultiples(newColumnsCount), threadsCount);

            var normalizedPair = NormalizePairs(pairs, newRowsCount, newColumnsCount);
            GotNewPair(normalizedPair);

            var k = newMatrix.GetLength(0) / normalizedPair.Item1;
            var l = newMatrix.GetLength(1) / normalizedPair.Item2;

            var threadMultipliers = new ThreadMultiplier[threadsCount];
            var resetEvents = new AutoResetEvent[threadsCount];

            blocks = new Block[normalizedPair.Item1, normalizedPair.Item2];
            blockVectors = new Block[normalizedPair.Item1, normalizedPair.Item2];
            resultBlockVectors = new Block[normalizedPair.Item1, normalizedPair.Item2];
            

            int index = default;

            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    blockVectors[i, j] = new Block(l, 1);
                    for (int a = 0; a < l; a++)
                    {
                        blockVectors[i, j][a, 0] = newVector[l * j + a, 0];
                    }
                }
            }
           
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    blocks[i, j] = new Block(k, l);
                    for (int a = 0; a < k; a++)
                    {
                        for (int b = 0; b < l; b++)
                        {
                            blocks[i, j][a, b] = newMatrix[k * i + a, l * j + b];
                        }

                    }
                    index = i * blocks.GetLength(1) + j;
                    resetEvents[index] = new AutoResetEvent(false);
                    threadMultipliers[index] = new ThreadMultiplier(resetEvents[index], blocks[i, j], blockVectors[i, j]);
                    threadMultipliers[index].Perform();
                }
            }

            WaitHandle.WaitAll(resetEvents);

            int position = 0;
            
            for (int i = 0; i < resultBlockVectors.GetLength(0); i++)
            {
                for (int j = 0; j < resultBlockVectors.GetLength(1); j++)
                {
                    resultBlockVectors[i, j] = threadMultipliers[position++].ResultBlockVector;
                }
            }


            double[,] resultNumericVector = new double[matrix.Rows, vector.Columns];
            List<double[,]> numericBlocksList = new List<double[,]>();

            for (int i = 0; i < resultBlockVectors.GetLength(0); i++)
            {
                Block victor = new Block(k, vector.Columns);

                for (int j = 0; j < resultBlockVectors.GetLength(1); j++)
                {
                    victor += resultBlockVectors[i, j];
                }

                numericBlocksList.Add(Block.ToArray(victor));
            }

            int resultCurrentPossition = 0;
            for (int i = 0; i < numericBlocksList.Count; i++)
            {
                var item = numericBlocksList[i];
                int otherCurrrentPossition = 0;

                while (resultCurrentPossition < resultNumericVector.GetLength(0) && otherCurrrentPossition < item.GetLength(0))
                {
                    resultNumericVector[resultCurrentPossition++, 0] = item[otherCurrrentPossition++, 0];
                }
            }

            for (int i = 0; i < resultNumericVector.GetLength(0); i++)
            {
                for (int j = 0; j < resultNumericVector.GetLength(1); j++)
                {
                    Console.WriteLine(resultNumericVector[i,j]);
                }
            }
            return matrix;
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

        #region events 
        private void OnNewNormalizedPair(NormalizedPairEventArgs pair)
        {
            var onEvent = Volatile.Read(ref OnNormalizedPair);
            onEvent?.Invoke(this, pair);
        }
        private void GotNewPair((int, int) pair) => OnNewNormalizedPair(new NormalizedPairEventArgs(pair));
        #endregion


    }
}
