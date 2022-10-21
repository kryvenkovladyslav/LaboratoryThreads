using System;

namespace LaboratoryThreads.Events
{
    public class GetPairEventArgs : EventArgs
    {
        public (int, int) Pair;
        public GetPairEventArgs((int, int) pair) => Pair = pair;
    }
}
