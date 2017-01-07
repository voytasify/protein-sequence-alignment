using System;
using System.Collections.Generic;
using System.IO;
using ProteinSequenceAlignment.Interfaces;
using ProteinSequenceAlignment.Models;

namespace ProteinSequenceAlignment.Implementations
{
	public class DataProvider : IDataProvider
	{
		private const string MultialignmentsFileName = "Multialignments.txt";
		private const string SimilarityMatrixFileName = "SimilarityMatrix.txt";
		private const string DictionaryFileName = "Dictionary.txt";

		public IList<Multialignment> GetMultialignments()
		{
			var lines = File.ReadLines(MultialignmentsFileName);
			var multialignments = new List<Multialignment>();

			var multialignment = new Multialignment { Sequences = new List<string>() };
			foreach (var line in lines)
			{
				if (line == "-")
				{
					multialignments.Add(multialignment);
					multialignment = new Multialignment { Sequences = new List<string>() };
				}
				else
				{
					multialignment.Sequences.Add(line);
				}
			}

			return multialignments;
		}

		public float[,] GetSimilarityMatrix()
		{
			var lines = File.ReadAllLines(SimilarityMatrixFileName);
			var matrix = new float[lines.Length, lines.Length];

			for (var i = 0; i < lines.Length; ++i)
			{
				var values = Array.ConvertAll(lines[i].Split(' '), float.Parse);
				for (var j = 0; j < values.Length; ++j)
				{
					matrix[i, j] = values[j];
				}
			}

			return matrix;
		}

		public string GetDictionary()
			=> File.ReadAllText(DictionaryFileName);
	}
}
