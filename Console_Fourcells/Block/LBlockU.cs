using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class LBlockU : Block {

		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert, basePoint + 2 * convert, basePoint + 2 * convert + 1 };
		}
	}

	class LBlockR : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + 1, basePoint + 2, basePoint + convert };
		}
	}

	class LBlockD : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + 1, basePoint + convert + 1, basePoint + 2 * convert + 1 };
		}
	}

	class LBlockL : Block {
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + convert, basePoint + convert - 1, basePoint + convert - 2 };
		}
	}
}
