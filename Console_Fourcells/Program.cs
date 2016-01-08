using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Fourcells {
		static int size;
		static int convert;
		const int cells = 4;
		static Dictionary<string, List<int>> numList = new Dictionary<string, List<int>>();
		private static SetBlock set;
		private static Block[] blocks;
		private static Board board;
		private static Guess currentGuess = null;

		/****************************************
		 * 入力の数字部分の処理
		 * "1"または"3"に連続するものがあれば非連結の処理を行う
		 ****************************************/
		public static string[,] extend(string[,] tmp) {
			size = tmp.GetLength(0) + 2;
			string[,] board = new string[size, size];
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(i == 0 || i == size - 1) {
						board[i, j] = "-";
					}
					else if(j == 0 || j == size - 1) {
						board[i, j] = "-";
					}
					else {
						board[i, j] = tmp[i - 1, j - 1];
					}
				}
			}
			return board;
		}

		public static void entryNum(string[,] initial) {
			for(int i = 1; i < size - 1; i++) {
				for(int j = 1; j < size - 1; j++) {
					if(initial[i, j] != "-") {
						addNumList(initial[i, j], i * convert + j);
					}
				}
			}
		}

		//入力の数字部分を数字毎のリストで管理それを辞書化
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

		//入力の数字についてある領域が条件を満たすか否かをチェック
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

		public static void guess() {
			int point = board.firstUnsettled();
			blocks = set.AllBlocks();
			foreach(Block bl in blocks) {
				if(board.isMatch(bl.region(point))) {
					Guess tmp = board.boardProperty;
					board.boardProperty = tmp;
					board.paint(bl.region(point));
					currentGuess.entryChild(tmp);
				}
				board.boardProperty = currentGuess;
			}
		}
		public static void nextGuess() {
			currentGuess = currentGuess.next();
			if(currentGuess != null) {
				board.boardProperty = currentGuess;
			}
		}


		//指定した座標の上下左右の座標を返す
		public static int[] cross(int basePoint) {
			return new int[] { basePoint - convert, basePoint - 1, basePoint + 1, basePoint + convert };
		}

		static void Main(string[] args) {
			string[,] initial ={{"-","1","-","-","-","-","-","-","-","-"},
							    {"-","-","-","-","-","1","-","-","1","-"},
							    {"-","-","-","1","-","-","-","-","-","-"},
							    {"-","1","-","-","-","1","-","-","-","-"},
							    {"-","-","-","-","-","-","-","-","-","-"},
							    {"-","-","-","1","-","-","-","1","-","-"},
								{"1","-","-","-","-","-","-","-","-","-"},
								{"-","-","-","-","-","-","-","-","-","-"},
								{"-","-","-","-","-","-","1","-","-","1"},
								{"-","-","-","1","-","-","-","-","-","-"}};
			initial = extend(initial);
			size = initial.GetLength(0);
			convert = size + 1;
			set = new SetBlock(convert);
			board = new Board(size);
			entryNum(initial);
			currentGuess = board.boardProperty;
			/****************************************
			 * 終わるまでループ
			 ****************************************/
			while(true) {
				if(!currentGuess.Check) {
					currentGuess.Check = true;
					guess();
				}
				if(numList.ContainsKey("1")) {
					board.generate("1", numList["1"]);
				}
				if(numList.ContainsKey("2")) {
					board.generate("2", numList["2"]);
				}
				if(numList.ContainsKey("3")) {
					board.generate("3", numList["3"]);
				}
				if(board.complete()) {
					board.debugWrite();
				}
				nextGuess();
				if(currentGuess == null) {
					System.Console.WriteLine("以上");
					break;
				}
			}
			/****************************************
			 * 盤面の表示やデバック関係
			 ****************************************/
			System.Console.WriteLine("\n==========以下入力関係==========");
			for(int i = 1; i < size - 1; i++) {
				for(int j = 1; j < size - 1; j++) {
					System.Console.Write("{0, 2}", initial[i, j]);
				}
				System.Console.WriteLine();
			}
			System.Console.Write("Enterで終了");
			System.Console.ReadLine();
		}
	}
}
