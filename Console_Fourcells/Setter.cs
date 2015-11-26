using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Setter {
		private Block[] blocks;

		public Setter(){
			blocks = setBlockAll();
		}

		public Block numBlock(int num) {
			return blocks[num];
		}

		public int blocksLength() {
			return blocks.Length;
		}

		public Block[] setBlockAll() {
			Block[] blocks =
			{	new LBlockU(),
				new TurnLBlockU(),
				new SBlockH(),
				new TurnSBlockH(),
				new OBlock(),
				new TBlockU(),
				new IBlockV(),
				new LBlockR(),
				new LBlockD(),
				new LBlockL(),
				new TurnLBlockR(),
				new TurnLBlockD(),
				new TurnLBlockL(),
				new SBlockV(),
				new TurnSBlockV(),
				new TBlockR(),
				new TBlockD(),
				new TBlockL(),
				new IBlockH()
			};
			return blocks;
		}
	}
}
