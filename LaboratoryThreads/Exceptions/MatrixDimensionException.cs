using System;
using System.Text;

namespace LaboratoryThreads.Exceptions
{
    public sealed class MatrixDimensionException : Exception
    {
        public (int, int) ExpectedDimension { get; }
        public (int, int) ActualDimension { get; }
        public MatrixDimensionException(string message, (int, int) expectedDimension, (int, int) actualDimension)
            : base(message)
        {
            ExpectedDimension = expectedDimension;
            ActualDimension = actualDimension;
        }

        public override sealed string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .Append("\nMessage: " + Message)
                .Append("\nExpected Dimension: " + ExpectedDimension)
                .Append("\nActual Dimension: " + ActualDimension);

            return stringBuilder.ToString();
        }
    }
}
