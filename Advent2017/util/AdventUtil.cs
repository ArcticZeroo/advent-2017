using Advent2017.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Advent2017.util
{
	class AdventUtil
	{
		private const string InputPath = "../../days/inputs/";

		private static string getPath(string name)
		{
			return InputPath + name + ".txt";
		}

		public static AdventInput getInput(string name, InputSeparator separator)
		{
			// invalid path name
			if (name == null || name.Contains(":"))
			{
				return null;
			}

			string path = getPath(name);

			string[] lines;
 
			switch (separator)
			{
				case InputSeparator.Newline:
					lines = File.ReadAllLines(path);

					break;
				case InputSeparator.Character:
					string text = File.ReadAllText(path);

					lines = text.ToCharArray().Select(char.ToString).ToArray();
					break;
				case InputSeparator.None:
					lines = new string[] { File.ReadAllText(path) };
					break;
				default:
					lines = null;
					break;
			}

			if (lines == null)
			{
				return null;
			}

			return new AdventInput(lines);
		}

		public static AdventInput getInput(int day, int part, InputSeparator separator)
		{
			return getInput(day + "." + part, separator);
		}
	}
}
