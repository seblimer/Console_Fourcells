﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class TurnSBlockH : Block{
		public TurnSBlockH() {
			num = 4;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 1, basePoint + 11, basePoint + 12 };
			return point;
		}
		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}
	class TurnSBlockV : Block {
		public TurnSBlockV() {
			num = 15;
		}
		public override int[] region(int basePoint) {
			int[] point = { basePoint, basePoint + 9, basePoint + 10, basePoint + 19 };
			return point;
		}
		public override int[] charRegion(int basePoint) {
			return new int[0];
		}
	}
}
