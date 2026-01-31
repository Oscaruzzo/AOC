namespace _2025_03 {
    internal class Program {
        static int getMaxDigitIndex(string s, int idx1, int idx2) {
            int maxDigitIndex = idx1;
            for (int i = idx1; i < idx2; i++) {
                if (s[i] > s[maxDigitIndex])
                    maxDigitIndex = i;
            }
            return maxDigitIndex;
        }

        static void Main(string[] args) {
            int tot1 = 0;
            long tot2 = 0;
            foreach (string line in File.ReadLines("input.txt")) {
                ////////// part 1
                int a = getMaxDigitIndex(line, 0, line.Length - 1);
                int b = getMaxDigitIndex(line, a + 1, line.Length - 0);
                int n1 = 10 * (line[a] - '0') + (line[b] - '0');
                //Console.WriteLine("{0} --- {1}", line, n1);
                tot1 += n1;

                ////////// part 2
                const int nDigits = 12;
                int idx1 = 0;
                int idx2 = line.Length - nDigits + 1;
                long n2 = 0;
                for (int i = 0; i < nDigits; i++) {
                    int idx = getMaxDigitIndex(line, idx1, idx2);
                    n2 = 10 * n2 + line[idx] - '0';

                    idx1 = idx + 1;
                    idx2 = idx2 + 1;
                }
                //Console.WriteLine("{0} --- {1}", line, n2);
                tot2 += n2;
            }
            Console.WriteLine(tot1);
            Console.WriteLine(tot2);
        }
    }
}
