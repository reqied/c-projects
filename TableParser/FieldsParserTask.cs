using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TableParser;

[TestFixture]
public class FieldParserTaskTests
{
	public static void Test(string input, string[] expectedResult)
	{
		var actualResult = FieldsParserTask.ParseLine(input);
		Assert.AreEqual(expectedResult.Length, actualResult.Count);
		for (var i = 0; i < expectedResult.Length; ++i)
		{
			Assert.AreEqual(expectedResult[i], actualResult[i].Value);
		}
	}

	[TestCase("text", new[] {"text"})]
	[TestCase("hello world", new[] {"hello", "world"})]
	[TestCase("''", new[]{""})]
	[TestCase("'a'", new[] {"a"})]
	[TestCase("\"abcd\"", new[]{"abcd"})]
	[TestCase("abcd\"abcd\\\\a\"", new[] {"abcd", "abcd\\a"})]
	[TestCase("\\аЕЕслиТолькоСлеши\\ещеСлеши\\", new[]{"\\аЕЕслиТолькоСлеши\\ещеСлеши\\"})]
	[TestCase(@"'a\' b'", new[] {"a' b"})]
	[TestCase("'bububu'", new[] {"bububu"})]
	[TestCase("'работал' и 'усталь'", new[] {"работал", "и", "усталь"})]
	[TestCase(@"'a", new[] {"a"})]
	[TestCase("'a'b", new[] {"a", "b"})]
	[TestCase("a'b'", new[] {"a", "b"})]
	[TestCase("блин еще много", new[] {"блин","еще","много"})]
	[TestCase("\'\"1\" \"2\" \"3\"\'", new[] {"\"1\" \"2\" \"3\""})]
	[TestCase(@"усталь""сильна""", new[] {"усталь", "сильна"})]
	[TestCase(@"""def g h", new[] {"def g h"})]
	[TestCase("\" \"\"\"", new[]{" ", ""})]
	[TestCase("bukva \"bukva", new[] {"bukva", "bukva"})]
	[TestCase("\"''\"", new[] {"''"})]
	[TestCase(@"'\'\''", new[] {"''"})]
	[TestCase("hello \\", new[] {"hello", "\\"})]
	[TestCase("bububu \\ jjj", new[] {"bububu", "\\", "jjj"})]
	[TestCase(@"""\""a""", new[] { @"""a" })]
	[TestCase("\'a ", new[] { "a " })]
	[TestCase("many  spaces", new[] { "many", "spaces" })]
	[TestCase(" a b ", new[] { "a", "b" })]
	[TestCase(@"'godblessed\\'", new[] { @"godblessed\" })]
	[TestCase("", new string[0])]


	public static void RunTests(string input, string[] expectedOutput)
	{
		Test(input, expectedOutput);
	}
}

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var list = new List<Token>();
            var token = new Token("", 0, 0);
            for (var i = 0; i < line.Length; i++)
            {
	            token = IsQuotation(line[i]) ? ReadQuotedField(line, i) : ReadField(line, i);
	            
	            list.Add(token);
                if (token.Length > 1)
                {
	                i += token.Length - 1;
                }
            }
            return CreateFinalList(list);
        }

        public static List<Token> CreateFinalList(List<Token> list)
        {
	        return list.Where(l => l.Length > 0).ToList();
        }

        private static Token ReadField(string line, int startIndex)
        {
            var word = new StringBuilder();

            for (var i = startIndex; i < line.Length; i++)
            {
                if (IsQuotation(line[i]) || line[i] == ' ')
                {
                    break;
                }
                word.Append(line[i]);
            }
            return new Token(word.ToString(), startIndex, word.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }

        public static bool IsQuotation(char line)
        {
	        return line is '\'' or '\"' ? true : false;
        }
    }
