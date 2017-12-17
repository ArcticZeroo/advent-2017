using Advent2017.api;
using Advent2017.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class day2 : AdventDay
	{
		private List<int[]> Rows = new List<int[]>();

		public day2 () : base(2, 2, InputSeparator.Newline)
		{
			string[] lines = Input.GetAll();

			foreach (string s in lines)
			{
				Rows.Add(ConvertLineToNums(s));
			}

			Start();
		}

		private int[] ConvertLineToNums(string input)
		{
			string[] chars = input.Split('\t');

			return StringUtil.ParseMembersToInt(chars);
		}

		protected override void Part1()
		{
			int total = 0;

			foreach (int[] row in Rows)
			{
				Logger.Debug("This row has " + row.Length + " members.");

				int lowest = int.MaxValue;
				int highest = 0;

				foreach (int n in row)
				{
					lowest = Math.Min(lowest, n);
					highest = Math.Max(highest, n);
				}

				Logger.Debug("The highest is " + highest + " and the lowest is " + lowest + ", meaning there is a difference of " + (highest - lowest));

				total += highest - lowest;

				Logger.Debug("New Total: " + total);
			}

			Logger.Solution(total.ToString());
		}

		private int GetDivisiblePair(int[] row)
		{
			Logger.Debug("This row has " + row.Length + " members...");

			for (int i = 0; i < row.Length - 1; i++)
			{
				int num1 = row[i];

				for (int j = i + 1; j < row.Length; j++)
				{
					int num2 = row[j];

					if (num1 % num2 == 0)
					{
						Logger.Debug("Found a pair of divisible numbers: " + num1 + " and " + num2);

						return num1 / num2;
					}

					if (num2 % num1 == 0)
					{
						Logger.Debug("Found a pair of divisible numbers: " + num1 + " and " + num2);

						return num2 / num1;
					}
				}
			}

			Logger.Debug("No divisible pair found, returning zero...");

			return 0;
		}

		protected override void Part2()
		{
			int total = 0;

			foreach (int[] row in Rows)
			{
				total += GetDivisiblePair(row);
			}

			Logger.Solution(total.ToString());
		}
	}
}
