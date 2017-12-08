using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.util
{
	class StringUtil
	{
		public static int[] ParseMembersToInt(string[] chars)
		{
			List<int> nums = new List<int>();

			foreach (string s in chars)
			{
				int parsed;

				if (int.TryParse(s, out parsed))
				{
					nums.Add(parsed);
				}
			}

			return nums.ToArray();
		}
	}
}
