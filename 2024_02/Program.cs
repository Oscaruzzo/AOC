using System.Text.RegularExpressions;

namespace _2024_02
{
	internal partial class Program
	{
		static bool checkErrorInLine(int[] lNumbers, out int rErrorPosition) {
			Console.Write("[{0}]", string.Join(", ", lNumbers));
			var lDirection = (lNumbers[1] > lNumbers[0]) ? +1 : -1;
			for (int i = 0; i < lNumbers.Length - 1; i++) {
				var delta = lDirection * (lNumbers[i + 1] - lNumbers[i]);
				if (delta < 1 || delta > 3) {
					rErrorPosition = i;
					Console.WriteLine("KO - {0}", i);
					return false;
				}
			}

			// safe, no errors
			rErrorPosition = -1;
			Console.WriteLine("OK");
			return true;
		}

		static void Main(string[] args) {
			var lLines = File.ReadLines("input.txt");

			var nSafe = 0;
			var nSafe2 = 0;
			int nLines = 0;
			foreach (var line in lLines) {
				nLines++;
				var lNumbers = Regex.Matches(line, @"\d+").Select(m => int.Parse(m.Value)).ToArray();
				int lErrorPosition;
				bool isSafe = checkErrorInLine(lNumbers, out lErrorPosition);
				if (isSafe) {
					nSafe++;
					nSafe2++;
				} else {
					int lMin = Math.Max(lErrorPosition - 1, 0);
					int lMax = Math.Min(lErrorPosition + 1, lNumbers.Length - 1);
					for (int i = lMin; i <= lMax; i++) {
						var lLessNumbers = lNumbers.Where((number, index) => index != i).ToArray();
						if (checkErrorInLine(lLessNumbers, out int lErrorPosition2)) {
							nSafe2++;
							break;
						}
					}
					Console.WriteLine();
				}
			}
			Console.WriteLine(nSafe);
			Console.WriteLine(nSafe2);
			Console.WriteLine(nLines);
		}
	}
}
