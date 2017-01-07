using ProteinSequenceAlignment.Models;

namespace ProteinSequenceAlignment.Interfaces
{
	public interface INeedlemanWunschService
	{
		Sequence Merge(string sequence, string otherSequence);
	}
}
