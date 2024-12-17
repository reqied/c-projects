using System.Text;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static readonly char[] SentenceDelimiters = new[] { '.', '!', ':', ';', '?', '(', ')'};
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        var sentences = text.Split(SentenceDelimiters);
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
        var wordsList = SplitSentenceToWords(sentence);
        return (from word in wordsList where word.Length != 0 select word.ToLower()).ToList();
    }

    public static List<string> SplitSentenceToWords(string sentence)
    {
        var wordsList = new List<string>();
        var word = new StringBuilder();
        foreach (var letter in sentence)
        {
            if (!char.IsLetter(letter) && letter != '\'')
            {
                if (word.ToString() != "")
                {
                    wordsList.Add(word.ToString());
                }
                word.Clear();
                continue;
            }
            word.Append(letter);
        }
        wordsList.Add(word.ToString());
        return wordsList;
    }
}