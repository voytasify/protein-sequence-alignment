using System.Collections.Generic;
using ProteinSequenceAlignment.Models;

namespace ProteinSequenceAlignment.Interfaces
{
	public interface IDataProvider
	{
		IList<Multialignment> GetMultialignments();
		float[,] GetSimilarityMatrix();
		string GetDictionary();
	}
}
