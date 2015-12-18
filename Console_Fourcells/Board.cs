using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Board {
		private int[,] board;
		private int size;
		Dictionary<int, List<int>> disconnect = new Dictionary<int, List<int>>();
		FinishedPart finished;
		Sequence sequence;

		public Board(int size) {
			sequence = new Sequence(size);
			this.size = size + 2;
			board = new int[this.size, this.size];
			for(int i = 0; i < this.size; i++) {
				for(int j = 0; j < this.size; j++) {
					if(i == 0 || i == this.size - 1) {
						board[i, j] = -1;
					}
					else if(j == 0 || j == this.size - 1) {
						board[i, j] = -1;
					}
				}
			}
			finished = new FinishedPart(board);
		}

		public void entryRegistByPoints(int[] points) {
			if(points.Length == 2) {
				foreach(int po in points) {
					if(board[po / 10, po % 10] == 0) {
						paint(new int[] { po });
					}
				}
				registList(board[points[0] / 10, points[0] % 10], board[points[1] / 10, points[1] % 10]);
				registList(board[points[1] / 10, points[1] % 10], board[points[0] / 10, points[0] % 10]);
			}
		}
		private void registList(int key, int value) {
			if(!disconnect.ContainsKey(key)) {
				List<int> tmp = new List<int>();
				tmp.Add(value);
				disconnect.Add(key, tmp);
			}
			else {
				if(!disconnect[key].Contains(value)) {
					disconnect[key].Add(value);
				}
			}
		}

		public bool isMatch(int[] region) {
			int totalWeight = new int();
			int num = new int();
			List<int> strange = new List<int>();
			foreach(int re in region) {
				num = board[re / 10, re % 10];
				if(strange.Contains(num)) {
					if(num == 0) {
						totalWeight++;
					}
				}
				else {
					totalWeight += sequence.blockWeight(num);
					strange.Add(num);
				}

			}
			if(totalWeight == 4) {
				return discoCheck(region) && finished.isolationCheck(region) && Fourcells.checkNumRule(region);
			}
			return false;
		}
		private bool discoCheck(int[] region) {
			List<int> nums = new List<int>();
			foreach(int re in region) {
				if(!nums.Contains(board[re / 10, re % 10])) {
					nums.Add(board[re / 10, re % 10]);
				}
			}
			if(nums.Count == 1) {
				return true;
			}
			foreach(int nu in nums) {
				if(disconnect.ContainsKey(nu)) {
					foreach(int dc in nums) {
						if(disconnect[nu].Contains(dc)) {
							return false;
						}
					}
				}
			}
			return true;
		}

		public bool roopNumAreaGenerate(string num, List<int> points) {
			bool isChange = false;
			for(int i = points.Count - 1; i >= 0; i--) {
				isChange = numAreaGenerate(int.Parse(num), points[i]);
			}
			return isChange;
		}
		private bool numAreaGenerate(int num, int basePoint) {
			int[] region = cross(basePoint);
			num = region.Length - num;
			List<int> possibility = new List<int>();
			int same = new int();
			int baseNum = board[basePoint / 10, basePoint % 10];
			int baseWeight = sequence.blockWeight(baseNum);
			foreach(int re in region) {
				if(board[re / 10, re % 10] == baseNum) {
					if(baseNum == 0) {
						possibility.Add(re);
					}
					else {
						same++;
					}
				}
				else {
					if(baseWeight + sequence.blockWeight(board[re / 10, re % 10]) <= 4) {
						possibility.Add(re);
					}
				}
			}
			if(same == num) {
				Fourcells.finishedList((region.Length - num).ToString(), basePoint);
			}
			else if(possibility.Count <= num - same) {
				possibility.Add(basePoint);
				paint(possibility.ToArray());
				return true;
			}
			return false;
		}

		public void paint(int[] point) {
			int min = sequence.seqNum();
			int num = new int();
			List<int> diffNum = new List<int>();
			for(int i = 0; i < point.Length; i++) {
				num = board[point[i] / 10, point[i] % 10];
				if(!diffNum.Contains(num)) {
					diffNum.Add(num);
				}
				if(num != 0) {
					min = Math.Min(num, min);
				}
			}
			foreach(int po in point) {
				int change = board[po / 10, po % 10];
				if(change != min) {
					if(change == 0) {
						board[po / 10, po % 10] = min;
						sequence.add(min);
					}
					else {
						for(int i = 0; i < size; i++) {
							for(int j = 0; j < size; j++) {
								if(board[i, j] == change) {
									board[i, j] = min;
									sequence.add(min);
								}
							}
						}
						sequence.clear(change);
					}
					if(disconnect.ContainsKey(change)) {
						if(min != change) {
							foreach(int vl in disconnect[change]) {
								disconnect[vl].Remove(change);
								disconnect[vl].Add(min);
								registList(min, vl);
							}
							disconnect.Remove(change);
						}

					}
				}
			}
			sequence.entryAchive(this);
		}
		public void achivedAdd(List<int> achived) {
			List<int> region = new List<int>();
			foreach(int ac in achived) {
				for(int i = 1; i < size - 1; i++) {
					for(int j = 1; j < size - 1; j++) {
						if(board[i, j] == ac) {
							region.Add(i * 10 + j);
							if(region.Count == 4) {
								i = j = size;
							}
						}
					}
				}
				finished.add(region.ToArray());
				region.Clear();
			}
		}

		public bool generate() {
			int count = new int();
			while(generateZero() || generateNotZero()) {
				count++;
			}
			return count > 0 ? true : false;
		}
		private bool generateZero() {
			bool isChange = false;
			List<int> possibility = new List<int>();
			int[] region;
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(board[i, j] == 0) {
						region = cross(i * 10 + j);
						foreach(int re in region) {
							if(sequence.blockWeight(board[re / 10, re % 10]) <= 3) {
								possibility.Add(re);
							}
						}
					}
					if(possibility.Count == 1) {
						possibility.Add(i * 10 + j);
						paint(possibility.ToArray());
						isChange = true;
					}
					possibility.Clear();
				}
			}
			return isChange;
		}
		private bool generateNotZero() {
			bool isChange = false;
			int weight, num, repre = new int();
			List<int> possibility = new List<int>();
			foreach(int li in sequence.notZero()) {
				weight = sequence.blockWeight(li);
				for(int i = 1; i < size - 1; i++) {
					for(int j = 1; j < size - 1; j++) {
						if(board[i, j] == li) {
							repre = i * 10 + j;
							foreach(int re in cross(i * 10 + j)) {
								num = board[re / 10, re % 10];
								if(li != num && weight + sequence.blockWeight(num) <= 4) {
									if(!possibility.Contains(re)) {
										if(disconnect.ContainsKey(li)) {
											if(!disconnect[li].Contains(num)) {
												possibility.Add(re);
											}
										}
										else {
											possibility.Add(re);
										}
									}
								}
							}
						}
					}
				}
				if(possibility.Count == 1) {
					possibility.Add(repre);
					paint(possibility.ToArray());
					isChange = true;
				}
				possibility.Clear();
			}
			return isChange;
		}

		
		//public bool complete() {
		//	sequenceに対して終了したかどうかを問う
		//}

		public int[] cross(int basePoint) {
			return new int[] { basePoint - 10, basePoint - 1, basePoint + 1, basePoint + 10 };
		}

		/****************************************
		 * デバッグ関係
		 ***************************************/
		public void debugArrayWrite() {
			sequence.write();
		}

		public void debugWrite() {
			for(int i = 1; i < size - 1; i++) {
				for(int j = 1; j < size - 1; j++) {
					System.Console.Write("{0,3}", board[i, j]);
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
					System.Console.Write("{0,2}", j);
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
	}
}
