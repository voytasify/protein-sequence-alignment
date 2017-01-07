using System;
using System.Linq;
using System.Text;
using ProteinSequenceAlignment.Interfaces;
using ProteinSequenceAlignment.Models;

namespace ProteinSequenceAlignment.Implementations
{
	public class NeedlemanWunschService : INeedlemanWunschService
	{
		// trace back
		const string DONE = @"�";
		const string DIAG = @"\";
		const string UP = @"|";
		const string LEFT = @"-";

		// print alignment
		const string GAP = @"-";

		public Sequence Merge(string sequence, string otherSequence)
		{
			var m = sequence.Length;
			var n = otherSequence.Length;

			var M = new float[m + 1, n + 1];
			var T = new string[m + 1, n + 1];

			for (var i = 0; i < m + 1; i++)
				M[i, 0] = i;
			for (var j = 0; j < n + 1; j++)
				M[0, j] = j;

			T[0, 0] = DONE;
			for (var i = 1; i < m + 1; i++)
				T[i, 0] = UP;
			for (var j = 1; j < n + 1; j++)
				T[0, j] = LEFT;

			// calc
			for (var i = 1; i < m + 1; i++)
			{
				for (var j = 1; j < n + 1; j++)
				{
					var alpha = Alpha(sequence.ElementAt(i - 1), otherSequence.ElementAt(j - 1));
					var diag = alpha + M[i - 1, j - 1];
					var up = M[i - 1, j];
					var left = M[i, j - 1];
					var max = Max(diag, up, left);
					M[i, j] = max;

					if (max == diag)
						T[i, j] = DIAG;
					else if (max == up)
						T[i, j] = UP;
					else
						T[i, j] = LEFT;
				}
			}

			var traceBack = ParseTraceBack(T, m + 1, n + 1);

			var sb = new StringBuilder();
			string first, second;

			if (sequence.Length != otherSequence.Length)
			{
				string s;
				if (sequence.Length > otherSequence.Length)
				{
					s = otherSequence;
					first = sequence;
				}
				else
				{
					s = sequence;
					first = otherSequence;
				}


				var i = 0;
				foreach (var trace in traceBack)
				{
					if (trace.ToString() == DIAG)
						sb.Append(s.ElementAt(i++).ToString());
					else
						sb.Append(GAP);
				}

				second = sb.ToString();
			}
			else
			{
				first = sequence;
				second = otherSequence;
			}

			PrintMatrix(T, m + 1, n + 1);
			var result = new Sequence { Score = M[m, n], Path = traceBack, One = first, Two = second };
			Console.WriteLine(result.ToString());
			return result;
		}

		private int LookUpIndex(char character)
		{
			return Program.Dictionary.IndexOf(character);
		}

		private float Alpha(char character, char otherCharacter)
		{
			var charIndex = LookUpIndex(character);
			var otherCharIndex = LookUpIndex(otherCharacter);
			if (charIndex == -1 || otherCharIndex == -1)
				throw new ArgumentException($"Similarity matrix does not contain value for specified keys: {character}, {otherCharIndex}");

			return Program.SimilarityMatrix[charIndex, otherCharIndex];
		}

		private void PrintMatrix<T>(T[,] A, int I, int J)
		{
			for (var i = 0; i < I; i++)
			{
				for (var j = 0; j < J; j++)
				{
					var v = A[i, j];
					Console.Write(v + " ");
				}
				Console.WriteLine();
			}
		}

		private string ParseTraceBack(string[,] T, int I, int J)
		{
			var sb = new StringBuilder();
			var i = I - 1;
			var j = J - 1;
			var path = T[i, j];
			while (path != DONE)
			{
				sb.Append(path);
				if (path == DIAG)
				{
					i--;
					j--;
				}
				else if (path == UP)
					i--;
				else if (path == LEFT)
					j--;

				path = T[i, j];
			}
			return ReverseString(sb.ToString());
		}

		private string ReverseString(string s)
		{
			var arr = s.ToCharArray();
			Array.Reverse(arr);
			return new string(arr);
		}

		private float Max(float a, float b, float c)
		{
			if (a >= b && a >= c)
				return a;
			if (b >= a && b >= c)
				return b;
			return c;
		}
	}
}