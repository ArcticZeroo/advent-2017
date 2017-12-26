using Advent2017.api;
using Advent2017.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class Day10 : AdventDay
	{
		List<int> hash;
		List<int> lengths;

		public Day10 () : base(10, 2, InputSeparator.None)
		{
			hash = Enumerable.Range(0, 256).ToList();

			Start();
		}

		protected override void Part1()
		{
			lengths = StringUtil.ParseMembersToInt(Input.GetNext().Split(',')).ToList();

			int cursor = 0;
			int skipSize = 0;

			foreach (int length in lengths)
			{
				if (length > hash.Count)
				{
					continue;
				}

				List<int> toReverse = new List<int>();

				int localCursor = cursor;

				for (int i = 0; i < length; i++)
				{
					toReverse.Add(hash[localCursor]);

					localCursor = (localCursor + 1) % hash.Count;
				}

				toReverse.Reverse();

				for (int i = 0; i < length; i++)
				{
					hash[cursor] = toReverse[i];

					cursor = (cursor + 1) % hash.Count;
				}

				cursor = (cursor + skipSize) % hash.Count;

				skipSize++;
			}

			Logger.Solution(String.Format("The first two numbers are {0} and {1}, whose product is {2}", hash[0], hash[1], hash[0] * hash[1]));
		}

		private int XorSeries(List<int> toXor, int index)
		{
			if (index == toXor.Count)
			{
				return 0;
			}

			return toXor[index] ^ XorSeries(toXor, index + 1);
		}

		protected override void Part2()
		{
			// Convert each character to a byte, stringify it then parse it (so I don't have to deal with
			// stupid behavior regarding byte -> int), and then turn it into a list.
			lengths = new UTF32Encoding().GetBytes(Input.GetNext()).Select(b => Int32.Parse(b.ToString())).Where(i => i != 0).ToList();

			// The specified lengths to add to the end, for some reason.
			lengths.AddRange(new int[] { 17, 31, 73, 47, 32 });

			int cursor = 0;
			int skipSize = 0;

			// Lord AdventOfCode wants 64 full rounds
			for (int round = 0; round < 64; round++)
			{
				foreach (int length in lengths)
				{
					if (length > hash.Count)
					{
						continue;
					}

					List<int> toReverse = new List<int>();

					int localCursor = cursor;

					for (int i = 0; i < length; i++)
					{
						toReverse.Add(hash[localCursor]);

						localCursor = (localCursor + 1) % hash.Count;
					}

					toReverse.Reverse();

					for (int i = 0; i < length; i++)
					{
						hash[cursor] = toReverse[i];

						cursor = (cursor + 1) % hash.Count;
					}

					cursor = (cursor + skipSize) % hash.Count;

					skipSize++;
				}
			}

			List<int> dense = new List<int>();

			for (int i = 0; i < hash.Count; i += 16)
			{
				dense.Add(XorSeries(hash.GetRange(i, 16), 0));
			}

			string hexHash = "";

			foreach (int denseNum in dense)
			{
				hexHash += denseNum.ToString("X2");
			}

			Logger.Debug("Hash size: " + hexHash.Length.ToString());

			Logger.Solution(hexHash.ToLower());
		}
	}
}
