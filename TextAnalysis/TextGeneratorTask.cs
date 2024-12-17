using System.Text;
using Microsoft.VisualStudio.TestPlatform.CoreUtilities.Extensions;

namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        var phrase = new List<string>(phraseBeginning.Split(' '));
        var wordsArray = phrase.Count >= 2 ? new[] {phrase[^2], phrase[^1]} : new[] {phrase[0]};
        for (var i = 0; i < wordsCount; i++)
        {
            if (!nextWords.TryGetValue(string.Join(" ", wordsArray), out var value))
            {
                if (wordsArray.Length >= 2 && nextWords.TryGetValue(wordsArray[1], out var value1))
                {
                    value = value1;
                }
                else
                {
                    break;
                }
                
            }
            phrase.Add(value);
            wordsArray = phrase.Count >= 2 ? new[] {phrase[^2], phrase[^1]} : new[] {value};
        }
        return string.Join(" ", phrase);
    }
}