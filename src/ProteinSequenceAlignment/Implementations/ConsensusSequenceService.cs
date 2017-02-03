using System.Linq;
using System.Reflection.Emit;
using ProteinSequenceAlignment.Extensions;
using ProteinSequenceAlignment.Interfaces;
using static ProteinSequenceAlignment.Program;

namespace ProteinSequenceAlignment.Implementations
{
	public class ConsensusSequenceService : IConsesnsusSequenceService
	{
		public string ComputeConsensusSequence(float[,] similarityMatrix, float[,] profileMatrix)
		{
			var consensusArray = profileMatrix;

			for (var row = 0; row < consensusArray.GetLength(1); ++row)
			{
				for (var col = 0; col < consensusArray.GetLength(0); ++col)
				{
					consensusArray[col, row] *= similarityMatrix.GetCol(row).Sum();
				}
			}

			var consensusSequence = new char[consensusArray.GetLength(0)];
			for (var col = 0; col < consensusArray.GetLength(0); ++col)
			{
				var column = consensusArray.GetRow(col).ToList();
				var maxRowIndex = column.IndexOf(column.Max());
				consensusSequence[col] = Dictionary[maxRowIndex];
			}

			return new string(consensusSequence);
		}
	}
}
