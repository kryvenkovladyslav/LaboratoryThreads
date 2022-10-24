using System.Collections.Generic;
using System.Text;

namespace LaboratoryThreads.Events
{
    public sealed class FindNewPairsEventArgs
    {
        public List<(int, int)> Pairs { get; }
        public FindNewPairsEventArgs(List<(int, int)> pairs) => Pairs = pairs;

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < Pairs.Count; i++)
            {
                stringBuilder
                    .Append("(")
                    .Append(Pairs[i].Item1)
                    .Append(", ")
                    .Append(Pairs[i].Item2)
                    .Append(")");
            }

            return stringBuilder.ToString();
        }
    }
}
