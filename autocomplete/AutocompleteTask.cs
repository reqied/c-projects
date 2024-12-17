using System;
using System.Collections.Generic;
using NUnit.Framework;
using static Autocomplete.LeftBorderTask;

namespace Autocomplete;

internal class AutocompleteTask
{ 
	public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			return phrases[index];
		return null;
	}
	
	public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
	{
		var index = GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		var result = new List<string>();
		for (var i = index; i < phrases.Count && result.Count < count; i++)
		{
			if (phrases[i].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			{
				result.Add(phrases[i]);
			}
		}

		return result.ToArray();
	}
	
	public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var leftIndex = GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);

		return Math.Max(0, rightIndex - leftIndex);
	}
}


[TestFixture]
public class AutocompleteTests
{
	private List<string> phrases;

	[SetUp]
	public void SetUp()
	{
		phrases = new List<string>
		{
			"approve", "apologise", 
			"brave", "broke", "browse", "be", 
			"challenge",
			"dip", "depression",
			"escape",
			"fiit", "forest",
			"ghost",
			"mice",
			"storage"
		};
	}

	[Test]
	public void TopByPrefix_WhenNoSuchPrefix()
	{
		var expectedTopWords = Array.Empty<object>();
		var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, "ye", 5);
		CollectionAssert.AreEqual(expectedTopWords, actualTopWords);
	}
	
	[Test]
	public void TopByPrefix_IsEmpty_WhenNoPhrases()
	{
		var actualTopWords = AutocompleteTask.GetTopByPrefix(new List<string>(), "a", 5);
		CollectionAssert.IsEmpty(actualTopWords);
	}

	[Test]
	public void TopByPrefix_ReturnsMatchingPhrases_WhenTheyExist()
	{
		var expectedTopWords = new[] {"approve", "apologise"};
		var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, "ap", 5);
		CollectionAssert.AreEqual(expectedTopWords, actualTopWords);
	}

	[Test]
	public void TopByPrefix_ReturnsLimitedCount_WhenMoreThanCountExist()
	{
		var expectedTopWords = new[] {"brave", "broke", "browse"};
		var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, "br", 3);
		CollectionAssert.AreEqual(expectedTopWords, actualTopWords);
	}

	[Test]
	public void CountByPrefix_IsZero_WhenNoMatchingPrefix()
	{
		var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "z");
		Assert.AreEqual(0, actualCount);
	}

	[Test]
	public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
	{
		var expectedCount = phrases.Count;
		var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "");
		Assert.AreEqual(expectedCount, actualCount);
	}

	[Test]
	public void CountByPrefix_ReturnsCorrectCount_WhenSomeMatch()
	{
		var expectedCount = 4; //"brave", "broke", "browse", "be"
		var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "b");
		Assert.AreEqual(expectedCount, actualCount);
	}
}
