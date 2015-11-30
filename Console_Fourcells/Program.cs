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
			string[,] iniBoard ={{"-","1","-","-","-","3"},
								 {"-","-","-","-","1","3"},
								 {"1","-","-","-","-","-"},
								 {"-","-","1","-","-","-"},
								 {"-","-","-","-","-","-"},
								 {"-","-","-","-","-","2"}};
			Board fillBoard = new Board(size + 2);

			List<int> oneList = new List<int>();
			List<int> blackList = new List<int>();
			List<Block> checkList = new List<Block>();
			int maxSeqNum = size * size / 4;
			int seqNum = 1;

			/****************************************
			 * 同一ブロックに複数存在しない"1","3"が並ぶ場合、
			 * 別のブロック番号で置く
			 ****************************************/
			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(iniBoard[i, j] == "1") {
						oneList.Add(i * 10 + j);

						try {
							if(iniBoard[i, j + 1] == "1"
								&& fillBoard.check(i * 10 + j + 1)) {
								fillBoard.fill(seqNum++, i * 10 + j + 1);
							}
						}
						catch { /*無視*/ }

						try {
							if(iniBoard[i + 1, j] == "1"
								&& fillBoard.check((i + 1) * 10 + j)) {
								fillBoard.fill(seqNum++, (i + 1) * 10 + j);
							}
						}
						catch { /*無視*/ }

						if(fillBoard.check(i * 10 + j)) {
							fillBoard.fill(seqNum++, i * 10 + j);
						}
					}
					//else if(iniBoard[i, j] == "3") {
					//	try {
					//		if(iniBoard[i, j + 1] == "3") {
					//			if(fillBoard.check(i * 10 + j)) {
					//				fillBoard.fill(seqNum++, i * 10 + j);
					//			}
					//			if(fillBoard.check(i * 10 + j + 1)) {
					//				fillBoard.fill(seqNum++, i * 10 + (j + 1));
					//			}
					//		}
					//	}
					//	catch { /*無視*/ }
					//	try {
					//		if(iniBoard[i + 1, j] == "3") {
					//			if(fillBoard.check(i * 10 + j)) {
					//				fillBoard.fill(seqNum++, i * 10 + j);
					//			}
					//			if(fillBoard.check((i + 1) * 10 + j)) {
					//				fillBoard.fill(seqNum++, (i + 1) * 10 + j);
					//			}
					//		}
					//	}
					//	catch { /*無視*/ }
					//}
				}
			}

			/****************************************
			 * 終わる、もしくは駄目だとわかるまでループ
			 ****************************************/
			//while(!fillBoard.compOr()) {

			//"1"のマスについてチェック
			blocks = set.TBlocks();
			foreach(int li in oneList) {	
				foreach(Block bl in blocks) {
					if(fillBoard.check(bl.charRegion(li))) {
						checkList.Add(bl);
					}
				}
				if(checkList.Count == 1) {
					if(fillBoard.NextNum() == 0) {
						fillBoard.fill(seqNum++, checkList[0].charRegion(li));
					}
					else {
						fillBoard.fill(fillBoard.NextNum(), checkList[0].charRegion(li));
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
			//ここまで
			//}

			fillBoard.debugWrite();
			//fillBoard.debugArrayWrite();
			//System.Console.WriteLine("oneList : {0}", oneList.Count);
			foreach(int li in oneList){
				System.Console.WriteLine(li);
			}
			System.Console.WriteLine();
			System.Console.Write("Enterで終了 + seqNum = {0}", seqNum);
			System.Console.ReadLine();
		}
	}
}
