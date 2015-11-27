using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Program {
		static void Main(string[] args) {
			Setter set = new Setter();
			int blocksLength = set.blocksLength();
			int size = 4;
			if(size % 2 != 0) {
				System.Console.Write("盤面は偶数*偶数で入力してください");
				System.Console.ReadLine();
				return;
			}
			int[,] iniBoard ={ {-1, -1, -1, -1},
								{-1, 1, -1, -1},
								{-1, 1, -1, -1},
								{-1, -1, -1, -1}};
			Board fillBoard = new Board(size + 2);

			int maxSeqNum = size * size / 4;
			int[] seqArray = new int[maxSeqNum];
			int seqNum = 1;

			for(int i = 0; i < size; i++) {
				for(int j = 0; j < size; j++) {
					if(iniBoard[i, j] == 1) {
						if(iniBoard[i, j + 1] == 1) {
							fillBoard.fill(seqNum, i* 10 + j);
							seqArray[seqNum - 1]++;
							seqNum++;
							fillBoard.fill(seqNum, i * 10 + j + 1);
							seqArray[seqNum - 1]++;
							seqNum++;
						}
						else if(iniBoard[i + i, j] == 1) {
							fillBoard.fill(seqNum, i * 10 + j);
							seqArray[seqNum - 1]++;
							seqNum++;
							fillBoard.fill(seqNum, (i + 1) * 10 + j);
							seqArray[seqNum - 1]++;
							seqNum++;
						}
					}
					else if(iniBoard[i, j] == 3) {
						if(iniBoard[i, j + 1] == 3) {
							fillBoard.fill(seqNum, i * 10 + j);
							seqArray[seqNum - 1]++;
							seqNum++;
							fillBoard.fill(seqNum, i * 10 + j + 1);
							seqArray[seqNum - 1]++;
							seqNum++;
						}
						else if(iniBoard[i + i, j] == 3) {
							fillBoard.fill(seqNum, i * 10 + j);
							seqArray[seqNum - 1]++;
							seqNum++;
							fillBoard.fill(seqNum, (i + 1) * 10 + j);
							seqArray[seqNum - 1]++;
							seqNum++;
						}
					}
				}
			}

			//Block[] block = 
			//	{	new LBlockDown() , 
			//		new LBlockLeft()
			//	};
			//Block block = new LBlockU();
			//fillBoard.fill(block.Num, block.region(1, 1));

			//fillBoard.debugWrite();
			//for(int i = 0; i < blocksLength; i++) {
			//	if(fillBoard.check(set.numBlock(i).region(1, 1))) {
			//		System.Console.Write("{0,3}", set.numBlock(i).Num);
			//	}
			//}
			System.Console.WriteLine();
			System.Console.Write("Enterで終了");
			System.Console.ReadLine();
		}
	}
}
