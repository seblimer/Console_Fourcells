using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Program {
		static void Main(string[] args) {
			Setter set = new Setter();
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
								 {"-","-","-","-","1","-","-","-"},};
			Board fillBoard = new Board(size);

			List<int> oneList = new List<int>();
			List<int> blackList = new List<int>();
			List<Block> checkList = new List<Block>();
			List<int> poss = new List<int>();
			int[] cross;
			int oneDig = new int();
			int tenDig = new int();

			/****************************************
			 * 1の登録
			 * 3の登録
			 * 繋がらない部分の処理
			 ****************************************/
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(iniBoard[i, j] == "1") {
						oneList.Add(i * 10 + j);
						if(fillBoard.check(i * 10 + j) == 0) {
							fillBoard.fill(i * 10 + j);
						}
						cross = fillBoard.cross(i * 10 + j);
						for(int k = 0; k < cross.Length; k++) {
							oneDig = cross[k] % 10;
							tenDig = cross[k] / 10;
							if(0 <= tenDig && tenDig < size) {
								if(0 <= oneDig && oneDig < size) {
									if(iniBoard[tenDig, oneDig] == "1") {
										if(fillBoard.check(cross[k]) == 0) {
											fillBoard.fill(cross[k]);
										}
										fillBoard.registList(fillBoard.check(i * 10 + j), fillBoard.check(cross[k]));
									}
								}
							}
						}
					}
					if(iniBoard[i, j] == "3") {
						if(fillBoard.check(i * 10 + j) == 0) {
							fillBoard.fill(i * 10 + j);
						}
						cross = fillBoard.cross(i * 10 + j);
						for(int k = 0; k < cross.Length; k++) {
							oneDig = cross[k] % 10;
							tenDig = cross[k] / 10;
							if(0 <= tenDig && tenDig < size) {
								if(0 <= oneDig && oneDig < size) {
									if(iniBoard[tenDig, oneDig] == "3") {
										if(fillBoard.check(cross[k]) == 0) {
											fillBoard.fill(cross[k]);
										}
										fillBoard.registList(fillBoard.check(i * 10 + j), fillBoard.check(cross[k]));
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
			//while(!fillBoard.compOr()) {
			for(int y = 0; y < 10; y++) {
				//"1"のマスについてチェック
				if(oneList.Count() != 0) {
					blocks = set.TBlocks();
					foreach(int li in oneList) {
						foreach(Block bl in blocks) {
							if(fillBoard.isMatch(bl.charRegion(li))) {
								checkList.Add(bl);
							}
						}
						if(checkList.Count == 1) {
							fillBoard.paint(checkList[0].charRegion(li));
							blackList.Add(li);
						}
						checkList.Clear();
					}
					try {
						foreach(int bl in blackList) {
							oneList.Remove(bl);
						}
					}
					catch { }
					finally {
						blackList.Clear();
					}
				}

				/****************************************
				 * 各数字のマスの上下左右をチェック
				 * 4-数字分周りが確定したブロックならばそれ以外のマスと繋がる
				 * **************************************/
				for(int i = 0; i < size; i++) {
					for(int j = 0; j < size; j++) {
						if(iniBoard[i, j] == "2" || iniBoard[i, j] == "3") {
							fillBoard.extend(int.Parse(iniBoard[i, j]), i * 10 + j);
						}
					}
				}

				fillBoard.accrual();
			}

			fillBoard.debugWrite();
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					System.Console.Write("{0, 4}", iniBoard[i, j]);
				}
				System.Console.WriteLine();
			}
			fillBoard.debugArrayWrite();
			//System.Console.WriteLine("oneList : {0}", oneList.Count);
			//foreach(int li in oneList) {
			//	System.Console.WriteLine(li);
			//}
			fillBoard.showDisc();

			System.Console.WriteLine();
			System.Console.Write("Enterで終了 + seqNum = {0}", fillBoard.seqNum());
			System.Console.ReadLine();
		}
	}
}
