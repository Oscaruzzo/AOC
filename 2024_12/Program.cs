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

		const string input = "input.txt";
		const int w = 140;
		const int h = 140;

		private static int[,] data = new int[w + 2, h + 2];
		static readonly int[] xDirs = [+1, 0, 0, -1];
		static readonly int[] yDirs = [0, +1, -1, 0];

		protected static void PrintMatrix(int cx, int cy) {
			for (int y = 0; y < h + 2; y++) {
				Console.SetCursorPosition(0, y);
				for (int x = 0; x < w + 2; x++) {
					if (x == cx && y == cy)
						Console.BackgroundColor = ConsoleColor.DarkBlue;
					else if (data[x, y] < 0)
						Console.BackgroundColor = ConsoleColor.DarkGreen;
					else
						Console.BackgroundColor = ConsoleColor.Black;

					if (data[x, y] == 0)
						Console.Write('#');
					else
						Console.Write((char) Math.Abs(data[x, y]));
				}
			}
			Console.BackgroundColor = ConsoleColor.Black;
		}

		private static (int area, int borders) MeasureRegion(int c, int x, int y) {
			int area = 1, borders = 0;
			data[x, y] = -data[x, y];
			PrintMatrix(x, y);
			for (int d = 0; d < 4; d++) {
				int xn = x + xDirs[d];
				int yn = y + yDirs[d];
				int cn = data[xn, yn];
				if (cn == -c) {
					// già visto
				} else if (cn == c) {
					// contiguo
					var nRegion = MeasureRegion(c, xn, yn);
					PrintMatrix(x, y);
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
				for (int y = 1; y <= h; y++) {
					for (int x = 1; x <= w; x++) {
						int c = data[x, y];
						// if unseen
						if (c > 0) {
							var region = MeasureRegion(c, x, y);
							Console.WriteLine(
								"{0} - Area {1} - Borders {2}                                                    ",
								(char) c, region.area, region.borders);
						}
					}
				}
			}
		}
	}
}
