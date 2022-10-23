using System.Text;

namespace LaboratoryThreads.Events
{
    public sealed class FindSpecialParametersEvetArgs
    {
        public int K { get; }
        public int L { get; }

        public FindSpecialParametersEvetArgs(int k, int l) => (K, L) = (k, l);

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder
                .Append("L: ")
                .Append(L)
                .Append("\n")
                .Append("K: ")
                .Append(K);

            return stringBuilder.ToString();
        }
    }
}
