using System.Text;

namespace LaboratoryThreads.Events
{
    public sealed class CreateBlocksEventArgs
    {
        public Block[,] BlockMatrices { get; }
        public Block[,] BlockVectors { get; }

        public CreateBlocksEventArgs(Block[,] blockMatrices, Block[,] blockVectors)
            => (BlockMatrices, BlockVectors) = (blockMatrices, blockVectors);

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Block Matrices:");
            for (int i = 0; i < BlockMatrices.GetLength(0); i++)
            {
                for (int j = 0; j < BlockMatrices.GetLength(1); j++)
                {
                    stringBuilder.Append(BlockMatrices[i, j]);
                }
            }

            stringBuilder.Append("Block Vectors:");
            for (int i = 0; i < BlockVectors.GetLength(0); i++)
            {
                for (int j = 0; j < BlockVectors.GetLength(1); j++)
                {
                    stringBuilder.Append(BlockVectors[i, j]);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
