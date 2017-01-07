using System;
using System.Collections.Generic;
using ProteinSequenceAlignment.Implementations;
using ProteinSequenceAlignment.Interfaces;
using ProteinSequenceAlignment.Models;

namespace ProteinSequenceAlignment
{
	public class Program
	{
		public static IList<Multialignment> Multialignments;
		public static float[,] SimilarityMatrix;
		public static string Dictionary;

		static void Main(string[] args)
		{
			Multialignments = DataProvider.GetMultialignments();
			SimilarityMatrix = DataProvider.GetSimilarityMatrix();
			Dictionary = DataProvider.GetDictionary();

			var profileMatrix = ProfileMatrixService.ComputeProfileMatrix(Multialignments[0]);
			var consensusSequence = ConsesnsusSequenceService.ComputeConsensusSequence(SimilarityMatrix, profileMatrix);

			Console.WriteLine($"CONSENUS SEQUENCE: {consensusSequence}");
			Console.Read();
		}

		public static IDataProvider DataProvider = new DataProvider();
		public static IProfileMatrixService ProfileMatrixService = new ProfileMatrixService();
		public static IConsesnsusSequenceService ConsesnsusSequenceService = new ConsensusSequenceService();
	}
}
