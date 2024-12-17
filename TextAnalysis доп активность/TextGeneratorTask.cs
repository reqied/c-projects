using System.Text;

namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string[]> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        var phrase = new StringBuilder(phraseBeginning );
        var wordsArray = phraseBeginning.Split(' ');
        var tempPhrase = wordsArray.Length >= 2  
        ? wordsArray[^2] + " " + wordsArray[^1]
        : phraseBeginning;
        for (var i = 0; i < wordsCount; i++)
        {
            if (!nextWords.TryGetValue(tempPhrase, out var value))
            {
                var tempPhraseArray = tempPhrase.Split(" ");
                if (tempPhraseArray.Length >= 2 && nextWords.TryGetValue(tempPhraseArray[1], out var value1))
                {
                    value = value1;
                }
                else
                {
                    break;
                }
                
            }
            var rand = new Random();
            int chooseByte = rand.Next(value.Length);
            phrase.Append(" " + value[chooseByte]);
            wordsArray = phrase.ToString().Split(" ");
            tempPhrase = wordsArray.Length >= 2 
            ? wordsArray[^2] + " " + wordsArray[^1]
            : value[chooseByte];
        }
        return phrase.ToString();
    }
}