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
		Dictionary<int, List<int>> disconnect = new Dictionary<int, List<int>>();

		public Board(int size) {
			seqArray = new int[size * size / 4];
			this.size = size;
			board = new int[this.size, this.size];
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

		public void registList(int key, int value) {
			if(!disconnect.ContainsKey(key)) {
				List<int> tmp = new List<int>();
				tmp.Add(value);
				disconnect.Add(key, tmp);
			}
			else {
				disconnect[key].Add(value);
			}
		}

		public int check(int point) {
			return board[point / 10, point % 10];
		}

		public void fill(int point) {
			board[point / 10, point % 10] = seqNum();
			seqArray[seqNum() - 1]++;
		}

		public bool discoCheck(int[] point) {
			List<int> nums = new List<int>();
			foreach(int po in point) {
				if(!nums.Contains(board[po / 10, po % 10])) {
					nums.Add(board[po / 10, po % 10]);
				}
			}
			if(nums.Count == 1) {
				return true;
			}
			foreach(int fnu in nums) {
				if(disconnect.ContainsKey(fnu)) {
					foreach(int snu in nums) {
						if(disconnect[fnu].Contains(snu)) {
							return false;
						}
					}
				}
			}
			return true;
		}

		public bool isMatch(int[] point) {
			int totalWeight = new int();
			int num = new int();
			int oneDig = new int();
			int tenDig = new int();
			List<int> strange = new List<int>();
			for(int i = 0; i < point.Length; i++) {
				tenDig = point[i] / 10;
				oneDig = point[i] % 10;
				if(0 <= tenDig && tenDig < size) {
					if(0 <= oneDig && oneDig < size) {
						num = board[tenDig, oneDig];
						if(strange.Contains(num)) {
							if(num == 0) {
								totalWeight++;
							}
						}
						else {
							totalWeight += blockWeight(num);
							strange.Add(num);
						}
					}
				}
			}

			if(totalWeight == 4) {
				return discoCheck(point);
			}
			return false;
		}

		public bool extend(int num, int basePoint) {
			bool change = false;
			List<int> poss = new List<int>();
			int sameNum = new int();
			int[] point = cross(basePoint);
			num = point.Length - num;
			int baseNum = board[basePoint / 10, basePoint % 10];
			int baseWeight = blockWeight(baseNum);
			int oneDig = new int();
			int tenDig = new int();
			for(int i = 0; i < point.Length; i++) {
				tenDig = point[i] / 10;
				oneDig = point[i] % 10;
				if(0 <= tenDig && tenDig < size) {
					if(0 <= oneDig && oneDig < size) {
						if(board[tenDig, oneDig] == baseNum && baseNum != 0) {
							sameNum++;
						}
						else if(blockWeight(board[tenDig, oneDig]) + baseWeight <= 4) {
							poss.Add(point[i]);
						}
					}
				}
			}
			if(sameNum == num) {
				change = true;
			}
			else if(poss.Count == num - sameNum) {
				poss.Add(basePoint);
				paint(poss.ToArray());
				change = true;
			}
			return change;
		}

		public void paint(int[] point) {
			int diffNumMin = seqArray.Length + 1;
			int diffNum = new int();
			List<int> tmp = new List<int>();
			for(int i = 0; i < point.Length; i++) {
				diffNum = board[point[i] / 10, point[i] % 10];
				if(!tmp.Contains(diffNum)) {
					tmp.Add(diffNum);
				}
				if(diffNum != 0) {
					diffNumMin = Math.Min(diffNum, diffNumMin);
				}
			}
			if(diffNumMin == seqArray.Length + 1) {
				diffNumMin = seqNum();
			}
			if(diffNumMin != 0) {
				foreach(int po in point) {
					int change = board[po / 10, po % 10];
					if(change != diffNumMin) {
						if(change == 0) {
							board[po / 10, po % 10] = diffNumMin;
							seqArray[diffNumMin - 1]++;
						}
						else {
							for(int i = 0; i < size; i++) {
								for(int j = 0; j < size; j++) {
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
				foreach(int tm in tmp) {
					if(disconnect.ContainsKey(tm)) {
						if(diffNumMin != tm) {
							foreach(int vl in disconnect[tm]) {
								disconnect[vl].Remove(tm);
								disconnect[vl].Add(diffNumMin);
								registList(diffNumMin, vl);
							}
							disconnect.Remove(tm);
						}
					}
				}
			}
		}

		public bool accrual() {
			bool change = false;
			List<int> poss = new List<int>();
			int[] cro;
			int num = new int();
			int weight = new int();
			int repre = new int();
			int tenDig = new int();
			int oneDig = new int();
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(board[i, j] == 0) {
						repre = i * 10 + j;
						cro = cross(i * 10 + j);
						for(int k = 0; k < cro.Length; k++) {
							tenDig = cro[k] / 10;
							oneDig = cro[k] % 10;
							if(0 <= tenDig && tenDig < size) {
								if(0 <= oneDig && oneDig < size) {
									if(blockWeight(board[tenDig, oneDig]) <= 3) {
										poss.Add(cro[k]);
									}
								}
							}
						}
					}
					if(poss.Count == 1) {
						poss.Add(repre);
						paint(poss.ToArray());
						change = true;
					}
					poss.Clear();
				}
			}

			List<int> blackList = new List<int>();
			List<int> checkList = new List<int>();
			for(int i = 0; i < seqArray.Length; i++) {
				if(0 < seqArray[i] && seqArray[i] < 4) {
					checkList.Add(i + 1);
				}
			}
			foreach(int ch in checkList) {
				weight = blockWeight(ch);
				for(int i = 0; i < size; i++) {
					for(int j = 0; j < size; j++) {
						if(board[i, j] == ch) {
							repre = i * 10 + j;
							cro = cross(i * 10 + j);
							for(int k = 0; k < cro.Length; k++) {
								tenDig = cro[k] / 10;
								oneDig = cro[k] % 10;
								if(0 <= tenDig && tenDig < size) {
									if(0 <= oneDig && oneDig < size) {
										num = board[tenDig, oneDig];
										if(ch != num && weight + blockWeight(num) <= 4) {
											if(!poss.Contains(cro[k])) {
												poss.Add(cro[k]);
											}
										}
									}
								}
							}
						}
					}
				}
				if(disconnect.ContainsKey(ch)) {
					foreach(int po in poss) {
						if(disconnect[ch].Contains(board[po / 10, po % 10])) {
							blackList.Add(po);
						}
					}
					foreach(int bl in blackList) {
						poss.Remove(bl);
					}
					blackList.Clear();
				}
				if(poss.Count == 1) {
					poss.Add(repre);
					paint(poss.ToArray());
					change = true;
				}
				poss.Clear();
			}

			return change;
		}

		public bool compOr() {
			for(int i = 0; i < seqArray.Length; i++) {
				if(seqArray[i] != 4) {
					return false;
				}
			}
			return true;
		}

		public void debugArrayWrite() {
			for(int i = 0; i < seqArray.Length; i++) {
				System.Console.Write("{0,2} : {1}\n", i + 1, seqArray[i]);
			}
			System.Console.WriteLine();
		}

		public void debugWrite() {
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					System.Console.Write("{0,4}", board[i, j]);
				}
				System.Console.WriteLine();
			}
		}

		public int[] saltire(int basePoint) {
			return new int[] { basePoint - 11, basePoint - 9, basePoint + 9, basePoint + 11 };
		}

		public int[] cross(int basePoint) {
			return new int[] { basePoint - 10, basePoint - 1, basePoint + 1, basePoint + 10 };
		}

		public void showDisc() {
			foreach(int i in disconnect.Keys) {
				System.Console.Write("{0,2} : ", i);
				foreach(int j in disconnect[i]) {
					System.Console.Write("{0,2}", j);
				}
				System.Console.WriteLine();
			}
		}

		public bool checkRule(int num, int pos) {
			int[] reg = cross(pos);
			int baseNum = board[pos / 10, pos % 10];
			if(baseNum == 0) {
				return false;
			}
			int count = new int();
			foreach(int re in reg) {
				try {
					if(baseNum != board[re / 10, re % 10] && board[re/10, re%10] != 0) {
						count++;
					}

				}
				catch {
					count++;
				}
			}
			return count == num ? true : false;
		}
	}
}
