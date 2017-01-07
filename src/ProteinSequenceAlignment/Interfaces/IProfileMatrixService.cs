using ProteinSequenceAlignment.Models;

namespace ProteinSequenceAlignment.Interfaces
{
	public interface IProfileMatrixService
	{
		float[,] ComputeProfileMatrix(Multialignment multialignment);
	}
}
