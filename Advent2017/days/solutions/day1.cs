using Advent2017.api;
using Advent2017.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class day1
	{
		AdventInput input = AdventUtil.getInput(1, 1, InputSeparator.Character);

		public day1()
		{
			//s1();
			s2();
		}

		private void s1()
		{
			List<int> nums = new List<int>();

			foreach (string s in input.GetAll())
			{
				int parsed;

				if (int.TryParse(s, out parsed))
				{
					nums.Add(parsed);
				}
			}

			int total = 0;

			for (int i = 0; i < nums.Count; i++)
			{
				int current = nums[i];
				int next;

				// If the next index goes past the last index
				if (i + 1 > nums.Count - 1)
				{
					next = nums[0];
				}
				else
				{
					next = nums[i + 1];
				}

				if (current == next)
				{
					total += current;
				}
			}

			Console.WriteLine("Total: {0}", total);
		}

		private void s2()
		{
			List<int> nums = new List<int>();

			foreach (string s in input.GetAll())
			{
				int parsed;

				if (int.TryParse(s, out parsed))
				{
					nums.Add(parsed);
				}
			}

			int total = 0;

			for (int i = 0; i < nums.Count; i++)
			{
				int current = nums[i];
				int next = nums[((nums.Count / 2) + i) % nums.Count];

				if (current == next)
				{
					total += current;
				}
			}

			Console.WriteLine("Total: {0}", total);
		}
	}
}
