using ProteinSequenceAlignment.Interfaces;
using ProteinSequenceAlignment.Models;
using static ProteinSequenceAlignment.Program;

namespace ProteinSequenceAlignment.Implementations
{
	public class ProfileMatrixService : IProfileMatrixService
	{
		public float[,] ComputeProfileMatrix(Multialignment multialignment)
		{
//			var profileMatrix = new float[Dictionary.Length, Dictionary.Length];
            var profileMatrix = new float[multialignment.Sequences[0].Length, Dictionary.Length]; 
			foreach (var sequence in multialignment.Sequences)
			{
				for (var col = 0; col < sequence.Length; ++col)
				{
					var c = sequence[col];
					var row = Dictionary.IndexOf(c);

					profileMatrix[col, row] += 1 / (float)multialignment.Sequences.Count;
				}
			}

			return profileMatrix;
		}
	}
}
