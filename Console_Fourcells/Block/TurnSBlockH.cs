using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class TurnSBlockH : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + 1, basePoint + convert + 1, basePoint + convert + 2 };
		}
	}
	class TurnSBlockV : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert - 1, basePoint + convert, basePoint + 2 * convert - 1 };
		}
	}
}
