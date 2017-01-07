namespace ProteinSequenceAlignment.Models
{
	public class Sequence
	{
		public float Score { get; set; }
		public string Path { get; set; }
		public string One { get; set; }
		public string Two { get; set; }

		public override string ToString()
		{
			return $"score = {Score}\none = {One}\ntwo = {Two}\n\n";
		}
	}
}
