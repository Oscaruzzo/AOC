namespace _2025_01 {
    internal class Program {
        static void Main(string[] args) {
            {
                int pos = 50;
                int zeroCount1 = 0;
                int zeroCount2 = 0;
                bool wasZero = false;
                foreach (var line in File.ReadLines("input.txt")) {
                    var dir = line[0];
                    var value = int.Parse(line[1..]);
                    
                    if (dir == 'R') {
                        pos += value;
                        zeroCount2 += (pos / 100);
                    } else if (dir == 'L') {
                        pos -= value;
                        zeroCount2 += ((100-pos) / 100) - (wasZero ? 1 : 0);
                    }

                    pos = (pos%100 + 100) % 100; // keep pos within [0, 99]
                    
                    if (pos == 0) {
                        zeroCount1++;
                        wasZero = true;
                    } else {
                        wasZero = false;
                    }
                }
                Console.WriteLine(zeroCount1);
                Console.WriteLine(zeroCount2);
            }
        }
    }
}
