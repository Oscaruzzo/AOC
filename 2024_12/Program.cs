namespace _2024_12
{
	internal class Program
	{
		//private const string input = "sample1.txt";
		//private const int w = 4;
		//private const int h = 4;

		//const string input = "sample2.txt";
		//const int w = 5;
		//const int h = 5;

		//const string input = "sample3.txt";
		//const int w = 10;
		//const int h = 10;

		const string input = "sample4.txt";
		const int w = 6;
		const int h = 6;

		private static int[,] data = new int[w + 2, h + 2];
		static readonly int[] xDirs = [+1, 0, 0, -1];
		static readonly int[] yDirs = [0, +1, -1, 0];

		protected static void PrintChar(int c, int x, int y, bool highlight) {
			if (highlight)
				Console.ForegroundColor = ConsoleColor.White;
			else
				Console.ForegroundColor = ConsoleColor.Black;

			if (c < 0)
				Console.BackgroundColor = ConsoleColor.DarkBlue;
			else
				Console.BackgroundColor = ConsoleColor.Black;
		}

		protected static void PrintMatrix() {
			Console.SetCursorPosition(0, 0);
			for (int y = 0; y < h + 2; y++) {
				for (int x = 0; x < w + 2; x++) {
					if (data[x, y] == 0)
						Console.Write('#');
					else
						Console.Write((char) Math.Abs(data[x, y]));
				}
				Console.WriteLine();
			}
		}

		private static (int area, int borders) MeasureRegion(int c, int x, int y) {
			int area = 1, borders = 0;
			data[x, y] = -data[x, y];

			for (int d = 0; d < 4; d++) {
				int xn = x + xDirs[d];
				int yn = y + yDirs[d];
				int cn = data[xn, yn];
				if (cn == -c) {
					// già visto
				} else if (cn == c) {
					// contiguo
					var nRegion = MeasureRegion(c, xn, yn);
					area += nRegion.area;
					borders += nRegion.borders;
				} else {
					// bordo
					borders++;
				}
			}
			return (area, borders);
		}

		static void Main(string[] args) {
			{ // read input
				int y = 1;
				foreach (var line in File.ReadLines(input)) {
					int x = 1;
					foreach (var c in line) {
						data[x, y] = c;
						x++;
					}
					y++;
				}
			}

			{ // part 1
				int tot = 0;
				PrintMatrix();
				for (int y = 1; y <= h; y++) {
					for (int x = 1; x <= w; x++) {
						int c = data[x, y];
						// if unseen
						if (c > 0) {
							var region = MeasureRegion(c, x, y);
							Console.WriteLine(
								"{0} - Area {1} - Borders {2}                                                    ",
								(char) c, region.area, region.borders);
							tot += region.area * region.borders;
						}
					}
				}
				Console.WriteLine();
				Console.WriteLine("TOT {0}", tot);
			}
		}
	}
}
