using Advent2017.util;
using System;
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

		public AdventDay(int day, int part, InputSeparator? separator = null)
		{
			Day = day;
			Part = part;
			Logger = new AdventLogger(day, part);

			if (separator != null)
			{
				Separator = separator.GetValueOrDefault();
				Input = AdventUtil.getInput(day, part, separator.GetValueOrDefault());
			}
		}

		protected void Start()
		{
			Logger.Info("Begin Solution");

			if (Part == 1)
			{
				Part1();
			}
			else if (Part == 2)
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
