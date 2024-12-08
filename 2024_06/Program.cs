namespace _2024_06
{
	internal class Program
	{
		private const int w = 130;
		private const int h = 130;
		private const string input = "input.txt";
		private const bool print = false;

		static readonly int[] xDirs = [0, +1, 0, -1];
		static readonly int[] yDirs = [-1, 0, +1, 0];

		private const int WALL = 1000;
		private const int SPACE = 0;

		protected static void WriteAt<T>(T s, int x, int y) {
			try {
				Console.SetCursorPosition(x, y);
				Console.Write(s);
			} catch (ArgumentOutOfRangeException e) {
				Console.Clear();
				Console.WriteLine(e.Message);
			}
		}
		protected static void PrintMatrix(int[,] data) {
			for (int y = 0; y < h; y++) {
				for (int x = 0; x < w; x++) {
					if (data[x, y] == WALL) {
						WriteAt('█', x, y);
					} else if (data[x, y] == SPACE) {
						WriteAt('·', x, y);
					}
				}
			}
			Console.WriteLine();
		}

		protected static int Walk(int[,] data, byte[,] path, int startX, int startY) {
			int x = startX, y = startY;
			int currentDir = 0;
			int len = 0;
			bool done = false;

			if (print)
				PrintMatrix(data);

			while (!done) {
				// check current position
				if (data[x, y] == SPACE && path[x, y] == 0) {
					len++;
				}
				if (0 != (path[x, y] & 1 << currentDir)) {
					// already been here in this direction! it's a loop
					return -1;
				}
				path[x, y] |= (byte) (1 << currentDir);

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

				if (print) {
					WriteAt('░', x, y);

					if (!done) {
						switch (currentDir) {
						case 0:
							WriteAt('↑', newX, newY);
							break;
						case 1:
							WriteAt('→', newX, newY);
							break;
						case 2:
							WriteAt('↓', newX, newY);
							break;
						case 3:
							WriteAt('←', newX, newY);
							break;
						}
					}
				}

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

		protected static void ResetPath(byte[,] path) {
			for (int x = 0; x < w; x++)
				for (int y = 0; y < h; y++)
					path[x, y] = 0;
		}

		static void Main() {
			int[,] data = new int[w, h];
			byte[,] firstPath = new byte[w, h];
			byte[,] tentativePath = new byte[w, h];
			int startX = -1, startY = -1;

			{ // read input
				int y = 0;
				foreach (var line in File.ReadLines(input)) {
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
				int walkLen = Walk(data, firstPath, startX, startY);
				Console.SetCursorPosition(0, h + 2);
				Console.WriteLine(walkLen);
			}

			{ // part 2
				int nLoops = 0;
				for (int x = 0; x < w; x++) {
					for (int y = 0; y < h; y++) {
						if (firstPath[x, y] != 0) {
							ResetPath(tentativePath);
							if (data[x, y] != WALL && (x != startX || y != startY)) {
								data[x, y] = WALL;
								if (Walk(data, tentativePath, startX, startY) == -1)
									nLoops++;
								data[x, y] = SPACE;
							}
						}
					}
				}
				Console.SetCursorPosition(0, h + 3);
				Console.WriteLine(nLoops);
			}
		}
	}
}
