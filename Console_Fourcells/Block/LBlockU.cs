using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class LBlockU : Block{
		public LBlockU() {
			num = 1;
		}
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 10, basePoint + 20, basePoint + 21 };
			return point;
		}
	}

	class LBlockR : Block { 
		public LBlockR() {
			num = 8;
		}
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 1, basePoint + 2, basePoint + 10 };
			return point;
		}
	}

	class LBlockD : Block {
		public LBlockD() {
			num = 9;
		}
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 1, basePoint + 11, basePoint + 21 };
			return point;
		}
	}

	class LBlockL : Block { 
		public LBlockL() {
			num = 10;
		}
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 10, basePoint + 9, basePoint + 8 };
			return point;
		}
	}
}
