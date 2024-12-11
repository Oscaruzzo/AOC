namespace _2024_08
{
	internal class Program
	{
		const int nAntennaTypes = 62;
		const int w = 50, h = 50;
		private const string input = "input.txt";
		//const int w = 10, h = 10;
		//private const string input = "sample2.txt";

		static int CharToId(char c) {
			if (c >= '0' && c <= '9') {
				// 0 - 9
				return c - '0';
			} else if (c >= 'A' && c <= 'Z') {
				// 10 - 35
				return (c - 'A') + 10;
			} else if (c >= 'a' && c <= 'z') {
				// 36 - 61
				return (c - 'a') + 36;
			}
			return -1;
		}

		static bool IsInField((int x, int y) node) {
			if (node.x >= 0 && node.y >= 0 && node.x < w && node.y < h) {
				return true;
			} else {
				return false;
			}
		}

		static void Main(string[] args) {
			List<(int x, int y)>[] Antennas = new List<(int x, int y)>[nAntennaTypes];
			for (int i = 0; i < Antennas.Length; i++) {
				Antennas[i] = [];
			}

			{ // read input
				int y = 0;
				foreach (var line in File.ReadLines(input)) {
					int x = 0;
					foreach (char c in line) {
						if (c != '.') {
							var antId = CharToId(c);
							Antennas[antId].Add((x, y));
						}
						x++;
					}
					y++;
				}
				Console.WriteLine();
			}

			{ // part 1
				HashSet<(int x, int y)> nodes = [];
				foreach (var antennaType in Antennas) {
					for (int i = 0; i < antennaType.Count - 1; i++) {
						for (int j = i + 1; j < antennaType.Count; j++) {
							(int x, int y) a = antennaType[i];
							(int x, int y) b = antennaType[j];

							int dx = a.x - b.x;
							int dy = a.y - b.y;

							(int x, int y) n1 = (a.x + dx, a.y + dy);
							(int x, int y) n2 = (b.x - dx, b.y - dy);

							if (IsInField(n1))
								nodes.Add(n1);
							if (IsInField(n2))
								nodes.Add(n2);
						}
					}
				}
				Console.WriteLine(nodes.Count);
			}

			{ // part 1
				HashSet<(int x, int y)> nodes = [];
				foreach (var antennaType in Antennas) {
					for (int i = 0; i < antennaType.Count - 1; i++) {
						for (int j = i + 1; j < antennaType.Count; j++) {
							(int x, int y) a = antennaType[i];
							(int x, int y) b = antennaType[j];

							int dx = a.x - b.x;
							int dy = a.y - b.y;

							(int x, int y) n;

							n = a;
							while (IsInField(n)) {
								nodes.Add(n);
								n = (n.x + dx, n.y + dy);
							}

							n = b;
							while (IsInField(n)) {
								nodes.Add(n);
								n = (n.x - dx, n.y - dy);
							}
						}
					}
				}
				Console.WriteLine(nodes.Count);
			}
		}
	}
}
