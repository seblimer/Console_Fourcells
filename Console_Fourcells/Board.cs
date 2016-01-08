using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Board {
		static int convert;
		const int cells = 4;
		private int[,] board;
		private int size;
		FinishedPart finished;
		Sequence sequence;

		public Board(int size) {
			sequence = new Sequence(size - 2);
			this.size = size;
			convert = size + 1;
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

		public Guess boardProperty {
			get { return new Guess((int[,])board.Clone(), finished.Finished, sequence.sequenceProperty); }
			set {
				board = value.getBoard();
				finished.Finished = value.getFinished();
				sequence.sequenceProperty = value.getSequence();
			}
		}

		/******************************
		 * isMatchでは指定した領域がブロックを置ける条件を満たすかチェック
		 * 満たす条件は以下
		 * ・領域のブロックの重さの合計が基準以下
		 * ・繋がらないリストに登録された数字が領域内に混合しない
		 * ・埋めた結果孤立が生じない
		 * ・領域内に入力の数字マスが含まれる場合,入力を満たすかチェック
		 *****************************/
		public bool isMatch(int[] region) {
			int totalWeight = new int();
			int num = new int();
			List<int> strange = new List<int>();
			foreach(int re in region) {
				try {
					num = board[re / convert, re % convert];
				}
				catch {
					return false;
				}
				if(strange.Contains(num)) {
					if(num == 0) {
						totalWeight++;
					}
				}
				else {
					totalWeight += sequence.blockWeight(num);
					if(num > 0) {
						strange.Add(num);
					}
				}
			}
			if(totalWeight == 4) {
				return finished.isolationCheck(region) && Fourcells.checkNumRule(region);
			}
			return false;
		}

		/******************************
		 * 入力で受け取った数字部分について埋める
		 * roopの方はそれぞれの数字のリストを受け取り,roopでない方で一つずつ処理する
		 * 周囲に入力の数字分確定して入らないマスがある場合,入る可能性があるマスを駄目なリストに移し,自分自身を再度チェックしないように別リストへ移す
		 *****************************/
		public void generate(string num, List<int> points) {
			for(int i = points.Count - 1; i >= 0; i--) {
				numAreaGenerate(int.Parse(num), points[i]);
			}
		}
		private void numAreaGenerate(int num, int basePoint) {
			int numSame = cells - num;
			List<int> possibility = new List<int>();
			int same = new int();
			int baseNum = board[basePoint / convert, basePoint % convert];
			int weight = sequence.blockWeight(baseNum);
			foreach(int cr in cross(basePoint)) {
				if(baseNum == 0) {
					if(weight + sequence.blockWeight(board[cr / convert, cr % convert]) <= 4) {
						possibility.Add(cr);
					}
				}
				else {
					if(board[cr / convert, cr % convert] == baseNum) {
						same++;
					}
					else if(weight + sequence.blockWeight(board[cr / convert, cr % convert]) <= 4) {
						possibility.Add(cr);
					}
				}
			}
			if(numSame == same) {
				//何もしない
			}
			else if(possibility.Count == numSame - same) {
				possibility.Add(basePoint);
				paint(possibility.ToArray());
			}
		}

		/******************************
		 * 代表点の集合を受け取り、その点と同じブロック番号の領域を新たに振りなおす
		 * 振りなおす際の番号は代表点の中で最小のブロック番号
		 * 最小が0のときはseqNumからまだ使ってない最小のブロック番号を受け取る
		 *****************************/
		public void paint(int[] point) {
			int min = sequence.seqNum();
			int num = new int();
			List<int> diffNum = new List<int>();
			for(int i = 0; i < point.Length; i++) {
					num = board[point[i] / convert, point[i] % convert];
					if(!diffNum.Contains(num)) {
						diffNum.Add(num);
					}
					if(num != 0) {
						min = Math.Min(num, min);
					}
			}
			foreach(int po in point) {
				int change = board[po / convert, po % convert];
				if(change != min) {
					if(change == 0) {
						board[po / convert, po % convert] = min;
						sequence.add(min);
					}
					else {
						for(int i = 1; i < size - 1; i++) {
							for(int j = 1; j < size - 1; j++) {
								if(board[i, j] == change) {
									board[i, j] = min;
									sequence.add(min);
								}
							}
						}
						sequence.clear(change);
					}
				}
			}
			sequence.entryAchive(this);
		}

		/****************************************
		 * ブロック数から完成したと判明した番号の領域を求めて、finishedに登録
		 ***************************************/
		public void achivedAdd(List<int> achived) {
			List<int> region = new List<int>();
			foreach(int ac in achived) {
				for(int i = 1; i < size - 1; i++) {
					for(int j = 1; j < size - 1; j++) {
						if(board[i, j] == ac) {
							region.Add(i * convert + j);
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

		/*****************************************
		 * 推測用に未確定の最初のマスを返す
		 ****************************************/
		public int firstUnsettled() {
			return finished.firstZeroPoint();
		}

		public bool complete() {
			return sequence.isBlockComplete() && finished.isComplete();
		}

		static int[] cross(int basePoint) {
			return new int[] { basePoint - convert, basePoint - 1, basePoint + 1, basePoint + convert };
		}

		/****************************************
		 * デバッグ関係
		 ***************************************/
		public void debugWrite() {
			for(int i = 1; i < size - 1; i++) {
				for(int j = 1; j < size - 1; j++) {
					System.Console.Write("{0,3}", board[i, j]);
				}
				System.Console.WriteLine();
			}
			System.Console.WriteLine();
		}

		public bool checkRule(int num, int position) {
			int[] reg = cross(position);
			int baseNum = board[position / convert, position % convert];
			if(baseNum == 0) {
				return false;
			}
			int count = new int();
			foreach(int re in reg) {
				try {
					if(baseNum != board[re / convert, re % convert] && board[re / convert, re % convert] != 0) {
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
