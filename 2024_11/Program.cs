using System.Text.RegularExpressions;

namespace _2024_11
{
	internal class Program
	{
		private static string input = "input.txt";

		private record Stone
		{
			public required string Str {
				get; init;
			}
			public required Int64 Count {
				get; set;
			}
		}

		private static Dictionary<Int64, Stone> stonesIn = [], stonesOut = [];

		private static void AddStone(Int64 n, string s, Int64 c) {
			if (stonesOut.TryGetValue(n, out Stone? stone)) {
				stone.Count += c;
			} else {
				stonesOut.Add(n, new Stone { Str = s, Count = c });
			}
		}

		private static void PrintStones() {
			foreach (var stoneEntry in stonesOut) {
				Console.Write("{0}x{1}  ", stoneEntry.Key, stoneEntry.Value.Count);
			}
			Console.WriteLine();
		}

		static void Main() {
			string line = File.ReadAllText(input);
			foreach (Match match in Regex.Matches(line, @"\d+")) {
				Int64 n = Int64.Parse(match.Value);
				AddStone(n, match.Value, 1);
			}
			PrintStones();
			Console.WriteLine();

			for (int nBlink = 1; nBlink <= 75; nBlink++) {
				stonesIn = stonesOut;
				stonesOut = [];

				foreach (var stoneEntry in stonesIn) {
					if (stoneEntry.Key == 0) {
						AddStone(1, "1", stoneEntry.Value.Count);
					} else if (stoneEntry.Value.Str.Length % 2 == 0) {
						string wholeStone = stoneEntry.Value.Str;
						int splitSize = wholeStone.Length / 2;
						string l = wholeStone.Substring(0, splitSize);
						string r = wholeStone.Substring(splitSize, splitSize);
						Int64 nl = Int64.Parse(l);
						Int64 nr = Int64.Parse(r);
						AddStone(nl, nl.ToString(), stoneEntry.Value.Count);
						AddStone(nr, nr.ToString(), stoneEntry.Value.Count);
					} else {
						Int64 nn = stoneEntry.Key * 2024;
						AddStone(nn, nn.ToString(), stoneEntry.Value.Count);
					}
				}
				//PrintStones();

				Int64 tot = stonesOut.Sum(entry => entry.Value.Count);

				Console.WriteLine("{0} {1} {2}", nBlink, stonesOut.Count, tot);

				Console.WriteLine();
			}
			//Console.WriteLine(stonesIn.Count);
		}
	}
}
