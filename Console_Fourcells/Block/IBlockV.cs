using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class IBlockV : Block{
		public IBlockV() {
			num = 7;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 10, basePoint + 20, basePoint + 30 };
			return point;
		}

		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}
	class IBlockH : Block {
		public IBlockH() {
			num = 19;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 1, basePoint + 2, basePoint + 3 };
			return point;
		}
		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}
}
