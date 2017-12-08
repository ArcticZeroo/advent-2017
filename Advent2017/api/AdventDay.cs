﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.api
{
	abstract class AdventDay
	{
		protected int Day;
		protected int Part;
		protected InputSeparator Separator;
		protected AdventLogger Logger;
		protected AdventInput Input;

		public AdventDay(int day, int part, InputSeparator separator)
		{
			Day = day;
			Part = part;
			Separator = separator;
			Logger = new AdventLogger(day, part);

			Logger.Info("Begin Solution");

			if (part == 1)
			{
				Part1();
			} else if (part == 2)
			{
				Part2();
			}

			Logger.Info("End Solution");

			Console.ReadKey();
		}

		protected abstract void Part1();

		protected abstract void Part2();
	}
}