using Advent2017.api;
using Advent2017.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class Day5 : AdventDay
	{
		public Day5 () : base(5, 2, InputSeparator.Newline)
		{
			Start();
		}

		protected override void Part1()
		{
			int[] instructions = StringUtil.ParseMembersToInt(Input.GetAll());

			int index = 0;
			int steps = 0;

			while (index >= 0 && index < instructions.Length)
			{
				int newIndex = index + instructions[index];

				instructions[index]++;

				index = newIndex;

				steps++;
			}

			Logger.Solution(String.Format("Steps: {0}", steps));
		}

		protected override void Part2()
		{
			int[] instructions = StringUtil.ParseMembersToInt(Input.GetAll());

			int index = 0;
			int steps = 0;

			while (index >= 0 && index < instructions.Length)
			{
				int offset = instructions[index];

				int newIndex = index + offset;

				if (offset >= 3)
				{
					instructions[index]--;
				} else
				{
					instructions[index]++;
				}

				index = newIndex;

				steps++;
			}

			Logger.Solution(String.Format("Steps: {0}", steps));
		}
	}
}
