using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class TurnLBlockU : Block{
		public TurnLBlockU() {
			num = 2;
		}
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 10, basePoint + 20, basePoint + 19 };
			return point;
		}
	}
	class TurnLBlockR : Block {
		public TurnLBlockR() {
			num = 11;
		}
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 10, basePoint + 11, basePoint + 12 };
			return point;
		}
	}
	class TurnLBlockD : Block {
		public TurnLBlockD() {
			num = 12;
		}
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 1, basePoint + 10, basePoint + 20 };
			return point;
		}
	}
	class TurnLBlockL : Block {
		public TurnLBlockL() {
			num = 13;
		}
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 1, basePoint + 2, basePoint + 12 };
			return point;
		}
	}
}
