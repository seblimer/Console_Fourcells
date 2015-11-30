using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class OBlock : Block{
		public OBlock() {
			num = 5;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 1, basePoint + 10, basePoint + 11 };
			return point;
		}

		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}
}
