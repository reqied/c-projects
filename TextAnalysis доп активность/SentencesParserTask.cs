using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static readonly char[] SentenceDelimiters = new[] { '.', '!', ':', ';', '?', '(', ')'};    
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        string[] sentences = text.Split(SentenceDelimiters);
        foreach (var sentence in sentences)
        {
            if (sentence.Length == 0)
            {
                continue;
            }
            var result = SentenceParse(sentence);

            if (result.Count == 0)
            {
                continue;
            }
            sentencesList.Add(result);
        }
        return sentencesList;
    }

    public static List<string> SentenceParse(string sentence)
    {
        var wordsArray = SplitSentenceToWords(sentence);
        var result = new List<string>();
        foreach (var word in wordsArray)
            {
                if (word.Length == 0)
                {
                    continue;
                }
                result.Add(word.ToLower());
            }
        return result;
    }
    public static List<string> SplitSentenceToWords(string sentence)
    {
        var wordsArray = new List<string>();
        var word = new StringBuilder();
        foreach (var letter in sentence)
        {
            if (!char.IsLetter(letter) && letter != '\'')
            {
                if (word.ToString() != "")
                {
                    wordsArray.Add(word.ToString());
                }
                word.Clear();
                continue;
            }
            word.Append(letter);
        }
        wordsArray.Add(word.ToString());
        return wordsArray;
    }
}