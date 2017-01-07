using System.Collections.Generic;
using ProteinSequenceAlignment.Implementations;
using ProteinSequenceAlignment.Interfaces;
using ProteinSequenceAlignment.Models;

namespace ProteinSequenceAlignment
{
	public class Program
	{			
		public static IEnumerable<Multialignment> Multialignments;
		public static float[,] SimilarityMatrix;
		public static string Dictionary;

		static void Main(string[] args)
		{
			Multialignments = DataProvider.GetMultialignments();
			SimilarityMatrix = DataProvider.GetSimilarityMatrix();
			Dictionary = DataProvider.GetDictionary();
		}

		public static IDataProvider DataProvider = new DataProvider();
	}
}
