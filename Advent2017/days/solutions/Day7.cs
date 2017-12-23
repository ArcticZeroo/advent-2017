using Advent2017.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent2017.days.solutions
{
	class Item
	{
		// name, weight, children
		public string Name;
		public int Weight;
		public List<string> Children;
		public Item Parent;

		public Item(string name, int weight, List<string> children, Item parent)
		{
			Name = name;
			Weight = weight;
			Children = children;
			Parent = parent;
		}

		public Item(string name, int weight, List<string> children) : this(name, weight, children, null) { }

		public Item(string name, int weight) : this(name, weight, new List<string>(), null) { }

		public string ToString()
		{
			return String.Format("Item[name: {0}, weight: {1}, childrenCount: {2}, parentExists: {3}]", Name, Weight, Children.Count, Parent != null);
		}
	}

	class Day7 : AdventDay
	{
		Dictionary<string, Item> items;

		public Day7() : base(7, 2, InputSeparator.Newline)
		{
			items = GetDictionary();

			Start();
		}

		private Item ParseItemFromString(string str)
		{
			Regex regex = new Regex(@"(?<name>\w+) \((?<weight>\d+)\)(?: -> (?<children>[\w, ]+))?");

			if (!regex.IsMatch(str))
			{
				return null;
			}

			Match match = regex.Match(str);

			Group name = match.Groups["name"];
			Group weight = match.Groups["weight"];
			Group children = match.Groups["children"];

			List<string> childrenList = children.Success ? children.Value.Split(new string[] { ", " }, StringSplitOptions.None).ToList() : new List<string>();

			return new Item(name.Value, int.Parse(weight.Value), childrenList);
		}

		private Dictionary<string, Item> GetDictionary()
		{
			Dictionary<string, Item> dictionary = new Dictionary<string, Item>();

			foreach (string str in Input.GetAll())
			{
				Item item = ParseItemFromString(str);
				//Logger.Debug("Checking item " + item.Name);

				foreach (string child in item.Children)
				{
					if (dictionary.ContainsKey(child))
					{
						dictionary[child].Parent = item;
					}
				}

				foreach (Item possibleParent in dictionary.Values)
				{
					if (possibleParent.Children.Contains(item.Name))
					{
						//Logger.Debug("Found a parent for this item (" + possibleParent.Name + ")");
						item.Parent = possibleParent;
						break;
					}
				}

				dictionary.Add(item.Name, item);
			}

			return dictionary;
		}

		protected override void Part1()
		{
			Item topDog = items.First().Value;

			while (topDog.Parent != null)
			{
				topDog = topDog.Parent;
			}

			Logger.Solution(topDog.Name);
		}

		private int GetWeightOfAllChildren(Item item)
		{
			int weight = 0;

			foreach (string childName in item.Children)
			{
				if (!items.ContainsKey(childName))
				{
					continue;
				}

				// Must be ALL programs above it
				weight += items[childName].Weight + GetWeightOfAllChildren(items[childName]);
			}

			return weight;
		}

		private List<Item> GetChildren(Item item)
		{
			List<Item> children = new List<Item>();

			foreach (string childName in item.Children)
			{
				if (!items.ContainsKey(childName))
				{
					continue;
				}

				children.Add(items[childName]);
			}

			return children;
		}

		private int GetRequiredDisplacement(Item item)
		{
			if (item.Children.Count < 2)
			{
				return 0;
			}

			List<int> totalWeights = new List<int>();

			foreach (Item child in GetChildren(item))
			{
				totalWeights.Add(GetWeightOfAllChildren(child) + child.Weight);
			}

			if (totalWeights.Distinct().ToArray().Length == 1)
			{
				return 0;
			}

			Dictionary<int, int> occurrences = new Dictionary<int, int>();

			foreach (int weight in totalWeights)
			{
				if (!occurrences.ContainsKey(weight))
				{
					occurrences[weight] = 0;
				}

				occurrences[weight]++;
			}

			KeyValuePair<int, int>[] valuePair = occurrences.ToArray();

			int mostCommonWeight;
			int leastCommonWeight;

			// If the first one has more occurrences than the second...
			if (valuePair[0].Value > valuePair[1].Value)
			{
				mostCommonWeight = valuePair[0].Key;
				leastCommonWeight = valuePair[1].Key;
			}
			else
			{
				mostCommonWeight = valuePair[1].Key;
				leastCommonWeight = valuePair[0].Key;
			}

			int lowestChildIndex = totalWeights.IndexOf(leastCommonWeight);

			int requiredDisplacement = mostCommonWeight - leastCommonWeight;

			return requiredDisplacement;

		}

		private List<int> GetChildWeights(Item item)
		{
			List<int> totalWeights = new List<int>();

			foreach (Item child in GetChildren(item))
			{
				totalWeights.Add(GetWeightOfAllChildren(child) + child.Weight);
			}

			return totalWeights;
		}

		private bool IsBalanced(List<int> totalWeights)
		{
			return (totalWeights.Distinct().ToArray().Length == 1);
		}

		protected override void Part2()
		{
			foreach (Item item in items.Values)
			{
				if (item.Children.Count < 2)
				{
					continue;
				}

				List<int> totalWeights = GetChildWeights(item);

				if (!IsBalanced(totalWeights))
				{
					Dictionary<int, int> occurrences = new Dictionary<int, int>();

					foreach (int weight in totalWeights)
					{
						if (!occurrences.ContainsKey(weight))
						{
							occurrences[weight] = 0;
						}

						occurrences[weight]++;
					}

					KeyValuePair<int, int>[] valuePair = occurrences.ToArray();

					int mostCommonWeight;
					int leastCommonWeight;

					// If the first one has more occurrences than the second...
					if (valuePair[0].Value > valuePair[1].Value)
					{
						mostCommonWeight = valuePair[0].Key;
						leastCommonWeight = valuePair[1].Key;
					}
					else
					{
						mostCommonWeight = valuePair[1].Key;
						leastCommonWeight = valuePair[0].Key;
					}

					int lowestChildIndex = totalWeights.IndexOf(leastCommonWeight);

					Item child = items[item.Children[lowestChildIndex]];

					// If the child is not balanced, this is not the one
					// that needs to change. Something in that child must change.
					// so... just keep iterating. Not my problem.
					if (!IsBalanced(GetChildWeights(child)))
					{
						continue;
					}

					int requiredDisplacement = mostCommonWeight - leastCommonWeight;

					Logger.Solution("Required weight of the program: " + (child.Weight + requiredDisplacement));
					break;
				}
			}
		}
	}
}
