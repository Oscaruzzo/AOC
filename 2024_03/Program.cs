using System.Text.RegularExpressions;

namespace _2024_03
{
	internal class Program
	{
		static void Main(string[] args) {
			var input = File.ReadAllText("input.txt");

			var muls = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)");
			int tot = 0;
			foreach (Match mul in muls) {
				tot += int.Parse(mul.Groups[1].Value) * int.Parse(mul.Groups[2].Value);
			}
			Console.WriteLine(tot);

			bool active = true;
			var dodontmuls = Regex.Matches(input, @"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\)");
			int tot2 = 0;
			foreach (Match m in dodontmuls) {
				if (active && m.Value.StartsWith("mul")) {
					tot2 += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
				} else if (m.Value.StartsWith("don't")) {
					active = false;
				} else if (m.Value.StartsWith("do")) { // controllo pleonastico
					active = true;
				}
			}
			Console.WriteLine(tot2);
		}
	}
}