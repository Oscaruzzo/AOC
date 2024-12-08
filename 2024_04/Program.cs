namespace _2024_04
{
	internal class Program
	{
		private const int w = 140;
		private const int h = 140;
		private const string xmas = "XMAS";

		static bool SearchString(char[,] data, string s, int startX, int startY, int dx, int dy) {
			int x = startX, y = startY;
			foreach (char c in s) {
				if (x < 0 || x >= w || y < 0 || y >= h)
					return false;
				if (data[x, y] != c)
					return false;
				x += dx;
				y += dy;
			}
			return true;
		}

		static bool SearchMatrix(char[,] data, string[] m, int startX, int startY) {
			int y = startY;
			foreach (string s in m) {
				if (y >= h)
					return false;
				int x = startX;
				foreach (char c in s) {
					if (x >= w)
						return false;
					char d = data[x, y];
					if (c != '.' && c != d)
						return false;
					x++;
				}
				y++;
			}
			return true;
		}

		static void Main(string[] args) {
			char[,] data = new char[w, h];

			{
				int y = 0;
				foreach (var line in File.ReadLines("input.txt")) {
					int x = 0;
					foreach (var c in line) {
						data[x, y] = c;
						x++;
					}
					y++;
				}
			}

			{
				int xmasCount = 0;
				for (int x = 0; x < w; x++) {
					for (int y = 0; y < h; y++) {
						if (SearchString(data, xmas, x, y, +1, -1))
							xmasCount++;
						if (SearchString(data, xmas, x, y, +1, 0))
							xmasCount++;
						if (SearchString(data, xmas, x, y, +1, +1))
							xmasCount++;

						if (SearchString(data, xmas, x, y, 0, +1))
							xmasCount++;
						if (SearchString(data, xmas, x, y, 0, -1))
							xmasCount++;

						if (SearchString(data, xmas, x, y, -1, -1))
							xmasCount++;
						if (SearchString(data, xmas, x, y, -1, 0))
							xmasCount++;
						if (SearchString(data, xmas, x, y, -1, +1))
							xmasCount++;
					}
				}
				Console.WriteLine(xmasCount);
			}

			{
				int x_masCount = 0;
				string[] X1 = {
					"M.S",
					".A.",
					"M.S"};
				string[] X2 = {
					"S.S",
					".A.",
					"M.M" };
				string[] X3 = {
					"M.M",
					".A.",
					"S.S" };
				string[] X4 = {
					"S.M",
					".A.",
					"S.M" };

				for (int x = 0; x < w; x++) {
					for (int y = 0; y < h; y++) {
						if (SearchMatrix(data, X1, x, y)) {
							Console.WriteLine("X1 in {0},{1}", x, y);
							x_masCount++;
						}
						if (SearchMatrix(data, X2, x, y)) {
							Console.WriteLine("X2 in {0},{1}", x, y);
							x_masCount++;
						}
						if (SearchMatrix(data, X3, x, y)) {
							Console.WriteLine("X3 in {0},{1}", x, y);
							x_masCount++;
						}
						if (SearchMatrix(data, X4, x, y)) {
							Console.WriteLine("X4 in {0},{1}", x, y);
							x_masCount++;
						}
					}
				}
				Console.WriteLine(x_masCount);
			}
		}
	}
}