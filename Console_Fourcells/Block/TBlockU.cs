using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class TBlockU : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert - 1, basePoint + convert, basePoint + convert + 1 };
		}
	}
	class TBlockR : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert, basePoint + convert + 1, basePoint + 2 * convert };

		}
	}
	class TBlockD : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + 1, basePoint + 2, basePoint + convert + 1 };
		}
	}
	class TBlockL : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert - 1, basePoint + convert, basePoint + 2 * convert };
		}
	}
}
