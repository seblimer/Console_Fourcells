using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class TBlockU : Block{
		public TBlockU() {
			num = 6;
		}
		
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 9, basePoint + 10, basePoint + 11 };
			return point;
		}

		public int[] charRegion(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint - 10, basePoint - 1, basePoint, basePoint + 1 };
			return point;
		}
	}
	class TBlockR : Block { 
		public TBlockR() {
			num = 16;
		}
		
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 10, basePoint + 11, basePoint + 20 };
			return point;
		}

		public int[] charRegion(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint - 10, basePoint, basePoint + 1, basePoint + 10 };
			return point;
		}
	}
	class TBlockD : Block { 
		public TBlockD() {
			num = 17;
		}
		
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 1, basePoint + 2, basePoint + 11 };
			return point;
		}

		public int[] charRegion(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint - 1, basePoint, basePoint + 1, basePoint + 10 };
			return point;
		}
	}
	class TBlockL : Block { 
		public TBlockL() {
			num = 18;
		}
		
		public override int[] region(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint, basePoint + 9, basePoint + 10, basePoint + 20 };
			return point;
		}

		public int[] charRegion(int row, int col) {
			int basePoint = 10 * row + col;
			int[] point = { basePoint - 10, basePoint - 1, basePoint, basePoint + 10 };
			return point;
		}
	}
}
