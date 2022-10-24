using System;
using System.Text;

namespace LaboratoryThreads.Events
{
    public sealed class NormalizedPairEventArgs : EventArgs
    {
        public (int, int) Pair { get; }
        public NormalizedPairEventArgs((int, int) pair) => Pair = pair;
        public override string ToString() => Pair.ToString();
    }
}
