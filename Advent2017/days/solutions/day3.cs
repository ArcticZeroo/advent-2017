using Advent2017.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class Point
	{
		int x;
		int y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Point() : this(0, 0) { }

		// The manhattan distance between the two
		public int Manhattan(Point to)
		{
			return Math.Abs(to.x - x) + Math.Abs(to.y - y);
		}

		public static Point operator +(Point a, Point b)
		{
			return new Point(a.x + b.x, a.y + b.y);
		}

		public static Point operator -(Point a, Point b)
		{ 
			return new Point(a.x - b.x, a.y - b.y);
		}

		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
	}

	class Day3 : AdventDay
	{
		public Day3() : base(3, 1)
		{
			Start();
		}

		private int GetSpiralCorner(int num)
		{
			double sqrtCeil = Math.Ceiling(Math.Sqrt(num));

			// If it's even...
			if (sqrtCeil % 2 == 0)
			{
				sqrtCeil++;
			}

			// sqrtCeil should now contain the value of the corner, in sqrt form
			return (int) Math.Pow(sqrtCeil, 2);
		}

		private Point GetOrigin(int sideLength)
		{
			int dist = (int) Math.Floor((double) sideLength / 2);

			return new Point(dist, dist);
		}

		private int GetSideLengthFromLast(int lastNumber)
		{
			return (int)Math.Sqrt(lastNumber);
		}

		private int GetSideLengthFromStart(int num)
		{
			int lastNumber = GetSpiralCorner(num);

			return GetSideLengthFromLast(lastNumber);
		}

		private Point GetPointFor(int num)
		{
			// Bottom right corner number
			int lastNumber = GetSpiralCorner(num);

			int sideLength = GetSideLengthFromLast(lastNumber);

			// 1 greater than the corner value of the last one
			int firstNumber = (int)Math.Pow(sideLength - 2, 2) + 1;

			List<int> corners = new List<int>();

			for (int i = lastNumber; i > firstNumber; i -= sideLength - 1)
			{
				corners.Add(i);
			}

			int lowestDistance = int.MaxValue;

			foreach (int corner in corners)
			{
				int distance = corner - num;

				if (distance < 0)
				{
					continue;
				}

				if (distance < lowestDistance)
				{
					lowestDistance = distance;
				}
			}

			return new Point(sideLength - 1, lowestDistance);
		}

		protected override void Part1()
		{
			int i = 325489;
			Logger.Solution(GetOrigin(GetSideLengthFromStart(i)).Manhattan(GetPointFor(i)).ToString());
		}

		protected override void Part2()
		{
			throw new NotImplementedException();
		}
	}
}
