using System.Text.RegularExpressions;

namespace _2024_07
{
	internal class Program
	{
		const string inputFile = "input.txt";
		private static bool CheckOps(long pResult, long pPartial, List<long> pOperands, int i) {
			if (i >= pOperands.Count) {
				return pPartial == pResult;
			}

			if (CheckOps(pResult, pPartial + pOperands[i], pOperands, i + 1)) {
				return true;
			}

			if (CheckOps(pResult, pPartial * pOperands[i], pOperands, i + 1)) {
				return true;
			}

			return false;
		}

		static void Main() {
			long tot = 0;
			foreach (string line in File.ReadLines(inputFile)) {
				string[] lSplitLine = line.Split(':');
				long lResult = long.Parse(lSplitLine[0]);
				List<long> lOperands = Regex.Matches(lSplitLine[1], @"\d+").Select(g => long.Parse(g.Value)).ToList();

				if (CheckOps(lResult, lOperands[0], lOperands, 1)) {
					tot += lResult;
				}
			}
			Console.WriteLine(tot);
		}
	}
}
