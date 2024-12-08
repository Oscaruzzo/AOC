namespace _2024_06
{
	internal class Program
	{
		private const int w = 130;
		private const int h = 130;

		static int[] xDirs = [0, +1, 0, -1];
		static int[] yDirs = [-1, 0, +1, 0];

		private const int WALL = 1000;
		private const int SPACE = 0;
		private const int SEEN = 2000;

		protected static void WriteAt(char s, int x, int y) {
			try {
				Console.SetCursorPosition(x, y);
				Console.Write(s);
			} catch (ArgumentOutOfRangeException e) {
				Console.Clear();
				Console.WriteLine(e.Message);
			}
		}
		protected static void PrintMatrix(int[,] data, int cx, int cy, int currentDir) {
			for (int y = 0; y < h; y++) {
				for (int x = 0; x < w; x++) {
					if (x == cx && y == cy) {
						switch (currentDir) {
						case 0:
							WriteAt('↑', x, y);
							break;
						case 1:
							WriteAt('→', x, y);
							break;
						case 2:
							WriteAt('↓', x, y);
							break;
						case 3:
							WriteAt('←', x, y);
							break;
						}
					} else if (data[x, y] == WALL) {
						WriteAt('#', x, y);
					} else if (data[x, y] == SPACE) {
						WriteAt('·', x, y);
					} else { // SEEN
						WriteAt('o', x, y);
					}
				}
			}
			Console.WriteLine();
		}

		protected static int Walk(int[,] data, int startX, int startY) {
			int x = startX, y = startY;
			int currentDir = 0;
			int len = 0;
			bool done = false;
			while (!done) {
				// check current position
				if (data[x, y] == SPACE) {
					len++;
				}
				if (0 != (data[x, y] & 1 << currentDir)) {
					// already been here in this direction! it's a loop
					return -1;
				}
				data[x, y] |= 1 << currentDir;

				//PrintMatrix(data, x, y, currentDir);
				int newX, newY;
				do {
					newX = x + xDirs[currentDir];
					newY = y + yDirs[currentDir];
					// is out of matrix?
					if (newX < 0 || newY < 0 || newX >= w || newY >= h) {
						done = true;
						break;
					}
					// is against wall?
					else if (data[newX, newY] == WALL) {
						currentDir = (currentDir + 1) % 4;
					}

					// can go
					else {
						break;
					}
				} while (true);
				x = newX;
				y = newY;
			}

			return len;
		}

		protected static void ResetData(int[,] data) {
			for (int x = 0; x < w; x++)
				for (int y = 0; y < h; y++)
					if (data[x, y] != WALL)
						data[x, y] = SPACE;
		}

		static void Main(string[] args) {
			int[,] data = new int[w, h];
			int startX = -1, startY = -1;

			{ // read input
				int y = 0;
				foreach (var line in File.ReadLines("input.txt")) {
					int x = 0;
					foreach (var c in line) {
						if (c == '#') {
							data[x, y] = WALL;
						} else if (c == '.') {
							data[x, y] = SPACE;
						} else if (c == '^') {
							data[x, y] = SPACE;
							startX = x;
							startY = y;
						}
						x++;
					}
					y++;
				}
			}

			{ // part 1
				int walkLen = Walk(data, startX, startY);
				Console.WriteLine(walkLen);
			}

			{ // part 2
				int nLoops = 0;
				for (int x = 0; x < w; x++) {
					for (int y = 0; y < h; y++) {
						ResetData(data);
						if (data[x, y] != WALL && (x != startX || y != startY)) {
							data[x, y] = WALL;
							if (Walk(data, startX, startY) == -1)
								nLoops++;
							data[x, y] = SPACE;
						}
					}
				}
				Console.WriteLine(nLoops);
			}
		}
	}
}
