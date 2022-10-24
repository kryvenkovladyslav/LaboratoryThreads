using System.Text;

namespace LaboratoryThreads.Events
{
    public sealed class CreateBlocksEventArgs
    {
        public Block[,] BlockMatrices { get; }
        public Block[,] BlockVectors { get; }

        public CreateBlocksEventArgs(Block[,] blockMatrices, Block[,] blockVectors)
            => (BlockMatrices, BlockVectors) = (blockMatrices, blockVectors);
    }
}
