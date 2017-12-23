using Advent2017.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	enum ActionType
	{
		INCREMENT,
		DECREMENT
	}

	enum Comparator
	{
		LESS_THAN,
		LESS_OR_EQUAL_TO,
		GREATER_THAN,
		GREATER_OR_EQUAL_TO,
		EQUAL_TO,
		NOT_EQUAL_TO,
		UNKNOWN
	}

	class Instruction
	{
		private static readonly Regex INPUT_MATCH = new Regex(@"^(?<actionRegister>\w+) (?<actionLabel>inc|dec) (?<actionAmount>[-]?\d+) if (?<conditionRegister>\w+) (?<conditionComparator>[!=<>]+) (?<conditionCount>[-]?\d+)$");

		public string ActionRegister;
		public ActionType ActionLabel;
		public int ActionCount;

		public string ConditionRegister;
		public Comparator ConditionComparator;
		public int ComparatorCount;

		public static Instruction ParseFromInput(string input)
		{
			if (!INPUT_MATCH.IsMatch(input))
			{
				return null;
			}

			Instruction instruction = new Instruction();

			Match match = INPUT_MATCH.Match(input);

			instruction.ActionRegister = match.Groups["actionRegister"].Value;
			instruction.ActionLabel = match.Groups["actionLabel"].Value == "inc" ? ActionType.INCREMENT : ActionType.DECREMENT;
			instruction.ActionCount = Int32.Parse(match.Groups["actionAmount"].Value);

			instruction.ConditionRegister = match.Groups["conditionRegister"].Value;
			instruction.ComparatorCount = Int32.Parse(match.Groups["conditionCount"].Value);

			Comparator comparator;
			switch (match.Groups["conditionComparator"].Value)
			{
				case "<":
					comparator = Comparator.LESS_THAN;
					break;
				case "<=":
					comparator = Comparator.LESS_OR_EQUAL_TO;
					break;
				case ">":
					comparator = Comparator.GREATER_THAN;
					break;
				case ">=":
					comparator = Comparator.GREATER_OR_EQUAL_TO;
					break;
				case "==":
					comparator = Comparator.EQUAL_TO;
					break;
				case "!=":
					comparator = Comparator.NOT_EQUAL_TO;
					break;
				default:
					comparator = Comparator.UNKNOWN;
					break;
			}

			instruction.ConditionComparator = comparator;

			return instruction;
		}
	}

	class Day8 : AdventDay
	{
		private Dictionary<string, int> registers;
		private List<Instruction> instructions;

		public Day8() : base(8, 2, InputSeparator.Newline)
		{
			registers = new Dictionary<string, int>();

			instructions = Input.GetAll().Select(Instruction.ParseFromInput).ToList();

			Start();
		}

		private void AddIfNotExists(string register)
		{
			if (!registers.ContainsKey(register))
			{
				registers[register] = 0;
			}
		}

		private void PerformAction(string register, ActionType action, int amount)
		{
			AddIfNotExists(register);

			if (action == ActionType.DECREMENT)
			{
				amount *= -1;
			}

			registers[register] += amount;
		}

		private bool ConditionValid(string register, Comparator comparator, int value)
		{
			AddIfNotExists(register);

			int registerValue = registers[register];

			switch (comparator)
			{
				case Comparator.EQUAL_TO:
					return registerValue == value;
				case Comparator.NOT_EQUAL_TO:
					return registerValue != value;
				case Comparator.GREATER_THAN:
					return registerValue > value;
				case Comparator.GREATER_OR_EQUAL_TO:
					return registerValue >= value;
				case Comparator.LESS_THAN:
					return registerValue < value;
				case Comparator.LESS_OR_EQUAL_TO:
					return registerValue <= value;
			}

			return false;
		}

		protected override void Part1()
		{
			foreach (Instruction instruction in instructions)
			{
				if (ConditionValid(instruction.ConditionRegister, instruction.ConditionComparator, instruction.ComparatorCount))
				{
					PerformAction(instruction.ActionRegister, instruction.ActionLabel, instruction.ActionCount);
				}
			}

			KeyValuePair<string, int> highestRegister = registers.OrderByDescending(kv => kv.Value).FirstOrDefault();

			Logger.Solution(String.Format("The highest register of name {0} has value {1}", highestRegister.Key, highestRegister.Value));
		}

		protected override void Part2()
		{
			KeyValuePair<string, int> highestRegister = new KeyValuePair<string, int>("INVALID", -1);

			foreach (Instruction instruction in instructions)
			{
				if (ConditionValid(instruction.ConditionRegister, instruction.ConditionComparator, instruction.ComparatorCount))
				{
					PerformAction(instruction.ActionRegister, instruction.ActionLabel, instruction.ActionCount);

					KeyValuePair<string, int> localHighestRegister = registers.OrderByDescending(kv => kv.Value).FirstOrDefault();

					if (localHighestRegister.Value > highestRegister.Value)
					{
						highestRegister = localHighestRegister;
					}
				}
			}

			Logger.Solution(String.Format("The highest register of name {0} has value {1}", highestRegister.Key, highestRegister.Value));
		}
	}
}
