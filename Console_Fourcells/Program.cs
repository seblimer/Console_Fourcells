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
			Board fillBoard = new Board(size + 2);

			//Block[] block = 
			//	{	new LBlockDown() , 
			//		new LBlockLeft()
			//	};
			//for(int i = 0; i < block.Length; i++) {
			//	fillBoard.fill(block[i].NUM, block[i].region(1, 3));
			//}

			//Block block = new LBlockU();
			//fillBoard.fill(block.Num, block.region(1, 1));

			//fillBoard.debugWrite();
			for(int i = 0; i < blocksLength; i++) {
				if(fillBoard.check(set.numBlock(i).region(1, 1))) {
					System.Console.Write("{0,3}", set.numBlock(i).Num);
				}
			}
			System.Console.WriteLine();
			System.Console.Write("Enterで終了");
			System.Console.ReadLine();
		}
	}
}
