using Advent2017.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class Point3D {
		public int x, y, z;

		public Point3D (int x = 0, int y = 0, int z = 0)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
	}

	class Day11 : AdventDay
	{
		List<string> instructions;

		public Day11 () : base(11, 1, InputSeparator.None)
		{
			instructions = Input.GetNext().Split(',').ToList();

			Start();
		}

		private int DistanceFromOrigin(int x, int y, int z)
		{
			return (new int[] { x, y, z }).Select(Math.Abs).Max();
		}

		protected override void Part1()
		{
			int x = 0;
			int y = 0;
			int z = 0;

			foreach (string instruction in instructions)
			{
				if (instruction == "n")
				{
					y++;
					z--;
				}
				else if (instruction == "s")
				{
					y--;
					z++;
				}
				else if (instruction == "ne")
				{
					x++;
					z--;
				}
				else if (instruction == "nw")
				{
					x--;
					y++;
				}
				else if (instruction == "se")
				{
					y--;
					x++;
				}
				else if (instruction == "sw")
				{
					x--;
					z++;
				}
			}

			Logger.Solution(String.Format("({0}, {1}, {2})", x, y, z));

			Logger.Solution((new int[] { x, y, z }).Select(Math.Abs).Max().ToString());
		}

		protected override void Part2()
		{
			int x = 0;
			int y = 0;
			int z = 0;

			int maxDistance = 0;

			foreach (string instruction in instructions)
			{
				if (instruction == "n")
				{
					y++;
					z--;
				}
				else if (instruction == "s")
				{
					y--;
					z++;
				}
				else if (instruction == "ne")
				{
					x++;
					z--;
				}
				else if (instruction == "nw")
				{
					x--;
					y++;
				}
				else if (instruction == "se")
				{
					y--;
					x++;
				}
				else if (instruction == "sw")
				{
					x--;
					z++;
				}

				maxDistance = Math.Max(maxDistance, DistanceFromOrigin(x, y, z));
			}

			Logger.Solution(String.Format("({0}, {1}, {2})", x, y, z));

			Logger.Solution(maxDistance.ToString());
		}
	}
}
