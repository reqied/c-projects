using System.Drawing.Printing;
using System.Text;
using NUnit.Framework;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
	[TestCase("''", 0, "", 2)]
	[TestCase("'a'", 0, "a", 3)]
	[TestCase("\"abcd\"", 0, "abcd", 6)]
	[TestCase("\"abcd", 0, "abcd", 5)]
	[TestCase("abcd\"abcd\\\\a\"", 4, "abcd\\a", 9)]
	[TestCase("b \"a'\"", 2, "a'", 4)]
	[TestCase(@"'a\' b'", 0, "a' b", 7)]
	[TestCase("'a'b", 0, "a", 3)]
	[TestCase("a'b'", 1, "b", 3)]
	[TestCase("cb\"a'", 2, "a'", 3)]
	[TestCase(@"""a 'b' 'c' d"" '""1"" ""2"" ""3""'", 0, "a 'b' 'c' d", 13)]
	[TestCase(@"""a 'b' 'c' d"" '""1"" ""2"" ""3""'", 14, @"""1"" ""2"" ""3""", 13)]
	[TestCase(@"""", 0, "", 1)]
	[TestCase(@"'", 0, "", 1)]
	[TestCase(@"'' ""bcd ef"" 'x y'",0,"",2)]
	[TestCase(@"'' ""bcd ef"" 'x y'", 3, "bcd ef", 8)]
	[TestCase(@"a""b c d e""f", 1, "b c d e", 9)]
	[TestCase(@"abc ""def g h", 4, "def g h", 8)]
	[TestCase(@"""a \""c\""", 0, "a \"c\"", 8)]
	[TestCase(@"""\\"" b", 0, @"\", 4)]
	[TestCase(@"""\\"" b", 3, " b", 3)]
	[TestCase(@"\""a b\""", 1, @"a b""", 6)]
	
	public void Test(string line, int startIndex, string expectedValue, int expectedLength)
	{
		var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
		Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
	}
}

class QuotedFieldTask
{
	public static Token ReadQuotedField(string line, int startIndex)
	{
		var tokenValue = new StringBuilder();
		var quote = line[startIndex];
		var realLength = 1;
		for (var i = startIndex + 1; i < line.Length; i++)
		{
			if (line[i] == '\\')
			{
				tokenValue.Append(line[i+1]);
				realLength += 2;
				i++;
				continue;
			}
			if (i != 0 && line[i] == quote)
			{
				realLength++; 
				break;
			}
			realLength++;
			tokenValue.Append(line[i]);
		}
		return new Token(tokenValue.ToString(), startIndex, realLength);
	}
}