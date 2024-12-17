namespace TestCase;
using System.Collections.Generic;
using NUnit.Framework;

public class Tests
{
    static void TestSolution(IReadOnlyList<string> phrases, string prefix, int left, int right, int expected)
    {
        var result = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, left, right);
    
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [Test]
    public void LeftBorderTestCase()
    {
        TestSolution(new string[]{"h", "z", "ё", "я"}, "я", -1, 11, 2);
    }
}