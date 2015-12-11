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

		public int pointNum(int point) {
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

		public bool roopNumAreaGenerate(string num, List<int> points) {
			bool isChange = false;
			for(int i = points.Count - 1; i >= 0; i--) {
				isChange = numAreaGenerate(int.Parse(num), points[i]);
			}
			return isChange;
		}

		public bool numAreaGenerate(int num, int basePoint) {
			int[] region = cross(basePoint);
			num = region.Length - num;
			List<int> possibility = new List<int>();
			int same = new int();
			int baseNum = board[basePoint / 10, basePoint % 10];
			int baseWeight = blockWeight(baseNum);
			int oneDig = new int();
			int tenDig = new int();
			for(int i = 0; i < region.Length; i++) {
				tenDig = region[i] / 10;
				oneDig = region[i] % 10;
				if(0 <= tenDig && tenDig < size) {
					if(0 <= oneDig && oneDig < size) {
						if(board[tenDig, oneDig] == baseNum) {
							if(baseNum == 0) {
								possibility.Add(region[i]);
							}
							else {
								same++;
							}
						}
						else {
							if(baseWeight + blockWeight(board[tenDig,oneDig]) <= 4) {
								possibility.Add(region[i]);
							}
						}
					}
				}
			}
			if(same == num) {
				return false; //あやしい。くっつけないリストへ移動させるようにさせる
			}
			else if(possibility.Count == num - same) {
				possibility.Add(basePoint);
				paint(possibility.ToArray());
				return true;
			}
			return false;
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

		public bool generate() {
			bool change = false;
			List<int> possibility = new List<int>();
			int[] region;
			int num = new int();
			int weight = new int();
			int repre = new int();
			int tenDig = new int();
			int oneDig = new int();
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(board[i, j] == 0) {
						repre = i * 10 + j;
						region = cross(i * 10 + j);
						for(int k = 0; k < region.Length; k++) {
							tenDig = region[k] / 10;
							oneDig = region[k] % 10;
							if(0 <= tenDig && tenDig < size) {
								if(0 <= oneDig && oneDig < size) {
									if(blockWeight(board[tenDig, oneDig]) <= 3) {
										possibility.Add(region[k]);
									}
								}
							}
						}
					}
					if(possibility.Count == 1) {
						possibility.Add(repre);
						paint(possibility.ToArray());
						change = true;
					}
					possibility.Clear();
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
							region = cross(i * 10 + j);
							for(int k = 0; k < region.Length; k++) {
								tenDig = region[k] / 10;
								oneDig = region[k] % 10;
								if(0 <= tenDig && tenDig < size) {
									if(0 <= oneDig && oneDig < size) {
										num = board[tenDig, oneDig];
										if(ch != num && weight + blockWeight(num) <= 4) {
											if(!possibility.Contains(region[k])) {
												possibility.Add(region[k]);
											}
										}
									}
								}
							}
						}
					}
				}
				if(disconnect.ContainsKey(ch)) {
					foreach(int po in possibility) {
						if(disconnect[ch].Contains(board[po / 10, po % 10])) {
							blackList.Add(po);
						}
					}
					foreach(int bl in blackList) {
						possibility.Remove(bl);
					}
					blackList.Clear();
				}
				if(possibility.Count == 1) {
					possibility.Add(repre);
					paint(possibility.ToArray());
					change = true;
				}
				possibility.Clear();
			}

			return change;
		}

		public bool complete() {
			int size = seqArray.Length;
			for(int i = 0; i < size; i++) {
				if(seqArray[i] != 4) {
					return false;
				}
			}
			return true;
		}

		public void debugArrayWrite() {
			int size = seqArray.Length;
			for(int i = 0; i < size; i = i + 3) {
				try {
					System.Console.Write("{0,2} : {1}  ", i + 1, seqArray[i]);
					System.Console.Write("{0,2} : {1}  ", i + 2, seqArray[i + 1]);
					System.Console.WriteLine("{0,2} : {1}", i + 3, seqArray[i + 2]);
				}
				catch {
					System.Console.WriteLine();
				}
			}
		}

		public void debugWrite() {
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					System.Console.Write("{0,4}", board[i, j]);
				}
				System.Console.WriteLine();
			}
			System.Console.WriteLine();
		}

		public void showDisconnect() {
			System.Console.WriteLine("Disconnect :");
			foreach(int i in disconnect.Keys) {
				System.Console.Write("{0,2} : ", i);
				foreach(int j in disconnect[i]) {
					System.Console.Write(" {0,2}", j);
				}
				System.Console.WriteLine();
			}
		}

		public bool checkRule(int num, int position) {
			int[] reg = cross(position);
			int baseNum = board[position / 10, position % 10];
			if(baseNum == 0) {
				return false;
			}
			int count = new int();
			foreach(int re in reg) {
				try {
					if(baseNum != board[re / 10, re % 10] && board[re / 10, re % 10] != 0) {
						count++;
					}

				}
				catch {
					count++;
				}
			}
			return count == num ? true : false;
		}

		public int[] cross(int basePoint) {
			return new int[] { basePoint - 10, basePoint - 1, basePoint + 1, basePoint + 10 };
		}

		public int[] saltire(int basePoint) {
			return new int[] { basePoint - 11, basePoint - 9, basePoint + 9, basePoint + 11 };
		}
	}
}
