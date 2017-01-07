using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProteinSequenceAlignment.Interfaces;

namespace ProteinSequenceAlignment.Implementations
{
	public class NeedlemanWunschService : INeedlemanWunschService
	{
		readonly Dictionary<string, int> lookup = new Dictionary<string, int>();
		static readonly string NL = Environment.NewLine;

		// trace back
		const string DONE = @"¤";
		const string DIAG = @"\";
		const string UP = @"|";
		const string LEFT = @"-";

		// print alignment
		const string GAP = @"-";

		public string Merge(string sequence, string otherSequence)
		{
			var res = SequenceAlign(sequence, otherSequence);
			return string.Empty;
		}

		Sequence SequenceAlign(string xs, string ys)
		{
			int m = xs.Length;
			int n = ys.Length;

			// init the matrix
			var M = new int[m + 1, n + 1]; // dynamic programming buttom up memory table
			var T = new string[m + 1, n + 1]; // trace back

			for (int i = 0; i < m + 1; i++)
				M[i, 0] = i;
			for (int j = 0; j < n + 1; j++)
				M[0, j] = j;

			T[0, 0] = DONE;
			for (int i = 1; i < m + 1; i++)
				T[i, 0] = UP;
			for (int j = 1; j < n + 1; j++)
				T[0, j] = LEFT;

			// calc
			for (int i = 1; i < m + 1; i++)
			{
				for (int j = 1; j < n + 1; j++)
				{
					var alpha = Alpha(xs.ElementAt(i - 1).ToString(), ys.ElementAt(j - 1).ToString());
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

			if (xs.Length != ys.Length)
			{
				string s;
				if (xs.Length > ys.Length)
				{
					s = ys;
					first = xs;
				}
				else
				{
					s = xs;
					first = ys;
				}


				int i = 0;
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
				first = xs;
				second = ys;
			}

			PL("\nScore table");
			PrintMatrix(M, m + 1, n + 1);
			PL("\nTraceBack");
			PrintMatrix(T, m + 1, n + 1);
			PL();

			var sequence = new Sequence() { Score = M[m, n], Path = traceBack, One = first, Two = second };
			return sequence;
		}

		int Alpha(string x, string y)
		{
			if (!lookup.ContainsKey(x) || !lookup.ContainsKey(y))
				throw new ArgumentException($"Similarity matrix does not contain value for specified keys: {x}, {y}");

			var i = lookup[x];
			var j = lookup[y];
			return matrix[i, j];
		}

		static void PrintMatrix<T>(T[,] A, int I, int J)
		{
			for (int i = 0; i < I; i++)
			{
				for (int j = 0; j < J; j++)
				{
					var v = A[i, j];
					P(v + " ");
				}
				PL();
			}
		}

		static string ParseTraceBack(string[,] T, int I, int J)
		{
			var sb = new StringBuilder();
			int i = I - 1;
			int j = J - 1;
			string path = T[i, j];
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

		static string ReverseString(string s)
		{
			char[] arr = s.ToCharArray();
			Array.Reverse(arr);
			return new string(arr);
		}

		static int Max(int a, int b, int c)
		{
			if (a >= b && a >= c)
				return a;
			if (b >= a && b >= c)
				return b;
			return c;
		}

		class Sequence
		{
			public int Score { get; set; }
			public string Path { get; set; }
			public string One { get; set; }
			public string Two { get; set; }
			public new string ToString()
			{
				var s = string.Format("score = {0}{1}one = {2}{3}two = {4}\n\n", Score, NL, One, NL, Two);
				return s;
			}
		}

		public static void PL(object o) { Console.WriteLine(o); } //alias
		public static void PL() { Console.WriteLine(); } //alias
		public static void P(object o) { Console.Write(o); } //alias
	}
}