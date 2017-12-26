using Advent2017.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class Day9 : AdventDay
	{
		public Day9 () : base(9, 2, InputSeparator.None)
		{
			Start();
		}

		protected override void Part1()
		{
			bool garbageActive = false;
			int depth = 1;
			int score = 0;

			char[] chars = Input.GetNext().ToCharArray();

			for (int i = 0; i < chars.Length; i++)
			{
				char c = chars[i];

				if (c == '!')
				{
					i++;
				}
				else if (c == '<')
				{
					garbageActive = true;
				}
				else if (c == '>')
				{
					garbageActive = false;
				}
				else if (c == '{' && !garbageActive)
				{
					score += depth;

					depth++;
				}
				else if (c == '}' && !garbageActive)
				{
					depth--;
				}
			}

			Logger.Solution(score.ToString());
		}

		protected override void Part2()
		{
			bool garbageActive = false;
			int score = 0;

			char[] chars = Input.GetNext().ToCharArray();

			for (int i = 0; i < chars.Length; i++)
			{
				char c = chars[i];

				if (c == '!')
				{
					i++;
				}
				else if (c == '>')
				{
					garbageActive = false;
				}
				else if (garbageActive)
				{
					score++;
				}
				else if (c == '<')
				{
					garbageActive = true;
				}
			}

			Logger.Solution(score.ToString());
		}
	}
}
