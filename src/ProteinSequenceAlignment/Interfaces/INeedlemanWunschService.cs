namespace ProteinSequenceAlignment.Interfaces
{
	public interface INeedlemanWunschService
	{
		string Merge(string sequence, string otherSequence);
	}
}
