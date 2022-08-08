using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ulearn_Quest
{
	
	class Program
	{
		public class Document
		{
			public int Id;
			public string Text;
		}

		public static void Main()
		{
			Document[] documents =
			{
				new Document {Id = 1, Text = "Hello world!"},
				new Document {Id = 2, Text = "World, world, world... Just words..."},
				new Document {Id = 3, Text = "Words — power"},
				new Document {Id = 4, Text = ""},
				new Document {Id = 5, Text = "Words — power"},
				new Document {Id = 6, Text = "Words — power"},
				new Document {Id = 7, Text = "Hello world!"}
			};
			var index = BuildInvertedIndex(documents);
			foreach (var w in index)
			{
				Console.Write(w.Key + " - ");
				foreach (var g in w)
					Console.Write(g + " ");
				Console.WriteLine();
			}
		}

		public static ILookup<string, int> BuildInvertedIndex(Document[] documents)
		{
			//var namesByLetter2 = new Dictionary<int, List<string>>();
			//foreach (var group in documents.GroupBy(name => name.Id))
			//	namesByLetter2.Add(group.Key, group.Select(w => w.Text).ToList());
			//ILookup<string, int> namesByLetter2 = documents
			//	.ToLookup(name => Regex
			//		.Split(name.Text, @"\W+"), name => name.Id)

			return documents
				.SelectMany(doc => Regex.Split(doc.Text.ToLower(), @"\W+")
					.Distinct()
					.Where(word => word != "")
					.Select(word => Tuple.Create(word, doc.Id)))
				.ToLookup(tuple => tuple.Item1, tuple => tuple.Item2);
		}
	}
}