using System.Text;

namespace LaboratoryThreads.Events
{
    public sealed class FindNewPairsEventArgs
    {
        public (int, int)[] Pairs { get; }
        public FindNewPairsEventArgs((int, int)[] pairs) => Pairs = pairs;

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Found Pairs:");
            for (int i = 0; i < Pairs.Length; i++)
            {
                stringBuilder
                    .Append("(")
                    .Append(Pairs[i].Item1)
                    .Append(", ")
                    .Append(Pairs[i].Item2)
                    .Append(")")
                    .Append("\n");
            }

            return stringBuilder.ToString();
        }
    }
}
