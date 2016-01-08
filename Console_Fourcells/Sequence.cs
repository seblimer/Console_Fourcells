using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Fourcells {
	class Sequence {
		private int[] sequence;
		List<int> achieved = new List<int>();
		List<int> complete = new List<int>();
		public Sequence(int size) {
			sequence = new int[size * size];
		}

		public int[] sequenceProperty {
			get { return (int[])sequence.Clone(); }
			set { sequence = value; }
		}

		public int seqNum() {
			int len = sequence.Length;
			for(int i = 0; i < len; i++) {
				if(sequence[i] == 0) {
					return i + 1;
				}
			}
			return 0;
		}

		public int blockWeight(int num) {
			if(num < 0) {
				return 4;
			}
			return --num >= 0 ? sequence[num] : 1;
		}

		public void add(int num) {
			add(num, 1);
		}
		public void add(int num, int value) {
			sequence[num - 1] += value;
			if(sequence[num - 1] == 4) {
				if(!achieved.Contains(num)) {
					achieved.Add(num);
				}
			}
		}

		public void clear(int num) {
			sequence[num - 1] = 0;
		}

		public void entryAchive(Board board) {
			board.achivedAdd(achieved);
			complete.AddRange(achieved);
			complete.Sort();
			achieved.Clear();
		}

		public bool isBlockComplete() {
			foreach(int se in sequence) {
				if(se % 4 != 0){
					return false;
				}
			}
			return true;
		}
	}
}
