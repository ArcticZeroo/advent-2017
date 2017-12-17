using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.api
{
	class AdventLogger
	{
		public int Day = 0;
		public int Part = 0;

		public AdventLogger(int day, int part)
		{
			Day = day;
			Part = part;
		}

		public void Print(string text, bool line = true, bool withFormat = true)
		{
			string format = (withFormat) ? String.Format("[{0}.{1}] {2}", Day, Part, text) : text;

			if (line)
			{
				Console.WriteLine(format);
			} else
			{
				Console.Write(format);
			}
		}

		private void PrintColored(string text, ConsoleColor color, bool line = false, bool withFormat = true)
		{
			ConsoleColor priorColor = Console.ForegroundColor;

			Console.ForegroundColor = color;

			Print(text, line);

			Console.ForegroundColor = priorColor;
		}

		public void Debug(string text)
		{
			PrintColored("DEBUG: ", ConsoleColor.Yellow);

			Print(text, withFormat: false);
		}


		public void Info(string text)
		{
			PrintColored("INFO: ", ConsoleColor.Green);

			Print(text, withFormat: false);
		}

		public void Solution(string text)
		{
			PrintColored("SOLUTION: ", ConsoleColor.Magenta);

			Print(text, withFormat: false);
		}
	}
}
