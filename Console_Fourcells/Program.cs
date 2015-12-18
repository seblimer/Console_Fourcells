using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Fourcells {
		const int cells = 4;
		const int referencePoint = 11;
		static Dictionary<string, List<int>> numList = new Dictionary<string, List<int>>();
		static Dictionary<string, List<int>> checkedList = new Dictionary<string, List<int>>();

		public static void addNumList(string key, int value) {
			if(!numList.ContainsKey(key)) {
				List<int> nums = new List<int>();
				nums.Add(value);
				numList.Add(key, nums);
			}
			else {
				numList[key].Add(value);
			}
		}

		public static void finishedList(string key, int value) {
			if(!checkedList.ContainsKey(key)) {
				List<int> nums = new List<int>();
				nums.Add(value);
				checkedList.Add(key, nums);
			}
			else {
				checkedList[key].Add(value);
			}
			numList[key].Remove(value);
			if(numList.Count == 0) {
				numList.Remove(key);
			}
		}

		public static bool checkNumRule(int[] region) {
			int count = new int();
			foreach(string key in numList.Keys) {
				foreach(int re in region) {
					count = 0;
					if(numList[key].Contains(re)) {
						foreach(int cr in cross(re)) {
							if(!region.Contains<int>(cr)) {
								count++;
							}
						}
						if(int.Parse(key) != count) {
							return false;
						}
					}
				}
			}
			return true;
		}

		public static int[] cross(int basePoint) {
			return new int[] { basePoint - 10, basePoint - 1, basePoint + 1, basePoint + 10 };
		}

		static void Main(string[] args) {
			SetBlock set = new SetBlock();
			Block[] blocks;
			int size = 8;
			if(size % 2 != 0) {
				System.Console.Write("盤面は偶数*偶数で入力してください");
				System.Console.ReadLine();
				return;
			}
			string[,] iniBoard ={{"-","-","-","1","-","-","-","-"},
								 {"-","-","-","-","-","-","-","1"},
								 {"1","-","2","-","-","1","-","-"},
								 {"-","-","-","2","-","-","-","-"},
								 {"-","-","-","-","-","-","-","-"},
								 {"-","2","-","-","-","-","-","1"},
								 {"1","-","-","-","-","-","-","-"},
								 {"-","-","-","-","1","-","-","-"}};
			Board fillBoard = new Board(size);
			List<Block> checkList = new List<Block>();
			List<int> poss = new List<int>();
			int oneDig = new int();
			int tenDig = new int();

			/****************************************
			 * 1の登録
			 * 3の登録
			 * 繋がらない部分の処理
			 ****************************************/
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(iniBoard[i, j] != "-") {
						addNumList(iniBoard[i, j], i * 10 + j + referencePoint);
						if(iniBoard[i, j] != "2") {
							foreach(int cr in fillBoard.cross(i * 10 + j)) {
								oneDig = cr % 10;
								tenDig = cr / 10;
								if(0 <= tenDig && tenDig < size) {
									if(0 <= oneDig && oneDig < size) {
										if(iniBoard[tenDig, oneDig] == iniBoard[i, j]) {
											fillBoard.entryRegistByPoints(new int[] { i * 10 + j + referencePoint, cr + referencePoint });
										}
									}
								}
							}
						}
					}
				}
			}

			/****************************************
			 * 終わる、もしくは駄目だとわかるまでループ
			 ****************************************/
			List<int> share = new List<int>();
			int[] tmpShare;
			//while(!fillBoard.compOr()) {
			bool change = true;
			//for(int y = 0; y < 14; y++) {
			while(change) {
				change = false;
				//"1"のマスについてチェック
				if(numList.ContainsKey("1")) {
					blocks = set.TBlocks();
					for(int i = numList["1"].Count - 1; i >= 0; i--) {
						foreach(Block bl in blocks) {
							tmpShare = bl.charRegion(numList["1"][i]);
							if(fillBoard.isMatch(tmpShare)) {
								checkList.Add(bl);
								if(share.Count == 0) {
									share.AddRange(tmpShare.ToList<int>());
								}
								else {
									for(int j = share.Count - 1; j >= 0; j--) {
										if(!tmpShare.Contains<int>(share[j])) {
											share.RemoveAt(j);
										}
									}
								}
							}
						}
						if(checkList.Count == 1) {
							fillBoard.paint(checkList[0].charRegion(numList["1"][i]));
							finishedList("1", numList["1"][i]);
							change = true;
						}
						else if(checkList.Count > 1 && share.Count > 1) {
							fillBoard.paint(share.ToArray());
							change = true;
						}
						checkList.Clear();
						share.Clear();
					}
				}
				/****************************************
				 * 各数字のマスの上下左右をチェック
				 * 4-数字分周りが確定したブロックならばそれ以外のマスと繋がる
				 ****************************************/
				if(numList.ContainsKey("2")) {
					change = fillBoard.roopNumAreaGenerate("2", numList["2"]);
				}
				if(numList.ContainsKey("3")) {
					change = fillBoard.roopNumAreaGenerate("3", numList["3"]);
				}

				change = fillBoard.generate();
			}

			/****************************************
			 * 盤面の表示やデバック関係
			 ****************************************/
			fillBoard.debugWrite();
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					System.Console.Write("{0, 2}", iniBoard[i, j]);
				}
				System.Console.WriteLine();
			}
			System.Console.WriteLine();
			//fillBoard.debugArrayWrite();
			fillBoard.showDisconnect();
			System.Console.WriteLine("\n終わった入力部分");
			foreach(string key in checkedList.Keys) {
				System.Console.WriteLine("key :{0,2}", key);
				checkedList[key].Sort();
				foreach(int value in checkedList[key]) {
					System.Console.Write("{0} ", value);
				}
				System.Console.WriteLine();
			}
			bool check = false;
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(iniBoard[i, j] == "1" || iniBoard[i, j] == "2" || iniBoard[i, j] == "3") {
						check = fillBoard.checkRule(int.Parse(iniBoard[i, j]), i * 10 + j + referencePoint);
					}
				}
			}
			System.Console.WriteLine();
			System.Console.WriteLine("Result : {0,5}", check);
			System.Console.Write("Enterで終了");
			System.Console.ReadLine();
		}
	}
}
