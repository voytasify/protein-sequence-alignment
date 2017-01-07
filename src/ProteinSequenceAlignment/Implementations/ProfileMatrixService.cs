using ProteinSequenceAlignment.Interfaces;
using ProteinSequenceAlignment.Models;
using static ProteinSequenceAlignment.Program;

namespace ProteinSequenceAlignment.Implementations
{
	public class ProfileMatrixService : IProfileMatrixService
	{
		public float[,] ComputeProfileMatrix(Multialignment multialignment)
		{
			var profileMatrix = new float[Dictionary.Length, Dictionary.Length];

			foreach (var sequence in multialignment.Sequences)
			{
				for (var i = 0; i < sequence.Length; ++i)
				{
					var c = sequence[i];
					var row = Dictionary.IndexOf(c);
					var col = i;

					profileMatrix[col, row] += 1 / (float)multialignment.Sequences.Count;
				}
			}

			return profileMatrix;
		}
	}
}
