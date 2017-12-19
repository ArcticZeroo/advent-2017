using Advent2017.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class Day4 : AdventDay
	{
		public Day4() : base(4, 2, InputSeparator.Newline)
		{
			Start();
		}

		// Used for part 1, when passphrase has only unique chunks
		bool IsValidUniquePassphrase(string input)
		{
			string[] chunks = input.Split(' ');

			return chunks.Distinct().ToArray().Length == chunks.Length;
		}

		bool IsValidAnagramPassphrase(string input)
		{
			string[] chunks = input.Split(' ');

			//foreach (string chunk in chunks.Select(s => String.Concat(s.OrderBy(c => c)).ToArray())) 
			//{

			//}

			return chunks.Select(s => s.OrderBy(c => c)).Distinct().ToArray().Length == chunks.Length;
		}
			 
		protected override void Part1()
		{
			int valid = 0;

			foreach (string line in Input.GetAll())
			{
				if (IsValidUniquePassphrase(line))
				{
					valid++;
				}
			}

			Logger.Solution(valid.ToString());
		}

		protected override void Part2()
		{
			int valid = 0;

			foreach (string line in Input.GetAll())
			{
				if (IsValidAnagramPassphrase(line))
				{
					valid++;
				}
			}

			Logger.Solution(valid.ToString());
		}
	}
}
