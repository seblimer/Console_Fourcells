using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class SBlockH : Block{
		public SBlockH() {
			num = 3;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 1, basePoint + 9, basePoint + 10 };
			return point;
		}

		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}
	class SBlockV : Block {
		public SBlockV() {
			num = 14;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 10, basePoint + 11, basePoint + 21 };
			return point;
		}

		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}
}
