using Advent2017.api;
using Advent2017.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class Day6 : AdventDay
	{
		List<int> banks;

		public Day6() : base(6, 2, InputSeparator.None)
		{
			banks = StringUtil.ParseMembersToInt(Input.GetAt(0).Split('\t')).ToList();
			//banks = new List<int>(new int[] { 0, 2, 7, 0 });

			Start();
		}

		protected override void Part1()
		{
			List<List<int>> seenBanks = new List<List<int>>();

			bool hasBeenSeen = false;
			int cycles = 0;

			while (!hasBeenSeen)
			{
				cycles++;

				//Logger.Debug(String.Format("Cycle #{0}: {1}", cycles, String.Concat(banks.Select(i => i.ToString() + " "))));

				int highestVal = -1;
				int highestIndex = -1;

				// Find the highest value in the bank
				for (int i = 0; i < banks.Count; i++)
				{
					int bankVal = banks[i];

					if (bankVal > highestVal)
					{
						highestIndex = i;
						highestVal = bankVal;
					}
				}

				// Clear the bank value of the
				// highest index
				banks[highestIndex] = 0;

				int cursor = (highestIndex + 1) % banks.Count;
				int toDistribute = highestVal;

				// Distribute across banks
				while (toDistribute > 0)
				{
					banks[cursor] = banks[cursor] + 1;

					toDistribute--;
					cursor = (cursor + 1) % banks.Count;
				}

				// Store this permutation
				List<int> thisBank = new List<int>(banks);

				//Logger.Debug("There are currently " + seenBanks.Count.ToString() + " seen banks.");

				for (int bankIndex = 0; bankIndex < seenBanks.Count; bankIndex++)
				{
					if (seenBanks[bankIndex].SequenceEqual(thisBank))
					{
						hasBeenSeen = true;
						break;
					}
				}

				if (!hasBeenSeen)
				{
					seenBanks.Add(thisBank);
				}
			}

			Logger.Solution(String.Format("Cycles: {0}", cycles));
		}

		protected override void Part2()
		{
			List<List<int>> seenBanks = new List<List<int>>();

			bool seenFirstDuplicate = false;
			List<int> bankToFindAgain = new List<int>();

			bool seenSecondDuplicate = false;
			int cycles = 0;

			while (!seenSecondDuplicate)
			{
				//Logger.Debug(String.Format("Cycle #{0}: {1}", cycles, String.Concat(banks.Select(i => i.ToString() + " "))));

				int highestVal = -1;
				int highestIndex = -1;

				// Find the highest value in the bank
				for (int i = 0; i < banks.Count; i++)
				{
					int bankVal = banks[i];

					if (bankVal > highestVal)
					{
						highestIndex = i;
						highestVal = bankVal;
					}
				}

				// Clear the bank value of the
				// highest index
				banks[highestIndex] = 0;

				int cursor = (highestIndex + 1) % banks.Count;
				int toDistribute = highestVal;

				// Distribute across banks
				while (toDistribute > 0)
				{
					banks[cursor] = banks[cursor] + 1;

					toDistribute--;
					cursor = (cursor + 1) % banks.Count;
				}

				// Store this permutation
				List<int> thisBank = new List<int>(banks);

				//Logger.Debug("There are currently " + seenBanks.Count.ToString() + " seen banks.");

				if (!seenFirstDuplicate)
				{
					for (int bankIndex = 0; bankIndex < seenBanks.Count; bankIndex++)
					{
						if (seenBanks[bankIndex].SequenceEqual(thisBank))
						{
							bankToFindAgain = thisBank;
							seenFirstDuplicate = true;
							break;
						}
					}
				} else
				{
					cycles++;

					if (thisBank.SequenceEqual(bankToFindAgain))
					{
						seenSecondDuplicate = true;
						break;
					}
				}

				if (!seenSecondDuplicate)
				{
					seenBanks.Add(thisBank);
				}
			}

			Logger.Solution(String.Format("Cycles: {0}", cycles));
		}
	}
}
