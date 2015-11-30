using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Setter {
		private Block[] blocks;
		private Block[] tBlocks= new Block[4];

		public Setter(){
			blocks = setBlockAll();
			Array.Copy(blocks, 13, tBlocks, 0, 4);
		}

		public Block[] setBlockAll() {
			Block[] blocks =
			{	new LBlockU(),
				new LBlockR(),
				new LBlockD(),
				new LBlockL(),
				new TurnLBlockU(),
				new TurnLBlockR(),
				new TurnLBlockD(),
				new TurnLBlockL(),
				new SBlockH(),
				new SBlockV(),
				new TurnSBlockH(),
				new TurnSBlockV(),
				new OBlock(),
				new TBlockU(),
				new TBlockR(),
				new TBlockD(),
				new TBlockL(),
				new IBlockV(),
				new IBlockH()
			};
			return blocks;
		}

		public Block[] AllBlocks() {
			return blocks;
		}
		public Block[] TBlocks() {
			return tBlocks;
		}
	}
}
