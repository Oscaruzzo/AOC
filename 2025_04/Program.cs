namespace _2025_04 {
    internal class Program {
        static char[,] data;
        static int w, h;

        static char[,] readCharMatrix(string filename, int w, int h) {
            char[,] data = new char[h, w];
            int row = 0;
            foreach (var line in File.ReadLines(filename)) {
                int col = 0;
                foreach (var c in line) {
                    data[row, col] = c;
                    col++;
                }
                row++;
            }
            return data;
        }

        static int hasRoll(int row, int col) {
            if (row < 0 || row >= h || col < 0 || col >= w)
                return 0;
            else if (data[row, col] == '@') {
                return 1;
            } else {
                return 0;
            }
        }
        static int countNeighbours(int row, int col) {
            return
                hasRoll(row + 1, col - 1) +
                hasRoll(row + 1, col) +
                hasRoll(row + 1, col + 1) +
                hasRoll(row, col - 1) +
                hasRoll(row, col + 1) +
                hasRoll(row - 1, col - 1) +
                hasRoll(row - 1, col) +
                hasRoll(row - 1, col + 1);
        }

        static void Main(string[] args) {
            //data = readCharMatrix("sample.txt", 10, 10);
            //w = 10;
            //h = 10;

            data = readCharMatrix("input.txt", 140, 140);
            w = 140;
            h = 140;

            int n2 = 0;
            int n1 = 0;
            do {
                n1 = 0;
                for (int row = 0; row < h; row++)
                    for (int col = 0; col < w; col++)
                        if (data[row, col] == '@' && countNeighbours(row, col) < 4) {
                            n1++;
                            data[row, col] = ' ';
                        }
                n2 += n1;
            } while (n1 != 0);

            Console.WriteLine(n2);
        }
    }
}
