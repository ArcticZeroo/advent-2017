using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent2017.api
{
	class AdventInput
	{
		private string[] lines;
		private int lastLine;

		public AdventInput(string[] lines)
		{
			this.lines = lines;

			this.lastLine = 0;
		}

		public string GetNext()
		{
			if (lastLine > lines.Length)
			{
				return null;
			}

			return lines[lastLine++];
		}

		public int GetIndex()
		{
			return lastLine;
		}

		public string GetAt(int index)
		{
			return lines[index];
		}

		public string[] GetAll()
		{
			return lines;
		}
	}
}
