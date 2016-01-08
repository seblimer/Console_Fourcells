using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class OBlock : Block{
		public override int[] region(int basePoint) {
			return new int[] { basePoint, basePoint + 1, basePoint + convert, basePoint + convert + 1 };
		}
	}
}
