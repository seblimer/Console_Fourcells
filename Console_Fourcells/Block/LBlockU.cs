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
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 10, basePoint + 20, basePoint + 21 };
			return point;
		}

		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}

	class LBlockR : Block { 
		public LBlockR() {
			num = 8;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 1, basePoint + 2, basePoint + 10 };
			return point;
		}

		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}

	class LBlockD : Block {
		public LBlockD() {
			num = 9;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 1, basePoint + 11, basePoint + 21 };
			return point;
		}

		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}

	class LBlockL : Block { 
		public LBlockL() {
			num = 10;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 10, basePoint + 9, basePoint + 8 };
			return point;
		}

		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}
}
