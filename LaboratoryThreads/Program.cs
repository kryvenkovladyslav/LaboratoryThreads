using LaboratoryThreads.Events;
using System;
using System.Collections.Generic;

namespace LaboratoryThreads
{
    class Program
    {
        private static readonly List<int> threadsCount = new List<int>(new int[] { 4, 6, 8 });
        private static BlockMultiplier blockMultiplier = new BlockMultiplier();
        private static string answerMode = string.Empty;
        private const string parallel = "PARALLEL";
        private const string single = "SINGLE";
        private static bool done = false;
        public static void Main(string[] args)
        {
            NumericMatrix matrix = new NumericMatrix(new double[,]
            {
                {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
                {11, 12, 13, 14, 15, 16, 17, 18, 19, 20},
                {21, 22, 23, 24, 25, 26, 27, 28, 29, 30 },
                {31, 32, 33, 34, 35, 36, 37, 38, 39, 40 },
                {41, 42, 43, 44, 45, 46, 47, 48, 49, 50 },
                {51, 52, 53, 54, 55, 56, 57, 58, 59, 60 }

            });

            NumericMatrix vector = new NumericMatrix(new double[,]
            {
                {15 },
                {225 },
                {34 },
                {0 },
                {51 },
                {666 },
                {17 },
                {2000 },
                {4 },
                {-50 }
            });

            NumericMatrix result = null;


            while (!done)
            {
                Console.WriteLine("Choose a way you want to perform multiplication [in SINGlE thread or in PARALLEL threading]: ");
                var answerMode = Console.ReadLine().ToUpper();

                switch (answerMode)
                {
                    case single:
                        result = matrix * vector;
                        done = true;
                        break;

                    case parallel:
                        Console.WriteLine("You can use only 4, 6 and 8 thread.");
                        Console.Write("How many do you want:");
                        var answer = int.TryParse(Console.ReadLine(), out int threadCount);

                        if (answer != false && threadsCount.Contains(threadCount))
                        {
                            Console.Write("Do you want to see the multiplication process [YES or NO]: ");
                            SetOutput(Console.ReadLine().ToUpper().Equals("YES") ? true : false);
                            Console.WriteLine("\n");

                            result = matrix.TreadMultiply(blockMultiplier, vector, threadCount);
                            done = true;
                        }
                        else
                        {
                            Console.WriteLine("Something went wrong.");
                            Console.WriteLine("You must have choose wrong threads count or write string line. Retry...");
                        }
                        break;

                    default:
                        Console.WriteLine("Something went wrong.");
                        Console.WriteLine("Retry...");
                        break;
                }
                
            }

            Console.WriteLine("Result:");
            Console.WriteLine(result);
        }
        private static void SetOutput(bool showOutput)
        {
            if (showOutput)
            {
                blockMultiplier.OnNormalizedPair += GetNormalizedPairCallback;
                blockMultiplier.OnCreatedBlocks += GetCreatedBlocksCallbak;
                blockMultiplier.OnFoundNewPair += GetFindPairsCallBack;
                blockMultiplier.OnFoundSpecialParameters += GetFindSpecialParametesCallbak;
            }
        }
        private static void GetNormalizedPairCallback(object sender, NormalizedPairEventArgs args)
        {
            Console.WriteLine("Got normalized pair: " + args.ToString());
        }
        private static void GetCreatedBlocksCallbak(object sender, CreateBlocksEventArgs args)
        {
            Console.WriteLine("Created blocks:");
            for (int i = 0; i < args.BlockMatrices.GetLength(0); i++)
            {
                for (int j = 0; j < args.BlockMatrices.GetLength(1); j++)
                {
                    Console.WriteLine(args.BlockMatrices[i, j]);
                }
            }
        }
        private static void GetFindSpecialParametesCallbak(object sender, FindSpecialParametersEvetArgs args)
        {
            Console.WriteLine("Found Parameters:");
            Console.WriteLine(args.ToString());
        }
        private static void GetFindPairsCallBack(object sender, FindNewPairsEventArgs args)
        {
            Console.WriteLine("Found Pairs: " + args.ToString());
        }
    }
}
