using System;
using System.Text;

namespace LaboratoryThreads.Exceptions
{
    class ThreadCountException : Exception
    {
        public int MaxThreadsCount { get; }
        public int ActualTheadsCount { get; }
        public ThreadCountException(string message, int actualThreadsCount)
            : base(message)
        {
            MaxThreadsCount = 8;
            ActualTheadsCount = actualThreadsCount;
        }

        public override sealed string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .Append("\nMessage: " + Message)
                .Append("\nMax Threads Count: " + MaxThreadsCount)
                .Append("\nActual Threads Count: " + ActualTheadsCount);

            return stringBuilder.ToString();
        }
    }
}
