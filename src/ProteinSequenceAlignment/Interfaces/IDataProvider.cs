using System.Collections.Generic;
using ProteinSequenceAlignment.Models;

namespace ProteinSequenceAlignment.Interfaces
{
	public interface IDataProvider
	{
		IEnumerable<Multialignment> GetMultialignments();
		float[,] GetSimilarityMatrix();
		string GetDictionary();
	}
}
