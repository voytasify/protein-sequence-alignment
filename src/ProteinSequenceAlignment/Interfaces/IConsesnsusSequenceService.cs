namespace ProteinSequenceAlignment.Interfaces
{
	public interface IConsesnsusSequenceService
	{
		string ComputeConsensusSequence(float[,] similarityMatrix, float[,] profileMatrix);
	}
}
