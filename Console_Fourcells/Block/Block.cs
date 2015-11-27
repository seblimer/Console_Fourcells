using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	abstract class Block {
		protected int num;

		public int Num {
			get{return num;}
		}

		public abstract int[] region(int row, int col);

		//public abstract int[] charRegion(int row, int col);
	}
}
