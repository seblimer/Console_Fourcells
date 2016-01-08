using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class TurnLBlockU : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert, basePoint + 2 * convert, basePoint + 2 * convert - 1 };
		}
	}
	class TurnLBlockR : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert, basePoint + convert + 1, basePoint + convert + 2 };
		}
	}
	class TurnLBlockD : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + 1, basePoint + convert, basePoint + 2 * convert };
		}
	}
	class TurnLBlockL : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + 1, basePoint + 2, basePoint + convert + 2 };
		}
	}
}
