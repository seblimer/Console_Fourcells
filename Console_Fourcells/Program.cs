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
			int size = 6;
			if(size % 2 != 0) {
				System.Console.Write("盤面は偶数*偶数で入力してください");
				System.Console.ReadLine();
				return;
			}
			string[,] iniBoard ={{"-","-","1","-","2","-"},
								 {"-","2","-","3","-","-"},
								 {"-","-","-","3","-","-"},
								 {"-","-","1","-","-","-"},
								 {"-","-","-","2","-","-"},
								 {"2","-","3","-","-","2"}};
			Board fillBoard = new Board(size);

			List<int> oneList = new List<int>();
			List<int> blackList = new List<int>();
			List<Block> checkList = new List<Block>();
			int[] salReg = new int[5];
			int maxSeqNum = size * size / 4;

			/****************************************
			 * 同一ブロックに複数存在しない"1"に
			 * それぞれの番号を付与
			 ****************************************/
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(iniBoard[i, j] == "1") {
						oneList.Add(i * 10 + j);
						if(fillBoard.check(i * 10 + j)) {
							fillBoard.fill(fillBoard.seqNum(), i * 10 + j);
						}
						iniBoard[i, j] = "-";
					}
				}
			}

			/****************************************
			 * 終わる、もしくは駄目だとわかるまでループ
			 ****************************************/
			//while(!fillBoard.compOr()) {

			//"1"のマスについてチェック
			if(oneList.Count() != 0) {
				blocks = set.TBlocks();
				foreach(int li in oneList) {
					foreach(Block bl in blocks) {
						if(fillBoard.check(bl.charRegion(li))) {
							checkList.Add(bl);
						}
					}
					if(checkList.Count == 1) {
						if(fillBoard.getNum() == 0) {
							fillBoard.fill(fillBoard.seqNum(), checkList[0].charRegion(li));
						}
						else {
							fillBoard.fill(fillBoard.getNum(), checkList[0].charRegion(li));
						}
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
						if(fillBoard.extend(int.Parse(iniBoard[i, j]), i * 10 + j)) {
							iniBoard[i, j] = "-";
						}
					}
				}
			}

			fillBoard.accrual();

			fillBoard.debugWrite();
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					System.Console.Write("{0, 3}", iniBoard[i, j]);
				}
				System.Console.WriteLine();
			}
			//fillBoard.debugArrayWrite();
			//System.Console.WriteLine("oneList : {0}", oneList.Count);
			foreach(int li in oneList) {
				System.Console.WriteLine(li);
			}
			System.Console.WriteLine();
			System.Console.Write("Enterで終了 + seqNum = {0}", fillBoard.seqNum());
			System.Console.ReadLine();
		}
	}
}
