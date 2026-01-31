namespace _2025_04 {
    internal class Range {
        public long a { get; set; }
        public long b { get; set; }

        public Range() { }

        public Range(long a, long b) {
            this.a = a;
            this.b = b;
        }

        public long Length() {
            return b - a + 1;
        }

        public override string ToString() {
            return $"[{a}, {b}]";
        }
    };


    internal class Program {
        private static bool AreOverlapping(Range r1, Range r2) {
            if (r2.a > r1.b)
                return false;
            else
                return true;
        }
        private static Range Merge(Range r1, Range r2) {
            return new Range(r1.a, long.Max(r1.b, r2.b));
        }

        static void Main(string[] args) {
            string input = "input.txt";

            List<Range> ranges = [];
            var sortedRanges = new SortedSet<Range>(
                Comparer<Range>.Create((r1, r2) => {
                    int cmp = r1.a.CompareTo(r2.a);
                    return cmp != 0 ? cmp : r1.b.CompareTo(r2.b);
                })
            );

            bool readingRanges = true;
            int nGood = 0;
            foreach (var line in File.ReadLines(input)) {
                if (readingRanges && line.Length > 0) {
                    long a = long.Parse(line.Split('-')[0]);
                    long b = long.Parse(line.Split('-')[1]);
                    ranges.Add(new Range(a, b));
                    sortedRanges.Add(new Range(a, b));
                } else if (readingRanges && line.Length == 0) {
                    readingRanges = false;
                } else {
                    // readingRanges = false
                    long n = long.Parse(line);
                    foreach (var r in ranges) {
                        if (n >= r.a && n <= r.b) {
                            // n in range
                            nGood++;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(nGood);

            var mergedRanges = new List<Range>();
            var i = sortedRanges.GetEnumerator();
            i.MoveNext();
            Range r1 = i.Current;
            bool shouldAdd = true;
            while (i.MoveNext()) {
                Range r2 = i.Current;

                if (AreOverlapping(r1, r2)) {
                    r1 = Merge(r1, r2);
                    shouldAdd = true;
                } else {
                    mergedRanges.Add(r1);
                    r1 = r2;
                    shouldAdd = false;
                }
            }
            if (shouldAdd)
                mergedRanges.Add(r1);

            long tot = mergedRanges.Sum(r => r.Length());
            Console.WriteLine(tot);

        }
    }
}
