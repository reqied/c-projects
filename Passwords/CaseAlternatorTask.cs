using System.Collections;

namespace Passwords;

public class CaseAlternatorTask
{
    public static List<string> AlternateCharCases(string lowercaseWord)
    {
        var result = new List<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
        result = result.GroupBy(x => x).Select(x => x.First()).ToList();
        result.Sort();
        return result;
    }

    static void AlternateCharCases(char[] word, int startIndex, List<string> result)
    {
        if (startIndex == word.Length)
        {
            result.Add(new string (word));
            return;
        }

        var exactLetter = word[startIndex];
        if (char.IsLetter(exactLetter))
        {
            word[startIndex] = char.ToUpper(exactLetter);
            AlternateCharCases(word, startIndex + 1, result);
            word[startIndex] = char.ToLower(exactLetter);
        }
        AlternateCharCases(word, startIndex + 1, result);
    }
}