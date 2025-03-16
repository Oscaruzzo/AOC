using System.Text.RegularExpressions;

namespace _2024_13 {
	internal class Program {
		const string input = "input.txt";
		static void Main() {
			string[] allLines = File.ReadAllLines(input);
			long tot1 = 0;
			long tot2 = 0;
			for (long l = 0; l < allLines.Length; l += 4) {
				(long, long) da = ReadTwoNumbers(allLines[l]);
				(long, long) db = ReadTwoNumbers(allLines[l + 1]);
				(long, long) prize = ReadTwoNumbers(allLines[l + 2]);

				(long a1, long b1) = Solve(da, db, prize);
				(long a2, long b2) = Solve(da, db, (prize.Item1 + 10000000000000, prize.Item2 + 10000000000000));

				tot1 += 3 * a1 + b1;
				tot2 += 3 * a2 + b2;
				Console.WriteLine("{0} {1}", a1, b1);
				Console.WriteLine("{0} {1}", a2, b2);
				Console.WriteLine();
			}
			Console.WriteLine("[{0}]", tot1);
			Console.WriteLine("[{0}]", tot2);
		}

		private static (long a, long b) Solve((long x, long y) da, (long x, long y) db, (long x, long y) prize) {
			long a1 = da.x;
			long a2 = da.y;
			long b1 = db.x;
			long b2 = db.y;
			long c1 = prize.x;
			long c2 = prize.y;

			long det = a2 * b1 - a1 * b2;
			if (det == 0) {
				return (0, 0);
			}

			long xn = b1 * c2 - b2 * c1;
			long yn = a1 * c2 - a2 * c1;

			if (xn % det == 0 && yn % det == 0) {
				return (xn / det, -yn / det);
			} else {
				return (0, 0);
			}
		}

		private static (long, long) ReadTwoNumbers(string line) {
			var m = Regex.Matches(line, @"\d+");
			if (m.Count < 2) {
				throw new ArgumentException("The input line does not contain at least two numbers.");
			}
			return (
				long.Parse(m[0].Value),
				long.Parse(m[1].Value));
		}
	}
}
