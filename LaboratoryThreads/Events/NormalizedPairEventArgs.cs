using System;
using System.Text;

namespace LaboratoryThreads.Events
{
    public sealed class NormalizedPairEventArgs : EventArgs
    {
        public (int, int) Pair { get; }
        public NormalizedPairEventArgs((int, int) pair) => Pair = pair;
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .Append("(")
                .Append(Pair.Item1)
                .Append(", ")
                .Append(Pair.Item2)
                .Append(")");

            return stringBuilder.ToString();
        }
    }
}
