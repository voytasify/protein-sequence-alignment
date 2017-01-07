namespace ProteinSequenceAlignment.Extensions
{
	public static class ArrayExtensions
	{
		public static T[] GetRow<T>(this T[,] matrix, int row)
		{
			var rowLength = matrix.GetLength(1);
			var rowVector = new T[rowLength];

			for (var i = 0; i < rowLength; i++)
				rowVector[i] = matrix[row, i];

			return rowVector;
		}
		public static T[] GetCol<T>(this T[,] matrix, int col)
		{
			var colLength = matrix.GetLength(0);
			var colVector = new T[colLength];

			for (var i = 0; i < colLength; i++)
				colVector[i] = matrix[i, col];

			return colVector;
		}
	}
}
