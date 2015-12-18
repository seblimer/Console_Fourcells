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

		public int Length {
			get { return sequence.Length; }
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

		public List<int> notZero() {
			List<int> list = new List<int>();
			for(int i = 0; i < sequence.Length; i++) {
				if(0 < sequence[i] && sequence[i] < 4) {
					list.Add(i + 1);
				}
			}
			return list;
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

		public void sub(int num) {
			sub(num, 1);
		}
		public void sub(int num, int value) {
			sequence[num - 1] -= value;
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
	
		//デバッグ関係
		public void write() {
			int size = sequence.Length;
			for(int i = 0; i < size; i = i + 3) {
				try {
					System.Console.Write("{0,2} : {1}  ", i + 1, sequence[i]);
					System.Console.Write("{0,2} : {1}  ", i + 2, sequence[i + 1]);
					System.Console.WriteLine("{0,2} : {1}", i + 3, sequence[i + 2]);
				}
				catch {
					System.Console.WriteLine();
				}
			}
		}
	}
}
