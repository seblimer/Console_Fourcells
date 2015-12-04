using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Board {
		private int[,] board;
		private int size;
		private int[] seqArray;
		private int nextNum;

		public Board(int size) {
			seqArray = new int[size * size / 4];
			this.size = size + 2;
			board = new int[this.size, this.size];
			for(int i = 0; i < this.size; i++) {
				for(int j = 0; j < this.size; j++) {
					if((i == 0) || (i == this.size - 1)) {
						board[i, j] = -1;
					}
					else if((j == 0) || (j == this.size - 1)) {
						board[i, j] = -1;
					}
				}
			}
		}

		public int blockWeight(int num) {
			if(num < 0) {
				return 4;
			}
			num--;
			return num >= 0 ? seqArray[num] : 1;
		}

		public int seqNum() {
			int len = seqArray.Length;
			for(int i = 0; i < len; i++) {
				if(seqArray[i] == 0) {
					return i + 1;
				}
			}
			return 0;
		}

		public bool check(int point) {
			point = point + 11;
			nextNum = board[point / 10, point % 10];
			return nextNum != 0 ? false : true;

		}

		public bool check(int[] point) {
			List<int> strange = new List<int>();
			strange.Add(0);
			for(int i = 0; i < point.Length; i++) {
				point[i] = point[i] + 11;
				if(board[point[i] / 10, point[i] % 10] > 0) {
					int tmp = board[point[i] / 10, point[i] % 10];
					try {
						foreach(int st in strange) {
							if(st != tmp) {
								strange.Add(tmp);
							}
						}
					}
					catch { }
				}
				else if(board[point[i] / 10, point[i] % 10] < 0) {
					return false;
				}
			}

			if(strange.Count == 2) {
				nextNum = strange[1];
			}
			else {
				nextNum = 0;
			}
			return strange.Count > 2 ? false : true;
		}

		public void fill(int num, int[] point) {
			for(int i = 0; i < point.Length; i++) {
				point[i] = point[i] + 11;
				if(board[point[i] / 10, point[i] % 10] != num) {
					board[point[i] / 10, point[i] % 10] = num;
					seqArray[num - 1]++;
				}
			}
		}

		public void fill(int num, int point) {
			point = point + 11;
			board[point / 10, point % 10] = num;
			seqArray[num - 1]++;
		}

		public bool extend(int num, int basePoint) {
			int[] point = Cross(basePoint);
			num = point.Length - (1 + num);
			List<int> poss = new List<int>();
			int sameNum = new int();
			for(int i = 0; i < point.Length; i++) {
				point[i] = point[i] + 11;
			}
			int baseNum = board[point[0] / 10, point[0] % 10];
			int baseWeight = blockWeight(baseNum);
			for(int i = 1; i < point.Length; i++) {
				if(board[point[i] / 10, point[i] % 10] == baseNum && baseNum != 0) {
					sameNum++;
				}
				else if(blockWeight(board[point[i] / 10, point[i] % 10]) + baseWeight <= 4) {
					poss.Add(point[i]);
				}
			}
			if(sameNum == num) {
				return true;
			}
			if(poss.Count == num - sameNum) {
				poss.Add(point[0]);
				repaint(poss);
				return true;
			}
			return false;
		}

		public void repaint(List<int> point) {
			int diffNumMin = seqArray.Length + 1;
			int diffNum = new int();
			foreach(int po in point) {
				diffNum = board[po / 10, po % 10];
				if(diffNum != 0) {
					diffNumMin = Math.Min(diffNum, diffNumMin);
				}
			}
			if(diffNumMin == seqArray.Length + 1) {
				diffNumMin = seqNum();
			}
			foreach(int po in point) {
				int change = board[po / 10, po % 10];
				if(change == 0) {
					board[po / 10, po % 10] = diffNumMin;
					seqArray[diffNumMin - 1]++;
				}
				else {
					for(int i = 1; i < size - 1; i++) {
						for(int j = 1; j < size - 1; j++) {
							if(board[i, j] == change) {
								board[i, j] = diffNumMin;
								seqArray[diffNumMin - 1]++;
							}
						}
					}
					seqArray[change - 1] = 0;
				}
			}
		}

		public void accrual() {
			List<int> checkList = new List<int>();
			checkList.Add(0);
			for(int i = 0; i < seqArray.Length; i++) {
				if(0 < seqArray[i] && seqArray[i] < 4) {
					checkList.Add(i + 1);
				}
			}
			List<int> poss = new List<int>();
			foreach(int ch in checkList) {
				for(int i = 1; i < size - 1; i++) {
					for(int j = 1; j < size - 1; j++) {
						if(board[i, j] == ch) {
						
						}
					}
				}
			}
		}

		public bool compOr() {
			for(int i = 0; i < seqArray.Length; i++) {
				if(seqArray[i] != 4) {
					return false;
				}
			}
			return true;
		}

		public int getNum() {
			return nextNum;
		}

		public void debugArrayWrite() {
			for(int i = 0; i < seqArray.Length; i++) {
				System.Console.Write("{0,2} : {1}\n", i + 1, seqArray[i]);
			}
			System.Console.WriteLine();
		}

		public void debugWrite() {
			for(int i = 1; i < size - 1; i++) {
				for(int j = 1; j < size - 1; j++) {
					System.Console.Write("{0,3}", board[i, j]);
				}
				System.Console.WriteLine();
			}
		}

		public int[] Saltire(int basePoint) {
			return new int[] { basePoint, basePoint - 11, basePoint - 9, basePoint + 9, basePoint + 11 };
		}

		public int[] Cross(int basePoint) {
			return new int[] { basePoint, basePoint - 10, basePoint - 1, basePoint + 1, basePoint + 10 };
		}
	}
}
