using System.Text.RegularExpressions;

namespace _2025_02 {
    internal class Program {
        static bool IsInvalid1(string a) {
            return Regex.IsMatch(a, @"^(\d+)\1$");
        }
        static bool IsInvalid2(string a) {
            return Regex.IsMatch(a, @"^(\d+)\1+$");
        }

        static void Main(string[] args) {
            string input = File.ReadLines("input.txt").First();
            long tot1 = 0, tot2 = 0;
            foreach (string r in input.Split(',')) {
                long a = long.Parse(r.Split('-')[0]);
                long b = long.Parse(r.Split('-')[1]);
                for (long n=a; n<=b; n++) { 
                    if (IsInvalid1(n.ToString())) {
                        tot1 += n;
                    }
                    if (IsInvalid2(n.ToString())) {
                        tot2 += n;
                    }
                }
            }
            Console.WriteLine(tot1);
            Console.WriteLine(tot2);
        } 
    }
}
