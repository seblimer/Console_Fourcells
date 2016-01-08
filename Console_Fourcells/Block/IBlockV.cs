using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class IBlockV : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert, basePoint + 2 * convert, basePoint + 3 * convert };	
		}
	}
	class IBlockH : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + 1, basePoint + 2, basePoint + 3 };
		}
	}
}
