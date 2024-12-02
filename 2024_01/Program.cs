using System.Text.RegularExpressions;

namespace _2024_01
{
	internal class Program
	{
		static void Main(string[] args) {
			var lLines = File.ReadAllLines(args[0]);

			List<int> lNumbers1 = [];
			List<int> lNumbers2 = [];

			foreach (var line in lLines) {
				var m = Regex.Matches(line, @"\d+");
				lNumbers1.Add(int.Parse(m[0].Value));
				lNumbers2.Add(int.Parse(m[1].Value));
			}

			lNumbers1.Sort();
			lNumbers2.Sort();

			int rTot = 0;
			for (int i = 0; i < lNumbers1.Count; i++) {
				int d = lNumbers1[i] - lNumbers2[i];
				rTot += d > 0 ? d : -d;
			}

			Console.WriteLine(rTot);

			var lCounts2 = lNumbers2.CountBy(n => n).ToDictionary();

			int rSim = 0;
			foreach (int n in lNumbers1) {
				int c;
				if (lCounts2.TryGetValue(n, out c)) {
					rSim += n * c;
				}
			}

			Console.WriteLine(rSim);
		}
	}
}