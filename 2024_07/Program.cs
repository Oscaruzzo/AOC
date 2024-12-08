using System.Text.RegularExpressions;

namespace _2024_07
{
	internal class Program
	{
		const string inputFile = "input.txt";
		static int nCall0 = 0, nCall1 = 0, nCall2 = 0;
		private static bool CheckOps0(long pResult, long pPartial, List<long> pOperands, int i) {
			nCall0++;
			if (i >= pOperands.Count) {
				return pPartial == pResult;
			}

			if (CheckOps0(pResult, long.Parse(pPartial.ToString() + pOperands[i].ToString()), pOperands, i + 1)) {
				return true;
			}

			if (CheckOps0(pResult, pPartial * pOperands[i], pOperands, i + 1)) {
				return true;
			}

			if (CheckOps0(pResult, pPartial + pOperands[i], pOperands, i + 1)) {
				return true;
			}

			return false;
		}

		private static long CheckOps1(long pResult, long pPartial, List<long> pOperands, int i, string dbgStr) {
			nCall1++;
			//Console.WriteLine(dbgStr + " = " + pPartial.ToString());

			if (i >= pOperands.Count) {
				return pPartial - pResult;
			}

			if (pPartial > pResult) {
				return 1;
			}

			long lOperand = pOperands[i];
			long checkMul = CheckOps1(pResult, pPartial * lOperand, pOperands, i + 1, dbgStr + " * " + lOperand.ToString());
			if (checkMul == 0) {
				return 0;
			} else if (checkMul > 0 || lOperand <= 1) {
				long checkSum = CheckOps1(pResult, pPartial + lOperand, pOperands, i + 1, dbgStr + " + " + lOperand.ToString());
				if (checkSum == 0) {
					return 0;
				}
			}
			return 1;
		}

		private static long CheckOps2(long pResult, long pPartial, List<long> pOperands, int i, string dbgStr) {
			nCall2++;
			//Console.WriteLine(dbgStr + " = " + pPartial.ToString());

			if (i >= pOperands.Count) {
				return pPartial - pResult;
			}

			if (pPartial > pResult) {
				return 1;
			}

			long lOperand = pOperands[i];

			long checkConcat = CheckOps2(pResult, long.Parse(pPartial.ToString() + lOperand.ToString()), pOperands, i + 1, dbgStr + lOperand.ToString());
			if (checkConcat == 0) {
				return 0;
			} else if (checkConcat > 0) {
				long checkMul = CheckOps2(pResult, pPartial * lOperand, pOperands, i + 1, dbgStr + " * " + lOperand.ToString());
				if (checkMul == 0) {
					return 0;
				} else if (checkMul > 0 || lOperand <= 1) {
					long checkSum = CheckOps2(pResult, pPartial + lOperand, pOperands, i + 1, dbgStr + " + " + lOperand.ToString());
					if (checkSum == 0) {
						return 0;
					}
				}
			}
			return 1;
		}

		static void Main() {
			long tot1 = 0, tot2 = 0;
			foreach (string line in File.ReadLines(inputFile)) {
				string[] lSplitLine = line.Split(':');
				long lResult = long.Parse(lSplitLine[0]);
				List<long> lOperands = Regex.Matches(lSplitLine[1], @"\d+").Select(g => long.Parse(g.Value)).ToList();

				//bool res1 = CheckOps1(lResult, lOperands[0], lOperands, 1);

				// Part 1
				bool res1 = 0 == CheckOps1(lResult, lOperands[0], lOperands, 1, lOperands[0].ToString());
				if (res1) {
					tot1 += lResult;
				}

				// Part 2
				bool res2 = 0 == CheckOps2(lResult, lOperands[0], lOperands, 1, lOperands[0].ToString());
				if (res2) {
					tot2 += lResult;
				}
			}
			Console.WriteLine("tot1 = {0}, nCalls = {1}", tot1, nCall1);
			Console.WriteLine("tot2 = {0}, nCalls = {1}", tot2, nCall2);
		}
	}
}
