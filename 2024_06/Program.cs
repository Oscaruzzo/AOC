namespace _2024_06
{
	internal class Program
	{
		private const int w = 130;
		private const int h = 130;
		protected static void WriteAt(char s, int x, int y) {
			try {
				Console.SetCursorPosition(x, y);
				Console.Write(s);
			} catch (ArgumentOutOfRangeException e) {
				Console.Clear();
				Console.WriteLine(e.Message);
			}
		}
		static void printMatrix(char[,] data, int cx, int cy, int currentDir) {
			//Console.Clear();
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
					} else {
						WriteAt(data[x, y], x, y);
					}
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}
		static void Main(string[] args) {
			char[,] data = new char[w, h];
			int startX = -1, startY = -1;

			{ // read input
				int y = 0;
				foreach (var line in File.ReadLines("input.txt")) {
					int x = 0;
					foreach (var c in line) {
						if (c == '^') {
							data[x, y] = '.';
							startX = x;
							startY = y;
						} else {
							data[x, y] = c;
						}
						x++;
					}
					y++;
				}
			}

			{ // part 1
				int[] xDirs = { 0, +1, 0, -1 };
				int[] yDirs = { -1, 0, +1, 0 };

				int x = startX, y = startY;
				int currentDir = 0;
				int tot = 0;
				bool done = false;
				while (!done) {
					// check current position
					if (data[x, y] == '.') {
						tot++;
						data[x, y] = 'X';
					}
					//printMatrix(data, x, y, currentDir);
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
						else if (data[newX, newY] == '#') {
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

				Console.WriteLine(tot);
			}
		}
	}
}
